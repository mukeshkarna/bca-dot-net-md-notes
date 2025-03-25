# Unit 1: Introduction to C# and .NET Framework

## Comprehensive Teaching Notes (7 Hours)

### 1. Object Orientation (1 Hour)

#### Core Concepts
- **Definition**: C# is a fully object-oriented programming language designed around the concept that everything interacts as objects
- **Historical Context**: Created by Microsoft in 2000 as part of the .NET initiative, designed by Anders Hejlsberg (who also designed Turbo Pascal and Delphi)

#### Fundamental OOP Principles in C#
- **Encapsulation**
  - The bundling of data (fields) with methods that operate on that data
  - Implementation is hidden behind interfaces (information hiding)
  - Access modifiers: `public`, `private`, `protected`, `internal`
  - Properties provide controlled access to private fields

```csharp
public class Person
{
    // Private field (encapsulated data)
    private string _name;
    
    // Public property (controlled access)
    public string Name 
    { 
        get { return _name; } 
        set { _name = value; } 
    }
    
    // Auto-implemented property (shorthand syntax)
    public int Age { get; set; }
}
```

- **Inheritance**
  - Creating new classes that inherit attributes and behaviors from existing classes
  - C# supports single inheritance for classes (but multiple interface inheritance)
  - `base` keyword to access parent class members
  - Method overriding with `virtual` and `override` keywords

```csharp
public class Person
{
    public string Name { get; set; }
    public virtual void Introduce() => Console.WriteLine($"Hi, I'm {Name}");
}

public class Student : Person
{
    public string Major { get; set; }
    public override void Introduce() => Console.WriteLine($"Hi, I'm {Name}, studying {Major}");
}
```

- **Polymorphism**
  - Objects can take many forms while sharing a common interface
  - Method overriding: Same method signature, different implementation
  - Method overloading: Same method name, different parameters
  - Interfaces and abstract classes enable polymorphic behavior

```csharp
// Polymorphism through interfaces
public interface IShape
{
    double CalculateArea();
}

public class Circle : IShape
{
    public double Radius { get; set; }
    public double CalculateArea() => Math.PI * Radius * Radius;
}

public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double CalculateArea() => Width * Height;
}

// Using polymorphism
IShape shape1 = new Circle { Radius = 5 };
IShape shape2 = new Rectangle { Width = 4, Height = 6 };
Console.WriteLine($"Area: {shape1.CalculateArea()}"); // Works for any IShape
```

#### C# OOP Features
- **Classes and Objects**
  - Classes are blueprints, objects are instances
  - Constructors and destructors
  - Static vs instance members
- **Interfaces**
  - Pure abstraction, no implementation
  - A class can implement multiple interfaces
- **Structs**
  - Value types (unlike classes which are reference types)
  - Used for small, lightweight data structures
