public class Library
{
  private List<Book> books = new List<Book>();

  // Indexer by integer index
  public Book this[int index]
  {
    get
    {
      if (index < 0 || index >= books.Count)
        throw new IndexOutOfRangeException("Invalid book index");
      return books[index];
    }
    set
    {
      if (index < 0 || index >= books.Count)
        throw new IndexOutOfRangeException("Invalid book index");
      books[index] = value;
    }
  }

  // Indexer by ISBN (string)
  public Book this[string isbn]
  {
    get
    {
      Book book = books.FirstOrDefault(b => b.ISBN == isbn);
      if (book == null)
        throw new KeyNotFoundException($"No book found with ISBN: {isbn}");
      return book;
    }
  }

  // Method to add a book
  public void AddBook(Book book)
  {
    if (book == null)
      throw new ArgumentNullException(nameof(book));
    books.Add(book);
  }

  // Method to remove a book
  public bool RemoveBook(string isbn)
  {
    Book bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);
    if (bookToRemove != null)
    {
      books.Remove(bookToRemove);
      return true;
    }
    return false;
  }

  // Property to get the count of books
  public int Count
  {
    get { return books.Count; }
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    Library library = new Library();

    // Add books
    library.AddBook(new Book("The Hobbit", "J.R.R. Tolkien", "978-0-345-33968-3", 1937));
    library.AddBook(new Book("Pride and Prejudice", "Jane Austen", "978-0-141-43951-8", 1813));
    library.AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "978-0-06-112008-4", 1960));

    // Access by index
    Console.WriteLine("First book: " + library[0].ToString());

    // Access by ISBN
    Console.WriteLine("Book by ISBN: " + library["978-0-141-43951-8"].ToString());

    // Remove a book
    library.RemoveBook("978-0-345-33968-3");
    Console.WriteLine($"Library now has {library.Count} books");

    // Try to access removed book
    try
    {
      Book removedBook = library["978-0-345-33968-3"];
    }
    catch (KeyNotFoundException ex)
    {
      Console.WriteLine($"Error: {ex.Message}");
    }
  }
}