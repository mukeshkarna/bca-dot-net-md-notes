# Unit 4: Advanced C# - Theoretical Foundation

## 1. Delegates

### Concept and Definition
A delegate in C# is a type-safe function pointer that can reference methods with a specific signature. Delegates serve as the foundation for implementing callbacks, events, and lambda expressions in C#.

### Key Characteristics
- **Type Safety**: Delegates ensure that only methods with matching signatures can be assigned to them.
- **Reference Type**: Delegates are reference types that store references to methods.
- **Immutability**: Once created, a delegate instance cannot be modified; a new instance must be created.
- **Object-Oriented**: Delegate types can be passed as parameters, returned from methods, and stored in variables.

### Types of Delegates
1. **Single-Cast Delegates**: Reference a single method at a time.
2. **Multi-Cast Delegates**: Can reference multiple methods that are invoked sequentially.
3. **Generic Delegates**: Built-in generic delegate types provided by .NET:
   - `Action<T1, T2, ...>`: For methods that return void
   - `Func<T1, T2, ..., TResult>`: For methods that return a value
   - `Predicate<T>`: For methods that return a boolean value

### Applications of Delegates
- **Callback Mechanisms**: Allowing methods to be called back after an operation completes
- **Event Handling**: Serving as the foundation for the event model
- **Asynchronous Programming**: Enabling notification when asynchronous operations complete
- **LINQ Operations**: Powering many LINQ methods that accept predicate functions
- **Strategy Pattern**: Implementing runtime method selection based on conditions

### Benefits of Using Delegates
- Promotes loose coupling between components
- Enables method callbacks without requiring interface implementation
- Provides a foundation for event-driven programming
- Allows for flexible and composable code structures

## 2. Events

### Concept and Definition
Events are a high-level mechanism built on delegates that implement the publisher-subscriber pattern. They allow a class to notify other classes when something of interest occurs.

### Components of the Event Model
- **Publisher**: The class that contains and raises the event
- **Subscribers**: Classes that register methods to be called when the event is raised
- **Event Arguments**: Data associated with the event
- **Event Handler**: The method that responds to the event

### The Standard Event Pattern
1. **Delegate Declaration**: Typically using `EventHandler` or `EventHandler<TEventArgs>`
2. **Event Declaration**: Using the `event` keyword with the delegate type
3. **Protected Method**: A protected "On" method that raises the event
4. **Event Arguments**: A class deriving from `EventArgs` that carries event data

### Event Characteristics
- **Encapsulation**: Events can only be invoked from within the declaring class
- **Subscribe/Unsubscribe**: External classes can only add or remove handlers
- **Thread Safety**: The event invocation pattern (`Invoke(this, e)`) provides thread safety
- **Null Check**: Using the null-conditional operator (`?.`) prevents exceptions when no handlers are registered

### Benefits of the Event Model
- Promotes loose coupling between components
- Implements the observer pattern in a standardized way
- Provides a robust mechanism for inter-object communication
- Supports a consistent approach to handling UI and system notifications

### Customizing Events
- **Custom Event Accessors**: Using `add` and `remove` blocks to customize subscription behavior
- **Weak Event Pattern**: Preventing memory leaks by allowing subscribers to be garbage collected
- **Event Source Pattern**: Creating dedicated event source classes for complex event handling

## 3. Lambda Expressions

### Concept and Definition
Lambda expressions provide a concise syntax for defining anonymous methods using a more readable, expression-focused approach. They simplify delegate creation and are extensively used in LINQ and asynchronous programming.

### Syntax Forms
1. **Expression Lambda**: `(parameters) => expression`
2. **Statement Lambda**: `(parameters) => { statements; }`

### Key Features
- **Type Inference**: Parameter types are often inferred from context
- **Implicit Returns**: Expression lambdas automatically return the value of the expression
- **Variable Capture**: Lambdas can access variables from their containing scope
- **Closures**: The runtime creates closures to maintain captured variable state

