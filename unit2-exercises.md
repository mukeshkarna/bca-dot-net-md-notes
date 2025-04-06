# C# Unit 2 Practice Exercises

## Exercise 1: Console Application Basics
Create a console application that:
1. Asks the user for their name
2. Asks the user for their age
3. Calculates and displays the year they were born
4. Tells them how old they will be in 10 years

**Sample solution:**
```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for name
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        
        // Ask for age
        Console.Write("Please enter your age: ");
        string ageInput = Console.ReadLine();
        
        // Convert age string to integer
        if (int.TryParse(ageInput, out int age))
        {
            // Calculate birth year (approximate)
            int currentYear = DateTime.Now.Year;
            int birthYear = currentYear - age;
            
            // Calculate future age
            int futureAge = age + 10;
            
            // Display results
            Console.WriteLine($"Hello, {name}!");
            Console.WriteLine($"You were born around {birthYear}.");
            Console.WriteLine($"In 10 years, you will be {futureAge} years old.");
        }
        else
        {
            Console.WriteLine("Invalid age entered. Please enter a number next time.");
        }
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
```

## Exercise 2: Working with Strings
Create a program that:
1. Asks the user to enter a sentence
2. Counts and displays:
   - The number of characters (including spaces)
   - The number of characters (excluding spaces)
   - The number of words
   - The number of vowels
3. Converts the sentence to uppercase and lowercase and displays both

**Sample solution:**
```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // Ask for input
        Console.WriteLine("Enter a sentence:");
        string sentence = Console.ReadLine();
        
        // Count characters with spaces
        int totalChars = sentence.Length;
        
        // Count characters without spaces
        int charsNoSpaces = 0;
        foreach (char c in sentence)
        {
            if (!char.IsWhiteSpace(c))
            {
                charsNoSpaces++;
            }
        }
        
        // Count words
        string[] words = sentence.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        int wordCount = words.Length;
        
        // Count vowels
        int vowelCount = 0;
        foreach (char c in sentence.ToLower())
        {
            if ("aeiou".Contains(c))
            {
                vowelCount++;
            }
        }
        
        // Convert to uppercase and lowercase
        string upperCase = sentence.ToUpper();
        string lowerCase = sentence.ToLower();
        
        // Display results
        Console.WriteLine($"Total characters (including spaces): {totalChars}");
        Console.WriteLine($"Total characters (excluding spaces): {charsNoSpaces}");
        Console.WriteLine($"Number of words: {wordCount}");
        Console.WriteLine($"Number of vowels: {vowelCount}");
        Console.WriteLine($"Uppercase: {upperCase}");
        Console.WriteLine($"Lowercase: {lowerCase}");
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
```

## Exercise 3: Array Manipulation
Create a program that:
1. Creates an array of 10 random integers between 1 and 100
2. Displays the original array
3. Finds and displays:
   - The highest value
   - The lowest value
   - The average value
4. Sorts the array and displays it in ascending order
5. Reverses the array and displays it in descending order

**Sample solution:**
```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create array of 10 random integers
        int[] numbers = new int[10];
        Random random = new Random();
        
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 101); // 1 to 100
        }
        
        // Display original array
        Console.WriteLine("Original array:");
        DisplayArray(numbers);
        
        // Find highest value
        int highest = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] > highest)
            {
                highest = numbers[i];
            }
        }
        Console.WriteLine($"Highest value: {highest}");
        
        // Find lowest value
        int lowest = numbers[0];
        for (int i = 1; i < numbers.Length; i++)
        {
            if (numbers[i] < lowest)
            {
                lowest = numbers[i];
            }
        }
        Console.WriteLine($"Lowest value: {lowest}");
        
        // Calculate average
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }
        double average = (double)sum / numbers.Length;
        Console.WriteLine($"Average value: {average:F2}");
        
        // Sort array
        Array.Sort(numbers);
        Console.WriteLine("\nSorted array (ascending):");
        DisplayArray(numbers);
        
        // Reverse array
        Array.Reverse(numbers);
        Console.WriteLine("\nSorted array (descending):");
        DisplayArray(numbers);
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    static void DisplayArray(int[] arr)
    {
        foreach (int num in arr)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();
    }
}
```

## Exercise 4: Method Parameters
Create a program that demonstrates different types of method parameters:
1. Create a method that takes an array of integers by value and tries to modify it
2. Create a method that takes an array of integers by reference (`ref`) and modifies it
3. Create a method that takes an `out` parameter to return multiple values
4. Create a method with optional parameters
5. Demonstrate the use of the `params` keyword

