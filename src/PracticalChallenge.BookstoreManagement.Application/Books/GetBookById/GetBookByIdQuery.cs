using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;

namespace PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;

public sealed record GetBookByIdQuery(
    Guid Id) : IQuery<BookResponse>;
