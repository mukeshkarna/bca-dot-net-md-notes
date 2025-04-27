# Practice Questions for Unit 3: Creating Types in C#

## Knowledge-Based Questions

1. What are the four pillars of object-oriented programming supported by C# classes?

2. Differentiate between a constructor and a destructor in C#. When is each one called?

3. Explain the purpose of the 'this' keyword in C#. Provide three different scenarios where it is commonly used.

4. What is the difference between a property and a field in C#? Why would you use a property instead of a public field?

5. Explain what an indexer is and when you would use it in your code.

6. What is the difference between a static constructor and an instance constructor? What are the limitations of static constructors?

7. What is dynamic binding in C# and how does it differ from static binding?

8. What is operator overloading? List five common operators that can be overloaded in C#.

9. Explain the concept of inheritance in C#. What are its limitations compared to some other programming languages?

10. What is an abstract class? How does it differ from a concrete class?

## Analytical Questions

11. When would you choose to use an abstract class instead of an interface? When would an interface be more appropriate?

12. Explain the concepts of method overloading and method overriding. How do they differ in terms of compile-time and runtime behavior?

13. Compare and contrast classes and structs. When would you prefer to use a struct over a class?

14. What are the different access modifiers in C# and how do they affect the visibility of types and members?

15. What is the difference between early binding and late binding? When would you choose one over the other?

## Application Questions

16. Design a class hierarchy for a library management system that includes books, magazines, and digital media. Identify which classes should be abstract and which methods might be overridden.

17. How would you implement the IDisposable pattern for a class that manages both managed and unmanaged resources?

18. Create a simple specification for a generic collection class that could store any type of data but enforce that all elements are of the same type.

19. Design an interface for a banking system that different types of accounts (savings, checking, loan) would implement. What common functionality should all account types have?

20. How would you implement operator overloading for a custom Fraction class to support addition, subtraction, multiplication, and division of fractions?

## Critical Thinking Questions

21. What are some potential drawbacks of extensive inheritance hierarchies? When might composition be a better approach than inheritance?

22. How does the garbage collector's behavior impact how you should implement finalizers (destructors) in your classes?

23. Discuss the trade-offs between using abstract classes and interfaces when designing reusable frameworks.

24. How do C# generics improve on the concept of using the 'object' type for creating reusable code?

25. In what scenarios would you use the 'dynamic' keyword in C#, and what are the potential risks associated with its use?

26. How might you design a class to ensure it cannot be inherited from? What are the implications of making a class sealed?

27. Discuss how encapsulation is supported in C# and why it's considered an important principle in object-oriented programming.

28. What design considerations should you keep in mind when creating a class that will be used as a base class for many other classes?

29. How does the concept of polymorphism relate to interfaces and abstract classes in C#?

30. Compare and contrast automatic properties, full properties, and expression-bodied properties. When would you use each?