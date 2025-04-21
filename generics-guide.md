# Understanding Generics in C#

## What Are Generics?

Generics allow you to write code that works with any data type, without having to rewrite that code for each type.

## The Problem Generics Solve

Imagine you need to create:
- A list of integers
- A list of strings
- A list of students

Without generics, you would need three different classes with nearly identical code.

## Generics: "Type as a Parameter"

Think of generics like a function that takes a parameter, but instead of a value, the parameter is a *type*.

## Simple Example

```csharp
// Without generics (specific to int)
public class IntBox
{
    private int _value;
    
    public IntBox(int value)
    {
        _value = value;
    }
    
    public int GetValue()
    {
        return _value;
    }
}

// With generics (works with any type)
public class Box<T>
{
    private T _value;
    
    public Box(T value)
    {
        _value = value;
    }
    
    public T GetValue()
    {
        return _value;
    }
}
```

## How to Use Generic Classes

```csharp
// Create boxes for different types
Box<int> intBox = new Box<int>(42);
Box<string> stringBox = new Box<string>("Hello");
Box<DateTime> dateBox = new Box<DateTime>(DateTime.Now);

// Get values from boxes
int number = intBox.GetValue();        // 42
string text = stringBox.GetValue();    // "Hello"
DateTime date = dateBox.GetValue();    // Current date and time
```

## Generic Methods

You can also create generic methods within non-generic classes:

```csharp
public class Utilities
{
    // Generic method that can swap any two values of the same type
    public void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}

// Usage
Utilities utils = new Utilities();
int x = 5, y = 10;
utils.Swap<int>(ref x, ref y);  // x is now 10, y is now 5

// Type can often be inferred
string s1 = "hello", s2 = "world";
utils.Swap(ref s1, ref s2);    // s1 is "world", s2 is "hello"
```

## Generic Collections

The most common use of generics is with collections:

```csharp
// A list that only accepts integers
List<int> numbers = new List<int>();
numbers.Add(1);
numbers.Add(2);
numbers.Add(3);
// numbers.Add("text");  // Error: cannot add string to List<int>

// A dictionary with string keys and Student values
Dictionary<string, Student> studentsByID = new Dictionary<string, Student>();
studentsByID.Add("S001", new Student("Alice"));
studentsByID.Add("S002", new Student("Bob"));

Student alice = studentsByID["S001"];
```

## Type Constraints

You can restrict generic types using constraints:

```csharp
// T must be a class (reference type)
public class Repository<T> where T : class
{
    // Implementation
}

// T must implement IComparable<T>
public class Sorter<T> where T : IComparable<T>
{
    public void Sort(List<T> items)
    {
        // Can use CompareTo because T implements IComparable<T>
        // Implementation
    }
}

// Multiple constraints
public class AdvancedProcessor<T> where T : class, IDisposable, new()
{
    // T must be: 
    // 1. A reference type (class)
    // 2. Implement IDisposable
    // 3. Have a parameterless constructor (new())
}
```

## Benefits of Generics

1. **Type Safety**: Compiler catches type errors
2. **Reusability**: Write code once, use with many types
3. **Performance**: No boxing/unboxing for value types
4. **Code Clarity**: Clear intentions in your API

## Common Generic Interfaces

- `IEnumerable<T>`: For collections you can iterate through
- `IList<T>`: For lists with indexed access
- `IDictionary<TKey, TValue>`: For key-value collections
- `IComparable<T>`: For types that can be compared/ordered
- `IEquatable<T>`: For types that can check equality

## Real-World Analogy

Think of generics like a recipe template:
- The recipe says "add fruit" (generic type parameter)
- You decide which fruit to use (concrete type)
- Same recipe, different outcomes based on your choice
