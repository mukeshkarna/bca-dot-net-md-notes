public class Student
{
  // Static counter to track total number of students
  private static int studentCount;

  // Instance properties
  public string Name { get; set; }
  public int StudentId { get; }

  // Static constructor
  static Student()
  {
    studentCount = 0;
    Console.WriteLine("Static constructor called. Initializing student counter.");
  }

  // Instance constructor
  public Student(string name)
  {
    Name = name;
    studentCount++;
    StudentId = 1000 + studentCount; // Auto-generate unique ID
  }

  // Static method to get total count
  public static int GetTotalStudents()
  {
    return studentCount;
  }

  // Instance method
  public override string ToString()
  {
    return $"Student ID: {StudentId}, Name: {Name}";
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Console.WriteLine($"Total students before creating any: {Student.GetTotalStudents()}");

    Student student1 = new Student("Alice");
    Student student2 = new Student("Bob");
    Student student3 = new Student("Charlie");

    Console.WriteLine(student1.ToString());
    Console.WriteLine(student2.ToString());
    Console.WriteLine(student3.ToString());

    Console.WriteLine($"Total students created: {Student.GetTotalStudents()}");
  }
}