### Captured Variables
When a lambda expression references variables from its containing scope:
- A closure is created to maintain the variable state
- The captured variables live as long as any delegate referring to the lambda exists
- Captured variables can cause unexpected behavior in loops if not properly handled

### Applications of Lambda Expressions
- **LINQ Queries**: Providing concise predicates and transformations
- **Event Handlers**: Defining short, inline event handling code
- **Asynchronous Programming**: Configuring continuations and callbacks
- **Functional Programming**: Enabling functional techniques like higher-order functions

### Lambda Expressions vs. Anonymous Methods
Lambda expressions are a more concise and readable evolution of anonymous methods, supporting:
- Type inference for parameters
- Expression form for single-statement methods
- More intuitive variable capture semantics

## 4. Exception Handling

### Concept and Definition
Exception handling is a structured mechanism for detecting and responding to runtime errors, allowing programs to recover gracefully from exceptional conditions without crashing.

### Exception Handling Flow
1. **Try Block**: Contains code that might throw exceptions
2. **Catch Block**: Handles specific exception types
3. **Finally Block**: Contains cleanup code that executes regardless of exceptions
4. **Exception Propagation**: Unhandled exceptions are propagated up the call stack

### Exception Class Hierarchy
- **System.Exception**: Base class for all exceptions
- **System.SystemException**: Base class for exceptions thrown by the .NET runtime
- **System.ApplicationException**: Base class for application-defined exceptions
- **Specific Exception Types**: Like `ArgumentException`, `IOException`, `NullReferenceException`, etc.

### Best Practices
1. **Specific Exception Handling**: Catch specific exception types rather than base `Exception`
2. **Appropriate Exception Types**: Throw exceptions appropriate to the error condition
3. **Meaningful Messages**: Include informative error messages with context
4. **Preserving Stack Traces**: Use `throw;` instead of `throw ex;` when rethrowing
5. **Exception Filters**: Use exception filters (`when` clause) for conditional catching
6. **Resource Cleanup**: Use `finally` blocks or `using` statements to ensure proper cleanup

### Custom Exceptions
Creating custom exception classes allows for:
- Domain-specific error information
- Distinct exception handling based on application logic
- Additional properties that provide context about the error
- Consistent error handling patterns throughout an application

### Advanced Exception Features
- **Exception Filters**: Conditions that determine if a catch block should handle an exception
- **Aggregated Exceptions**: Combining multiple exceptions into a single exception
- **Task Exception Handling**: Special considerations for async/await exception propagation
- **Global Exception Handling**: Application-wide exception management strategies

## 5. Introduction to LINQ

### Concept and Definition
Language Integrated Query (LINQ) is a set of features that extends C# with query capabilities, allowing for unified, expressive, and type-safe data manipulation across various data sources.

### LINQ Architecture
1. **Query Expressions**: SQL-like syntax integrated into C#
2. **Extension Methods**: Methods like `Where`, `Select`, `OrderBy` that operate on sequences
3. **Expression Trees**: Runtime representations of queries for translating to other query languages
4. **Query Providers**: Components that translate and execute queries against specific data sources

### Query Syntax vs. Method Syntax
1. **Query Syntax**: SQL-like syntax using `from`, `where`, `select` keywords
2. **Method Syntax**: Chained method calls using extension methods

### Key LINQ Operations
- **Filtering**: Selecting elements based on a condition (`Where`)
- **Projection**: Transforming elements into a new form (`Select`)
- **Ordering**: Arranging elements in a sequence (`OrderBy`, `ThenBy`)
- **Grouping**: Organizing elements into groups (`GroupBy`)
- **Joining**: Combining elements from different sequences (`Join`, `GroupJoin`)
- **Aggregation**: Calculating summary values (`Sum`, `Average`, `Min`, `Max`, `Count`)
- **Set Operations**: Combining or comparing sequences (`Union`, `Intersect`, `Except`)
- **Element Operations**: Retrieving specific elements (`First`, `Single`, `ElementAt`)
- **Quantifiers**: Testing conditions across sequences (`Any`, `All`, `Contains`)

