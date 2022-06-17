namespace API;

public record Book(string Title, string Author);

public class Query
{
    public Book GetBook()
    {
        return new Book("C# in depth", "Jon Skeet");
    }
}