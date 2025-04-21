# Writing Web Applications using ASP.NET

ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications.

## ASP.NET Core MVC

The Model-View-Controller (MVC) architectural pattern separates an application into three main components:
- **Model**: The data and business logic
- **View**: The user interface
- **Controller**: Handles requests, works with the model, and selects a view

### Setting Up an ASP.NET Core MVC Application

```csharp
// Program.cs in ASP.NET Core 6+
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register other services
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

### Models

Models represent the data structure of your application:

```csharp
public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [DataType(DataType.Currency)]
    [Range(0.01, 10000)]
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
}
```

### Controllers

Controllers handle HTTP requests and return responses:

```csharp
public class ProductsController : Controller
{
    private readonly IProductRepository _repository;
    
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    // GET: /Products
    public async Task<IActionResult> Index()
    {
        var products = await _repository.GetAllProductsAsync();
        return View(products);
    }
    
    // GET: /Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _repository.GetProductByIdAsync(id.Value);
        
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }
    
    // GET: /Products/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: /Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                CategoryId = model.CategoryId
            };
            
            await _repository.AddProductAsync(product);
            return RedirectToAction(nameof(Index));
        }
        
        return View(model);
    }
    
    // GET: /Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _repository.GetProductByIdAsync(id.Value);
        
        if (product == null)
        {
            return NotFound();
        }
        
        var viewModel = new ProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryId = product.CategoryId
        };
        
        return View(viewModel);
    }
    
    // POST: /Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
    {
        if (id != model.Id)
        {
            return NotFound();
        }
        
        if (ModelState.IsValid)
        {
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                
                if (product == null)
                {
                    return NotFound();
                }
                
                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.CategoryId = model.CategoryId;
                
                await _repository.UpdateProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle the case where the product was modified by another user
                var exists = await _repository.ProductExistsAsync(id);
                
                if (!exists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
        
        return View(model);
    }
    
    // GET: /Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _repository.GetProductByIdAsync(id.Value);
        
        if (product == null)
        {
            return NotFound();
        }
        
        return View(product);
    }
    
    // POST: /Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _repository.DeleteProductAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
```

### Views

Views define the HTML output using Razor syntax:

#### Index.cshtml
```html
@model IEnumerable<Product>

@{
    ViewData["Title"] = "Products";
}

<h1>Products</h1>

<p>
    <a asp-action="Create" class="btn btn-primary">Create New</a>
</p>

<table class="table">
    <thead>
        <tr>
            <th>@Html.DisplayNameFor(model => model.Name)</th>
            <th>@Html.DisplayNameFor(model => model.Price)</th>
            <th>@Html.DisplayNameFor(model => model.Category.Name)</th>
            <th>Actions</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>@Html.DisplayFor(modelItem => item.Name)</td>
                <td>@Html.DisplayFor(modelItem => item.Price)</td>
                <td>@Html.DisplayFor(modelItem => item.Category.Name)</td>
                <td>
                    <a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
                    <a asp-action="Details" asp-route-id="@item.Id">Details</a> |
                    <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```

#### Create.cshtml
```html
@model ProductCreateViewModel

@{
    ViewData["Title"] = "Create Product";
}

<h1>Create Product</h1>

<div class="row">
    <div class="col-md-6">
        <form asp-action="Create">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            
            <div class="form-group">
                <label asp-for="Name" class="control-label"></label>
                <input asp-for="Name" class="form-control" />
                <span asp-validation-for="Name" class="text-danger"></span>
            </div>
            
            <div class="form-group">
                <label asp-for="Price" class="control-label"></label>
                <input asp-for="Price" class="form-control" />
                <span asp-validation-for="Price" class="text-danger"></span>
            </div>
            
            <div class="form-group">
                <label asp-for="Description" class="control-label"></label>
                <textarea asp-for="Description" class="form-control"></textarea>
                <span asp-validation-for="Description" class="text-danger"></span>
            </div>
            
            <div class="form-group">
                <label asp-for="CategoryId" class="control-label"></label>
                <select asp-for="CategoryId" class="form-control" 
                        asp-items="@(new SelectList(ViewBag.Categories, "Id", "Name"))"></select>
                <span asp-validation-for="CategoryId" class="text-danger"></span>
            </div>
            
            <div class="form-group mt-3">
                <input type="submit" value="Create" class="btn btn-primary" />
                <a asp-action="Index" class="btn btn-secondary">Back to List</a>
            </div>
        </form>
    </div>
</div>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
```

### View Models

View Models are specialized models designed for specific views:

```csharp
public class ProductCreateViewModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Range(0.01, 10000)]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
}