**Sample solution:**
```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // Value parameters with arrays
        Console.WriteLine("--- Value Parameters with Arrays ---");
        int[] numbers = { 1, 2, 3, 4, 5 };
        Console.WriteLine("Original array:");
        DisplayArray(numbers);
        
        ModifyArray(numbers);
        Console.WriteLine("After ModifyArray (arrays are reference types, so changes persist):");
        DisplayArray(numbers);
        
        // Reset array
        numbers = new int[] { 1, 2, 3, 4, 5 };
        
        // Reference parameters
        Console.WriteLine("\n--- Reference Parameters ---");
        Console.WriteLine("Original array:");
        DisplayArray(numbers);
        
        ModifyArrayByRef(ref numbers);
        Console.WriteLine("After ModifyArrayByRef (new array assigned):");
        DisplayArray(numbers);
        
        // Output parameters
        Console.WriteLine("\n--- Output Parameters ---");
        int num1 = 20, num2 = 10;
        Console.WriteLine($"Numbers: {num1} and {num2}");
        
        Calculate(num1, num2, out int sum, out int difference, out int product, out double quotient);
        Console.WriteLine($"Sum: {sum}");
        Console.WriteLine($"Difference: {difference}");
        Console.WriteLine($"Product: {product}");
        Console.WriteLine($"Quotient: {quotient}");
        
        // Optional parameters
        Console.WriteLine("\n--- Optional Parameters ---");
        Greet("John");
        Greet("Jane", "Good evening");
        
        // Params keyword
        Console.WriteLine("\n--- Params Keyword ---");
        int total1 = AddNumbers(5, 10, 15);
        Console.WriteLine($"Total of 5, 10, 15: {total1}");
        
        int total2 = AddNumbers(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        Console.WriteLine($"Total of 1 through 10: {total2}");
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    // Value parameter (arrays are reference types)
    static void ModifyArray(int[] arr)
    {
        // This will modify the original array
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] *= 2;
        }
    }
    
    // Reference parameter
    static void ModifyArrayByRef(ref int[] arr)
    {
        // This creates a completely new array
        arr = new int[] { 10, 20, 30, 40, 50 };
    }
    
    // Output parameters
    static void Calculate(int a, int b, out int sum, out int difference, out int product, out double quotient)
    {
        sum = a + b;
        difference = a - b;
        product = a * b;
        quotient = b != 0 ? (double)a / b : 0;
    }
    
    // Optional parameters
    static void Greet(string name, string greeting = "Hello")
    {
        Console.WriteLine($"{greeting}, {name}!");
    }
    
    // Params keyword
    static int AddNumbers(params int[] numbers)
    {
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        return sum;
    }
    
    static void DisplayArray(int[] arr)
    {
        foreach (int num in arr)
        {
            Console.Write($"{num} ");
        }
        Console.WriteLine();
    }
}
```

## Exercise 5: Control Flow Statements
Create a program that demonstrates various control flow statements by implementing a simple number guessing game:
1. Generate a random number between 1 and 100
2. Ask the user to guess the number
3. Provide feedback if the guess is too high or too low
4. Allow the user to keep guessing until they find the correct number
5. Count and display the number of attempts needed
6. Ask if the user wants to play again

**Sample solution:**
```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        bool playAgain = true;
        Random random = new Random();
        
        Console.WriteLine("Welcome to the Number Guessing Game!");
        
        while (playAgain)
        {
            // Generate random number
            int targetNumber = random.Next(1, 101);
            int guessCount = 0;
            bool hasGuessedCorrectly = false;
            
            Console.WriteLine("\nI'm thinking of a number between 1 and 100.");
            
            // Main game loop
            while (!hasGuessedCorrectly)
            {
                // Get user's guess
                Console.Write("Enter your guess: ");
                string input = Console.ReadLine();
                
                // Validate input
                if (!int.TryParse(input, out int guess))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }
                
                // Increment guess count
                guessCount++;
                
                // Check guess
                if (guess < 1 || guess > 100)
                {
                    Console.WriteLine("Your guess must be between 1 and 100.");
                }
                else if (guess < targetNumber)
                {
                    Console.WriteLine("Too low! Try a higher number.");
                }
                else if (guess > targetNumber)
                {
                    Console.WriteLine("Too high! Try a lower number.");
                }
                else
                {
                    hasGuessedCorrectly = true;
                    Console.WriteLine($"Congratulations! You guessed the number {targetNumber} correctly!");
                    Console.WriteLine($"It took you {guessCount} {(guessCount == 1 ? "guess" : "guesses")}.");
                    
                    // Rate their performance
                    if (guessCount <= 5)
                    {
                        Console.WriteLine("Excellent guessing skills!");
                    }
                    else if (guessCount <= 10)
                    {
                        Console.WriteLine("Good job!");
                    }
                    else
                    {
                        Console.WriteLine("Keep practicing to improve your guessing strategy.");
                    }
                }
            }
            
            // Ask to play again
            Console.Write("\nDo you want to play again? (y/n): ");
            string playAgainResponse = Console.ReadLine().ToLower();
            
            playAgain = (playAgainResponse == "y" || playAgainResponse == "yes");
        }
        
        Console.WriteLine("\nThank you for playing the Number Guessing Game!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
```

