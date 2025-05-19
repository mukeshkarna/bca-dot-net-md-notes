// Abstract base class
public abstract class Shape
{
  public string Color { get; set; }

  protected Shape(string color)
  {
    Color = color;
  }

  // Abstract method
  public abstract double CalculateArea();

  // Virtual method with default implementation
  public virtual string GetDescription()
  {
    return $"This is a {Color} {GetType().Name} with area {CalculateArea():F2}";
  }
}

// Derived class - Circle
public class Circle : Shape
{
  public double Radius { get; set; }

  public Circle(double radius, string color) : base(color)
  {
    Radius = radius;
  }

  public override double CalculateArea()
  {
    return Math.PI * Radius * Radius;
  }

  public override string GetDescription()
  {
    return $"{base.GetDescription()} and radius {Radius}";
  }
}

// Derived class - Rectangle
public class Rectangle : Shape
{
  public double Width { get; set; }
  public double Height { get; set; }

  public Rectangle(double width, double height, string color) : base(color)
  {
    Width = width;
    Height = height;
  }

  public override double CalculateArea()
  {
    return Width * Height;
  }

  public override string GetDescription()
  {
    return $"{base.GetDescription()}, width {Width} and height {Height}";
  }
}

// Derived class - Triangle
public class Triangle : Shape
{
  public double Base { get; set; }
  public double Height { get; set; }

  public Triangle(double baseLength, double height, string color) : base(color)
  {
    Base = baseLength;
    Height = height;
  }

  public override double CalculateArea()
  {
    return 0.5 * Base * Height;
  }

  public override string GetDescription()
  {
    return $"{base.GetDescription()}, base {Base} and height {Height}";
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Shape[] shapes = new Shape[]
    {
            new Circle(5, "Red"),
            new Rectangle(4, 6, "Blue"),
            new Triangle(3, 4, "Green")
    };

    foreach (Shape shape in shapes)
    {
      Console.WriteLine(shape.GetDescription());
      Console.WriteLine($"Area: {shape.CalculateArea():F2}");
      Console.WriteLine();
    }

    // Demonstrating polymorphism
    CalculateAndDisplayArea(new Circle(10, "Yellow"));
    CalculateAndDisplayArea(new Rectangle(5, 5, "Purple"));
  }

  static void CalculateAndDisplayArea(Shape shape)
  {
    Console.WriteLine($"The area of the {shape.Color} {shape.GetType().Name} is {shape.CalculateArea():F2}");
  }
}