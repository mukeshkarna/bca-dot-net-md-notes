// Abstract base class
public abstract class Employee
{
  public string Name { get; set; }
  public string ID { get; set; }
  public string Department { get; set; }

  protected Employee(string name, string id, string department)
  {
    Name = name;
    ID = id;
    Department = department;
  }

  // Abstract method
  public abstract decimal CalculateSalary();

  // Virtual method with default implementation
  public virtual string GetEmployeeInfo()
  {
    return $"ID: {ID}, Name: {Name}, Department: {Department}, Salary: ${CalculateSalary():N2}";
  }
}

// Derived class - HourlyEmployee
public class HourlyEmployee : Employee
{
  public decimal HourlyRate { get; set; }
  public int HoursWorked { get; set; }

  public HourlyEmployee(string name, string id, string department, decimal hourlyRate)
      : base(name, id, department)
  {
    HourlyRate = hourlyRate;
    HoursWorked = 0;
  }

  public void LogHours(int hours)
  {
    HoursWorked += hours;
  }

  public override decimal CalculateSalary()
  {
    // Overtime calculation: 1.5x rate for hours over 40
    if (HoursWorked <= 40)
    {
      return HoursWorked * HourlyRate;
    }
    else
    {
      int regularHours = 40;
      int overtimeHours = HoursWorked - 40;
      return (regularHours * HourlyRate) + (overtimeHours * HourlyRate * 1.5m);
    }
  }

  public override string GetEmployeeInfo()
  {
    return base.GetEmployeeInfo() + $", Hourly Rate: ${HourlyRate}, Hours: {HoursWorked}";
  }
}

// Derived class - SalariedEmployee
public class SalariedEmployee : Employee
{
  public decimal AnnualSalary { get; set; }
  public decimal BonusPercentage { get; set; }

  public SalariedEmployee(string name, string id, string department, decimal annualSalary)
      : base(name, id, department)
  {
    AnnualSalary = annualSalary;
    BonusPercentage = 0;
  }

  public void SetBonus(decimal percentage)
  {
    BonusPercentage = percentage;
  }

  public override decimal CalculateSalary()
  {
    // Monthly salary calculation including bonus
    decimal monthlySalary = AnnualSalary / 12;
    decimal bonusAmount = monthlySalary * (BonusPercentage / 100);
    return monthlySalary + bonusAmount;
  }

  public override string GetEmployeeInfo()
  {
    return base.GetEmployeeInfo() + $", Annual: ${AnnualSalary:N2}, Bonus: {BonusPercentage}%";
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    List<Employee> employees = new List<Employee>();

    // Create hourly employees
    HourlyEmployee hourly1 = new HourlyEmployee("John Doe", "E001", "Production", 25.50m);
    hourly1.LogHours(45); // 5 hours overtime
    employees.Add(hourly1);

    HourlyEmployee hourly2 = new HourlyEmployee("Jane Smith", "E002", "Production", 30.00m);
    hourly2.LogHours(38);
    employees.Add(hourly2);

    // Create salaried employees
    SalariedEmployee salaried1 = new SalariedEmployee("Bob Johnson", "E003", "Management", 75000m);
    salaried1.SetBonus(10);
    employees.Add(salaried1);

    SalariedEmployee salaried2 = new SalariedEmployee("Alice Brown", "E004", "Engineering", 85000m);
    salaried2.SetBonus(15);
    employees.Add(salaried2);

    // Display all employees
    Console.WriteLine("Employee Information:");
    foreach (Employee emp in employees)
    {
      Console.WriteLine(emp.GetEmployeeInfo());
    }

    // Calculate total payroll
    decimal totalPayroll = employees.Sum(e => e.CalculateSalary());
    Console.WriteLine($"\nTotal Monthly Payroll: ${totalPayroll:N2}");

    // Group by department
    var departmentGroups = employees.GroupBy(e => e.Department);
    Console.WriteLine("\nEmployees by Department:");
    foreach (var group in departmentGroups)
    {
      Console.WriteLine($"{group.Key}: {group.Count()} employees");
      decimal deptTotal = group.Sum(e => e.CalculateSalary());
      Console.WriteLine($"Department Total: ${deptTotal:N2}");
    }
  }
}