# Practical Programming Questions for Unit 3: Creating Types in C#

## Basic Implementation Questions

### 1. Creating a Simple Class
**Task**: Create a `Book` class with the following requirements:
- Properties: Title, Author, ISBN, PublicationYear
- Constructor that initializes all properties
- Override ToString() to display book information
- Implement a method `IsNewerThan(Book otherBook)` that compares publication years

### 2. Implementing Properties
**Task**: Create a `Employee` class that demonstrates different types of properties:
- Auto-property for Name
- Property with backing field for Age with validation (must be between 18-65)
- Calculated property for YearsToRetirement (assuming retirement at 65)
- Read-only property for EmployeeId (set only in constructor)

### 3. Working with Constructors
**Task**: Create a `BankAccount` class with:
- Constructor chaining (default and parameterized constructors)
- Use 'this' keyword to call one constructor from another
- Initialize balance to zero in default constructor
- Initialize balance and account holder name in parameterized constructor

### 4. Using Static Members
**Task**: Create a `Student` class that:
- Has a static counter to track the total number of students created
- Auto-generates a unique student ID for each new student
- Includes a static method to get the total count of students
- Implements a static constructor to initialize the counter

### 5. Implementing an Indexer
**Task**: Create a `Library` class that:
- Stores a collection of books
- Implements an indexer to access books by index
- Implements a string indexer to find books by ISBN
- Includes methods to add and remove books

## Intermediate Implementation Questions

### 6. Operator Overloading
**Task**: Create a `Vector2D` class that:
- Has X and Y properties
- Overloads the + and - operators for vector addition and subtraction
- Overloads the == and != operators for equality comparison
- Overloads the * operator for scalar multiplication
- Provides both ToString() and Equals() overrides

### 7. Inheritance Hierarchy
**Task**: Create a hierarchy for a shape system:
- Abstract base class `Shape` with abstract method `CalculateArea()`
- Derived classes: `Circle`, `Rectangle`, and `Triangle`
- Each derived class implements its own area calculation
- Add a `Color` property in the base class
- Implement a virtual method `GetDescription()` that derived classes can override

### 8. Interface Implementation
**Task**: Create interfaces and implement them:
- Interface `IVehicle` with properties: Speed, FuelCapacity and method Start()
- Interface `IElectric` with property BatteryCapacity and method Charge()
- Create classes `Car` and `ElectricCar` that implement appropriate interfaces
- ElectricCar should implement both interfaces

### 9. Enum and Struct Usage
**Task**: Create a gaming system that uses:
- Enum `GameDifficulty` with Easy, Medium, Hard, Expert levels
- Struct `GameScore` that stores player name, score, and difficulty
- Class `GameSession` that tracks multiple scores
- Method to get high scores for a specific difficulty level

### 10. Abstract Class Implementation
**Task**: Create an employee management system:
- Abstract class `Employee` with abstract method `CalculateSalary()`
- Derived classes: `HourlyEmployee` and `SalariedEmployee`
- Each type calculates salary differently
- Include common properties in base class (Name, ID, Department)
- Add a virtual method `GetEmployeeInfo()` with default implementation

## Advanced Implementation Questions

### 11. Generic Class Implementation
**Task**: Create a generic `Stack<T>` class that:
- Implements Push and Pop operations
- Includes a Peek method to view the top element
- Throws appropriate exceptions when popping from empty stack
- Includes a generic method `Contains<T>(T item)`
- Implements IEnumerable<T> for foreach support

### 12. Complex Inheritance with Interfaces
**Task**: Create a media player system:
- Interface `IPlayable` with Play(), Pause(), Stop() methods
- Interface `IRecordable` with Record() method
- Abstract class `MediaDevice` with common properties
- Classes `CDPlayer` (implements IPlayable)
- Class `DVDPlayer` (implements IPlayable and IRecordable)
- Demonstrate polymorphism by storing different devices in an IPlayable collection

### 13. Encapsulation and Access Control
**Task**: Design a bank account system that demonstrates proper encapsulation:
- Private fields for balance and transaction history
- Public methods for deposit and withdrawal with validation
- Protected method for logging transactions (for inheritance)
- Internal method for bank-only operations
- Nested private class for transaction details

### 14. Dynamic Behavior Implementation
**Task**: Create a plugin system that uses dynamic binding:
- Base class `Plugin` with virtual Execute() method
- Several derived plugin classes
- A `PluginManager` that loads plugins dynamically
- Demonstrate dynamic binding by calling methods on plugins through base class reference

### 15. Full System Design
**Task**: Design a simple library management system using all OOP concepts:
- Abstract class `LibraryItem` for books, magazines, DVDs
- Interfaces `IBorrowable` and `IReservable`
- Concrete classes implementing the interfaces
- Static class for library statistics
- Generic collection for items
- Proper use of access modifiers
- Operator overloading for date comparisons
- Indexer for accessing items by ID

## Design Pattern Questions

### 16. Singleton Pattern
**Task**: Implement a `DatabaseConnection` class following the Singleton pattern:
- Ensure only one instance can be created
- Thread-safe implementation
- Provide a static method to get the instance
- Include connection pooling simulation

### 17. Factory Pattern
**Task**: Create a shape factory system:
- Interface `IShape` with Draw() method
- Concrete shapes implementing the interface
- Static factory class with CreateShape() method
- Method accepts shape type enum and returns appropriate shape

### 18. Observer Pattern
**Task**: Implement a simple notification system:
- Interface `IObserver` with Update() method
- Class `Subject` that maintains a list of observers
- Methods to attach and detach observers
- Notify all observers when state changes

### 19. Decorator Pattern
**Task**: Create a coffee ordering system:
- Abstract class `Coffee` with cost and description methods
- Concrete coffee types (Espresso, Latte)
- Abstract decorator class
- Concrete decorators (Milk, Sugar, Whip)
- Demonstrate adding multiple decorators

### 20. Strategy Pattern
**Task**: Implement different sorting strategies:
- Interface `ISortStrategy` with Sort() method
- Concrete strategies: BubbleSort, QuickSort, MergeSort
- Context class that uses a strategy
- Allow runtime strategy switching

These practical questions progressively build understanding of C# type creation concepts while providing hands-on coding experience. 