## Exercise 6: Working with Namespaces
Create a solution with multiple files that demonstrates namespace usage:

**File 1: Program.cs**
```csharp
using System;
using MyApp.Mathematics;
using MyApp.Statistics;
using Calc = MyApp.Mathematics.Calculator; // Using an alias

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create an array for testing
            int[] data = { 4, 7, 2, 9, 5, 3, 8, 1, 6 };
            
            // Use Calculator from Mathematics namespace
            Calculator mathCalc = new Calculator();
            int sum = mathCalc.Sum(data);
            int product = mathCalc.Product(data);
            
            Console.WriteLine("--- Mathematics Results ---");
            Console.WriteLine($"Sum: {sum}");
            Console.WriteLine($"Product: {product}");
            
            // Use the alias
            double powerResult = Calc.Power(2, 8);
            Console.WriteLine($"2^8 = {powerResult}");
            
            // Use StatisticsCalculator
            StatisticsCalculator statsCalc = new StatisticsCalculator();
            double mean = statsCalc.Mean(data);
            double median = statsCalc.Median(data);
            
            Console.WriteLine("\n--- Statistics Results ---");
            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Median: {median}");
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
```

**File 2: Calculator.cs**
```csharp
using System;

namespace MyApp.Mathematics
{
    public class Calculator
    {
        public int Sum(int[] numbers)
        {
            int total = 0;
            foreach (int num in numbers)
            {
                total += num;
            }
            return total;
        }
        
        public int Product(int[] numbers)
        {
            if (numbers.Length == 0)
                return 0;
                
            int result = 1;
            foreach (int num in numbers)
            {
                result *= num;
            }
            return result;
        }
        
        public static double Power(double baseNumber, double exponent)
        {
            return Math.Pow(baseNumber, exponent);
        }
    }
}
```

**File 3: StatisticsCalculator.cs**
```csharp
using System;
using System.Linq;

namespace MyApp.Statistics
{
    public class StatisticsCalculator
    {
        public double Mean(int[] numbers)
        {
            if (numbers.Length == 0)
                return 0;
                
            double sum = 0;
            foreach (int num in numbers)
            {
                sum += num;
            }
            return sum / numbers.Length;
        }
        
        public double Median(int[] numbers)
        {
            if (numbers.Length == 0)
                return 0;
                
            // Create a copy and sort it
            int[] sortedNumbers = (int[])numbers.Clone();
            Array.Sort(sortedNumbers);
            
            int middle = sortedNumbers.Length / 2;
            
            if (sortedNumbers.Length % 2 == 0)
            {
                // Even number of elements, average the two middle values
                return (sortedNumbers[middle - 1] + sortedNumbers[middle]) / 2.0;
            }
            else
            {
                // Odd number of elements, return the middle value
                return sortedNumbers[middle];
            }
        }
    }
}
```

## Exercise 7: String Formatting and Parsing
Create a program that:
1. Creates a "shopping cart" with item names, quantities, and prices
2. Formats and displays the cart as a receipt with proper alignment and currency formatting
3. Calculates subtotal, tax (at 8%), and total
4. Parses user input for payment and calculates change

**Sample solution:**
```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create shopping cart items
        List<(string Name, int Quantity, decimal Price)> cart = new List<(string, int, decimal)>
        {
            ("Laptop", 1, 899.99m),
            ("Mouse", 2, 24.95m),
            ("USB Cable", 3, 9.99m),
            ("Headphones", 1, 149.50m),
            ("Keyboard", 1, 49.99m)
        };
        
        // Constants
        const decimal taxRate = 0.08m; // 8%
        
        // Calculate subtotal
        decimal subtotal = 0;
        foreach (var item in cart)
        {
            subtotal += item.Quantity * item.Price;
        }
        
        // Calculate tax and total
        decimal tax = subtotal * taxRate;
        decimal total = subtotal + tax;
        
        // Display receipt
        Console.WriteLine("===== RECEIPT =====");
        Console.WriteLine($"{"Item",-20} {"Qty",5} {"Price",10} {"Total",10}");
        Console.WriteLine(new string('-', 47));
        
        foreach (var item in cart)
        {
            decimal itemTotal = item.Quantity * item.Price;
            Console.WriteLine($"{item.Name,-20} {item.Quantity,5} {item.Price,10:C2} {itemTotal,10:C2}");
        }
        
        Console.WriteLine(new string('-', 47));
        Console.WriteLine($"{"Subtotal",-25} {subtotal,20:C2}");
        Console.WriteLine($"{"Tax (8%)",-25} {tax,20:C2}");
        Console.WriteLine($"{"Total",-25} {total,20:C2}");
        
        // Get payment from user
        decimal payment = 0;
        bool validPayment = false;
        
        while (!validPayment)
        {
            Console.Write("\nEnter payment amount: $");
            string input = Console.ReadLine();
            
            if (decimal.TryParse(input, out payment))
            {
                if (payment >= total)
                {
                    validPayment = true;
                }
                else
                {
                    Console.WriteLine($"Payment must be at least {total:C2}. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a numeric value.");
            }
        }
        
        // Calculate and display change
        decimal change = payment - total;
        Console.WriteLine($"{"Payment",-25} {payment,20:C2}");
        Console.WriteLine($"{"Change",-25} {change,20:C2}");
        
        Console.WriteLine("\nThank you for your purchase!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
```

