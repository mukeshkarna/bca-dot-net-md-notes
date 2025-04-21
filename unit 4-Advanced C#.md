# Unit 4: Advanced C#

## Delegates

Delegates are type-safe function pointers that enable scenarios for callbacks, events, and lambda expressions. They represent references to methods with specific parameter lists and return types.

### Delegate Declaration

```csharp
// Declare a delegate type
public delegate int MathOperation(int x, int y);
```

### Using Delegates

```csharp
public class Calculator
{
    // Methods that match the delegate signature
    public static int Add(int x, int y) => x + y;
    public static int Subtract(int x, int y) => x - y;
    
    public static void Main()
    {
        // Create delegate instances
        MathOperation addition = Add;
        MathOperation subtraction = Subtract;
        
        // Invoke delegates
        int result1 = addition(5, 3);      // 8
        int result2 = subtraction(10, 4);  // 6
        
        // Pass delegate as parameter
        ProcessOperation(addition, 7, 3);  // Output: Result: 10
    }
    
    public static void ProcessOperation(MathOperation operation, int a, int b)
    {
        int result = operation(a, b);
        Console.WriteLine($"Result: {result}");
    }
}
```

### Multicast Delegates

Delegates can reference multiple methods. These are called multicast delegates.

```csharp
public delegate void Notification(string message);

public class NotificationSystem
{
    public static void SendEmail(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
    
    public static void SendSMS(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
    
    public static void Main()
    {
        // Create multicast delegate
        Notification notifier = SendEmail;
        notifier += SendSMS;
        
        // Calling this will execute both methods
        notifier("System alert");
        
        // Output:
        // Sending email: System alert
        // Sending SMS: System alert
        
        // Remove a method
        notifier -= SendEmail;
        
        // Now only SMS will be sent
        notifier("Second alert");
    }
}
```

### Predefined Delegate Types

C# includes several predefined delegate types:

1. **Action**: For methods that return void
   ```csharp
   Action<string> log = message => Console.WriteLine(message);
   log("Hello, World!");
   ```

2. **Func**: For methods that return a value
   ```csharp
   Func<int, int, int> add = (x, y) => x + y;
   int result = add(3, 5); // 8
   ```

3. **Predicate**: For methods that return a boolean
   ```csharp
   Predicate<int> isEven = x => x % 2 == 0;
   bool result = isEven(4); // true
   ```

## Events

Events provide a way for a class to notify other classes when something of interest occurs. Events are built on delegates.

### Declaring and Raising Events

```csharp
public class OrderProcessor
{
    // Declare an event using a delegate
    public event EventHandler<OrderEventArgs> OrderProcessed;
    
    // Method to raise the event
    protected virtual void OnOrderProcessed(OrderEventArgs e)
    {
        OrderProcessed?.Invoke(this, e);
    }
    
    public void ProcessOrder(Order order)
    {
        // Process order logic here
        Console.WriteLine($"Processing order {order.OrderId}");
        
        // After processing, raise the event
        OnOrderProcessed(new OrderEventArgs { Order = order });
    }
}

// Custom event args
public class OrderEventArgs : EventArgs
{
    public Order Order { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public string CustomerName { get; set; }
    public decimal Total { get; set; }
}
```

### Subscribing to Events

```csharp
public class Program
{
    static void Main()
    {
        var processor = new OrderProcessor();
        var notificationService = new NotificationService();
        
        // Subscribe to the event
        processor.OrderProcessed += notificationService.OnOrderProcessed;
        
        // Create and process an order
        var order = new Order
        {
            OrderId = 12345,
            CustomerName = "John Doe",
            Total = 99.95m
        };
        
        processor.ProcessOrder(order);
        
        // Unsubscribe from the event
        processor.OrderProcessed -= notificationService.OnOrderProcessed;
    }
}

public class NotificationService
{
    public void OnOrderProcessed(object sender, OrderEventArgs e)
    {
        Console.WriteLine($"Notification: Order {e.Order.OrderId} " +
                          $"for {e.Order.CustomerName} was processed. " +
                          $"Total: {e.Order.Total:C}");
    }
}
```

### Standard Event Pattern

The standard event pattern in C# has these components:

1. The event is declared with `event` keyword
2. An `EventHandler` or `EventHandler<T>` delegate type is used
3. Event arguments are derived from `EventArgs`
4. Protected virtual "On" methods are used to raise events

### Custom Event Implementation

You can implement custom events with add/remove accessors:

```csharp
public class Publisher
{
    private EventHandler<CustomEventArgs> _customEvent;
    
    public event EventHandler<CustomEventArgs> CustomEvent
    {
        add
        {
            // Custom logic before adding a handler
            Console.WriteLine("Handler added");
            _customEvent += value;
        }
        remove
        {
            // Custom logic before removing a handler
            Console.WriteLine("Handler removed");
            _customEvent -= value;
        }
    }
    
    protected virtual void OnCustomEvent(CustomEventArgs e)
    {
        _customEvent?.Invoke(this, e);
    }
    
    public void DoSomething()
    {
        // Raise the event
        OnCustomEvent(new CustomEventArgs());
    }
}

public class CustomEventArgs : EventArgs
{
    // Custom event data
}
```

## Lambda Expressions

Lambda expressions provide a concise way to represent anonymous methods using a more expressive syntax.

### Basic Lambda Syntax

```csharp
// Expression lambda (implicit return)
Func<int, int> square = x => x * x;

// Statement lambda (explicit return and multiple statements)
Func<int, int> factorial = n =>
{
    int result = 1;
    for (int i = 1; i <= n; i++)
    {
        result *= i;
    }
    return result;
};

// Usage
int squareOfFive = square(5);        // 25
int factorialOfFour = factorial(4);  // 24
```

### Capturing Variables

Lambda expressions can capture variables from the containing method:

```csharp
public void ProcessData(int[] numbers, int threshold)
{
    // Lambda captures 'threshold' from the outer scope
    var aboveThreshold = Array.FindAll(numbers, n => n > threshold);
    
    Console.WriteLine($"Numbers above {threshold}:");
    foreach (var number in aboveThreshold)
    {
        Console.WriteLine(number);
    }
}
```

### Using Lambda with LINQ

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Using lambda in Where
var evenNumbers = numbers.Where(n => n % 2 == 0);

// Using lambda in Select
var squares = numbers.Select(n => n * n);

// Using lambda in multiple operations
var sumOfEvenSquares = numbers
    .Where(n => n % 2 == 0)
    .Select(n => n * n)
    .Sum();
```

### Lambda with Events

```csharp
button.Click += (sender, e) =>
{
    Console.WriteLine("Button clicked!");
    ProcessUserInput();
};
```

## Exception Handling

Exception handling is a mechanism for dealing with runtime errors in a structured way, preventing application crashes.

### Basic Exception Handling

```csharp
try
{
    // Code that might throw an exception
    int result = Divide(10, 0);
    Console.WriteLine($"Result: {result}");
}
catch (DivideByZeroException ex)
{
    // Handle specific exception
    Console.WriteLine($"Division error: {ex.Message}");
}
catch (Exception ex)
{
    // Handle any other exception
    Console.WriteLine($"An error occurred: {ex.Message}");
}
finally
{
    // Code that executes whether an exception occurred or not
    Console.WriteLine("Calculation attempt completed.");
}

static int Divide(int a, int b)
{
    return a / b; // Will throw DivideByZeroException if b is 0
}
```

### Throwing Exceptions

```csharp
public double CalculateCircleArea(double radius)
{
    if (radius < 0)
    {
        throw new ArgumentException("Radius cannot be negative", nameof(radius));
    }
    
    return Math.PI * radius * radius;
}
```

### Custom Exceptions

```csharp
// Custom exception class
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
    
    public InsufficientFundsException(string message, decimal currentBalance, decimal withdrawalAmount, Exception innerException)
        : base(message, innerException)
    {
        CurrentBalance = currentBalance;
        WithdrawalAmount = withdrawalAmount;
    }
}

// Using the custom exception
public class BankAccount
{
    public decimal Balance { get; private set; }
    
    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
        {
            throw new InsufficientFundsException(
                "Insufficient funds for this withdrawal",
                Balance,
                amount);
        }
        
        Balance -= amount;
    }
}
```

### Exception Filters (C# 6.0+)

```csharp
try
{
    // Code that might throw an exception
    ProcessFile("data.txt");
}
catch (IOException ex) when (ex.HResult == -2147024893) // File not found
{
    Console.WriteLine("The file was not found. Please check the path.");
}
catch (IOException ex) when (ex.HResult == -2147024864) // Access denied
{
    Console.WriteLine("You don't have permission to access this file.");
}
catch (IOException ex)
{
    Console.WriteLine($"An I/O error occurred: {ex.Message}");
}
```

### Using `using` Statement for Resource Cleanup

```csharp
public void ReadFile(string path)
{
    try
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string content = reader.ReadToEnd();
            Console.WriteLine(content);
        } // Dispose is automatically called here
    }
    catch (FileNotFoundException)
    {
        Console.WriteLine("File not found!");
    }
}