### Deferred vs. Immediate Execution
- **Deferred Execution**: Queries are not executed until results are needed
  - Enables composition of complex queries before execution
  - Allows queries to reflect changes to source collections
  - Implemented through iterators (yield return)
  
- **Immediate Execution**: Queries are executed at the point of definition
  - Operators like `ToList()`, `ToArray()`, `Count()` force immediate execution
  - Useful for caching results or when source data might change
  - Required for side-effecting operations

### LINQ Providers
- **LINQ to Objects**: Queries in-memory collections
- **LINQ to SQL**: Queries SQL Server databases (legacy)
- **LINQ to Entities**: Queries databases through Entity Framework
- **LINQ to XML**: Queries XML documents
- **LINQ to DataSet**: Queries ADO.NET DataSets
- **Custom LINQ Providers**: Can be created for other data sources

### Benefits of LINQ
- **Unified Syntax**: Same query patterns across different data sources
- **Type Safety**: Compile-time checking of queries
- **IntelliSense Support**: Better development experience
- **Query Optimization**: Providers can optimize query execution
- **Expressiveness**: Complex queries can be written concisely and clearly

## 6. Working with Databases

### Database Access Approaches in .NET

#### 1. ADO.NET
The foundational data access technology in .NET that provides:
- **Direct Connection Control**: Explicit management of database connections
- **DataReader**: Forward-only, read-only access to data
- **DataSet/DataAdapter**: Disconnected data access with local caching
- **Command Objects**: Direct execution of SQL statements or stored procedures
- **Transaction Management**: Explicit control over database transactions

#### 2. Entity Framework Core
A modern Object-Relational Mapping (ORM) framework that offers:
- **Object-Oriented Approach**: Work with data as .NET objects
- **DbContext**: The primary class for database interaction
- **Change Tracking**: Automatic tracking of entity changes
- **Relationship Management**: Navigate and manage entity relationships
- **Migrations**: Versioned database schema evolution
- **LINQ Integration**: Query databases using LINQ expressions

#### 3. Dapper
A lightweight ORM that provides:
- **High Performance**: Minimal overhead compared to full ORMs
- **SQL-Centric Approach**: Write SQL directly with parameter mapping
- **Object Mapping**: Convert query results to strongly-typed objects
- **Multi-Mapping**: Handle complex queries returning multiple object types
- **Stored Procedure Support**: Execute stored procedures with parameter mapping

### Database Design Patterns

#### 1. Repository Pattern
- **Definition**: Abstracts the data access layer, providing collection-like interfaces
- **Benefits**: 
  - Centralizes data access logic
  - Facilitates testing through abstraction
  - Reduces duplication of query logic
  - Provides a consistent API for data access

#### 2. Unit of Work Pattern
- **Definition**: Maintains a list of objects affected by a business transaction
- **Benefits**:
  - Coordinates the writing out of changes
  - Manages concurrency problems
  - Groups operations into atomic units
  - Works with repository pattern to manage context

#### 3. CQRS (Command Query Responsibility Segregation)
- **Definition**: Separates read and write operations into different models
- **Benefits**:
  - Optimizes read and write operations independently
  - Scales read and write sides separately
  - Simplifies complex domain models
  - Facilitates event sourcing

### Database Performance Considerations
- **Connection Pooling**: Reuse database connections to reduce overhead
- **Eager vs. Lazy Loading**: Control when related data is loaded
- **N+1 Query Problem**: Avoid multiple queries when loading related entities
- **Batch Operations**: Combine multiple operations into a single database call
- **Indexes**: Properly index database tables based on query patterns
- **Query Optimization**: Structure queries to leverage database engine capabilities
- **Caching**: Implement appropriate caching strategies for frequently accessed data

