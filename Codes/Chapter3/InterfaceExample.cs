// First interface
public interface IVehicle
{
  double Speed { get; set; }
  double FuelCapacity { get; set; }
  void Start();
}

// Second interface
public interface IElectric
{
  double BatteryCapacity { get; set; }
  void Charge();
}

// Car class implementing IVehicle
public class Car : IVehicle
{
  public double Speed { get; set; }
  public double FuelCapacity { get; set; }
  public string Model { get; set; }

  public Car(string model, double fuelCapacity)
  {
    Model = model;
    FuelCapacity = fuelCapacity;
    Speed = 0;
  }

  public void Start()
  {
    Console.WriteLine($"{Model} car is starting with traditional engine.");
    Speed = 10;
  }

  public void Accelerate(double increment)
  {
    Speed += increment;
    Console.WriteLine($"{Model} accelerated to {Speed} km/h");
  }
}

// ElectricCar class implementing both interfaces
public class ElectricCar : IVehicle, IElectric
{
  public double Speed { get; set; }
  public double FuelCapacity { get; set; }
  public double BatteryCapacity { get; set; }
  public string Model { get; set; }

  public ElectricCar(string model, double batteryCapacity)
  {
    Model = model;
    BatteryCapacity = batteryCapacity;
    FuelCapacity = 0; // Electric cars don't use fuel
    Speed = 0;
  }

  public void Start()
  {
    Console.WriteLine($"{Model} electric car is starting silently.");
    Speed = 5;
  }

  public void Charge()
  {
    Console.WriteLine($"{Model} is charging. Battery capacity: {BatteryCapacity} kWh");
  }

  public void CheckBatteryStatus()
  {
    Console.WriteLine($"Battery status: {BatteryCapacity} kWh remaining");
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Car regularCar = new Car("Toyota Camry", 60);
    ElectricCar tesla = new ElectricCar("Tesla Model 3", 75);

    // Using as IVehicle
    IVehicle vehicle1 = regularCar;
    IVehicle vehicle2 = tesla;

    vehicle1.Start();
    vehicle2.Start();

    // Using specific car methods
    regularCar.Accelerate(50);

    // Using ElectricCar specific features
    tesla.Charge();
    tesla.CheckBatteryStatus();

    // Using as IElectric
    IElectric electricVehicle = tesla;
    electricVehicle.Charge();

    // Demonstrating polymorphism
    StartVehicle(regularCar);
    StartVehicle(tesla);
  }

  static void StartVehicle(IVehicle vehicle)
  {
    vehicle.Start();
    Console.WriteLine($"Vehicle started with speed: {vehicle.Speed} km/h");
  }
}