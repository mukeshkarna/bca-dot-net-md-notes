# Unit 4: Advanced C#

## 1. Delegates

Delegates are type-safe function pointers that allow methods to be passed as parameters, stored in variables, and invoked dynamically.

### Delegate Declaration and Usage

```csharp
// Delegate declaration
public delegate int MathOperation(int a, int b);

// Usage
MathOperation add = Add;
int result = add(5, 3); // Calls Add(5, 3)

// Method definition
static int Add(int a, int b) => a + b;
```

### Multicast Delegates

Delegates can reference multiple methods, which are called in sequence:

```csharp
Action<string> notify = LogToConsole;
notify += LogToFile;

// Both methods are called when invoked
notify("System event occurred");
```

### Predefined Delegate Types

- **Action<T1, T2, ...>**: For methods that return void
- **Func<T1, T2, ..., TResult>**: For methods that return a value
- **Predicate<T>**: For methods that return a boolean

### Applications of Delegates

1. **Callback Methods**: Passing methods to be called after an operation completes
2. **Event Handling**: Serving as the foundation for the event model
3. **LINQ Operations**: Powering LINQ methods that take predicates
4. **Asynchronous Programming**: Notification when async operations complete
5. **Strategy Pattern**: Selecting algorithms at runtime

## 2. Events

Events are a high-level mechanism built on delegates that implement the publisher-subscriber pattern, allowing classes to notify other classes when something interesting happens.

### Event Declaration and Subscription

```csharp
// Publisher class
public class Button
{
    // Event declaration
    public event EventHandler Clicked;
    
    // Method to raise the event
    protected virtual void OnClicked(EventArgs e)
    {
        Clicked?.Invoke(this, e);
    }
    
    public void Click()
    {
        OnClicked(EventArgs.Empty);
    }
}

// Subscriber
button.Clicked += (sender, e) => Console.WriteLine("Button was clicked");
```

### Custom Event Arguments

For passing data with events:

```csharp
public class MessageEventArgs : EventArgs
{
    public string Message { get; }
    
    public MessageEventArgs(string message)
    {
        Message = message;
    }
}

// Event with custom arguments
public event EventHandler<MessageEventArgs> MessageReceived;
```

### Key Characteristics of Events

1. **Encapsulation**: Events can only be invoked from within the declaring class
2. **External Access**: External classes can only subscribe or unsubscribe
3. **Null Check Pattern**: Using `?.Invoke()` prevents exceptions if no handlers are registered
4. **Thread Safety**: Consider synchronization in multi-threaded environments

### Standard Event Pattern

1. Define an event using `EventHandler` or `EventHandler<TEventArgs>`
2. Create a protected virtual method that raises the event
3. Check for null before invoking the event
4. Provide meaningful event arguments

### Applications of Events

1. **UI Notifications**: Button clicks, mouse movements, etc.
2. **State Changes**: Notifying when object properties change
3. **Long-Running Processes**: Progress updates and completion notifications
4. **Cross-Component Communication**: Loosely coupled message passing

## 3. Lambda Expressions

Lambda expressions provide a concise syntax for defining anonymous methods. They are particularly useful with delegates and LINQ.

### Syntax Forms

```csharp
// Expression lambda (implicit return)
Func<int, int> square = x => x * x;

// Statement lambda (explicit return)
Func<int, int> factorial = n => {
    int result = 1;
    for (int i = 1; i <= n; i++)
        result *= i;
    return result;
};
```

### Variable Capture

Lambda expressions can access variables from the containing scope:

```csharp
int factor = 10;
Func<int, int> multiply = x => x * factor;
```

### Common Applications

1. **LINQ Queries**: Predicates and projections in LINQ
   ```csharp
   var evenNumbers = numbers.Where(n => n % 2 == 0);
   ```

2. **Event Handlers**: Concise inline event handling
   ```csharp
   button.Click += (sender, e) => ProcessButtonClick();
   ```

3. **Collection Operations**: Sorting, filtering, transforming
   ```csharp
   list.Sort((x, y) => x.CompareTo(y));
   ```

4. **Asynchronous Programming**: Callbacks and continuations
   ```csharp
   Task.Run(() => ProcessData()).ContinueWith(t => DisplayResults());
   ```

### Key Features of Lambda Expressions

1. **Type Inference**: Parameter types often inferred from context
2. **Implicit Returns**: Expression lambdas return the expression result
3. **Closures**: Runtime mechanisms that maintain captured variable state
4. **Expression Trees**: Lambda expressions can be converted to data structures

## 4. Exception Handling

Exception handling provides a structured mechanism for detecting, propagating, and responding to runtime errors.

### Basic Exception Handling

