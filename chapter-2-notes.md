# Unit 2: The C# Language Basics

## Writing Console and GUI Applications

### Console Applications
Console applications are text-based programs that run in a command-line interface. They are simple to create and are often used for utilities, batch processing, or as a starting point for learning C#.

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Console.ReadKey(); // Waits for a key press before closing
    }
}
```

Key features:
- Entry point is the `Main` method
- Input/output through `Console` class methods
- Command-line arguments passed via `string[] args`

### GUI Applications
Graphical User Interface applications provide visual elements for user interaction. In C#, several frameworks are available for creating GUI applications:

1. **Windows Forms** - Traditional Windows desktop applications
   ```csharp
   using System;
   using System.Windows.Forms;

   class Program
   {
       static void Main()
       {
           Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
           Application.Run(new MainForm());
       }
   }

   class MainForm : Form
   {
       public MainForm()
       {
           Text = "My First Windows Form";
           Button button = new Button
           {
               Text = "Click Me",
               Location = new System.Drawing.Point(80, 80)
           };
           button.Click += (sender, e) => MessageBox.Show("Hello, World!");
           Controls.Add(button);
       }
   }
   ```

2. **WPF (Windows Presentation Foundation)** - Modern Windows UI framework using XAML
   ```csharp
   // C# code-behind
   using System.Windows;

   namespace MyWpfApp
   {
       public partial class MainWindow : Window
       {
           public MainWindow()
           {
               InitializeComponent();
           }

           private void Button_Click(object sender, RoutedEventArgs e)
           {
               MessageBox.Show("Hello, World!");
           }
       }
   }
   ```

   ```xml
   <!-- XAML file -->
   <Window x:Class="MyWpfApp.MainWindow"
           xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
           xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
           Title="WPF Application" Height="300" Width="400">
       <Grid>
           <Button Content="Click Me" HorizontalAlignment="Center" 
                   VerticalAlignment="Center" Click="Button_Click"/>
       </Grid>
   </Window>
   ```

3. **.NET MAUI** - Cross-platform UI framework for Windows, macOS, iOS, and Android

## Identifiers and Keywords

### Identifiers
Identifiers are names given to program elements like variables, methods, classes, etc.

Rules for C# identifiers:
- Must start with a letter or underscore
- Can contain letters, digits, and underscores
- Cannot be a C# keyword (unless prefixed with `@`)
- Are case-sensitive

Examples:
```csharp
int count;        // Valid
string userName;  // Valid (camelCase)
class UserProfile // Valid (PascalCase)
int 1stNumber;    // Invalid (starts with a digit)
string @class;    // Valid (using @ prefix for keyword)
```

Naming conventions:
- PascalCase for classes, methods, and properties (e.g., `ClassName`, `MethodName`)
- camelCase for local variables and parameters (e.g., `localVariable`, `parameterName`)
- _camelCase or _PascalCase for private fields (e.g., `_privateField`)

### Keywords
Keywords are reserved words with special meaning in C#. They cannot be used as identifiers without the `@` prefix.

Common C# keywords include:
- Type definitions: `class`, `struct`, `enum`, `interface`
- Access modifiers: `public`, `private`, `protected`, `internal`
- Method modifiers: `static`, `virtual`, `override`, `abstract`
- Control flow: `if`, `else`, `switch`, `case`, `while`, `for`, `foreach`, `do`, `break`, `continue`, `return`
- Value types: `int`, `long`, `float`, `double`, `decimal`, `bool`, `char`
- Reference handling: `new`, `this`, `base`, `null`
- Exception handling: `try`, `catch`, `finally`, `throw`
- Other: `namespace`, `using`, `void`, `ref`, `out`, `in`, `params`

## Writing Comments

C# supports three types of comments:

### Single-line Comments
Single-line comments start with `//` and continue to the end of the line.
```csharp
// This is a single-line comment
int x = 10; // Comment at the end of a line
```

### Multi-line Comments
Multi-line comments start with `/*` and end with `*/`.
```csharp
/* This is a multi-line comment
   that spans several lines
   and ends here */
```

### XML Documentation Comments
XML documentation comments start with `///` and are used to document classes, methods, and other code elements. They can be processed by tools to generate documentation.
```csharp
/// <summary>
/// Calculates the sum of two integers.
/// </summary>
/// <param name="a">First number to add</param>
/// <param name="b">Second number to add</param>
/// <returns>Sum of a and b</returns>
public int Add(int a, int b)
{
    return a + b;
}
```

