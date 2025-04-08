# Unit 3: Creating Types in C#

## Classes
- A class is the fundamental building block in C# that acts as a blueprint for creating objects
- Classes encapsulate data (fields/properties) and behavior (methods)
- Classes support the four pillars of OOP: encapsulation, inheritance, polymorphism, and abstraction
- Syntax: `class ClassName { /* members */ }`

### Class Structure
A class consists of:
- Fields (data members)
- Properties (accessor methods)
- Methods (functions)
- Constructors (special methods for initialization)
- Events (for notification)
- Indexers (for array-like access)

### Basic Class Syntax
```csharp
public class Person
{
    // Fields
    private string name;
    private int age;
    
    // Properties
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    
    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    
    // Methods
    public void Introduce()
    {
        Console.WriteLine($"Hello, my name is {name} and I'm {age} years old.");
    }
}
```

## Constructors and Deconstructors

### Constructors
- Constructors are special methods called when an object is created
- They initialize the object's state and allocate resources
- Default constructor has no parameters and is provided automatically if no constructor is defined
- Parameterized constructors accept arguments to initialize object properties
- Syntax for constructor: `public ClassName() { /* initialization code */ }`

#### Default Constructor
If you don't provide any constructor, C# creates a default parameterless constructor.

#### Parameterized Constructor
```csharp
public class Person
{
    private string name;
    private int age;
    
    // Parameterized constructor
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}
```

#### Constructor Overloading
A class can have multiple constructors with different parameters.

```csharp
public class Person
{
    private string name;
    private int age;
    
    // Default constructor
    public Person()
    {
        name = "Unknown";
        age = 0;
    }
    
    // Parameterized constructor
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}
```

#### Constructor Chaining
Constructors can call other constructors using the `this` keyword.

```csharp
public class Person
{
    private string name;
    private int age;
    
    public Person() : this("Unknown", 0)
    {
        // This constructor calls the parameterized constructor
    }
    
    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }
}
```

### Deconstructors (Destructors)
Deconstructors are used to deconstruct objects into their constituent parts, allowing you to extract multiple values from an object at once.

- Destructors (finalizers) are used to clean up unmanaged resources
- Syntax for destructor: `~ClassName() { /* cleanup code */ }`

```csharp
public class Point
{
    public int X { get; }
    public int Y { get; }
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    // Deconstructor
    public void Deconstruct(out int x, out int y)
    {
        x = X;
        y = Y;
    }
}

// Usage
var point = new Point(3, 4);
(int x, int y) = point; // Deconstruction
```

## `this` Reference
- The 'this' keyword refers to the current instance of the class
- Used to distinguish between class members and parameters with the same name
- Can be used to call other constructors in the same class
- Helps to make the code more readable and maintainable

```csharp
public class Customer
{
    private string name;
    
    public void SetName(string name)
    {
        // Use 'this' to refer to the class field
        this.name = name;
    }
    
    public void PassToMethod()
    {
        // Pass 'this' to another method
        ProcessCustomer(this);
    }
    
    private void ProcessCustomer(Customer customer)
    {
        // Process the customer
    }
}
```

## Properties
- Properties provide a flexible mechanism to read, write, or compute the value of private fields
- They expose fields to the outside world with additional logic (validation, computation)
- Auto-implemented properties allow for simplified property declaration
- Properties can be read-only, write-only, or read-write
- Syntax: `public Type PropertyName { get; set; }`

### Basic Property Syntax
```csharp
public class Person
{
    private string name; // Backing field
    
    // Property with get and set accessors
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}
```

### Auto-Implemented Properties
For simple properties that don't require custom logic, C# provides a shorter syntax:

```csharp
public class Person
{
    // Auto-implemented property (C# creates the backing field automatically)
    public string Name { get; set; }
}
```

### Property Access Modifiers
You can specify different access levels for get and set accessors:

```csharp
public class Person
{
    // Public getter, private setter
    public string Name { get; private set; }
    
    // Public getter, internal setter
    public int Age { get; internal set; }
}
```

### Read-Only Properties
Properties that can only be set during initialization:

```csharp
public class Person
{
    // Read-only property (can only be set in constructor or initializer)
    public string Id { get; }
    
    public Person(string id)
    {
        Id = id;
    }
}
```

### Computed Properties
Properties that calculate values rather than storing them:

```csharp
public class Rectangle
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    // Computed property
    public double Area
    {
        get { return Width * Height; }
    }
}
```

## Indexers
- Allow objects to be indexed like arrays
- Enable access to object elements using square bracket notation
- Can be overloaded to accept different parameter types
- Syntax: `public Type this[IndexType index] { get; set; }`

```csharp
public class StringCollection
{
    private string[] strings = new string[10];
    
    // Indexer
    public string this[int index]
    {
        get { return strings[index]; }
        set { strings[index] = value; }
    }
}

// Usage
var collection = new StringCollection();
collection[0] = "Hello";
string str = collection[0]; // str is "Hello"
```

Indexers can be overloaded to accept different types of indices:

