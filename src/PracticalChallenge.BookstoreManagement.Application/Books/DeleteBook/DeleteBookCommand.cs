using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

namespace PracticalChallenge.BookstoreManagement.Application.Books.DeleteBook;

public sealed record DeleteBookCommand(Guid BookId) : ICommand;
