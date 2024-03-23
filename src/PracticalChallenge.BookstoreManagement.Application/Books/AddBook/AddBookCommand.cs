using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

namespace PracticalChallenge.BookstoreManagement.Application.Books.AddBook;

public sealed record AddBookCommand(
    string Title,
    IEnumerable<string> Authors,
    IEnumerable<string> Genres,
    string MoneyCurrency,
    decimal MoneyValue,
    int QuantityInStock) : ICommand;
