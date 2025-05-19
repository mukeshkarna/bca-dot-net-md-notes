using System;
using System.Collections;
using System.Collections.Generic;

// Generic Stack class implementing IEnumerable<T>
public class Stack<T> : IEnumerable<T>
{
  private List<T> items;

  // Constructor
  public Stack()
  {
    items = new List<T>();
  }

  // Property to get the count of items
  public int Count
  {
    get { return items.Count; }
  }

  // Property to check if stack is empty
  public bool IsEmpty
  {
    get { return items.Count == 0; }
  }

  // Push method to add item to top of stack
  public void Push(T item)
  {
    items.Add(item);
  }

  // Pop method to remove and return top item
  public T Pop()
  {
    if (IsEmpty)
    {
      throw new InvalidOperationException("Stack is empty. Cannot pop.");
    }

    T item = items[items.Count - 1];
    items.RemoveAt(items.Count - 1);
    return item;
  }

  // Peek method to view top item without removing it
  public T Peek()
  {
    if (IsEmpty)
    {
      throw new InvalidOperationException("Stack is empty. Cannot peek.");
    }

    return items[items.Count - 1];
  }

  // Generic Contains method
  public bool Contains<TItem>(TItem item) where TItem : T
  {
    // Handle null values
    if (item == null)
    {
      return items.Contains(default(T));
    }

    // Use EqualityComparer for comparison
    EqualityComparer<T> comparer = EqualityComparer<T>.Default;
    foreach (T stackItem in items)
    {
      if (comparer.Equals(stackItem, item))
      {
        return true;
      }
    }
    return false;
  }

  // Clear all items from stack
  public void Clear()
  {
    items.Clear();
  }

  // Convert stack to array
  public T[] ToArray()
  {
    // Return items in stack order (top item at end)
    T[] array = new T[items.Count];
    items.CopyTo(array);
    return array;
  }

  // IEnumerable<T> implementation
  public IEnumerator<T> GetEnumerator()
  {
    // Enumerate from top to bottom of stack
    for (int i = items.Count - 1; i >= 0; i--)
    {
      yield return items[i];
    }
  }

  // IEnumerable implementation
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }

  // Optional: Override ToString for debugging
  public override string ToString()
  {
    return $"Stack<{typeof(T).Name}> with {Count} items";
  }
}

// Custom class for testing
public class Person
{
  public string Name { get; set; }
  public int Age { get; set; }

  public Person(string name, int age)
  {
    Name = name;
    Age = age;
  }

  public override string ToString()
  {
    return $"{Name} ({Age})";
  }

  public override bool Equals(object obj)
  {
    if (obj is Person other)
    {
      return Name == other.Name && Age == other.Age;
    }
    return false;
  }

  public override int GetHashCode()
  {
    return Name.GetHashCode() ^ Age.GetHashCode();
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Console.WriteLine("=== Testing Stack<int> ===");
    Stack<int> intStack = new Stack<int>();

    // Test Push
    intStack.Push(10);
    intStack.Push(20);
    intStack.Push(30);
    Console.WriteLine($"Pushed 10, 20, 30. Count: {intStack.Count}");

    // Test Peek
    Console.WriteLine($"Peek: {intStack.Peek()}");

    // Test Pop
    Console.WriteLine($"Pop: {intStack.Pop()}");
    Console.WriteLine($"Count after pop: {intStack.Count}");

    // Test Contains
    Console.WriteLine($"Contains 20: {intStack.Contains(20)}");
    Console.WriteLine($"Contains 30: {intStack.Contains(30)}");

    // Test foreach (IEnumerable)
    Console.WriteLine("Iterating through stack:");
    foreach (int item in intStack)
    {
      Console.WriteLine(item);
    }

    // Test exception handling
    try
    {
      intStack.Pop();
      intStack.Pop();
      intStack.Pop(); // This should throw an exception
    }
    catch (InvalidOperationException ex)
    {
      Console.WriteLine($"Exception caught: {ex.Message}");
    }

    Console.WriteLine("\n=== Testing Stack<string> ===");
    Stack<string> stringStack = new Stack<string>();

    stringStack.Push("Hello");
    stringStack.Push("World");
    stringStack.Push("!");

    Console.WriteLine("String stack contents:");
    foreach (string item in stringStack)
    {
      Console.WriteLine(item);
    }

    Console.WriteLine($"Contains 'World': {stringStack.Contains("World")}");

    Console.WriteLine("\n=== Testing Stack<Person> ===");
    Stack<Person> personStack = new Stack<Person>();

    personStack.Push(new Person("Alice", 30));
    personStack.Push(new Person("Bob", 25));
    personStack.Push(new Person("Charlie", 35));

    Console.WriteLine("Person stack contents:");
    foreach (Person person in personStack)
    {
      Console.WriteLine(person);
    }

    // Test Contains with custom object
    Person searchPerson = new Person("Bob", 25);
    Console.WriteLine($"Contains Bob (25): {personStack.Contains(searchPerson)}");

    // Test ToArray
    Person[] peopleArray = personStack.ToArray();
    Console.WriteLine("\nStack converted to array:");
    for (int i = 0; i < peopleArray.Length; i++)
    {
      Console.WriteLine($"[{i}]: {peopleArray[i]}");
    }

    // Test Clear
    personStack.Clear();
    Console.WriteLine($"Stack cleared. Count: {personStack.Count}");

    Console.WriteLine("\n=== Testing Stack<Stack<int>> (nested stacks) ===");
    Stack<Stack<int>> nestedStack = new Stack<Stack<int>>();

    Stack<int> innerStack1 = new Stack<int>();
    innerStack1.Push(1);
    innerStack1.Push(2);

    Stack<int> innerStack2 = new Stack<int>();
    innerStack2.Push(3);
    innerStack2.Push(4);

    nestedStack.Push(innerStack1);
    nestedStack.Push(innerStack2);

    Console.WriteLine("Nested stack structure:");
    foreach (Stack<int> innerStack in nestedStack)
    {
      Console.Write("Inner stack: ");
      foreach (int value in innerStack)
      {
        Console.Write($"{value} ");
      }
      Console.WriteLine();
    }

    // Demonstrating generic constraints
    Console.WriteLine("\n=== Testing with LINQ ===");
    Stack<int> numbersStack = new Stack<int>();
    for (int i = 1; i <= 10; i++)
    {
      numbersStack.Push(i);
    }

    // Using LINQ with our IEnumerable implementation
    var evenNumbers = numbersStack.Where(n => n % 2 == 0);
    Console.WriteLine("Even numbers in stack:");
    foreach (int num in evenNumbers)
    {
      Console.WriteLine(num);
    }

    var sum = numbersStack.Sum();
    Console.WriteLine($"Sum of all numbers: {sum}");
  }
}