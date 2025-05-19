public class Vector2D
{
  public double X { get; set; }
  public double Y { get; set; }

  // Constructor
  public Vector2D(double x, double y)
  {
    X = x;
    Y = y;
  }

  // Overload + operator for vector addition
  public static Vector2D operator +(Vector2D v1, Vector2D v2)
  {
    return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
  }

  // Overload - operator for vector subtraction
  public static Vector2D operator -(Vector2D v1, Vector2D v2)
  {
    return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
  }

  // Overload * operator for scalar multiplication
  public static Vector2D operator *(Vector2D v, double scalar)
  {
    return new Vector2D(v.X * scalar, v.Y * scalar);
  }

  // Overload * operator for scalar multiplication (scalar first)
  public static Vector2D operator *(double scalar, Vector2D v)
  {
    return v * scalar;
  }

  // Overload == operator
  public static bool operator ==(Vector2D v1, Vector2D v2)
  {
    if (ReferenceEquals(v1, v2))
      return true;
    if (ReferenceEquals(v1, null) || ReferenceEquals(v2, null))
      return false;

    return v1.X == v2.X && v1.Y == v2.Y;
  }

  // Overload != operator
  public static bool operator !=(Vector2D v1, Vector2D v2)
  {
    return !(v1 == v2);
  }

  // Override Equals method
  public override bool Equals(object obj)
  {
    if (obj is Vector2D other)
      return this == other;
    return false;
  }

  // Override GetHashCode method
  public override int GetHashCode()
  {
    return X.GetHashCode() ^ Y.GetHashCode();
  }

  // Override ToString method
  public override string ToString()
  {
    return $"({X}, {Y})";
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Vector2D v1 = new Vector2D(3, 4);
    Vector2D v2 = new Vector2D(1, 2);

    Console.WriteLine($"v1: {v1}");
    Console.WriteLine($"v2: {v2}");

    Vector2D sum = v1 + v2;
    Console.WriteLine($"v1 + v2: {sum}");

    Vector2D diff = v1 - v2;
    Console.WriteLine($"v1 - v2: {diff}");

    Vector2D scaled = v1 * 2;
    Console.WriteLine($"v1 * 2: {scaled}");

    Vector2D scaled2 = 3 * v2;
    Console.WriteLine($"3 * v2: {scaled2}");

    Console.WriteLine($"v1 == v2: {v1 == v2}");
    Console.WriteLine($"v1 != v2: {v1 != v2}");

    Vector2D v3 = new Vector2D(3, 4);
    Console.WriteLine($"v1 == v3: {v1 == v3}");
  }
}