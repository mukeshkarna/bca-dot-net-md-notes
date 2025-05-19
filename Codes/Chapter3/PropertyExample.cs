public class Employee
{
  // Auto-property for Name
  public string Name { get; set; }

  // Property with backing field for Age with validation
  private int _age;
  public int Age
  {
    get { return _age; }
    set
    {
      if (value < 18 || value > 65)
        throw new ArgumentOutOfRangeException("Age must be between 18 and 65");
      _age = value;
    }
  }

  // Calculated property for YearsToRetirement
  public int YearsToRetirement
  {
    get { return 65 - Age; }
  }

  // Read-only property for EmployeeId (set only in constructor)
  public string EmployeeId { get; }

  // Constructor
  public Employee(string employeeId, string name, int age)
  {
    EmployeeId = employeeId;
    Name = name;
    Age = age;
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    try
    {
      Employee emp1 = new Employee("EMP001", "John Doe", 30);
      Console.WriteLine($"Employee: {emp1.Name}");
      Console.WriteLine($"Age: {emp1.Age}");
      Console.WriteLine($"Years to retirement: {emp1.YearsToRetirement}");
      Console.WriteLine($"Employee ID: {emp1.EmployeeId}");

      // This will throw an exception
      // Employee emp2 = new Employee("EMP002", "Jane Smith", 70);
    }
    catch (ArgumentOutOfRangeException ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
}