```csharp
public class DictionaryCollection
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    
    // String indexer
    public int this[string key]
    {
        get { return items[key]; }
        set { items[key] = value; }
    }
    
    // Integer indexer
    public KeyValuePair<string, int> this[int index]
    {
        get { return items.ElementAt(index); }
    }
}
```

## Static Constructors and Classes

### Static Constructor
A static constructor is used to initialize static members of a class. It's called automatically before any instance of the class is created or any static member is accessed.

```csharp
public class DatabaseConnection
{
    public static string ConnectionString { get; private set; }
    
    // Static constructor
    static DatabaseConnection()
    {
        ConnectionString = "Server=myserver;Database=mydb;User Id=username;Password=password;";
        Console.WriteLine("Static constructor called");
    }
    
    // Instance constructor
    public DatabaseConnection()
    {
        Console.WriteLine("Instance constructor called");
    }
}
```

Key points about static constructors:
- They have no access modifiers
- They can't take parameters
- They run only once, when the class is first used
- You can't directly call them

### Static Classes
A static class can only contain static members and cannot be instantiated. It's useful for utility functions that don't require object state.

```csharp
public static class MathUtils
{
    public static double PI = 3.14159;
    
    public static double Square(double number)
    {
        return number * number;
    }
    
    public static double Cube(double number)
    {
        return number * number * number;
    }
}

// Usage
double area = MathUtils.PI * MathUtils.Square(5);
```

Key points about static classes:
- They can't be instantiated
- They can't inherit from other classes or be inherited from
- They can only contain static members
- They're sealed automatically

## Finalizers

Finalizers (also called destructors) are used to perform cleanup operations before an object is reclaimed by garbage collection. In C#, a finalizer is defined using a tilde (~) followed by the class name.
- Also known as destructors, they clean up unmanaged resources
- Cannot be explicitly called (invoked by garbage collector)
- Should be implemented when a class uses unmanaged resources
- Generally used with the IDisposable pattern for proper resource management
- Syntax: `~ClassName() { /* cleanup code */ }`

```csharp
public class ResourceHolder
{
    private IntPtr resource;
    
    public ResourceHolder()
    {
        // Acquire the resource
        resource = AcquireResource();
    }
    
    // Finalizer
    ~ResourceHolder()
    {
        // Release the resource if it hasn't been released already
        if (resource != IntPtr.Zero)
        {
            ReleaseResource(resource);
            resource = IntPtr.Zero;
        }
    }
    
    private IntPtr AcquireResource()
    {
        // Code to acquire a resource
        return new IntPtr(1);
    }
    
    private void ReleaseResource(IntPtr handle)
    {
        // Code to release the resource
    }
}
```

Important points about finalizers:
- They can't be explicitly called (only the garbage collector calls them)
- They can't take parameters
- They run on a separate finalization thread
- They introduce overhead in garbage collection
- Objects with finalizers require more GC cycles to be collected
- In modern C#, the IDisposable pattern is preferred over finalizers for most cleanup scenarios

## Dynamic Binding

Dynamic binding defers type checking until runtime rather than at compile time. The `dynamic` keyword tells the compiler to bypass static type checking.
- Resolves types and members at runtime instead of compile-time
- Implemented using the 'dynamic' keyword
- Bypasses compile-time type checking
- Useful for interoperability with dynamic languages and COM objects
- Can lead to runtime errors if used incorrectly

```csharp
// Using dynamic binding
dynamic obj = "Hello, World!";
Console.WriteLine(obj.Length); // Resolved at runtime

obj = 123;
Console.WriteLine(obj.ToString()); // Method binding resolved at runtime
```

Dynamic binding is useful for:
- Working with COM objects
- Interoperating with dynamic languages
- Simplifying code that would otherwise need reflection
- Working with objects whose structure isn't known at compile time

However, it comes with some downsides:
- No IntelliSense support
- Type errors only detected at runtime
- Potential performance impact
- Less safe than static typing

## Operator Overloading

Operator overloading allows custom types to work with C# operators like +, -, *, /, ==, etc.
- Allows custom implementation of operators for user-defined types
- Makes code more intuitive and readable
- Common operators to overload: +, -, *, /, ==, !=, <, >
- Must be declared as public and static
- Syntax: `public static ReturnType operator OperatorSymbol(Parameters) { /* code */ }`

```csharp
public struct Complex
{
    public double Real { get; }
    public double Imaginary { get; }
    
    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }
    
    // Overload the + operator
    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
    
    // Overload the - operator
    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }
    
    // Overload the * operator
    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real);
    }
    
    // Overload equality operator
    public static bool operator ==(Complex a, Complex b)
    {
        return a.Real == b.Real && a.Imaginary == b.Imaginary;
    }
    
    // When overloading ==, you must also overload !=
    public static bool operator !=(Complex a, Complex b)
    {
        return !(a == b);
    }
    
    // Override Equals and GetHashCode to maintain consistency
    public override bool Equals(object obj)
    {
        if (obj is Complex other)
        {
            return this == other;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return Real.GetHashCode() ^ Imaginary.GetHashCode();
    }
}

// Usage
Complex c1 = new Complex(2, 3);
Complex c2 = new Complex(1, 2);
Complex sum = c1 + c2; // Uses the overloaded + operator
```

