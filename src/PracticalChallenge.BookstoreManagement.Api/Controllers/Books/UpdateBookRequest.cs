namespace PracticalChallenge.BookstoreManagement.Api.Controllers.Books;

public sealed record UpdateBookRequest(
    string Title,
    IEnumerable<string> Authors,
    IEnumerable<string> Genres,
    string MoneyCurrency,
    decimal MoneyValue,
    int QuantityInStock);