public class ProductEditViewModel : ProductCreateViewModel
{
    public int Id { get; set; }
}
```

## ASP.NET Core Web API

ASP.NET Core also supports building RESTful APIs:

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductsApiController : ControllerBase
{
    private readonly IProductRepository _repository;
    
    public ProductsApiController(IProductRepository repository)
    {
        _repository = repository;
    }
    
    // GET: api/ProductsApi
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        var products = await _repository.GetAllProductsAsync();
        
        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Description = p.Description,
            CategoryName = p.Category.Name
        });
        
        return Ok(productDtos);
    }
    
    // GET: api/ProductsApi/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            CategoryName = product.Category.Name
        };
        
        return Ok(productDto);
    }
    
    // POST: api/ProductsApi
    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var product = new Product
        {
            Name = createDto.Name,
            Price = createDto.Price,
            Description = createDto.Description,
            CategoryId = createDto.CategoryId
        };
        
        await _repository.AddProductAsync(product);
        
        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description
        };
        
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, productDto);
    }
    
    // PUT: api/ProductsApi/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest();
        }
        
        var product = await _repository.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        product.Name = updateDto.Name;
        product.Price = updateDto.Price;
        product.Description = updateDto.Description;
        product.CategoryId = updateDto.CategoryId;
        
        try
        {
            await _repository.UpdateProductAsync(product);
        }
        catch (DbUpdateConcurrencyException)
        {
            var exists = await _repository.ProductExistsAsync(id);
            
            if (!exists)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        
        return NoContent();
    }
    
    // DELETE: api/ProductsApi/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);
        
        if (product == null)
        {
            return NotFound();
        }
        
        await _repository.DeleteProductAsync(id);
        
        return NoContent();
    }
}
```

## Blazor

Blazor is a framework for building interactive web UIs using C# instead of JavaScript:

### Blazor Server

```csharp
// Startup configuration for Blazor Server
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// ...

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
```

### Blazor Component

```html
@page "/products"
@using YourApp.Models
@using YourApp.Services
@inject IProductService ProductService

<h1>Product List</h1>

@if (products == null)
{
    <p>Loading...</p>
}
else if (!products.Any())
{
    <p>No products found.</p>
}
else
{
    <table class="table">
        <thead>
            <tr>
                <th>Name</th>
                <th>Price</th>
                <th>Category</th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var product in products)
            {
                <tr>
                    <td>@product.Name</td>
                    <td>@product.Price.ToString("C")</td>
                    <td>@product.Category.Name</td>
                    <td>
                        <button class="btn btn-primary" @onclick="() => ShowDetails(product.Id)">
                            Details
                        </button>
                        <button class="btn btn-danger" @onclick="() => DeleteProduct(product.Id)">
                            Delete
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@if (selectedProduct != null)
{
    <div class="card mt-4">
        <div class="card-header">
            @selectedProduct.Name
        </div>
        <div class="card-body">
            <p>Price: @selectedProduct.Price.ToString("C")</p>
            <p>Description: @selectedProduct.Description</p>
            <p>Category: @selectedProduct.Category.Name</p>
        </div>
    </div>
}

@code {
    private List<Product> products;
    private Product selectedProduct;
    
    protected override async Task OnInitializedAsync()
    {
        products = await ProductService.GetAllProductsAsync();
    }
    
    private async Task ShowDetails(int id)
    {
        selectedProduct = await ProductService.GetProductByIdAsync(id);
    }
    
    private async Task DeleteProduct(int id)
    {
        bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this product?");
        
        if (confirmed)
        {
            await ProductService.DeleteProductAsync(id);
            products = await ProductService.GetAllProductsAsync();
            
            if (selectedProduct?.Id == id)
            {
                selectedProduct = null;
            }
        }
    }
}
```