## Inheritance

Inheritance allows a class to acquire the properties and behaviors of another class. The derived class (child) inherits from the base class (parent).
- Allows a class to inherit properties and methods from another class
- Creates an "is-a" relationship between classes
- Base class (parent) passes its characteristics to derived class (child)
- C# supports single inheritance (a class can inherit from only one class)
- Syntax: `class DerivedClass : BaseClass { /* members */ }`

```csharp
// Base class
public class Animal
{
    public string Name { get; set; }
    
    public Animal(string name)
    {
        Name = name;
    }
    
    public virtual void MakeSound()
    {
        Console.WriteLine("The animal makes a sound");
    }
}

// Derived class
public class Dog : Animal
{
    public string Breed { get; set; }
    
    public Dog(string name, string breed) : base(name)
    {
        Breed = breed;
    }
    
    // Override the base class method
    public override void MakeSound()
    {
        Console.WriteLine("Woof! Woof!");
    }
    
    // Add a new method
    public void Fetch()
    {
        Console.WriteLine($"{Name} is fetching the ball!");
    }
}
```

Key points about inheritance:
- C# supports single inheritance for classes (a class can inherit from only one base class)
- All classes implicitly inherit from `Object`
- The `virtual` keyword allows a method to be overridden in derived classes
- The `override` keyword indicates that a method overrides a base class method
- The `sealed` keyword prevents further inheritance or method overriding

## Abstract Classes and Methods

Abstract classes provide a base for other classes to inherit from, but cannot be instantiated themselves. They can contain abstract methods that derived classes must implement.
- Abstract classes cannot be instantiated and are meant to be inherited
- They can contain abstract methods (methods without implementation)
- Derived classes must implement all abstract methods
- Abstract classes can contain both abstract and concrete methods
- Syntax for abstract class: `abstract class ClassName { /* members */ }`
- Syntax for abstract method: `abstract ReturnType MethodName(Parameters);`

```csharp
// Abstract base class
public abstract class Shape
{
    // Regular property
    public string Color { get; set; }
    
    // Regular constructor
    public Shape(string color)
    {
        Color = color;
    }
    
    // Regular method
    public void DisplayColor()
    {
        Console.WriteLine($"This shape is {Color}");
    }
    
    // Abstract method - no implementation
    public abstract double CalculateArea();
    
    // Virtual method - can be overridden
    public virtual void Draw()
    {
        Console.WriteLine("Drawing a shape");
    }
}

// Concrete derived class
public class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(string color, double radius) : base(color)
    {
        Radius = radius;
    }
    
    // Must implement the abstract method
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    // Optional: override the virtual method
    public override void Draw()
    {
        Console.WriteLine($"Drawing a circle with radius {Radius}");
    }
}
```

Key points about abstract classes:
- Cannot be instantiated directly
- Can contain abstract methods (methods without implementations)
- Derived classes must implement all abstract methods
- Can contain regular (non-abstract) methods, properties, fields, etc.
- Useful for creating a common base with some shared functionality

## `base` Keyword

The `base` keyword is used to access members of the base class from within a derived class:
- References the base class (parent) of the current instance
- Used to access members of the base class that are hidden by derived class members
- Used to call base class constructors from derived class constructors
- Syntax: `base.MemberName` or `base(parameters)`

1. Call a base class constructor
2. Access a base class method that has been overridden
3. Access a base class property that has been hidden

```csharp
public class Vehicle
{
    public string Make { get; set; }
    public string Model { get; set; }
    
    public Vehicle(string make, string model)
    {
        Make = make;
        Model = model;
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Vehicle: {Make} {Model}");
    }
}

public class Car : Vehicle
{
    public int Year { get; set; }
    
    // Call base constructor
    public Car(string make, string model, int year) : base(make, model)
    {
        Year = year;
    }
    
    // Override method but also call base implementation
    public override void DisplayInfo()
    {
        base.DisplayInfo(); // Call base class method
        Console.WriteLine($"Year: {Year}");
    }
}
```

## Overloading

Method overloading allows multiple methods with the same name but different parameters. The compiler determines which method to call based on the arguments passed.
- Creates multiple methods with the same name but different parameters
- Resolved at compile-time based on the method signature
- Differs from overriding, which is resolved at runtime
- Method signatures differ in the number, type, or order of parameters
- Return type alone is not sufficient to distinguish overloaded methods

```csharp
public class Calculator
{
    // Overloaded methods
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    public double Add(double a, double b)
    {
        return a + b;
    }
    
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
    
    public string Add(string a, string b)
    {
        return a + b;
    }
}

// Usage
var calc = new Calculator();
int sum1 = calc.Add(5, 10);         // Calls first method
double sum2 = calc.Add(2.5, 3.5);   // Calls second method
int sum3 = calc.Add(1, 2, 3);       // Calls third method
string text = calc.Add("Hello, ", "World!"); // Calls fourth method
```

