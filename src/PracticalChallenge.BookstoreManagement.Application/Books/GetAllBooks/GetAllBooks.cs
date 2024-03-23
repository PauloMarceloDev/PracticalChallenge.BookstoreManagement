using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;

namespace PracticalChallenge.BookstoreManagement.Application.Books.GetAllBooks;

public sealed record GetAllBooksQuery() : IQuery<IEnumerable<BookResponse>>;
