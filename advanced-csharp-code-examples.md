# Unit 4: Advanced C# - Code Examples

## 1. Delegates and Events Example

This example demonstrates delegates, events, and lambda expressions in a simple notification system that could be used for application-wide alerts:

```csharp
using System;
using System.Collections.Generic;

namespace NotificationSystem
{
    // Define delegate types
    public delegate void MessageHandler(string message);
    
    // Event args class
    public class NotificationEventArgs : EventArgs
    {
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public NotificationType Type { get; set; }
        
        public NotificationEventArgs(string message, NotificationType type)
        {
            Message = message;
            Timestamp = DateTime.Now;
            Type = type;
        }
    }
    
    public enum NotificationType
    {
        Information,
        Warning,
        Error
    }
    
    // Publisher class
    public class NotificationCenter
    {
        // Event using custom delegate
        public event MessageHandler MessageReceived;
        
        // Event using EventHandler<T>
        public event EventHandler<NotificationEventArgs> NotificationReceived;
        
        // Method to trigger the delegate
        public void SendMessage(string message)
        {
            Console.WriteLine($"NotificationCenter: Sending message: {message}");
            
            // Invoke the delegate
            MessageReceived?.Invoke(message);
        }
        
        // Method to trigger the event
        public void SendNotification(string message, NotificationType type)
        {
            Console.WriteLine($"NotificationCenter: Sending notification: {message} ({type})");
            
            // Create event args
            var args = new NotificationEventArgs(message, type);
            
            // Raise the event
            OnNotificationReceived(args);
        }
        
        // Protected method to raise the event
        protected virtual void OnNotificationReceived(NotificationEventArgs e)
        {
            NotificationReceived?.Invoke(this, e);
        }
    }
    
    // Subscriber classes
    public class EmailService
    {
        public void OnMessageReceived(string message)
        {
            Console.WriteLine($"EmailService: Sending email with message: {message}");
        }
        
        public void OnNotificationReceived(object sender, NotificationEventArgs e)
        {
            Console.WriteLine($"EmailService: Sending email notification: {e.Message} (Type: {e.Type}, Time: {e.Timestamp.ToShortTimeString()})");
        }
    }
    
    public class SmsService
    {
        public void OnMessageReceived(string message)
        {
            Console.WriteLine($"SmsService: Sending SMS with message: {message}");
        }
        
        public void OnNotificationReceived(object sender, NotificationEventArgs e)
        {
            // Only send SMS for warnings and errors
            if (e.Type != NotificationType.Information)
            {
                Console.WriteLine($"SmsService: Sending SMS notification: {e.Message} (Type: {e.Type})");
            }
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            // Create instances
            var notificationCenter = new NotificationCenter();
            var emailService = new EmailService();
            var smsService = new SmsService();
            
            // Subscribe to the delegate
            notificationCenter.MessageReceived += emailService.OnMessageReceived;
            notificationCenter.MessageReceived += smsService.OnMessageReceived;
            
            // Subscribe to the event
            notificationCenter.NotificationReceived += emailService.OnNotificationReceived;
            notificationCenter.NotificationReceived += smsService.OnNotificationReceived;
            
            // Add lambda expression subscriber
            notificationCenter.MessageReceived += (message) => 
            {
                Console.WriteLine($"Lambda handler: Message received: {message}");
            };
            
            // Trigger the delegate
            notificationCenter.SendMessage("System maintenance scheduled");
            
            Console.WriteLine();
            
            // Trigger the events
            notificationCenter.SendNotification("Application started successfully", NotificationType.Information);
            
            Console.WriteLine();
            
            notificationCenter.SendNotification("Disk space running low", NotificationType.Warning);
            
            Console.WriteLine();
            
            // Unsubscribe the SMS service from the delegate
            notificationCenter.MessageReceived -= smsService.OnMessageReceived;
            
            // Send another message (SMS service won't receive it)
            notificationCenter.SendMessage("Update completed");
            
            Console.WriteLine();
            
            // Using multicast delegates with return values
            Func<int, int, int> calculator = Add;
            calculator += Multiply;
            
            // Only the last result is returned (multiplication)
            int result = calculator(5, 3);
            Console.WriteLine($"Multicast delegate result (only last): {result}");
            
            // But we can get all results by using GetInvocationList
            foreach (Func<int, int, int> operation in calculator.GetInvocationList())
            {
                Console.WriteLine($"Operation result: {operation(5, 3)}");
            }
        }
        
        static int Add(int a, int b) => a + b;
        static int Multiply(int a, int b) => a * b;
    }
}
```