## Authentication and Authorization

ASP.NET Core has built-in support for authentication and authorization:

### Identity Setup

```csharp
// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Authentication middleware
app.UseAuthentication();
app.UseAuthorization();
```

### Securing Controllers and Actions

```csharp
[Authorize]
public class SecureController : Controller
{
    // Only authenticated users can access this controller
    
    [AllowAnonymous]
    public IActionResult PublicAction()
    {
        // Anyone can access this action
        return View();
    }
    
    [Authorize(Roles = "Admin")]
    public IActionResult AdminAction()
    {
        // Only users in the Admin role can access this action
        return View();
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    public IActionResult PolicyBasedAction()
    {
        // Only users satisfying the "RequireAdminRole" policy can access this action
        return View();
    }
}
```

### Authentication Controller

```csharp
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser 
            { 
                UserName = model.Email, 
                Email = model.Email 
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false);
                
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }
        
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
```

## Deployment and Hosting

ASP.NET Core applications can be deployed to various hosting environments:

### IIS Hosting

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseIIS(); // For IIS in-process hosting

// web.config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" 
                  arguments=".\YourApp.dll" 
                  stdoutLogEnabled="false" 
                  stdoutLogFile=".\logs\stdout" 
                  hostingModel="inprocess" />
    </system.webServer>
  </location>
</configuration>
```

### Docker Deployment

```dockerfile
# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["YourApp.csproj", "./"]
RUN dotnet restore "YourApp.csproj"
COPY . .
RUN dotnet build "YourApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "YourApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YourApp.dll"]
```

### Azure App Service

```xml
<!-- Azure Deployment Configuration -->
<Project>
  <PropertyGroup>
    <TargetFramework>net7.0</TargetFramework>
    <UserSecretsId>aspnet-YourApp-12345678-1234-1234-1234-1234567890ab</UserSecretsId>
    <ApplicationInsightsResourceId>/subscriptions/your-subscription-id/resourceGroups/your-resource-group/providers/microsoft.insights/components/YourApp</ApplicationInsightsResourceId>
  </PropertyGroup>
  
  <ItemGroup>
    <PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.21.0" />
  </ItemGroup>
</Project>
```

## Configuration and Environment Variables

ASP.NET Core provides a robust configuration system:

```csharp
// Reading configuration
var builder = WebApplication.CreateBuilder(args);

// Add services to the container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Custom configuration section
var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
builder.Services.AddSingleton(emailSettings);

// Environment-specific configuration
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDeveloperExceptionPage();
}
else
{
    builder.Services.AddExceptionHandler("/Home/Error");
    builder.Services.AddHsts(options =>
    {
        options.Includes_ubdomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });
}

// appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "EmailSettings": {
    "Server": "smtp.example.com",
    "Port": 587,
    "SenderName": "Your App",
    "SenderEmail": "noreply@yourapp.com"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

// Strong-typed configuration class
public class EmailSettings
{
    public string Server { get; set; }
    public int Port { get; set; }
    public string SenderName { get; set; }
    public string SenderEmail { get; set; }
}
```

## Middleware and Request Pipeline

ASP.NET Core uses a middleware pipeline to process HTTP requests:

```csharp
var app = builder.Build();

// Configure the HTTP request pipeline

// Exception handling middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Security middleware
app.UseHttpsRedirection();

// Static files middleware
app.UseStaticFiles();

// Routing middleware
app.UseRouting();

// Authentication middleware
app.UseAuthentication();
app.UseAuthorization();

// Custom middleware
app.Use(async (context, next) =>
{
    // Do something before the next middleware
    await next.Invoke();
    // Do something after the next middleware
});

// Endpoint middleware
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
    endpoints.MapBlazorHub();
});

// Custom middleware class
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");
        
        // Call the next middleware
        await _next(context);
        
        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}

// Extension method to register the middleware
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}

// Using the custom middleware
app.UseRequestLogging();
```