Methods can be overloaded based on:
- Different number of parameters
- Different parameter types
- Different parameter order
- Different parameter modifiers (ref, out)

## Object Type

The `object` (or `System.Object`) type is the ultimate base class for all types in C#. Every class implicitly inherits from `object`.
- The ultimate base class for all types in C#
- All types implicitly inherit from System.Object
- Provides basic functionality like ToString(), Equals(), GetHashCode()
- Can be used to store any type through implicit conversion

Key methods available in the `object` class:
- `Equals()`: Determines whether two object instances are equal
- `GetHashCode()`: Returns a hash code for the object
- `GetType()`: Gets the type of the current instance
- `ToString()`: Returns a string representation of the object

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }
    
    // Override ToString method from Object
    public override string ToString()
    {
        return $"{Name}, {Age} years old";
    }
    
    // Override Equals method from Object
    public override bool Equals(object obj)
    {
        if (obj is Person other)
        {
            return Name == other.Name && Age == other.Age;
        }
        return false;
    }
    
    // Override GetHashCode method from Object
    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Age.GetHashCode();
    }
}
```

## Structs

Structs are value types (unlike classes, which are reference types). They're useful for small, lightweight objects.
- Value types (stored on stack) unlike classes (stored on heap)
- More efficient for small data structures
- Cannot have explicit parameterless constructors
- Cannot inherit from other structs or classes, but can implement interfaces
- Syntax: `struct StructName { /* members */ }`

```csharp
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    public double DistanceFromOrigin()
    {
        return Math.Sqrt(X * X + Y * Y);
    }
    
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
```

Key differences between structs and classes:
- Structs are value types, classes are reference types
- Structs are stored on the stack, classes on the heap
- Structs cannot inherit or be inherited from (except they implicitly inherit from `System.ValueType`)
- Structs cannot have a finalizer
- Struct constructors must initialize all fields
- Structs cannot have an explicit parameterless constructor
- Structs are generally more efficient for small data structures

When to use structs:
- For small data structures that primarily contain data
- When instances are short-lived or frequently embedded in other objects
- When value semantics are desired (e.g., equality means identical values, not identical references)

## Access Modifiers

Access modifiers control the accessibility of classes, methods, properties, and other members.
- Control the accessibility/visibility of types and members
- public: Accessible from anywhere
- private: Accessible only within the containing class
- protected: Accessible within the containing class and derived classes
- internal: Accessible within the same assembly
- protected internal: Accessible within the same assembly or derived classes
- private protected: Accessible within the containing class or derived classes in the same assembly

| Modifier | Description |
|----------|-------------|
| `public` | Accessible from any code |
| `private` | Accessible only within the containing type |
| `protected` | Accessible within the containing type and derived types |
| `internal` | Accessible within the same assembly |
| `protected internal` | Accessible within the same assembly OR derived types |
| `private protected` | Accessible within the same assembly AND derived types |

```csharp
public class AccessExample
{
    // Fields with different access levels
    public int PublicField;
    private int privateField;
    protected int protectedField;
    internal int internalField;
    protected internal int protectedInternalField;
    private protected int privateProtectedField;
    
    // Methods with different access levels
    public void PublicMethod() { }
    private void PrivateMethod() { }
    protected void ProtectedMethod() { }
    internal void InternalMethod() { }
    protected internal void ProtectedInternalMethod() { }
    private protected void PrivateProtectedMethod() { }
}
```

## Interfaces

Interfaces define a contract that implementing classes must follow. They can include methods, properties, events, and indexers, but do not provide implementations.
- Define a contract that implementing classes must follow
- Cannot contain implementation, only method signatures
- A class can implement multiple interfaces
- Promote loose coupling and facilitate polymorphism
- Syntax: `interface IInterfaceName { /* member declarations */ }`

```csharp
// Interface definition
public interface IShape
{
    double CalculateArea();
    double CalculatePerimeter();
    string GetShapeType();
}

// Implementing the interface
public class Rectangle : IShape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    // Implementing interface methods
    public double CalculateArea()
    {
        return Width * Height;
    }
    
    public double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
    
    public string GetShapeType()
    {
        return "Rectangle";
    }
}

// Another implementation
public class Circle : IShape
{
    public double Radius { get; set; }
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
    
    public string GetShapeType()
    {
        return "Circle";
    }
}
```

Since C# 8.0, interfaces can also include default implementations:

```csharp
public interface ILogger
{
    void LogError(string message);
    void LogWarning(string message);
    
    // Default implementation
    void LogInfo(string message)
    {
        Console.WriteLine($"INFO: {message}");
    }
}
```

Key points about interfaces:
- A class can implement multiple interfaces
- Interfaces can inherit from other interfaces
- Interface members are implicitly public
- Starting with C# 8.0, interfaces can have default implementations
- Interfaces are used to define common functionality across unrelated classes

## Enums

Enums define a set of named constants. They improve code readability and type safety.

```csharp
// Basic enum definition
public enum DayOfWeek
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday
}