// C# 8.0+ using declaration
public void ModernReadFile(string path)
{
    try
    {
        using StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        Console.WriteLine(content);
    } // Dispose is called at the end of the scope
    catch (FileNotFoundException)
    {
        Console.WriteLine("File not found!");
    }
}
```

### Best Practices for Exception Handling

1. **Only catch exceptions you can handle**
2. **Don't use exceptions for normal flow control**
3. **Throw exceptions instead of returning error codes**
4. **Use specific exception types** rather than catching the base Exception class
5. **Include meaningful information** in exception messages
6. **Clean up resources** in finally blocks or using statements
7. **Log exceptions** appropriately for debugging
8. **Rethrow exceptions correctly** to preserve the stack trace:
   ```csharp
   try
   {
       // Code that might throw
   }
   catch (Exception ex)
   {
       // Log the exception
       Logger.Log(ex);
       
       // Rethrow preserving the stack trace
       throw;  // Not "throw ex;" which resets the stack trace
   }
   ```

## Introduction to LINQ (Language Integrated Query)

LINQ provides a consistent syntax for querying various data sources, including collections, databases, XML, and more.

### LINQ to Objects

Querying in-memory collections:

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Query syntax
var evenNumbers = from num in numbers
                 where num % 2 == 0
                 select num;

// Method syntax
var evenNumbersMethod = numbers.Where(num => num % 2 == 0);

// Both produce the same result: 2, 4, 6, 8, 10
```

### LINQ Operations

```csharp
var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 1200, Category = "Electronics" },
    new Product { Id = 2, Name = "Desk", Price = 400, Category = "Furniture" },
    new Product { Id = 3, Name = "Mouse", Price = 25, Category = "Electronics" },
    new Product { Id = 4, Name = "Chair", Price = 100, Category = "Furniture" },
    new Product { Id = 5, Name = "Keyboard", Price = 50, Category = "Electronics" }
};

// Filtering
var electronicsProducts = products.Where(p => p.Category == "Electronics");

// Ordering
var orderedByPrice = products.OrderBy(p => p.Price);
var orderedByPriceDescending = products.OrderByDescending(p => p.Price);

// Projection
var productNames = products.Select(p => p.Name);
var nameAndPrice = products.Select(p => new { p.Name, p.Price });

// Aggregation
var totalPrice = products.Sum(p => p.Price);
var averagePrice = products.Average(p => p.Price);
var productCount = products.Count();
var maxPrice = products.Max(p => p.Price);
var minPrice = products.Min(p => p.Price);

// Grouping
var groupedByCategory = products.GroupBy(p => p.Category);
foreach (var group in groupedByCategory)
{
    Console.WriteLine($"Category: {group.Key}");
    foreach (var product in group)
    {
        Console.WriteLine($"  {product.Name}: {product.Price:C}");
    }
}

// Set operations
var category1 = new List<string> { "Electronics", "Books" };
var category2 = new List<string> { "Books", "Furniture" };
var allCategories = category1.Union(category2);          // Electronics, Books, Furniture
var commonCategories = category1.Intersect(category2);   // Books
var uniqueToCategory1 = category1.Except(category2);     // Electronics

// Quantifiers
bool anyExpensive = products.Any(p => p.Price > 1000);   // true
bool allCheap = products.All(p => p.Price < 500);        // false

// First, Last, Single
var firstElectronics = products.First(p => p.Category == "Electronics");
var lastFurniture = products.Last(p => p.Category == "Furniture");
```

### Deferred Execution

LINQ queries are executed when the result is needed, not when the query is defined:

```csharp
var numbers = new List<int> { 1, 2, 3 };
var query = numbers.Where(n => n > 1);  // Query is defined but not executed

numbers.Add(4);  // Modify the source collection

foreach (var n in query)  // Query is executed here
{
    Console.WriteLine(n);  // Output: 2, 3, 4
}
```

### Forcing Immediate Execution

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5 };

// Force immediate execution by calling a method like ToList(), ToArray(), etc.
var evenList = numbers.Where(n => n % 2 == 0).ToList();

numbers.Add(6);  // Modifying the source after query execution