```csharp
try
{
    // Code that might throw exceptions
    int result = Divide(10, 0);
}
catch (DivideByZeroException ex)
{
    // Handle specific exception
    Console.WriteLine($"Division error: {ex.Message}");
}
catch (Exception ex)
{
    // Handle any other exception
    Console.WriteLine($"Unexpected error: {ex.Message}");
}
finally
{
    // Code that always executes
    CleanupResources();
}
```

### Creating Custom Exceptions

```csharp
public class InsufficientFundsException : Exception
{
    public decimal CurrentBalance { get; }
    public decimal WithdrawalAmount { get; }
    
    public InsufficientFundsException(string message, decimal currentBalance, decimal withdrawalAmount) 
        : base(message)
    {
        CurrentBalance = currentBalance;
        WithdrawalAmount = withdrawalAmount;
    }
}
```

### Resource Cleanup with `using`

```csharp
// Traditional using statement
using (var connection = new SqlConnection(connectionString))
{
    connection.Open();
    // Use the connection
}  // Automatically calls Dispose

// C# 8.0+ using declaration
using var reader = new StreamReader(path);
// reader is disposed at the end of the scope
```

### Exception Filters

```csharp
try
{
    // Code that might throw
}
catch (IOException ex) when (ex.Message.Contains("network"))
{
    // Only catches IOExceptions with "network" in the message
}
```

### Best Practices for Exception Handling

1. **Use Specific Exception Types**: Catch specific exceptions rather than base `Exception`
2. **Include Context Information**: Provide meaningful exception messages
3. **Clean Up Resources**: Use `finally` blocks or `using` statements
4. **Preserve Stack Traces**: Use `throw;` instead of `throw ex;` when rethrowing
5. **Only Catch What You Can Handle**: Don't catch exceptions you can't properly handle
6. **Don't Use Exceptions for Flow Control**: Exceptions are for exceptional conditions
7. **Document Exceptions**: Document exceptions that methods can throw

## 5. Introduction to LINQ

Language Integrated Query (LINQ) provides a unified syntax for querying data across different data sources.

### Query Syntax vs. Method Syntax

```csharp
// Query syntax
var queryResult = from num in numbers
                 where num % 2 == 0
                 select num * num;

// Equivalent method syntax
var methodResult = numbers
    .Where(num => num % 2 == 0)
    .Select(num => num * num);
```

### Common LINQ Operations

1. **Filtering**: Selecting elements that match a condition
   ```csharp
   var adults = people.Where(p => p.Age >= 18);
   ```

2. **Projection**: Transforming elements
   ```csharp
   var names = people.Select(p => p.Name);
   ```

3. **Ordering**: Sorting elements
   ```csharp
   var ordered = people.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
   ```

4. **Grouping**: Organizing elements into groups
   ```csharp
   var groups = people.GroupBy(p => p.Department);
   ```

5. **Joining**: Combining elements from different collections
   ```csharp
   var joined = employees.Join(departments,
                              e => e.DepartmentId,
                              d => d.Id,
                              (e, d) => new { e.Name, Department = d.Name });
   ```

6. **Aggregation**: Computing summary values
   ```csharp
   var total = numbers.Sum();
   var average = numbers.Average();
   ```

### Deferred vs. Immediate Execution

LINQ queries use deferred execution - they're not executed until the results are needed:

```csharp
// Query is defined here (not executed yet)
var query = numbers.Where(n => n > 5);

// Adding to the source affects the query results
numbers.Add(10);

// Query executes here
foreach (var num in query) { ... }
```

To force immediate execution:
```csharp
var result = query.ToList(); // Executes immediately and caches results
```

### LINQ Providers

1. **LINQ to Objects**: For in-memory collections
2. **LINQ to Entities**: For Entity Framework
3. **LINQ to SQL**: For SQL Server (legacy)
4. **LINQ to XML**: For XML documents
5. **Custom Providers**: For other data sources

### Benefits of LINQ

1. **Unified Syntax**: Same query patterns across different data sources
2. **Type Safety**: Compile-time checking of queries
3. **IntelliSense Support**: Better development experience
4. **Query Optimization**: Providers can optimize execution
5. **Expressiveness**: Complex queries can be written concisely

## 6. Working with Databases

### ADO.NET

ADO.NET provides direct access to database data:

```csharp
using (var connection = new SqlConnection(connectionString))
using (var command = new SqlCommand("SELECT * FROM Customers", connection))
{
    connection.Open();
    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var name = reader["Name"].ToString();
            // Process data
        }
    }
}
```

#### Key Components:

1. **Connection**: Establishes database connection
2. **Command**: Represents SQL query or stored procedure
3. **DataReader**: Forward-only, read-only data access
4. **DataAdapter**: Fills DataSet with data
5. **DataSet/DataTable**: In-memory cache of data
6. **Transaction**: Manages atomic operations