// Usage
DayOfWeek today = DayOfWeek.Monday;
if (today == DayOfWeek.Saturday || today == DayOfWeek.Sunday)
{
    Console.WriteLine("It's the weekend!");
}
```

You can specify the underlying type and values:

```csharp
// Enum with explicit values
public enum HttpStatusCode : int
{
    OK = 200,
    Created = 201,
    Accepted = 202,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500
}

// Usage
HttpStatusCode status = HttpStatusCode.OK;
Console.WriteLine($"Status code: {(int)status}"); // Outputs: Status code: 200
```

Enums can be used with flags for bitwise operations:

```csharp
// Enum with flags
[Flags]
public enum Permissions
{
    None = 0,
    Read = 1,
    Write = 2,
    Execute = 4,
    Delete = 8,
    All = Read | Write | Execute | Delete
}

// Usage
Permissions userPermissions = Permissions.Read | Permissions.Write;

// Check if user has read permission
if ((userPermissions & Permissions.Read) == Permissions.Read)
{
    Console.WriteLine("User has read permission");
}

// Add execute permission
userPermissions |= Permissions.Execute;

// Remove write permission
userPermissions &= ~Permissions.Write;
```

## Generics

Generics allow you to define type-safe data structures without committing to specific data types. They provide type safety, code reuse, and performance benefits.

### Generic Classes
```csharp
// Generic class
public class GenericList<T>
{
    private T[] items;
    private int count;
    
    public GenericList(int capacity)
    {
        items = new T[capacity];
        count = 0;
    }
    
    public void Add(T item)
    {
        if (count < items.Length)
        {
            items[count] = item;
            count++;
        }
    }
    
    public T GetItem(int index)
    {
        if (index >= 0 && index < count)
        {
            return items[index];
        }
        throw new IndexOutOfRangeException();
    }
}

// Usage
var intList = new GenericList<int>(10);
intList.Add(5);
intList.Add(10);

var stringList = new GenericList<string>(5);
stringList.Add("Hello");
stringList.Add("World");
```

### Generic Methods
```csharp
public class Utilities
{
    // Generic method
    public static bool AreEqual<T>(T a, T b)
    {
        return a.Equals(b);
    }
    
    // Generic method with constraints
    public static T Max<T>(T a, T b) where T : IComparable<T>
    {
        return a.CompareTo(b) > 0 ? a : b;
    }
    
    // Multiple type parameters
    public static void Swap<T1, T2>(ref T1 a, ref T2 b)
    {
        var temp1 = a;
        var temp2 = b;
        a = (T1)Convert.ChangeType(temp2, typeof(T1));
        b = (T2)Convert.ChangeType(temp1, typeof(T2));
    }
}

// Usage
bool areEqual = Utilities.AreEqual(5, 5); // true
int max = Utilities.Max(10, 20); // 20
```

### Generic Constraints
You can constrain the types that can be used with a generic parameter:

```csharp
// Generic class with constraints
public class GenericRepository<T> where T : class, new()
{
    public T CreateNew()
    {
        return new T();
    }
}

// Multiple constraints
public class Calculator<T> where T : struct, IComparable<T>, IConvertible
{
    public T Add(T a, T b)
    {
        // Convert to double, add, then convert back to T
        double result = Convert.ToDouble(a) + Convert.ToDouble(b);
        return (T)Convert.ChangeType(result, typeof(T));
    }
}
```

Common constraints:
- `where T : class` - T must be a reference type
- `where T : struct` - T must be a value type
- `where T : new()` - T must have a parameterless constructor
- `where T : <base-class>` - T must derive from the specified base class
- `where T : <interface>` - T must implement the specified interface
- `where T : U` - T must be or derive from the type argument U

## Comprehensive Coding Examples

Here are comprehensive examples that demonstrate many of the concepts we've covered:

### Example 1: Banking System

This example demonstrates inheritance, polymorphism, properties, constructors, and more:

```csharp
using System;
using System.Collections.Generic;

// Base class
public abstract class BankAccount
{
    public string AccountNumber { get; }
    public string OwnerName { get; set; }
    protected decimal balance;
    
    // Property with get accessor and protected set
    public decimal Balance 
    { 
        get { return balance; }
        protected set { balance = value; }
    }
    
    // Constructor
    public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
    {
        AccountNumber = accountNumber;
        OwnerName = ownerName;
        Balance = initialBalance;
    }
    
    // Virtual method
    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amount must be positive");
        }
        
        Balance += amount;
        Console.WriteLine($"Deposited {amount:C}. New balance: {Balance:C}");
    }
    
    // Virtual method
    public virtual bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive");
        }
        
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
            return false;
        }
        
        Balance -= amount;
        Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
        return true;
    }
    
    // Abstract method - must be implemented by derived classes
    public abstract void ApplyInterest();
    
    // Override Object.ToString()
    public override string ToString()
    {
        return $"Account Number: {AccountNumber}, Owner: {OwnerName}, Balance: {Balance:C}";
    }
}

