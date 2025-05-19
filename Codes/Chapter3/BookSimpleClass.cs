public class Book
{
    // Properties
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }

    // Constructor
    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }

    // Override ToString method
    public override string ToString()
    {
        return $"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Year: {PublicationYear}";
    }

    // Method to compare publication years
    public bool IsNewerThan(Book otherBook)
    {
        if (otherBook == null)
            return false;
        
        return this.PublicationYear > otherBook.PublicationYear;
    }
}

// Usage example:
class Program
{
    static void Main()
    {
        Book book1 = new Book("The Great Gatsby", "F. Scott Fitzgerald", "978-0-7432-7356-5", 1925);
        Book book2 = new Book("1984", "George Orwell", "978-0-452-28423-4", 1949);

        Console.WriteLine(book1.ToString());
        Console.WriteLine(book2.ToString());
        Console.WriteLine($"Is book2 newer than book1? {book2.IsNewerThan(book1)}");
    }
}