### Database Transaction Management
- **Transaction Fundamentals**: ACID properties (Atomicity, Consistency, Isolation, Durability)
- **Transaction Scopes**: Defining the boundaries of transactions
- **Isolation Levels**: Controlling how transactions interact
- **Distributed Transactions**: Managing transactions across multiple resources
- **Optimistic vs. Pessimistic Concurrency**: Strategies for handling concurrent data access

## 7. Writing Web Applications using ASP.NET

### ASP.NET Core Overview
ASP.NET Core is a cross-platform, high-performance framework for building modern, cloud-based, Internet-connected applications.

### Key Features
- **Cross-Platform**: Runs on Windows, macOS, and Linux
- **Modular Architecture**: Built as composable NuGet packages
- **Dependency Injection**: Built-in container for service dependencies
- **Configuration System**: Flexible configuration from various sources
- **Environment-Based Configuration**: Different settings per environment (Development, Staging, Production)
- **Middleware Pipeline**: Composable request processing pipeline
- **Hosting Models**: Versatile deployment options (IIS, self-host, Docker)

### ASP.NET Core MVC Pattern
The Model-View-Controller architectural pattern:
- **Model**: Represents the data and business logic
- **View**: Handles the user interface and presentation
- **Controller**: Processes requests, interacts with the model, and selects views

### Key Components of ASP.NET Core MVC
1. **Controllers**: Classes that handle HTTP requests and return responses
2. **Actions**: Methods within controllers that process specific requests
3. **Routing**: System for mapping URLs to controller actions
4. **Model Binding**: Automatic mapping of HTTP request data to method parameters
5. **Validation**: Framework for validating user input
6. **Views/Razor Pages**: Template system for generating HTML
7. **Tag Helpers**: Server-side components that create HTML in Razor views
8. **View Components**: Reusable UI components with server-side logic
9. **Areas**: Way to organize related functionality into separate namespaces

### ASP.NET Core Web API
A framework for building HTTP services that reach a broad range of clients:
- **RESTful Design**: Following REST principles for resource access
- **Content Negotiation**: Supporting multiple formats (JSON, XML)
- **Status Codes**: Using appropriate HTTP status codes for responses
- **Model Binding and Validation**: Automatic request processing and validation
- **OpenAPI/Swagger**: Automatic API documentation and testing tools

### Blazor
A framework for building interactive web UIs using C# instead of JavaScript:
- **Blazor Server**: Runs C# on the server with SignalR connection
- **Blazor WebAssembly**: Runs C# directly in the browser via WebAssembly
- **Component Model**: UI built from composable components
- **JavaScript Interop**: Interaction with JavaScript libraries when needed
- **Forms and Validation**: Form handling with validation support

### Authentication and Authorization
ASP.NET Core provides comprehensive security features:
- **Identity System**: User management, authentication, and roles
- **External Providers**: Integration with OAuth and OpenID Connect
- **Policy-Based Authorization**: Flexible permission rules beyond roles
- **Data Protection**: APIs for protecting sensitive data
- **HTTPS Enforcement**: Tools to require secure connections
- **CSRF Protection**: Defense against cross-site request forgery
- **CORS Support**: Controlled cross-origin resource sharing

### Middleware Pipeline
Request processing in ASP.NET Core occurs through a pipeline of middleware components:
- **Request Delegation**: Each component can process the request or pass it to the next
- **Order Matters**: Middleware executes in the order added to the pipeline
- **Short-Circuiting**: Middleware can terminate request processing
- **Built-in Middleware**: Authentication, static files, routing, etc.
- **Custom Middleware**: Creating application-specific request processing

### Deployment and Hosting
ASP.NET Core applications can be deployed in various ways:
- **IIS Hosting**: Traditional Windows web server deployment
- **Self-Hosting**: Running in a standalone process
- **Docker Containers**: Containerized deployment for cloud and microservices
- **Azure App Service**: Fully managed platform for web applications
- **Azure Kubernetes Service**: Container orchestration for scalable deployments
- **Platform Independence**: Same application can run across different environments