- **Records** (C# 9.0+)
  - Immutable data classes with simplified syntax
  - Built-in value equality

#### Teaching Activities
- **Demonstration**: Create a class hierarchy representing various vehicles
- **Exercise**: Students implement a bank account class with proper encapsulation
- **Discussion**: When to use interfaces vs. abstract classes

### 2. Type Safety (1 Hour)

#### Core Concepts
- **Definition**: C# is a strongly-typed language where type checking is performed at compile-time
- **Benefits**: Prevents type-related runtime errors, improves code clarity, enables IDE tooling

#### Type System Features
- **Static Typing**
  - Variables must be declared with a specific type
  - Type checking occurs at compile time
  - Prevents type-related runtime errors

```csharp
string name = "John"; // Explicitly typed
var age = 30;         // Implicitly typed, but still statically typed
```

- **Type Conversions**
  - Implicit conversions (safe, no data loss)
  - Explicit conversions/casting (potentially unsafe)
  - `Convert` class and parsing methods
  - User-defined conversions

```csharp
// Implicit conversion (int → long)
int num = 100;
long bigNum = num; // Safe, no data loss

// Explicit conversion (potentially unsafe)
double price = 29.99;
int wholePrice = (int)price; // Loses decimal portion

// Parse methods
string input = "42";
int value = int.Parse(input);

// TryParse pattern (safer)
if (int.TryParse(input, out int result))
{
    Console.WriteLine($"Parsed: {result}");
}
```

- **Boxing and Unboxing**
  - Boxing: Converting a value type to a reference type (object)
  - Unboxing: Converting a boxed object back to a value type
  - Performance implications

```csharp
// Boxing (value type → reference type)
int number = 42;
object boxed = number;

// Unboxing (reference type → value type)
int unboxed = (int)boxed;
```

- **Generics**
  - Type-safe code without boxing/unboxing
  - Type parameters in classes, methods, interfaces
  - Constraints on type parameters

```csharp
// Generic class
public class Stack<T>
{
    private T[] items;
    private int count;
    
    public void Push(T item) { /* ... */ }
    public T Pop() { /* ... */ }
}

// Using generics
var numbers = new Stack<int>();  // Type-safe for integers
var names = new Stack<string>(); // Type-safe for strings
```

#### Advanced Type Features
- **Nullable Types**
  - Adding `null` capability to value types
  - Denoted with `?` suffix
  - Null-conditional operator `?.` and null-coalescing operator `??`

```csharp
int? nullableInt = null;  // Can be null
int regularInt = nullableInt ?? 0;  // Default if null
```

- **Dynamic Type**
  - Type checking at runtime instead of compile time
  - Used for interop scenarios (COM, scripting languages)
  - Trade-off between flexibility and safety

```csharp
dynamic value = "Hello";
value = 42;  // Type can change at runtime
```

- **Pattern Matching**
  - Enhanced type checking at runtime
  - Switch expressions, property patterns, etc.

```csharp
object item = "test";
if (item is string text)
{
    Console.WriteLine($"Length: {text.Length}");
}
```

#### Teaching Activities
- **Demonstration**: Show compiler errors from type mismatches
- **Exercise**: Convert between different types safely
- **Discussion**: Strong typing vs. dynamic typing trade-offs

### 3. Memory Management (1 Hour)

#### Core Concepts
- **Automatic Memory Management**: C# uses garbage collection to handle memory
- **Value vs. Reference Types**: Different memory allocation strategies

#### Garbage Collection

The Garbage Collector works by identifying and collecting objects that are no longer accessible by the application. It uses a process called “mark and sweep” to determine which objects are still in use and which are not.

When an application creates objects, they are stored in what’s called the “managed heap.” The Garbage Collector then periodically checks this heap to find objects that are no longer referenced by the application. It marks these objects as eligible for garbage collection.

During the collection process, the Garbage Collector halts the execution of the application temporarily and performs two steps:

- **Mark:** It traverses the object graph, starting from the root objects (such as static variables and method parameters), to mark all the objects that are still in use.

- **Sweep:** It frees up memory by deallocating the memory occupied by the objects that were not marked during the marking phase. This memory is then added back to the available memory pool.

- **How It Works**
  - Tracks object references
  - Reclaims memory from unused objects
  - Runs in a separate thread
  - Generational design (Gen 0, 1, 2)

- **Garbage Collector Behavior**
  - Memory pressure triggers collection
  - Objects move between generations
  - Compaction to reduce fragmentation
  - Finalization process for cleanup

- **GC Optimization**
  - `GC.Collect()` (rarely needed)
  - Weak references
  - Large Object Heap considerations

#### Memory Types
- **Stack Memory**
  - Fast allocation/deallocation
  - Stores value types and references
  - Method local variables
  - Limited in size
  - Deterministic cleanup

- **Heap Memory**
  - Managed by the garbage collector
  - Stores all reference type instances
  - Dynamic size
  - Non-deterministic cleanup

```csharp
void MemoryExample()
{
    int x = 10;                  // Value type on stack
    Person p = new Person();     // Reference on stack, object on heap
    
    // Stack frame deleted when method exits
    // Person object remains on heap until GC
}
```

#### Resource Management
- **IDisposable Pattern**
  - For deterministic cleanup of unmanaged resources
  - `Dispose()` method
  - `using` statement for automatic disposal

```csharp
// Manual disposal
FileStream file = new FileStream("data.txt", FileMode.Open);
try
{
    // Use the file
}
finally
{
    if (file != null)
        file.Dispose();
}

// Automatic disposal with using
using (FileStream file = new FileStream("data.txt", FileMode.Open))
{
    // Use the file
} // Dispose called automatically
```

- **Finalizers**
  - Last resort for cleanup
  - Non-deterministic execution
  - Performance impact
  - Rarely needed with proper `IDisposable` implementation

```csharp
public class ResourceHeavy
{
    private IntPtr _handle; // Unmanaged resource
    
    // Constructor acquires resource
    public ResourceHeavy() => _handle = NativeMethods.Allocate();
    
    // Finalizer (last resort)
    ~ResourceHeavy() => Cleanup();
    
    // IDisposable implementation
    public void Dispose()
    {
        Cleanup();
        GC.SuppressFinalize(this);
    }
    
    private void Cleanup()
    {
        if (_handle != IntPtr.Zero)
        {
            NativeMethods.Free(_handle);
            _handle = IntPtr.Zero;
        }
    }
}
```

- **Memory Leaks**
  - Common causes: event handlers, static references
  - Detection tools: memory profilers
  - Prevention strategies

#### Teaching Activities
- **Demonstration**: View memory allocation in profiler
- **Exercise**: Implement the IDisposable pattern correctly
- **Discussion**: Performance implications of GC vs manual memory management

### 4. Platform Support (1 Hour)

#### Core Concepts
- **Cross-Platform Development**: C# has evolved from Windows-only to cross-platform
- **Runtime Environment**: .NET Core/.NET 5+ enables true cross-platform execution

#### Supported Platforms
- **Desktop**
  - Windows (WPF, Windows Forms, UWP)
  - macOS (via .NET MAUI, Avalonia, etc.)
  - Linux (via GTK#, Avalonia, etc.)

- **Web**
  - ASP.NET Core (cross-platform web framework)
  - Blazor (C# in the browser via WebAssembly)
  - RESTful APIs and microservices

- **Mobile**
  - iOS and Android (via .NET MAUI, formerly Xamarin)
  - Progressive Web Apps

- **Cloud**
  - Azure Functions
  - AWS Lambda (via .NET Core)
  - Containerization with Docker

- **IoT and Embedded**
  - IoT devices
  - Microcontrollers (.NET nanoFramework)

#### Platform-Specific Considerations
- **Runtime Differences**
  - File system paths and separators
  - Environment variables
  - Process management
  - UI frameworks

- **Handling Platform Differences**
  - Compiler directives (`#if WINDOWS, #if LINUX`)
  - Runtime environment detection
  - Dependency injection for platform services

```csharp
// Platform detection
if (OperatingSystem.IsWindows())
{
    // Windows-specific code
}
else if (OperatingSystem.IsMacOS())
{
    // macOS-specific code
}
else if (OperatingSystem.IsLinux())
{
    // Linux-specific code
}

// Compile-time platform targeting
#if WINDOWS
    // Windows-specific code
#elif LINUX
    // Linux-specific code
#endif
```

- **Deployment Strategies**
  - Framework-dependent deployment
  - Self-contained deployment
  - Single-file applications
  - Ahead-of-Time (AOT) compilation

#### Cross-Platform Development Approaches
- **Code Sharing Strategies**
  - Shared Projects
  - .NET Standard libraries
  - Multi-targeting
  - Conditional compilation

- **UI Approaches**
  - Platform-specific UI (native look and feel)
  - Cross-platform UI frameworks (MAUI, Avalonia)
  - Web-based UI (Blazor, Electron.NET)

- **Testing Across Platforms**
  - CI/CD pipelines for multiple OSes
  - Virtual machines and containers
  - Testing libraries with cross-platform support

#### Teaching Activities
- **Demonstration**: Run the same C# code on Windows and Linux
- **Exercise**: Create a simple cross-platform console application
- **Discussion**: Native vs. cross-platform development trade-offs

### 5. C# and CLR (Common Language Runtime) (1 Hour)

#### Core Concepts
- **Execution Model**: C# code compiles to Intermediate Language (IL) code
- **Runtime Environment**: CLR manages execution, JIT compilation, memory, security

#### Compilation Process
- **C# Compilation Pipeline**
  - Source code (`.cs` files)
  - Compiler (`csc.exe` or Roslyn compiler)
  - Intermediate Language (IL) code in assemblies (`.dll` or `.exe`)
  - Just-In-Time compilation to native code
  - Execution

```
.cs files → Compiler → IL code → JIT → Native code → Execution
```

- **IL Code Examination**
  - Tools: ILDASM, dnSpy, dotPeek
  - What IL code looks like
  - Metadata in assemblies

```csharp
// C# code
public int Add(int a, int b)
{
    return a + b;
}

// Approximate IL code
.method public instance int32 Add(int32 a, int32 b) cil managed
{
    .maxstack 2
    ldarg.1     // Load argument 'a'
    ldarg.2     // Load argument 'b'
    add         // Add them together
    ret         // Return the result
}
```

- **Assembly Structure**
  - Manifest (metadata about the assembly)
  - Type metadata
  - IL code
  - Resources

#### Common Language Runtime
- **Core Services**
  - Memory management (garbage collection)
  - Type safety enforcement
  - Exception handling
  - Thread management
  - Security (Code Access Security)

- **JIT Compilation**
  - Converting IL to machine code at runtime
  - Optimization based on runtime information
  - Tiered compilation (quick startup, progressive optimization)
  - NGEN and ReadyToRun for ahead-of-time compilation

- **Application Domains**
  - Isolation boundaries within a process (.NET Framework)
  - Replaced by AssemblyLoadContext in .NET Core/.NET 5+

- **Language Interoperability**
  - Common Type System (CTS)
  - Common Language Specification (CLS)
  - Using multiple .NET languages together

```csharp
// C# code calling F# library
using FSharpLibrary;

var calculator = new MathFunctions();
int result = calculator.Calculate(5, 10);
```

#### CLR Evolution
- **.NET Framework CLR**
  - Windows-only
  - Tightly integrated with OS
  - In maintenance mode

- **.NET Core CLR (CoreCLR)**
  - Cross-platform
  - Open-source
  - Performance-focused
  - Side-by-side versioning

- **.NET 5+ Runtime**
  - Unified successor to .NET Framework and .NET Core
  - Modern features and performance

#### Teaching Activities
- **Demonstration**: Using ILDASM to examine compiled code
- **Exercise**: Create and use a mixed-language solution
- **Discussion**: Performance considerations with JIT compilation

### 6. CLR and .NET Framework (1 Hour)

#### Core Concepts
- **Framework Architecture**: How the CLR fits into the larger .NET Framework
- **Framework Components**: Libraries and services that make up the ecosystem

#### .NET Framework Structure
- **Common Language Runtime (CLR)**
  - Execution engine
  - Memory management
  - Type system
  - Exception handling

- **Base Class Library (BCL)**
  - Fundamental types
  - Collections
  - I/O operations
  - Threading
  - Common utilities

- **Framework Class Library (FCL)**
  - Superset of BCL
  - All libraries included in .NET Framework
  - Domain-specific functionality
  - UI frameworks

```
┌─────────────────────────────────────────┐
│              Applications               │
├─────────────────────────────────────────┤
│  ASP.NET  │ WPF │ WinForms │ Services   │
├─────────────────────────────────────────┤
│        Framework Class Library          │
├─────────────────────────────────────────┤
│         Base Class Library              │
├─────────────────────────────────────────┤
│    Common Language Runtime (CLR)        │
└─────────────────────────────────────────┘
```

#### Evolution of .NET Frameworks
- **.NET Framework (Original)**
  - Windows-only platform
  - Tightly coupled with Windows
  - Versions 1.0 through 4.8
  - Maintenance mode only now

- **.NET Core**
  - Cross-platform, open-source reimagining
  - Modular design
  - Performance improvements
  - Versions 1.0 through 3.1

- **.NET 5+**
  - Unified platform (merger of .NET Core and .NET Framework)
  - Modern features
  - Performance-focused
  - Regular release schedule

- **Mono**
  - Open-source alternative implementation
  - Powers Xamarin
  - Used in game development (Unity)

#### Key Framework Libraries
- **System Namespace**
  - Core types (`Object`, `String`, `DateTime`, etc.)
  - Collections
  - Diagnostics
  - Reflection

- **System.IO**
  - File operations
  - Streams
  - Readers and writers

- **System.Net**
  - Web requests and responses
  - Sockets
  - Network utilities

- **System.Threading**
  - Thread management
  - Synchronization primitives
  - Task Parallel Library (TPL)
  - Asynchronous programming

- **System.Data**
  - Database connectivity
  - Data access abstractions
  - ADO.NET

#### Teaching Activities
- **Demonstration**: Explore core framework libraries in documentation
- **Exercise**: Use various framework libraries to solve problems
- **Discussion**: Framework selection for different project types

### 7. .NET Standard 2.0 and Applied Technologies (1 Hour)

#### Core Concepts
- **.NET Standard**: A specification that unifies different .NET implementations
- **Applied Technologies**: Practical applications of C# and .NET

#### .NET Standard
- **Purpose and Benefits**
  - Code sharing across platforms
  - Library compatibility
  - Predictable API surface

- **Version History**
  - .NET Standard 1.0-1.6
  - .NET Standard 2.0 (major milestone)
  - .NET Standard 2.1
  - Future: .NET 5+ as the unification point

- **Implementation Support**
  - .NET Framework support (≥4.6.1 supports .NET Standard 2.0)
  - .NET Core/5+ support
  - Xamarin support
  - Mono support

```csharp
// Library targeting .NET Standard 2.0
// Can be used by .NET Framework, .NET Core, Xamarin
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>netstandard2.0</TargetFramework>
  </PropertyGroup>
</Project>
```

- **API Coverage**
  - Core functionality
  - Gaps compared to specific implementations
  - How to handle platform-specific features

#### Applied Technologies
- **Web Development**
  - ASP.NET Core MVC
  - Blazor (WebAssembly and Server)
  - RESTful APIs
  - gRPC services

```csharp
// ASP.NET Core minimal API example
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/users/{id}", (int id) => $"User: {id}");

app.Run();
```

- **Desktop Development**
  - WPF (Windows Presentation Foundation)
  - Windows Forms
  - UWP (Universal Windows Platform)
  - MAUI (Multi-platform App UI)

- **Mobile Development**
  - .NET MAUI (Evolution of Xamarin.Forms)
  - Native iOS and Android with .NET

```csharp
// MAUI example
public class MainPage : ContentPage
{
    int count = 0;
    
    public MainPage()
    {
        var button = new Button
        {
            Text = "Click me"
        };
        
        button.Clicked += OnButtonClicked;
        Content = new StackLayout
        {
            Children = { button }
        };
    }
    
    void OnButtonClicked(object sender, EventArgs e)
    {
        count++;
        ((Button)sender).Text = $"Clicked {count} times";
    }
}
```

- **Cloud and Microservices**
  - Azure Functions
  - ASP.NET Core microservices
  - Docker containerization
  - Kubernetes orchestration

- **Data Access and ORM**
  - Entity Framework Core
  - Dapper
  - ADO.NET

```csharp
// Entity Framework Core example
public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Connection String");
    }
}

// Using the context
using (var context = new BloggingContext())
{
    var blogs = context.Blogs.Where(b => b.Rating > 3).ToList();
}
```

- **Machine Learning**
  - ML.NET
  - Integration with Python (via .NET for Python)

- **Gaming**
  - Unity game development
  - MonoGame framework

#### Teaching Activities
- **Demonstration**: Create a library targeting .NET Standard
- **Exercise**: Build a simple web API with ASP.NET Core
- **Discussion**: Selecting the right .NET technology for different scenarios

### Recommended Lab Exercises

1. **Object-Oriented Programming**
   - Create a class hierarchy for a university management system
   - Implement proper encapsulation, inheritance, and polymorphism

2. **Type Safety and Conversion**
   - Build a currency converter with appropriate type handling
   - Practice safe type conversions and error handling

3. **Memory Management**
   - Create a resource-intensive application
   - Implement proper resource disposal with IDisposable
   - Monitor memory usage

4. **Cross-Platform Development**
   - Develop a console application that runs on both Windows and Linux
   - Handle platform-specific differences appropriately

5. **Framework Exploration**
   - Build a mini-project that utilizes multiple framework libraries
   - Compare library availability across different .NET versions

### Assessment Ideas

1. **Multiple Choice Quiz**
   - Basic concepts of C# and .NET
   - Type system questions
   - Memory management scenarios

2. **Coding Assignments**
   - Implement a specified class hierarchy
   - Fix memory leaks in provided code
   - Create a cross-platform utility

3. **Research Presentation**
   - History and evolution of .NET
   - Comparison of .NET implementations
   - Real-world case studies of C# applications

4. **Practical Exam**
   - Timed programming challenge
   - Debugging exercises
   - Code optimization tasks

### Additional Resources

1. **Books**
   - "C# in Depth" by Jon Skeet
   - "CLR via C#" by Jeffrey Richter
   - "Pro C# 9 with .NET 5" by Andrew Troelsen and Phil Japikse

2. **Online Tutorials**
   - Microsoft Learn C# Path
   - Pluralsight C# courses
   - freeCodeCamp C# tutorials

3. **Documentation**
   - Microsoft .NET Documentation
   - C# Language Specification
   - .NET API Browser

4. **Community Resources**
   - Stack Overflow
   - .NET Foundation
   - GitHub sample repositories