### Entity Framework Core

Entity Framework Core is an Object-Relational Mapper (ORM) that enables working with database data using .NET objects:

```csharp
public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}

// Querying data
using (var context = new ApplicationDbContext())
{
    var customers = context.Customers
        .Where(c => c.Country == "USA")
        .OrderBy(c => c.Name)
        .ToList();
}
```

#### Key Features:

1. **Code-First Approach**: Define models in code, create database from them
2. **Database-First Approach**: Generate models from existing database
3. **Migrations**: Version control for database schema
4. **Change Tracking**: Automatic tracking of entity changes
5. **Lazy/Eager Loading**: Control loading of related entities
6. **LINQ Integration**: Query databases using LINQ

### Repository Pattern

The Repository pattern abstracts data access logic:

```csharp
public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task AddAsync(Customer customer);
    Task UpdateAsync(Customer customer);
    Task DeleteAsync(int id);
}
```

### Database Operations Best Practices

1. **Connection Management**: Use connection pooling
2. **Parameterized Queries**: Prevent SQL injection
3. **Transaction Scope**: Maintain data integrity
4. **Error Handling**: Catch and handle database exceptions
5. **Performance Optimization**: Index design, query optimization
6. **Security**: Principle of least privilege for database access

## 7. Writing Web Applications using ASP.NET

### ASP.NET Core MVC

ASP.NET Core MVC is a framework for building web applications using the Model-View-Controller pattern:

```csharp
// Controller
public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new HomeViewModel { Message = "Welcome" };
        return View(model);
    }
}
```

#### Key Components:

1. **Models**: Represent data and business logic
2. **Views**: Define the UI (using Razor syntax)
3. **Controllers**: Handle HTTP requests
4. **Routing**: Map URLs to controller actions
5. **Model Binding**: Map HTTP data to action parameters
6. **Filters**: Apply cross-cutting concerns
7. **Areas**: Organize related functionality

### ASP.NET Core Web API

ASP.NET Core Web API is for building HTTP services:

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _repository.GetAll();
        return Ok(products);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = _repository.GetById(id);
        if (product == null) return NotFound();
        return Ok(product);
    }
}
```

#### Key Features:

1. **RESTful Design**: Following REST principles
2. **Content Negotiation**: Support multiple formats
3. **Model Binding and Validation**: Automatic request processing
4. **Swagger/OpenAPI**: API documentation and testing

### Blazor

Blazor allows building interactive web UIs using C#:

```csharp
@page "/counter"

<h1>Counter</h1>
<p>Current count: @currentCount</p>
<button @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;
    
    private void IncrementCount()
    {
        currentCount++;
    }
}
```

#### Blazor Hosting Models:

1. **Blazor Server**: C# code runs on the server
2. **Blazor WebAssembly**: C# code runs in the browser

### Dependency Injection

ASP.NET Core has built-in dependency injection:

```csharp
// Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddScoped<IProductRepository, ProductRepository>();
}

// Controller
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;
    
    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }
}
```

### Middleware

ASP.NET Core uses a middleware pipeline to process requests:

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
```

### Authentication and Authorization

ASP.NET Core provides authentication and authorization:

```csharp
// Configure services
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { /* configuration */ });
    
services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Secure controller or action
[Authorize(Policy = "AdminOnly")]
public IActionResult AdminAction()
{
    return View();
}
```

### Key ASP.NET Core Features

1. **Cross-Platform**: Runs on Windows, macOS, and Linux
2. **Modular Architecture**: Composed of NuGet packages
3. **Configuration System**: Multiple configuration sources
4. **Environment-Based**: Different settings per environment
5. **Hosting Options**: IIS, self-hosting, Docker containers
6. **Performance**: High-performance, optimized framework

## Key Relationships Between Advanced C# Topics

1. **Delegates and Events**: Events are built on delegates
2. **Delegates and Lambda Expressions**: Lambdas are syntactic sugar for delegates
3. **LINQ and Lambda Expressions**: LINQ heavily uses lambdas for queries
4. **LINQ and Entity Framework**: LINQ to Entities for database queries
5. **Exception Handling and Database Access**: Proper error handling for data operations
6. **Dependency Injection and ASP.NET**: Services for controllers and components

## Summary of Advanced C# Concepts

1. **Delegates and Events** provide a robust mechanism for callbacks and publisher-subscriber patterns
2. **Lambda Expressions** offer concise syntax for defining methods inline
3. **Exception Handling** enables structured error management
4. **LINQ** provides a unified, expressive query syntax
5. **Database Access** techniques offer various levels of abstraction
6. **ASP.NET Core** delivers a modern, modular web framework

These advanced features enable C# developers to build maintainable, efficient applications across various domains from desktop to web to cloud services.