// Derived class
public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }
    
    // Constructor calling base constructor
    public SavingsAccount(string accountNumber, string ownerName, decimal initialBalance, decimal interestRate)
        : base(accountNumber, ownerName, initialBalance)
    {
        InterestRate = interestRate;
    }
    
    // Override virtual method
    public override void Deposit(decimal amount)
    {
        // Call base method implementation
        base.Deposit(amount);
        
        // Additional logic specific to SavingsAccount
        Console.WriteLine("Your savings are growing!");
    }
    
    // Implement abstract method
    public override void ApplyInterest()
    {
        decimal interest = Balance * InterestRate;
        Balance += interest;
        Console.WriteLine($"Interest applied: {interest:C}. New balance: {Balance:C}");
    }
}

// Another derived class
public class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; set; }
    
    public CheckingAccount(string accountNumber, string ownerName, decimal initialBalance, decimal overdraftLimit)
        : base(accountNumber, ownerName, initialBalance)
    {
        OverdraftLimit = overdraftLimit;
    }
    
    // Override with new implementation
    public override bool Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive");
        }
        
        // Allow withdrawals up to balance + overdraft limit
        if (amount > Balance + OverdraftLimit)
        {
            Console.WriteLine("Withdrawal would exceed overdraft limit");
            return false;
        }
        
        if (amount > Balance)
        {
            decimal overdraftAmount = amount - Balance;
            Balance = 0;
            Balance -= overdraftAmount; // This will make balance negative
            Console.WriteLine($"Withdrew {amount:C} using {overdraftAmount:C} from overdraft. New balance: {Balance:C}");
        }
        else
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew {amount:C}. New balance: {Balance:C}");
        }
        
        return true;
    }
    
    // Implement abstract method
    public override void ApplyInterest()
    {
        // If balance is negative (in overdraft), charge interest
        if (Balance < 0)
        {
            decimal interest = Balance * 0.15m; // 15% interest on overdraft
            Balance += interest; // This will make balance more negative
            Console.WriteLine($"Overdraft interest charged: {interest:C}. New balance: {Balance:C}");
        }
    }
}

// Interface definition
public interface ITransferable
{
    bool TransferTo(BankAccount destinationAccount, decimal amount);
}

// Class that implements both inheritance and interface
public class PremiumAccount : SavingsAccount, ITransferable
{
    public decimal MonthlyFee { get; set; }
    
    public PremiumAccount(string accountNumber, string ownerName, decimal initialBalance, 
                         decimal interestRate, decimal monthlyFee)
        : base(accountNumber, ownerName, initialBalance, interestRate)
    {
        MonthlyFee = monthlyFee;
    }
    
    // Implement interface method
    public bool TransferTo(BankAccount destinationAccount, decimal amount)
    {
        if (Withdraw(amount))
        {
            destinationAccount.Deposit(amount);
            return true;
        }
        return false;
    }
    
    // Additional method
    public void ApplyMonthlyFee()
    {
        Balance -= MonthlyFee;
        Console.WriteLine($"Monthly fee applied: {MonthlyFee:C}. New balance: {Balance:C}");
    }
}

// Bank class to manage accounts
public class Bank
{
    private List<BankAccount> accounts;
    
    public Bank()
    {
        accounts = new List<BankAccount>();
    }
    
    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
    }
    
    public BankAccount FindAccount(string accountNumber)
    {
        return accounts.Find(a => a.AccountNumber == accountNumber);
    }
    
    public void ApplyInterestToAllAccounts()
    {
        foreach (var account in accounts)
        {
            account.ApplyInterest();
        }
    }
}

// Usage example
public class Program
{
    public static void Main()
    {
        Bank bank = new Bank();
        
        // Create different types of accounts
        SavingsAccount savings = new SavingsAccount("S12345", "John Smith", 1000m, 0.05m);
        CheckingAccount checking = new CheckingAccount("C67890", "Jane Doe", 2500m, 500m);
        PremiumAccount premium = new PremiumAccount("P13579", "Robert Johnson", 5000m, 0.07m, 10m);
        
        // Add accounts to the bank
        bank.AddAccount(savings);
        bank.AddAccount(checking);
        bank.AddAccount(premium);
        
        // Perform operations
        savings.Deposit(500m);
        checking.Withdraw(3000m); // This will use the overdraft
        
        // Transfer between accounts
        premium.TransferTo(savings, 1000m);
        
        // Apply interest to all accounts
        bank.ApplyInterestToAllAccounts();
        
        // Display final state
        Console.WriteLine("\nFinal Account States:");
        Console.WriteLine(savings);
        Console.WriteLine(checking);
        Console.WriteLine(premium);
    }
}
```

### Example 2: Generic Data Structures and Interfaces

This example demonstrates generics, interfaces, and operator overloading:

```csharp
using System;
using System.Collections.Generic;