## Exercise 8: Working with DateTime
Create a program that:
1. Displays the current date and time in various formats
2. Asks the user for their birthdate and calculates:
   - Their exact age in years, months, and days
   - The day of the week they were born
   - How many days until their next birthday
3. Creates a simple calendar for the current month

**Sample solution:**
```csharp
using System;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        // Get current date and time
        DateTime now = DateTime.Now;
        
        // Display current date and time in various formats
        Console.WriteLine("Current Date and Time:");
        Console.WriteLine($"Default format: {now}");
        Console.WriteLine($"Short date: {now:d}");
        Console.WriteLine($"Long date: {now:D}");
        Console.WriteLine($"Short time: {now:t}");
        Console.WriteLine($"Long time: {now:T}");
        Console.WriteLine($"Full date/time: {now:F}");
        Console.WriteLine($"Custom format: {now:yyyy-MM-dd HH:mm:ss}");
        
        // Get user's birthdate
        DateTime birthDate = DateTime.MinValue;
        bool validDate = false;
        
        while (!validDate)
        {
            Console.Write("\nEnter your birthdate (yyyy-MM-dd): ");
            string input = Console.ReadLine();
            
            if (DateTime.TryParse(input, out birthDate))
            {
                if (birthDate < now)
                {
                    validDate = true;
                }
                else
                {
                    Console.WriteLine("Birthdate cannot be in the future. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd format.");
            }
        }
        
        // Calculate age
        int ageYears = now.Year - birthDate.Year;
        int ageMonths = now.Month - birthDate.Month;
        int ageDays = now.Day - birthDate.Day;
        
        if (ageDays < 0)
        {
            ageMonths--;
            ageDays += DateTime.DaysInMonth(now.Year, ((now.Month - 1) == 0) ? 12 : (now.Month - 1));
        }
        
        if (ageMonths < 0)
        {
            ageYears--;
            ageMonths += 12;
        }
        
        // Get day of week for birth date
        string dayOfWeek = birthDate.DayOfWeek.ToString();
        
        // Calculate days until next birthday
        DateTime nextBirthday = new DateTime(now.Year, birthDate.Month, birthDate.Day);
        if (nextBirthday < now)
        {
            nextBirthday = nextBirthday.AddYears(1);
        }
        
        int daysUntilNextBirthday = (nextBirthday - now).Days;
        
        // Display results
        Console.WriteLine("\nBirth Information:");
        Console.WriteLine($"You were born on {birthDate:D}");
        Console.WriteLine($"You were born on a {dayOfWeek}");
        Console.WriteLine($"You are {ageYears} years, {ageMonths} months, and {ageDays} days old");
        Console.WriteLine($"Days until your next birthday: {daysUntilNextBirthday}");
        
        // Display calendar for current month
        DisplayMonthCalendar(now.Year, now.Month);
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
    
    static void DisplayMonthCalendar(int year, int month)
    {
        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        
        // Get the month name
        string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
        
        // Display header
        Console.WriteLine($"\nCalendar for {monthName} {year}:");
        Console.WriteLine("Su Mo Tu We Th Fr Sa");
        
        // Calculate the first day offset
        int offset = (int)firstDayOfMonth.DayOfWeek;
        
        // Display the calendar
        for (int i = 0; i < offset; i++)
        {
            Console.Write("   ");
        }
        
        for (int day = 1; day <= daysInMonth; day++)
        {
            Console.Write($"{day,2} ");
            
            // Start a new line after Saturday
            if ((day + offset) % 7 == 0 || day == daysInMonth)
            {
                Console.WriteLine();
            }
        }
    }
}
```
