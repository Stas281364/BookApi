namespace ToDoAPI.Contracts
{
    public record BooksResponse(
        Guid Id,
        string Titile,
        string Description,
        decimal Price
        );
    
}