// evenList remains [2, 4] (doesn't include 6)
```

## Working with Databases

C# can interact with databases using various approaches. The most common are ADO.NET and Entity Framework.

### ADO.NET

The lowest-level approach to database access:

```csharp
using System.Data;
using System.Data.SqlClient;

public class CustomerRepository
{
    private readonly string _connectionString;
    
    public CustomerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public List<Customer> GetAllCustomers()
    {
        List<Customer> customers = new List<Customer>();
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Name, Email FROM Customers";
            SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2)
                    };
                    
                    customers.Add(customer);
                }
            }
        }
        
        return customers;
    }
    
    public Customer GetCustomerById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "SELECT Id, Name, Email FROM Customers WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            
            connection.Open();
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Customer
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2)
                    };
                }
                return null;
            }
        }
    }
    
    public void AddCustomer(Customer customer)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "INSERT INTO Customers (Name, Email) VALUES (@Name, @Email)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Email", customer.Email);
            
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    
    public void UpdateCustomer(Customer customer)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "UPDATE Customers SET Name = @Name, Email = @Email WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", customer.Id);
            command.Parameters.AddWithValue("@Name", customer.Name);
            command.Parameters.AddWithValue("@Email", customer.Email);
            
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    
    public void DeleteCustomer(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM Customers WHERE Id = @Id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

### Entity Framework Core

A modern Object-Relational Mapping (ORM) framework:

```csharp
// Install Entity Framework Core packages first:
// - Microsoft.EntityFrameworkCore
// - Microsoft.EntityFrameworkCore.SqlServer

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Define entity classes
public class Product
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public string Description { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    public ICollection<Product> Products { get; set; }
}

// DbContext class
public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure relationships, indices, constraints, etc.
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            
        // Seed data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Furniture" }
        );
    }
}

// Repository pattern with EF Core
public class ProductRepository
{
    private readonly StoreContext _context;
    
    public ProductRepository(StoreContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
    
    // LINQ with Entity Framework
    public async Task<List<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
    {
        return await _context.Products
            .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
            .OrderBy(p => p.Price)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<object>> GetProductSummaryAsync()
    {
        return await _context.Products
            .Select(p => new { p.Name, p.Price, CategoryName = p.Category.Name })
            .ToListAsync();
    }
}

// Using the repository in a controller or service
public class ProductService
{
    private readonly ProductRepository _repository;
    
    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<Product>> SearchProductsAsync(string searchTerm, decimal? maxPrice)
    {
        var query = await _repository.GetAllProductsAsync();
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => p.Name.Contains(searchTerm) || 
                                     p.Description.Contains(searchTerm))
                          .ToList();
        }
        
        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice).ToList();
        }
        
        return query;
    }
}
```

### LINQ to SQL (Legacy)

LINQ to SQL is an older ORM framework that's still used in some applications:

```csharp
// DataContext and mapping
[Table(Name = "Customers")]
public class Customer
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true)]
    public int Id { get; set; }
    
    [Column]
    public string Name { get; set; }
    
    [Column]
    public string Email { get; set; }
    
    [Column]
    public string Phone { get; set; }
}

public class CustomerDataContext : DataContext
{
    public CustomerDataContext(string connectionString) : base(connectionString) { }
    
    public Table<Customer> Customers { get; set; }
}

// Using LINQ to SQL
public void QueryCustomers()
{
    string connectionString = "Data Source=.;Initial Catalog=CustomerDB;Integrated Security=True";
    
    using (var db = new CustomerDataContext(connectionString))
    {
        // Query syntax
        var query = from c in db.Customers
                   where c.Name.StartsWith("J")
                   orderby c.Name
                   select c;
                   
        foreach (var customer in query)
        {
            Console.WriteLine($"{customer.Id}: {customer.Name} ({customer.Email})");
        }
        
        // Insert a new customer
        Customer newCustomer = new Customer
        {
            Name = "Jane Smith",
            Email = "jane@example.com",
            Phone = "555-1234"
        };
        
        db.Customers.InsertOnSubmit(newCustomer);
        db.SubmitChanges();
        
        // Update a customer
        Customer existingCustomer = db.Customers.FirstOrDefault(c => c.Id == 1);
        if (existingCustomer != null)
        {
            existingCustomer.Phone = "555-5678";
            db.SubmitChanges();
        }
        
        // Delete a customer
        Customer customerToDelete = db.Customers.FirstOrDefault(c => c.Id == 2);
        if (customerToDelete != null)
        {
            db.Customers.DeleteOnSubmit(customerToDelete);
            db.SubmitChanges();
        }
    }
}
```