## 2. Exception Handling Example

This example demonstrates various exception handling techniques including custom exceptions, exception filters, and cleanup with `using` statements:

```csharp
using System;
using System.IO;

namespace ExceptionHandlingDemo
{
    // Custom exception class
    public class InsufficientBalanceException : Exception
    {
        public decimal RequestedAmount { get; }
        public decimal AvailableBalance { get; }
        
        public InsufficientBalanceException(string message, decimal requestedAmount, decimal availableBalance)
            : base(message)
        {
            RequestedAmount = requestedAmount;
            AvailableBalance = availableBalance;
        }
        
        public InsufficientBalanceException(string message, decimal requestedAmount, decimal availableBalance, Exception innerException)
            : base(message, innerException)
        {
            RequestedAmount = requestedAmount;
            AvailableBalance = availableBalance;
        }
    }
    
    // Bank account class that throws custom exceptions
    public class BankAccount
    {
        public string AccountNumber { get; }
        public decimal Balance { get; private set; }
        
        public BankAccount(string accountNumber, decimal initialBalance)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
                throw new ArgumentException("Account number cannot be empty", nameof(accountNumber));
                
            if (initialBalance < 0)
                throw new ArgumentOutOfRangeException(nameof(initialBalance), "Initial balance cannot be negative");
                
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }
        
        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
                
            Balance += amount;
        }
        
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
                
            if (amount > Balance)
                throw new InsufficientBalanceException(
                    "Insufficient funds for this withdrawal",
                    amount,
                    Balance);
                    
            Balance -= amount;
        }
    }
    
    // Transaction logger
    public class TransactionLogger : IDisposable
    {
        private readonly StreamWriter _writer;
        private bool _disposed = false;
        
        public TransactionLogger(string logFilePath)
        {
            _writer = new StreamWriter(logFilePath, true);
            _writer.WriteLine($"Log started at: {DateTime.Now}");
        }
        
        public void LogTransaction(string message)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(TransactionLogger));
                
            _writer.WriteLine($"[{DateTime.Now}] {message}");
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writer.WriteLine($"Log ended at: {DateTime.Now}");
                    _writer.Dispose();
                }
                
                _disposed = true;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~TransactionLogger()
        {
            Dispose(false);
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            ExceptionHandlingExamples();
            UsingStatementExample();
            ExceptionFiltersExample();
        }
        
        static void ExceptionHandlingExamples()
        {
            Console.WriteLine("===== Basic Exception Handling =====");
            
            try
            {
                // Creating an account
                BankAccount account = new BankAccount("12345", 1000);
                Console.WriteLine($"Account created with balance: ${account.Balance}");
                
                // Deposit money
                account.Deposit(500);
                Console.WriteLine($"After deposit: ${account.Balance}");
                
                // Withdraw money - this will work
                account.Withdraw(300);
                Console.WriteLine($"After withdrawal of $300: ${account.Balance}");
                
                // Try to withdraw too much - this will throw an exception
                account.Withdraw(2000);
                Console.WriteLine("This line won't execute");
            }
            catch (InsufficientBalanceException ex)
            {
                // Handle custom exception
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Requested: ${ex.RequestedAmount}, Available: ${ex.AvailableBalance}");
                Console.WriteLine($"Shortage: ${ex.RequestedAmount - ex.AvailableBalance}");
            }
            catch (ArgumentException ex)
            {
                // Handle argument exceptions
                Console.WriteLine($"Invalid argument: {ex.Message}");
                
                // Access inner exception if any
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Caused by: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                // This code always runs
                Console.WriteLine("Transaction completed (or failed safely)");
            }
        }
        
        static void UsingStatementExample()
        {
            Console.WriteLine("\n===== Using Statement Example =====");
            
            try
            {
                // Traditional using statement
                using (TransactionLogger logger = new TransactionLogger("transactions.log"))
                {
                    logger.LogTransaction("Started processing payments");
                    
                    // Simulating work
                    Console.WriteLine("Processing payments...");
                    
                    logger.LogTransaction("Completed processing payments");
                } // Dispose is automatically called here
                
                // C# 8.0+ using declaration
                using var logger2 = new TransactionLogger("transactions2.log");
                logger2.LogTransaction("Using declaration example");
                Console.WriteLine("More processing...");
                
                // logger2.Dispose() will be called at the end of this scope
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        
        static void ExceptionFiltersExample()
        {
            Console.WriteLine("\n===== Exception Filters Example =====");
            
            try
            {
                string accountId = "12345";
                throw new IOException("Network error", new Exception("Server unavailable"));
            }
            catch (IOException ex) when (ex.Message.Contains("Network"))
            {
                // This will catch the exception because the message contains "Network"
                Console.WriteLine($"Network error: {ex.Message}");
            }
            catch (IOException ex) when (ex.InnerException != null)
            {
                // This would catch other IOExceptions with inner exceptions
                Console.WriteLine($"IO Error with inner exception: {ex.InnerException.Message}");
            }
            catch (IOException)
            {
                // This would catch any other IOExceptions
                Console.WriteLine("Other IO error");
            }
            
            try
            {
                // Example of rethrowing
                try
                {
                    throw new InvalidOperationException("Operation failed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Logging the exception...");
                    
                    // Rethrow preserving the stack trace
                    throw; // Correct way to rethrow
                    
                    // throw ex; // Incorrect - resets stack trace
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught rethrown exception: {ex.Message}");
                Console.WriteLine($"Stack trace preserved: {ex.StackTrace.Contains("ExceptionFiltersExample")}");
            }
        }
    }
}
```

