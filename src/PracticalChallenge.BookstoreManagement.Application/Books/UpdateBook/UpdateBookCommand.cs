using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

namespace PracticalChallenge.BookstoreManagement.Application.Books.UpdateBook;

public record UpdateBookCommand(
    Guid BookId,
    string Title,
    IEnumerable<string> Authors,
    IEnumerable<string> Genres,
    string MoneyCurrency,
    decimal MoneyValue,
    int QuantityInStock) : ICommand;
