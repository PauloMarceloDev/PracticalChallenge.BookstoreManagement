namespace PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;

public sealed record BookResponse(
    Guid BookId,
    string Title,
    IEnumerable<string> Authors,
    IEnumerable<string> Genres,
    string MoneyCurrency,
    decimal MoneyValue,
    int QuantityInStock,
    DateTime CreatedOnUtc,
    DateTime? UpdatedOnUtc);