Common XML documentation tags:
- `<summary>` - Brief description of the item
- `<param>` - Description of a parameter
- `<returns>` - Description of the return value
- `<exception>` - Description of an exception that may be thrown
- `<remarks>` - Additional information
- `<example>` - Example code
- `<see>` - Create a link to another documented element

## Data Types

C# is a strongly-typed language with two main categories of types:

### Value Types
Value types directly contain their data and are stored on the stack. When a value type is assigned to a new variable, its value is copied.

#### Predefined Value Types:
1. **Integral Types**
   - `byte`: 8-bit unsigned integer (0 to 255)
   - `sbyte`: 8-bit signed integer (-128 to 127)
   - `short`: 16-bit signed integer (-32,768 to 32,767)
   - `ushort`: 16-bit unsigned integer (0 to 65,535)
   - `int`: 32-bit signed integer (-2,147,483,648 to 2,147,483,647)
   - `uint`: 32-bit unsigned integer (0 to 4,294,967,295)
   - `long`: 64-bit signed integer (-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807)
   - `ulong`: 64-bit unsigned integer (0 to 18,446,744,073,709,551,615)

2. **Floating-Point Types**
   - `float`: 32-bit single-precision (±1.5 × 10^−45 to ±3.4 × 10^38, 7-digit precision)
   - `double`: 64-bit double-precision (±5.0 × 10^−324 to ±1.7 × 10^308, 15-16 digit precision)

3. **Decimal Type**
   - `decimal`: 128-bit high-precision decimal (±1.0 × 10^-28 to ±7.9 × 10^28, 28-29 significant digits)

4. **Boolean Type**
   - `bool`: Represents `true` or `false`

5. **Character Type**
   - `char`: 16-bit Unicode character (U+0000 to U+FFFF)

6. **User-defined Value Types**
   - `struct`
   - `enum`

### Reference Types
Reference types store a reference to their data, which is stored on the heap. When a reference type is assigned to a new variable, the reference is copied, not the actual data.

#### Predefined Reference Types:
1. **Object Type**
   - `object`: Base type for all other types in C#

2. **String Type**
   - `string`: Sequence of Unicode characters

3. **Dynamic Type**
   - `dynamic`: Bypasses compile-time type checking

