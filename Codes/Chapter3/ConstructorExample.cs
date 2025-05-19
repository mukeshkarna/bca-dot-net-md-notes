public class BankAccount
{
  public string AccountHolderName { get; set; }
  public decimal Balance { get; set; }
  public string AccountNumber { get; }

  // Default constructor
  public BankAccount() : this(0, "Unknown")
  {
    // Calls the parameterized constructor with default values
  }

  // Constructor with balance only
  public BankAccount(decimal initialBalance) : this(initialBalance, "Unknown")
  {
    // Calls the full parameterized constructor
  }

  // Full parameterized constructor
  public BankAccount(decimal initialBalance, string accountHolderName)
  {
    Balance = initialBalance;
    AccountHolderName = accountHolderName;
    AccountNumber = GenerateAccountNumber();
  }

  // Private method to generate account number
  private string GenerateAccountNumber()
  {
    Random random = new Random();
    return "ACC" + random.Next(100000, 999999);
  }

  public override string ToString()
  {
    return $"Account: {AccountNumber}, Holder: {AccountHolderName}, Balance: ${Balance}";
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    BankAccount account1 = new BankAccount();
    BankAccount account2 = new BankAccount(1000);
    BankAccount account3 = new BankAccount(5000, "John Smith");

    Console.WriteLine(account1.ToString());
    Console.WriteLine(account2.ToString());
    Console.WriteLine(account3.ToString());
  }
}