// Generic interface
public interface IDataProcessor<T>
{
    void ProcessData(T data);
    T GetResult();
}

// Generic class implementing the interface
public class DataAnalyzer<T> : IDataProcessor<T> where T : IComparable<T>
{
    private List<T> dataPoints;
    private T maxValue;
    private T minValue;
    
    public DataAnalyzer()
    {
        dataPoints = new List<T>();
    }
    
    public void ProcessData(T data)
    {
        dataPoints.Add(data);
        
        // Update max and min values
        if (dataPoints.Count == 1)
        {
            maxValue = data;
            minValue = data;
        }
        else
        {
            if (data.CompareTo(maxValue) > 0)
            {
                maxValue = data;
            }
            
            if (data.CompareTo(minValue) < 0)
            {
                minValue = data;
            }
        }
    }
    
    public T GetResult()
    {
        // For this example, we'll just return the max value
        return maxValue;
    }
    
    public T GetMinValue()
    {
        return minValue;
    }
    
    public int GetCount()
    {
        return dataPoints.Count;
    }
}

// Struct with operator overloading
public struct Temperature : IComparable<Temperature>
{
    private double celsius;
    
    public Temperature(double celsius)
    {
        this.celsius = celsius;
    }
    
    public double Celsius
    {
        get { return celsius; }
    }
    
    public double Fahrenheit
    {
        get { return celsius * 9 / 5 + 32; }
    }
    
    // Operator overloading
    public static Temperature operator +(Temperature t1, Temperature t2)
    {
        return new Temperature(t1.celsius + t2.celsius);
    }
    
    public static Temperature operator -(Temperature t1, Temperature t2)
    {
        return new Temperature(t1.celsius - t2.celsius);
    }
    
    public static bool operator >(Temperature t1, Temperature t2)
    {
        return t1.celsius > t2.celsius;
    }
    
    public static bool operator <(Temperature t1, Temperature t2)
    {
        return t1.celsius < t2.celsius;
    }
    
    public static bool operator ==(Temperature t1, Temperature t2)
    {
        return t1.celsius == t2.celsius;
    }
    
    public static bool operator !=(Temperature t1, Temperature t2)
    {
        return t1.celsius != t2.celsius;
    }
    
    // Implement IComparable
    public int CompareTo(Temperature other)
    {
        return celsius.CompareTo(other.celsius);
    }
    
    // Override Object methods for consistency
    public override bool Equals(object obj)
    {
        if (obj is Temperature other)
        {
            return this == other;
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        return celsius.GetHashCode();
    }
    
    public override string ToString()
    {
        return $"{celsius:F1}°C ({Fahrenheit:F1}°F)";
    }
}

// Example program using the generic analyzer and Temperature struct
public class WeatherStation
{
    private string location;
    private DataAnalyzer<Temperature> temperatureAnalyzer;
    
    public WeatherStation(string location)
    {
        this.location = location;
        temperatureAnalyzer = new DataAnalyzer<Temperature>();
    }
    
    public void RecordTemperature(double celsius)
    {
        Temperature temp = new Temperature(celsius);
        temperatureAnalyzer.ProcessData(temp);
        Console.WriteLine($"Recorded temperature at {location}: {temp}");
    }
    
    public void PrintStatistics()
    {
        Console.WriteLine($"\nWeather Statistics for {location}:");
        Console.WriteLine($"Number of readings: {temperatureAnalyzer.GetCount()}");
        Console.WriteLine($"Maximum temperature: {temperatureAnalyzer.GetResult()}");
        Console.WriteLine($"Minimum temperature: {temperatureAnalyzer.GetMinValue()}");
    }
    
    // Usage example
    public static void Main()
    {
        WeatherStation station = new WeatherStation("Downtown");
        
        station.RecordTemperature(25.5);
        station.RecordTemperature(27.3);
        station.RecordTemperature(22.1);
        station.RecordTemperature(26.8);
        
        station.PrintStatistics();
        
        // Demonstrate operator overloading
        Temperature t1 = new Temperature(20);
        Temperature t2 = new Temperature(15);
        
        Temperature sum = t1 + t2;
        Temperature diff = t1 - t2;
        
        Console.WriteLine($"\nOperator Overloading Examples:");
        Console.WriteLine($"{t1} + {t2} = {sum}");
        Console.WriteLine($"{t1} - {t2} = {diff}");
        Console.WriteLine($"{t1} > {t2}: {t1 > t2}");
    }
}
```

### Example 3: Shape Hierarchy with Abstract Classes and Interfaces

This example demonstrates abstract classes, interfaces, inheritance, and polymorphism:

```csharp
using System;
using System.Collections.Generic;

// Interface for objects that can be drawn
public interface IDrawable
{
    void Draw();
    string GetDrawingInstructions();
}

// Abstract base class
public abstract class Shape : IDrawable
{
    // Properties common to all shapes
    public string Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    
    // Constructor
    protected Shape(string color, int x, int y)
    {
        Color = color;
        X = x;
        Y = y;
    }
    