#### User-defined Reference Types:
- `class`
- `interface`
- `delegate`
- `record` (from C# 9.0)

### Special Types:
- `nullable` types: Allows value types to have a `null` value, e.g., `int?`
- `var`: Implicit type that is determined by the compiler

### Type Conversion
C# supports various ways to convert between types:
1. **Implicit conversion**: Automatic conversion between compatible types
   ```csharp
   int i = 10;
   long l = i; // Implicit conversion from int to long
   ```

2. **Explicit conversion (casting)**: Required for conversions that might result in data loss
   ```csharp
   double d = 12.34;
   int i = (int)d; // Explicit conversion, i will be 12
   ```

3. **Convert class methods**: Utility methods for type conversion
   ```csharp
   string s = "123";
   int i = Convert.ToInt32(s); // Converts string to int
   ```

4. **Parse methods**: Convert strings to specific types
   ```csharp
   string s = "123";
   int i = int.Parse(s);
   ```

5. **TryParse methods**: Safer version of Parse that doesn't throw exceptions
   ```csharp
   string s = "123";
   if (int.TryParse(s, out int result))
   {
       // Conversion succeeded, use result
   }
   ```

## Expressions and Operators

### Expressions
An expression is a sequence of operators and operands that specifies a computation. Expressions can:
- Produce a value
- Perform an action
- Create side effects

Examples:
```csharp
x + y             // Arithmetic expression
condition ? x : y // Conditional expression
methodCall()      // Method call expression
new MyClass()     // Object creation expression
```

### Operators
C# provides a rich set of operators categorized by functionality:

#### Arithmetic Operators
- `+`: Addition
- `-`: Subtraction
- `*`: Multiplication
- `/`: Division
- `%`: Modulus (remainder)
- `++`: Increment
- `--`: Decrement

#### Comparison Operators
- `==`: Equal to
- `!=`: Not equal to
- `>`: Greater than
- `<`: Less than
- `>=`: Greater than or equal to
- `<=`: Less than or equal to

#### Logical Operators
- `&&`: Logical AND
- `||`: Logical OR
- `!`: Logical NOT
- `&`: Bitwise AND (also used for logical AND without short-circuiting)
- `|`: Bitwise OR (also used for logical OR without short-circuiting)
- `^`: Bitwise XOR

#### Bitwise Operators
- `&`: Bitwise AND
- `|`: Bitwise OR
- `^`: Bitwise XOR
- `~`: Bitwise complement
- `<<`: Left shift
- `>>`: Right shift

#### Assignment Operators
- `=`: Simple assignment
- `+=`, `-=`, `*=`, `/=`, `%=`: Compound assignment
- `&=`, `|=`, `^=`, `<<=`, `>>=`: Compound assignment with bitwise operations

#### Other Operators
- `?:`: Conditional operator (ternary)
- `??`: Null-coalescing operator
- `?.`: Null-conditional operator
- `is`: Type testing
- `as`: Type conversion
- `sizeof`: Size of a value type
- `typeof`: Type information
- `new`: Object creation
- `default`: Default value
- `nameof`: Name of a code element

#### Operator Precedence
Operators have a defined precedence that determines the order of operations in expressions. When in doubt, use parentheses to clarify the intended order.

High to low precedence (simplified):
1. Primary operators (`()`, `[]`, `.`, `->`)
2. Unary operators (`+`, `-`, `!`, `~`, `++`, `--`, `(type)`)
3. Multiplicative operators (`*`, `/`, `%`)
4. Additive operators (`+`, `-`)
5. Shift operators (`<<`, `>>`)
6. Relational operators (`<`, `>`, `<=`, `>=`, `is`, `as`)
7. Equality operators (`==`, `!=`)
8. Logical/bitwise AND (`&`)
9. Logical/bitwise XOR (`^`)
10. Logical/bitwise OR (`|`)
11. Conditional AND (`&&`)
12. Conditional OR (`||`)
13. Null-coalescing operator (`??`)
14. Conditional operator (`?:`)
15. Assignment operators (`=`, `+=`, `-=`, etc.)

## Strings and Characters

### Characters (char)
The `char` type represents a single Unicode character and is a value type.

```csharp
char letter = 'A';
char symbol = '\u00A9'; // Copyright symbol (©) using Unicode
```

Escape sequences:
- `\'`: Single quote
- `\"`: Double quote
- `\\`: Backslash
- `\n`: New line
- `\r`: Carriage return
- `\t`: Tab
- `\b`: Backspace
- `\f`: Form feed
- `\uxxxx`: Unicode character

### Strings (string)
The `string` type represents a sequence of characters and is a reference type. Strings in C# are immutable, meaning once created, their content cannot be changed.

#### String Creation
```csharp
string s1 = "Hello";                      // String literal
string s2 = "Hello" + " " + "World";      // Concatenation
string s3 = $"The value is {value}";      // String interpolation
string s4 = @"C:\Program Files\dotnet";   // Verbatim string
string s5 = """
            This is a multi-line
            string literal (C# 11)
            """;                           // Raw string literal
```

#### Common String Operations
```csharp
// Length
int length = str.Length;

// Accessing characters
char firstChar = str[0];

// Substring
string sub = str.Substring(2, 5); // 5 characters starting at index 2

// Searching
int index = str.IndexOf("pattern");
bool contains = str.Contains("pattern");
bool startsWith = str.StartsWith("Hello");
bool endsWith = str.EndsWith("World");

// Modification (creates new strings)
string upper = str.ToUpper();
string lower = str.ToLower();
string trimmed = str.Trim();
string replaced = str.Replace("old", "new");

// Splitting
string[] parts = str.Split(',');

// Joining
string joined = string.Join(", ", stringArray);
```

#### String Formatting
```csharp
// Composite formatting
string formatted = string.Format("Name: {0}, Age: {1}", name, age);

// Interpolated strings (C# 6.0+)
string interpolated = $"Name: {name}, Age: {age}";

// Format specifiers
string formatted = $"Price: {price:C}"; // Currency format
string formatted = $"Date: {date:d}";   // Short date format
string formatted = $"Percentage: {value:P2}"; // Percentage with 2 decimal places
```

#### StringBuilder
For intensive string manipulation operations, use `StringBuilder` from the `System.Text` namespace to improve performance:

```csharp
using System.Text;

StringBuilder sb = new StringBuilder();
sb.Append("Hello");
sb.Append(" ");
sb.Append("World");
sb.AppendLine("!");
sb.Insert(5, " there");
sb.Replace("World", "Universe");
string result = sb.ToString(); // "Hello there Universe!"
```

## Arrays

Arrays are fixed-length data structures that store elements of the same type. Arrays in C# are reference types, but the elements can be either value or reference types.

### Declaring and Initializing Arrays

```csharp
// Declaration
int[] numbers;

// Initialization with size
numbers = new int[5]; // Creates an array with 5 elements, all initialized to 0

// Declaration and initialization in one step
int[] numbers = new int[5];

// Declaration and initialization with values
int[] numbers = new int[] { 10, 20, 30, 40, 50 };

// Simplified syntax
int[] numbers = { 10, 20, 30, 40, 50 };

// Multi-dimensional arrays
int[,] matrix = new int[3, 4]; // 3 rows, 4 columns
int[,] matrix = { { 1, 2, 3, 4 }, { 5, 6, 7, 8 }, { 9, 10, 11, 12 } };

// Jagged arrays (arrays of arrays)
int[][] jaggedArray = new int[3][];
jaggedArray[0] = new int[] { 1, 2, 3 };
jaggedArray[1] = new int[] { 4, 5 };
jaggedArray[2] = new int[] { 6, 7, 8, 9 };
```

### Accessing Array Elements

```csharp
// Accessing elements
int firstElement = numbers[0];
numbers[2] = 100;

// Accessing elements in multi-dimensional arrays
int value = matrix[1, 2]; // Row 1, column 2

// Accessing elements in jagged arrays
int value = jaggedArray[0][1]; // First array, second element
```

### Array Properties and Methods

```csharp
// Length of an array
int length = numbers.Length;

// For multi-dimensional arrays
int rows = matrix.GetLength(0);
int columns = matrix.GetLength(1);

// Common array methods
Array.Sort(numbers);            // Sort array
Array.Reverse(numbers);         // Reverse array
int index = Array.IndexOf(numbers, 30); // Find first occurrence
Array.Copy(sourceArray, destinationArray, length); // Copy elements
Array.Fill(numbers, 0);         // Fill array with value
bool exists = Array.Exists(numbers, x => x > 20); // Check if element exists
```

### Iterating Through Arrays

```csharp
// Using for loop
for (int i = 0; i < numbers.Length; i++)
{
    Console.WriteLine(numbers[i]);
}

// Using foreach loop
foreach (int number in numbers)
{
    Console.WriteLine(number);
}

// Iterating through multi-dimensional arrays
for (int i = 0; i < matrix.GetLength(0); i++)
{
    for (int j = 0; j < matrix.GetLength(1); j++)
    {
        Console.WriteLine(matrix[i, j]);
    }
}

// Iterating through jagged arrays
for (int i = 0; i < jaggedArray.Length; i++)
{
    for (int j = 0; j < jaggedArray[i].Length; j++)
    {
        Console.WriteLine(jaggedArray[i][j]);
    }
}
```

## Variables and Parameters

### Variables

Variables are named storage locations that hold values of a specific type.

#### Variable Declaration and Initialization

```csharp
// Declaration
int age;

// Initialization
age = 25;

// Declaration and initialization in one step
int age = 25;

// Multiple declarations
int x, y, z;

// Multiple declarations with initialization
int x = 1, y = 2, z = 3;

// Implicitly typed local variable
var name = "John"; // Compiler infers type as string
```

#### Variable Scope

1. **Block scope**: Variables declared within a block `{}` are accessible only within that block
   ```csharp
   {
       int x = 10; // Only accessible within this block
   }
   // x is not accessible here
   ```

2. **Method scope**: Variables declared in a method are accessible anywhere within that method
   ```csharp
   void Method()
   {
       int x = 10; // Accessible throughout this method
       
       if (condition)
       {
           // x is accessible here
           int y = 20; // y is only accessible within this if block
       }
       // y is not accessible here, but x is
   }
   ```

3. **Class scope**: Fields declared in a class are accessible throughout the class based on their access modifiers
   ```csharp
   class MyClass
   {
       private int x; // Accessible throughout this class
       
       public void Method1()
       {
           x = 10; // Accessing class-scoped variable
       }
       
       public void Method2()
       {
           x = 20; // Accessing the same class-scoped variable
       }
   }
   ```

#### Constants

Constants are immutable values that are known at compile time.

```csharp
const double Pi = 3.14159265359;
const string AppName = "My C# Application";
```

#### Read-only Fields

Read-only fields can be assigned only during declaration or in a constructor.

```csharp
class Circle
{
    private readonly double _radius;
    
    public Circle(double radius)
    {
        _radius = radius; // Can be assigned in constructor
    }
    
    public void ChangeRadius(double newRadius)
    {
        // _radius = newRadius; // Error: Cannot assign to readonly field
    }
}
```

### Parameters

Parameters allow passing values to methods. C# supports several parameter types:

#### Value Parameters

By default, parameters are passed by value, meaning a copy of the value is passed to the method.

```csharp
void Increment(int x)
{
    x++; // Only the local copy is modified
}

int number = 5;
Increment(number); // number is still 5 after the call
```

#### Reference Parameters (ref)

Reference parameters pass a reference to the original variable, allowing the method to modify it.

```csharp
void Increment(ref int x)
{
    x++; // Modifies the original variable
}

int number = 5;
Increment(ref number); // number is now 6
```

#### Output Parameters (out)

Output parameters are used to return multiple values from a method. They don't need to be initialized before passing to the method.

```csharp
void Divide(int dividend, int divisor, out int quotient, out int remainder)
{
    quotient = dividend / divisor;
    remainder = dividend % divisor;
}

int q, r;
Divide(10, 3, out q, out r); // q becomes 3, r becomes 1

// Simplified with out variables (C# 7.0+)
Divide(10, 3, out int quotient, out int remainder);
```

#### Input Parameters (in) (C# 7.2+)

Input parameters are similar to reference parameters but cannot be modified by the method.

```csharp
void Process(in int x)
{
    // x++; // Error: Cannot modify in parameter
    int y = x + 1; // Can read the value
}

int number = 5;
Process(in number);
```

#### Optional Parameters

Optional parameters have default values and can be omitted when calling the method.

```csharp
void Display(string message, bool uppercase = false)
{
    if (uppercase)
        message = message.ToUpper();
    Console.WriteLine(message);
}

Display("Hello"); // Uses default value for uppercase (false)
Display("Hello", true); // Overrides the default value
```

#### Named Parameters

Named parameters allow specifying arguments by parameter name rather than position.

```csharp
void Configure(string server, int port, bool useSSL)
{
    // Implementation
}

// Using named parameters
Configure(server: "localhost", port: 8080, useSSL: true);
Configure(useSSL: true, server: "localhost", port: 8080); // Order doesn't matter
```

#### Params Array

The `params` keyword allows a method to accept a variable number of arguments of the same type.

```csharp
int Sum(params int[] numbers)
{
    int total = 0;
    foreach (int number in numbers)
        total += number;
    return total;
}

int result1 = Sum(1, 2, 3); // Passing individual arguments
int result2 = Sum(new int[] { 1, 2, 3 }); // Passing an array
int result3 = Sum(); // Passing no arguments is valid
```

## Statements

Statements are the building blocks of C# programs that perform actions, control flow, or declare items.

### Declaration Statements

Declaration statements introduce new variables, constants, or types.

```csharp
int x; // Variable declaration
const double Pi = 3.14159; // Constant declaration
class MyClass { } // Type declaration
```

### Expression Statements

Expression statements evaluate expressions and discard the result.

```csharp
x = 10; // Assignment expression
methodCall(); // Method call expression
x++; // Increment expression
new MyClass(); // Object creation expression
```

### Selection Statements

Selection statements conditionally execute code based on the evaluation of expressions.

#### if Statement

```csharp
if (condition)
{
    // Executed if condition is true
}
else if (anotherCondition)
{
    // Executed if condition is false and anotherCondition is true
}
else
{
    // Executed if all conditions are false
}
```

#### switch Statement

```csharp
switch (value)
{
    case 1:
        // Code for case 1
        break;
    case 2:
        // Code for case 2
        break;
    case 3:
    case 4:
        // Code for cases 3 and 4
        break;
    default:
        // Code for all other cases
        break;
}
```

Pattern matching in switch (C# 7.0+):

```csharp
switch (obj)
{
    case int i:
        Console.WriteLine($"It's an integer: {i}");
        break;
    case string s when s.Length > 0:
        Console.WriteLine($"It's a non-empty string: {s}");
        break;
    case null:
        Console.WriteLine("It's null");
        break;
    default:
        Console.WriteLine("It's something else");
        break;
}
```

Switch expressions (C# 8.0+):

```csharp
string message = day switch
{
    DayOfWeek.Monday => "Start of work week",
    DayOfWeek.Friday => "End of work week",
    DayOfWeek.Saturday or DayOfWeek.Sunday => "Weekend",
    _ => "Midweek"
};
```

### Iteration Statements

Iteration statements repeatedly execute code blocks.

#### while Loop

Executes a block of code as long as a condition is true.

```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    i++;
}
```

#### do-while Loop

Similar to while, but guarantees at least one execution of the code block.

```csharp
int i = 0;
do
{
    Console.WriteLine(i);
    i++;
} while (i < 10);
```

#### for Loop

Combines initialization, condition checking, and iteration in a single statement.

```csharp
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

#### foreach Loop

Iterates through collections or arrays.

```csharp
string[] names = { "Alice", "Bob", "Charlie" };
foreach (string name in names)
{
    Console.WriteLine(name);
}
```

### Jump Statements

Jump statements transfer control to another point in the program.

#### break Statement

Exits the current loop or switch statement.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5)
        break; // Exits the loop when i equals 5
    Console.WriteLine(i);
}
```

#### continue Statement

Skips the rest of the current iteration and proceeds to the next iteration.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0)
        continue; // Skips even numbers
    Console.WriteLine(i);
}
```

#### return Statement

Exits the current method and returns control to the calling method.

```csharp
int Add(int a, int b)
{
    return a + b; // Returns the sum and exits the method
}
```

#### throw Statement

Throws an exception, transferring control to the nearest catch block.

```csharp
void Divide(int a, int b)
{
    if (b == 0)
        throw new DivideByZeroException("Cannot divide by zero");
    
    Console.WriteLine(a / b);
}
```

#### goto Statement

Transfers control to a labeled statement (rarely used).

```csharp
int i = 0;
start:
Console.WriteLine(i);
i++;
if (i < 10)
    goto start; // Jumps to the 'start' label
```

## Namespaces

Namespaces organize code into logical groups and prevent naming conflicts. They allow you to use the same name for different types as long as they are in different namespaces.

### Declaring Namespaces

```csharp
namespace MyApplication
{
    public class Program
    {
        // Class members
    }
}
```

File-scoped namespaces (C# 10.0+):

```csharp
namespace MyApplication;

public class Program
{
    // Class members
}
```

Nested namespaces:

```csharp
namespace MyApplication
{
    namespace Data
    {
        public class Database
        {
            // Class members
        }
    }
}

// Alternative syntax using dot notation
namespace MyApplication.Data
{
    public class Database
    {
        // Class members
    }
}
```

### Using Namespaces

The `using` directive makes types in a namespace available without qualifying them with the namespace name.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello"); // Instead of System.Console.WriteLine
        List<int> numbers = new List<int>(); // Instead of System.Collections.Generic.List<int>
    }
}
```

### Using Aliases

You can create aliases for namespaces or types to avoid naming conflicts or to simplify long names.

```csharp
using Console = System.Console;
using Dict = System.Collections.Generic.Dictionary<string, int>;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello");
        Dict dictionary = new Dict();
    }
}
```

### Global Using Directives (C# 10.0+)

Global using directives apply to all files in a project.

```csharp
// In a separate file or at the top of a file
global using System;
global using System.Collections.Generic;
global using static System.Console;

// In any other file
class Program
{
    static void Main()
    {
        WriteLine("Hello"); // Using static imported Console.WriteLine
        List<int> numbers = new List<int>();
    }
}
```

### Namespace Best Practices

- Use meaningful namespace names that reflect the purpose of the types
- Follow a hierarchical structure (e.g., `CompanyName.ProductName.ComponentName`)
- Avoid short or generic names that might conflict with other libraries
- Don't use the same name for a namespace and a type in that namespace
- Keep namespace declarations and type declarations separate (i.e., don't put types in the global namespace)