## 3. LINQ Example

This example demonstrates various LINQ operations on collections of products and orders:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public bool InStock { get; set; }
        
        public override string ToString() => $"{Name} (${Price})";
    }
    
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
    
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        
        public decimal TotalAmount => Items.Sum(i => i.Quantity * i.UnitPrice);
    }
    
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            // Sample data
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Price = 1200, Category = "Electronics", InStock = true },
                new Product { Id = 2, Name = "Desktop PC", Price = 1500, Category = "Electronics", InStock = true },
                new Product { Id = 3, Name = "Headphones", Price = 100, Category = "Electronics", InStock = true },
                new Product { Id = 4, Name = "Mouse", Price = 25, Category = "Electronics", InStock = false },
                new Product { Id = 5, Name = "Office Chair", Price = 250, Category = "Furniture", InStock = true },
                new Product { Id = 6, Name = "Desk", Price = 350, Category = "Furniture", InStock = true },
                new Product { Id = 7, Name = "Bookshelf", Price = 175, Category = "Furniture", InStock = false },
                new Product { Id = 8, Name = "Coffee Maker", Price = 60, Category = "Appliances", InStock = true },
                new Product { Id = 9, Name = "Blender", Price = 80, Category = "Appliances", InStock = true }
            };
            
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith", City = "New York" },
                new Customer { Id = 2, Name = "Jane Doe", City = "Los Angeles" },
                new Customer { Id = 3, Name = "Robert Johnson", City = "Chicago" },
                new Customer { Id = 4, Name = "Emily Wilson", City = "New York" }
            };
            
            var orders = new List<Order>
            {
                new Order 
                { 
                    Id = 1, 
                    CustomerId = 1, 
                    OrderDate = new DateTime(2023, 1, 15),
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 1, Quantity = 1, UnitPrice = 1200 },
                        new OrderItem { ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    }
                },
                new Order 
                { 
                    Id = 2, 
                    CustomerId = 2, 
                    OrderDate = new DateTime(2023, 2, 10),
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 6, Quantity = 1, UnitPrice = 350 },
                        new OrderItem { ProductId = 8, Quantity = 1, UnitPrice = 60 }
                    }
                },
                new Order 
                { 
                    Id = 3, 
                    CustomerId = 3, 
                    OrderDate = new DateTime(2023, 3, 5),
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 2, Quantity = 1, UnitPrice = 1500 }
                    }
                },
                new Order 
                { 
                    Id = 4, 
                    CustomerId = 1, 
                    OrderDate = new DateTime(2023, 4, 20),
                    Items = new List<OrderItem>
                    {
                        new OrderItem { ProductId = 5, Quantity = 1, UnitPrice = 250 },
                        new OrderItem { ProductId = 9, Quantity = 2, UnitPrice = 80 }
                    }
                }
            };
            
            // Basic LINQ queries
            Console.WriteLine("===== Basic LINQ Queries =====");
            
            // Query syntax
            Console.WriteLine("\n-- Products over $100 (Query syntax) --");
            var expensiveProducts = from p in products
                                  where p.Price > 100
                                  orderby p.Price descending
                                  select p;
                                  
            foreach (var product in expensiveProducts)
            {
                Console.WriteLine($"{product.Name}: ${product.Price}");
            }
            
            // Method syntax
            Console.WriteLine("\n-- Products over $100 (Method syntax) --");
            var expensiveProductsMethod = products
                .Where(p => p.Price > 100)
                .OrderByDescending(p => p.Price);
                
            foreach (var product in expensiveProductsMethod)
            {
                Console.WriteLine($"{product.Name}: ${product.Price}");
            }
            
            // Filtering
            Console.WriteLine("\n-- In-stock Electronics --");
            var inStockElectronics = products
                .Where(p => p.Category == "Electronics" && p.InStock)
                .ToList();
                
            foreach (var product in inStockElectronics)
            {
                Console.WriteLine(product);
            }
            
            // Projection
            Console.WriteLine("\n-- Product Names and Prices --");
            var productInfo = products.Select(p => new { p.Name, p.Price });
            
            foreach (var item in productInfo)
            {
                Console.WriteLine($"{item.Name}: ${item.Price}");
            }
            
            // Ordering
            Console.WriteLine("\n-- Products by Category, then by Price --");
            var orderedProducts = products
                .OrderBy(p => p.Category)
                .ThenBy(p => p.Price);
                
            foreach (var product in orderedProducts)
            {
                Console.WriteLine($"{product.Category} - {product.Name}: ${product.Price}");
            }
            
            // Grouping
            Console.WriteLine("\n-- Products Grouped by Category --");
            var groupedProducts = products.GroupBy(p => p.Category);
            
            foreach (var group in groupedProducts)
            {
                Console.WriteLine($"Category: {group.Key} ({group.Count()} products)");
                foreach (var product in group)
                {
                    Console.WriteLine($"  {product.Name}: ${product.Price}");
                }
            }
            
            // Aggregation
            Console.WriteLine("\n-- Aggregation Operations --");
            
            var totalProducts = products.Count();
            var totalValue = products.Sum(p => p.Price);
            var averagePrice = products.Average(p => p.Price);
            var minPrice = products.Min(p => p.Price);
            var maxPrice = products.Max(p => p.Price);
            
            Console.WriteLine($"Total products: {totalProducts}");
            Console.WriteLine($"Total value: ${totalValue}");
            Console.WriteLine($"Average price: ${averagePrice:F2}");
            Console.WriteLine($"Minimum price: ${minPrice}");
            Console.WriteLine($"Maximum price: ${maxPrice}");
            
            // Join operations
            Console.WriteLine("\n-- Customer Orders (Join) --");
            
            var customerOrders = customers.Join(
                orders,
                c => c.Id,
                o => o.CustomerId,
                (customer, order) => new
                {
                    CustomerName = customer.Name,
                    OrderId = order.Id,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount
                });
                
            foreach (var item in customerOrders)
            {
                Console.WriteLine($"Order #{item.OrderId} - Customer: {item.CustomerName}, Date: {item.OrderDate.ToShortDateString()}, Amount: ${item.TotalAmount}");
            }
            
            // Group Join
            Console.WriteLine("\n-- Customers with Order Counts (Group Join) --");
            
            var customersWithOrderCounts = customers.GroupJoin(
                orders,
                c => c.Id,
                o => o.CustomerId,
                (customer, customerOrders) => new
                {
                    CustomerName = customer.Name,
                    OrderCount = customerOrders.Count(),
                    TotalSpent = customerOrders.Sum(o => o.TotalAmount)
                });
                
            foreach (var item in customersWithOrderCounts)
            {
                Console.WriteLine($"{item.CustomerName} - Orders: {item.OrderCount}, Total Spent: ${item.TotalSpent}");
            }
            
            // Quantifiers
            Console.WriteLine("\n-- Quantifier Operations --");
            
            bool anyExpensiveProducts = products.Any(p => p.Price > 1000);
            bool allProductsInStock = products.All(p => p.InStock);
            
            Console.WriteLine($"Any products over $1000: {anyExpensiveProducts}");
            Console.WriteLine($"All products in stock: {allProductsInStock}");
            
            // Element operations
            Console.WriteLine("\n-- Element Operations --");
            
            try
            {
                var firstFurniture = products.First(p => p.Category == "Furniture");
                Console.WriteLine($"First furniture item: {firstFurniture}");
                
                var lastElectronic = products.Last(p => p.Category == "Electronics");
                Console.WriteLine($"Last electronic item: {lastElectronic}");
                
                var singleBlender = products.Single(p => p.Name == "Blender");
                Console.WriteLine($"The blender: {singleBlender}");
                
                // This would throw an exception (multiple matches)
                // var singleElectronic = products.Single(p => p.Category == "Electronics");
                
                // FirstOrDefault/SingleOrDefault return null instead of throwing
                var nonExistentProduct = products.FirstOrDefault(p => p.Price > 5000);
                Console.WriteLine($"Expensive product: {nonExistentProduct?.Name ?? "None found"}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            
            // Set operations
            Console.WriteLine("\n-- Set Operations --");
            
            var electronicsAndFurniture = products
                .Where(p => p.Category == "Electronics" || p.Category == "Furniture");
                
            var inStockProducts = products.Where(p => p.InStock);
            var outOfStockProducts = products.Where(p => !p.InStock);
            
            Console.WriteLine("Union example:");
            var allProducts = inStockProducts.Union(outOfStockProducts);
            Console.WriteLine($"Total products: {allProducts.Count()}");
            
            Console.WriteLine("\nIntersect example:");
            var inStockElectronicsSet = inStockProducts.Intersect(
                products.Where(p => p.Category == "Electronics"),
                new ProductComparer());
                
            foreach (var product in inStockElectronicsSet)
            {
                Console.WriteLine(product);
            }
            
            // Partitioning
            Console.WriteLine("\n-- Partitioning --");
            
            Console.WriteLine("Take first 3 products:");
            foreach (var product in products.Take(3))
            {
                Console.WriteLine(product);
            }
            
            Console.WriteLine("\nSkip first 5 products:");
            foreach (var product in products.Skip(5))
            {
                Console.WriteLine(product);
            }
            
            Console.WriteLine("\nProducts from index 2 to 4 (TakeWhile/SkipWhile):");
            int index = 0;
            var middleProducts = products
                .SkipWhile(p => index++ < 2)
                .TakeWhile(p => index <= 5);
                
            foreach (var product in middleProducts)
            {
                Console.WriteLine(product);
            }
            
            // Deferred execution example
            Console.WriteLine("\n-- Deferred Execution --");
            
            var productsList = new List<Product>(products); // make a copy
            var expensiveQuery = productsList.Where(p => p.Price > 100);
            
            Console.WriteLine("Before adding new product:");
            Console.WriteLine($"Number of expensive products: {expensiveQuery.Count()}");
            
            productsList.Add(new Product { Id = 10, Name = "Smart TV", Price = 800, Category = "Electronics", InStock = true });
            
            Console.WriteLine("After adding new product:");
            Console.WriteLine($"Number of expensive products: {expensiveQuery.Count()}"); // Increased due to deferred execution
            
            // Immediate execution
            var expensiveProductsListImmediate = productsList.Where(p => p.Price > 100).ToList();
            Console.WriteLine($"Number in immediate list: {expensiveProductsListImmediate.Count}");
            
            productsList.Add(new Product { Id = 11, Name = "Smart Watch", Price = 300, Category = "Electronics", InStock = true });
            
            Console.WriteLine("After adding another product:");
            Console.WriteLine($"Number in immediate list: {expensiveProductsListImmediate.Count}"); // Unchanged due to immediate execution
            Console.WriteLine($"Number in deferred query: {expensiveQuery.Count()}"); // Increased again
        }
    }
    
    // Custom comparer for Product equality
    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x == null && y == null)
                return true;
            if (x == null || y == null)
                return false;
                
            return x.Id == y.Id;
        }
        
        public int GetHashCode(Product obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
```

## 4. Database Access Example (Entity Framework Core)

This example demonstrates database operations using Entity Framework Core:

```csharp
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreDemo
{
    // Entity classes
    public class Blog
    {
        public int BlogId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(200)]
        public string Url { get; set; }
        
        public DateTime CreatedDate { get; set; }
        
        // Navigation property
        public List<Post> Posts { get; set; } = new List<Post>();
    }
    
    public class Post
    {
        public int PostId { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        public string Content { get; set; }
        
        public DateTime PublishedDate { get; set; }
        
        // Foreign key
        public int BlogId { get; set; }
        
        // Navigation property
        public Blog Blog { get; set; }
        
        // Tags relationship
        public List<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
    
    public class Tag
    {
        public int TagId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        // Posts relationship
        public List<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
    
    // Join entity for many-to-many relationship
    public class PostTag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }
        
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
    
    // DbContext class
    public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite("Data Source=blogging.db")
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many relationship
            modelBuilder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });
                
            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);
                
            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
                
            // Default values
            modelBuilder.Entity<Blog>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
            // Seed data
            modelBuilder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Technology" },
                new Tag { TagId = 2, Name = "Programming" },
                new Tag { TagId = 3, Name = "CSharp" },
                new Tag { TagId = 4, Name = "Database" }
            );
        }
    }
    
    // Repository pattern
    public class BlogRepository
    {
        private readonly BloggingContext _context;
        
        public BlogRepository(BloggingContext context)
        {
            _context = context;
        }
        
        // Create operations
        public async Task<Blog> CreateBlogAsync(Blog blog)
        {
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }
        
        public async Task<Post> CreatePostAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }
        
        // Read operations
        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs
                .Include(b => b.Posts)
                .ToListAsync();
        }
        
        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.Posts)
                .FirstOrDefaultAsync(b => b.BlogId == id);
        }
        
        public async Task<List<Post>> GetPostsByBlogIdAsync(int blogId)
        {
            return await _context.Posts
                .Where(p => p.BlogId == blogId)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }
        
        public async Task<Post> GetPostWithTagsAsync(int postId)
        {
            return await _context.Posts
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(p => p.PostId == postId);
        }
        
        // Update operations
        public async Task UpdateBlogAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
        
        // Delete operations
        public async Task DeleteBlogAsync(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }
        
        // Tag operations
        public async Task AddTagToPostAsync(int postId, int tagId)
        {
            var postTag = new PostTag { PostId = postId, TagId = tagId };
            _context.PostTags.Add(postTag);
            await _context.SaveChangesAsync();
        }
        
        public async Task RemoveTagFromPostAsync(int postId, int tagId)
        {
            var postTag = await _context.PostTags
                .FindAsync(postId, tagId);
                
            if (postTag != null)
            {
                _context.PostTags.Remove(postTag);
                await _context.SaveChangesAsync();
            }
        }
        
        // LINQ queries using Entity Framework
        public async Task<List<Post>> GetPostsByTagAsync(string tagName)
        {
            return await _context.Posts
                .Where(p => p.PostTags.Any(pt => pt.Tag.Name == tagName))
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }
        
        public async Task<List<Post>> SearchPostsAsync(string searchTerm)
        {
            return await _context.Posts
                .Where(p => p.Title.Contains(searchTerm) || p.Content.Contains(searchTerm))
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();
        }
        
        public async Task<List<object>> GetBlogStatisticsAsync()
        {
            return await _context.Blogs
                .Select(b => new
                {
                    b.Title,
                    PostCount = b.Posts.Count,
                    LatestPost = b.Posts.OrderByDescending(p => p.PublishedDate).FirstOrDefault().Title,
                    LatestPostDate = b.Posts.OrderByDescending(p => p.PublishedDate).FirstOrDefault().PublishedDate
                })
                .OrderByDescending(b => b.PostCount)
                .Cast<object>()
                .ToListAsync();
        }
    }
    
    // Main program
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Ensure database is created
            using (var context = new BloggingContext())
            {
                await context.Database.EnsureDeletedAsync(); // For demo purposes
                await context.Database.EnsureCreatedAsync();
            }
            
            await SeedDatabaseAsync();
            await QueryDatabaseAsync();
        }
        
        static async Task SeedDatabaseAsync()
        {
            Console.WriteLine("===== Seeding Database =====");
            
            using (var context = new BloggingContext())
            {
                var repository = new BlogRepository(context);
                
                // Create blogs
                var techBlog = new Blog
                {
                    Title = "Tech Insights",
                    Url = "https://techinsights.example.com"
                };
                
                var programmingBlog = new Blog
                {
                    Title = "Programming Adventures",
                    Url = "https://programming.example.com"
                };
                
                await repository.CreateBlogAsync(techBlog);
                await repository.CreateBlogAsync(programmingBlog);
                
                // Create posts
                var post1 = new Post
                {
                    Title = "Getting Started with EF Core",
                    Content = "Entity Framework Core is an ORM...",
                    PublishedDate = DateTime.Now.AddDays(-5),
                    BlogId = techBlog.BlogId
                };
                
                var post2 = new Post
                {
                    Title = "Advanced LINQ Techniques",
                    Content = "LINQ makes querying data easy...",
                    PublishedDate = DateTime.Now.AddDays(-3),
                    BlogId = techBlog.BlogId
                };
                
                var post3 = new Post
                {
                    Title = "C# 10 New Features",
                    Content = "C# 10 introduces several new features...",
                    PublishedDate = DateTime.Now.AddDays(-1),
                    BlogId = programmingBlog.BlogId
                };
                
                await repository.CreatePostAsync(post1);
                await repository.CreatePostAsync(post2);
                await repository.CreatePostAsync(post3);
                
                // Add tags to posts
                await repository.AddTagToPostAsync(post1.PostId, 2); // Programming
                await repository.AddTagToPostAsync(post1.PostId, 4); // Database
                
                await repository.AddTagToPostAsync(post2.PostId, 2); // Programming
                await repository.AddTagToPostAsync(post2.PostId, 3); // CSharp
                
                await repository.AddTagToPostAsync(post3.PostId, 2); // Programming
                await repository.AddTagToPostAsync(post3.PostId, 3); // CSharp
                
                Console.WriteLine("Database seeded successfully.");
            }
        }
        
        static async Task QueryDatabaseAsync()
        {
            Console.WriteLine("\n===== Querying Database =====");
            
            using (var context = new BloggingContext())
            {
                var repository = new BlogRepository(context);
                
                // Get all blogs with their posts
                Console.WriteLine("\n-- All Blogs --");
                var blogs = await repository.GetAllBlogsAsync();
                
                foreach (var blog in blogs)
                {
                    Console.WriteLine($"Blog: {blog.Title} ({blog.Url})");
                    Console.WriteLine($"Posts: {blog.Posts.Count}");
                    
                    foreach (var post in blog.Posts)
                    {
                        Console.WriteLine($"  - {post.Title} ({post.PublishedDate.ToShortDateString()})");
                    }
                    Console.WriteLine();
                }
                
                // Get posts with specific tag
                Console.WriteLine("\n-- Posts tagged with 'CSharp' --");
                var csharpPosts = await repository.GetPostsByTagAsync("CSharp");
                
                foreach (var post in csharpPosts)
                {
                    Console.WriteLine($"{post.Title} - {post.Blog.Title}");
                }
                
                // Search posts
                Console.WriteLine("\n-- Posts containing 'LINQ' --");
                var searchResults = await repository.SearchPostsAsync("LINQ");
                
                foreach (var post in searchResults)
                {
                    Console.WriteLine($"{post.Title}");
                }
                
                // Get blog statistics
                Console.WriteLine("\n-- Blog Statistics --");
                var blogStats = await repository.GetBlogStatisticsAsync();
                
                foreach (var stat in blogStats)
                {
                    // Use dynamic to access anonymous type properties
                    dynamic blogStat = stat;
                    Console.WriteLine($"Blog: {blogStat.Title}");
                    Console.WriteLine($"  Posts: {blogStat.PostCount}");
                    Console.WriteLine($"  Latest: {blogStat.LatestPost} ({blogStat.LatestPostDate.ToShortDateString()})");
                }
                
                // Update a post
                Console.WriteLine("\n-- Updating a Post --");
                var postToUpdate = await context.Posts.FirstAsync();
                Console.WriteLine($"Original title: {postToUpdate.Title}");
                
                postToUpdate.Title = $"{postToUpdate.Title} (Updated)";
                await repository.UpdatePostAsync(postToUpdate);
                
                // Verify update
                var updatedPost = await context.Posts.FindAsync(postToUpdate.PostId);
                Console.WriteLine($"Updated title: {updatedPost.Title}");
                
                // Delete a post
                Console.WriteLine("\n-- Deleting a Post --");
                var postCountBefore = await context.Posts.CountAsync();
                Console.WriteLine($"Post count before: {postCountBefore}");
                
                await repository.DeletePostAsync(postToUpdate.PostId);
                
                var postCountAfter = await context.Posts.CountAsync();
                Console.WriteLine($"Post count after: {postCountAfter}");
            }
        }
    }
}
```

## 5. ASP.NET Core Web API Example

This example demonstrates creating a RESTful API using ASP.NET Core:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BookStoreApi
{
    // Entity classes
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
    
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Book> Books { get; set; }
    }
    
    // DTOs (Data Transfer Objects)
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
    }
    
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
    
    public class UpdateBookDto : CreateBookDto
    {
        public int Id { get; set; }
    }
    
    // DbContext
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options)
        {
        }
        
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fiction", Description = "Fictional literature" },
                new Category { Id = 2, Name = "Non-Fiction", Description = "Factual literature" },
                new Category { Id = 3, Name = "Science Fiction", Description = "Science-based fiction" },
                new Category { Id = 4, Name = "Mystery", Description = "Mystery novels" }
            );
            
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", PublicationYear = 1925, Price = 12.99m, CategoryId = 1 },
                new Book { Id = 2, Title = "Sapiens", Author = "Yuval Noah Harari", ISBN = "9780062316097", PublicationYear = 2014, Price = 16.99m, CategoryId = 2 },
                new Book { Id = 3, Title = "Dune", Author = "Frank Herbert", ISBN = "9780441172719", PublicationYear = 1965, Price = 14.99m, CategoryId = 3 },
                new Book { Id = 4, Title = "The Da Vinci Code", Author = "Dan Brown", ISBN = "9780307474278", PublicationYear = 2003, Price = 10.99m, CategoryId = 4 }
            );
        }
    }
    
    // Repository interface
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<bool> BookExistsAsync(int id);
    }
    
    // Repository implementation
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Category)
                .ToListAsync();
        }
        
        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        
        public async Task<Book> CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }
        
        public async Task UpdateBookAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Where(b => b.Title.Contains(searchTerm) || 
                            b.Author.Contains(searchTerm) ||
                            b.ISBN.Contains(searchTerm))
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        
        public async Task<bool> BookExistsAsync(int id)
        {
            return await _context.Books.AnyAsync(b => b.Id == id);
        }
    }
    
    // API Controller
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        
        public BooksController(IBookRepository repository)
        {
            _repository = repository;
        }
        
        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBooks()
        {
            var books = await _repository.GetAllBooksAsync();
            
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublicationYear = b.PublicationYear,
                Price = b.Price,
                CategoryName = b.Category?.Name
            });
            
            return Ok(bookDtos);
        }
        
        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> GetBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }
            
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Price = book.Price,
                CategoryName = book.Category?.Name
            };
            
            return Ok(bookDto);
        }
        
        // GET: api/Books/search?term=searchTerm
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> SearchBooks([FromQuery] string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                return BadRequest("Search term cannot be empty");
            }
            
            var books = await _repository.SearchBooksAsync(term);
            
            var bookDtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Author = b.Author,
                ISBN = b.ISBN,
                PublicationYear = b.PublicationYear,
                Price = b.Price,
                CategoryName = b.Category?.Name
            });
            
            return Ok(bookDtos);
        }
        
        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookDto>> CreateBook(CreateBookDto createBookDto)
        {
            var book = new Book
            {
                Title = createBookDto.Title,
                Author = createBookDto.Author,
                ISBN = createBookDto.ISBN,
                PublicationYear = createBookDto.PublicationYear,
                Price = createBookDto.Price,
                CategoryId = createBookDto.CategoryId
            };
            
            await _repository.CreateBookAsync(book);
            
            var bookDto = new BookDto
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Price = book.Price,
                CategoryName = null // Category is not loaded here
            };
            
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
        }
        
        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDto updateBookDto)
        {
            if (id != updateBookDto.Id)
            {
                return BadRequest("Id mismatch");
            }
            
            var book = await _repository.GetBookByIdAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }
            
            book.Title = updateBookDto.Title;
            book.Author = updateBookDto.Author;
            book.ISBN = updateBookDto.ISBN;
            book.PublicationYear = updateBookDto.PublicationYear;
            book.Price = updateBookDto.Price;
            book.CategoryId = updateBookDto.CategoryId;
            
            try
            {
                await _repository.UpdateBookAsync(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.BookExistsAsync(id))
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
        
        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _repository.GetBookByIdAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }
            
            await _repository.DeleteBookAsync(id);
            
            return NoContent();
        }
        
        // GET: api/Books/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return Ok(await _repository.GetAllCategoriesAsync());
        }
    }
    
    // Startup configuration
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(options =>
                options.UseInMemoryDatabase("BookStore"));
                
            services.AddScoped<IBookRepository, BookRepository>();
            
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore API", Version = "v1" });
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader());
            });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore API v1"));
                
                // Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<BookStoreContext>();
                    context.Database.EnsureCreated();
                }
            }
            
            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseCors("AllowAllOrigins");
            
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
    
    // Program entry point
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```