    // Abstract methods that derived classes must implement
    public abstract double CalculateArea();
    public abstract double CalculatePerimeter();
    
    // Virtual method with default implementation
    public virtual void Move(int deltaX, int deltaY)
    {
        X += deltaX;
        Y += deltaY;
        Console.WriteLine($"Shape moved to ({X}, {Y})");
    }
    
    // Implementation of IDrawable interface
    public virtual void Draw()
    {
        Console.WriteLine($"Drawing a {GetType().Name} at ({X}, {Y}) with color {Color}");
    }
    
    public abstract string GetDrawingInstructions();
    
    // Override Object.ToString
    public override string ToString()
    {
        return $"{GetType().Name} at position ({X}, {Y}) with color {Color}";
    }
}

// Concrete class derived from Shape
public class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(string color, int x, int y, double radius) : base(color, x, y)
    {
        Radius = radius;
    }
    
    // Implement abstract methods
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * Math.PI * Radius;
    }
    
    // Override virtual method from base class
    public override void Draw()
    {
        base.Draw(); // Call base implementation
        Console.WriteLine($"Circle specifics: Radius = {Radius}");
    }
    
    public override string GetDrawingInstructions()
    {
        return $"Draw a circle with radius {Radius} at ({X}, {Y}) in {Color} color";
    }
}

// Another concrete class derived from Shape
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(string color, int x, int y, double width, double height) : base(color, x, y)
    {
        Width = width;
        Height = height;
    }
    
    // Implement abstract methods
    public override double CalculateArea()
    {
        return Width * Height;
    }
    
    public override double CalculatePerimeter()
    {
        return 2 * (Width + Height);
    }
    
    public override string GetDrawingInstructions()
    {
        return $"Draw a rectangle with width {Width} and height {Height} at ({X}, {Y}) in {Color} color";
    }
    
    // Additional method specific to Rectangle
    public bool IsSquare()
    {
        return Math.Abs(Width - Height) < 0.0001; // Account for floating-point precision
    }
}

// Special type of Rectangle
public class Square : Rectangle
{
    public Square(string color, int x, int y, double side) : base(color, x, y, side, side)
    {
    }
    
    // Property that ensures width and height stay equal
    public double Side
    {
        get { return Width; }
        set { Width = Height = value; }
    }
    
    public override string GetDrawingInstructions()
    {
        return $"Draw a square with side {Side} at ({X}, {Y}) in {Color} color";
    }
}

// Canvas class to demonstrate polymorphism
public class Canvas
{
    private List<IDrawable> drawables = new List<IDrawable>();
    
    public void AddDrawable(IDrawable drawable)
    {
        drawables.Add(drawable);
    }
    
    public void DrawAll()
    {
        Console.WriteLine("Drawing all shapes on canvas:");
        foreach (var drawable in drawables)
        {
            drawable.Draw();
        }
    }
    
    public void PrintInstructions()
    {
        Console.WriteLine("\nDrawing Instructions:");
        foreach (var drawable in drawables)
        {
            Console.WriteLine(drawable.GetDrawingInstructions());
        }
    }
}

// Custom drawable not derived from Shape
public class Text : IDrawable
{
    public string Content { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public string FontName { get; set; }
    
    public Text(string content, int x, int y, string fontName)
    {
        Content = content;
        X = x;
        Y = y;
        FontName = fontName;
    }
    
    public void Draw()
    {
        Console.WriteLine($"Drawing text '{Content}' at ({X}, {Y}) using font {FontName}");
    }
    
    public string GetDrawingInstructions()
    {
        return $"Write '{Content}' at ({X}, {Y}) using {FontName} font";
    }
}

// Usage example
public class ShapeProgram
{
    public static void Main()
    {
        // Create a canvas
        Canvas canvas = new Canvas();
        
        // Create different shapes
        Circle circle = new Circle("Red", 10, 20, 5);
        Rectangle rectangle = new Rectangle("Blue", 30, 40, 8, 6);
        Square square = new Square("Green", 50, 60, 7);
        Text text = new Text("Hello, World!", 70, 80, "Arial");
        
        // Add shapes to canvas
        canvas.AddDrawable(circle);
        canvas.AddDrawable(rectangle);
        canvas.AddDrawable(square);
        canvas.AddDrawable(text);
        
        // Draw all shapes
        canvas.DrawAll();
        
        // Print drawing instructions
        canvas.PrintInstructions();
        
        // Demonstrate polymorphism with a list of shapes
        List<Shape> shapes = new List<Shape> { circle, rectangle, square };
        
        Console.WriteLine("\nShape Areas and Perimeters:");
        foreach (var shape in shapes)
        {
            Console.WriteLine($"{shape}: Area = {shape.CalculateArea():F2}, Perimeter = {shape.CalculatePerimeter():F2}");
        }
    }
}
```

These examples demonstrate the core concepts of classes, inheritance, interfaces, abstraction, polymorphism, and other object-oriented programming principles in C#.