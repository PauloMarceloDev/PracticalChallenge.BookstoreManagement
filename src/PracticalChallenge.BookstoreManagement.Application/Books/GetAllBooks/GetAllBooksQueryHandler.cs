using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Application.Books.GetAllBooks;

internal sealed class GetAllBooksQueryHandler(IBookRepository bookRepository)
    : IQueryHandler<GetAllBooksQuery, IEnumerable<BookResponse>>
{
    public async Task<Result<IEnumerable<BookResponse>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Book> books = await bookRepository.GetAllAsync(cancellationToken);

        IEnumerable<BookResponse> booksResponse = books.Select(book =>
            new BookResponse(
                book.Id,
                book.Title.Value,
                book.Authors.Select(a => a.Value),
                book.Genres.Select(a => a.Value),
                book.Price.Currency.Code,
                book.Price.Amount,
                book.QuantityInStock.Value,
                book.CreatedOnUtc,
                book.UpdatedOnUtc));

        return Result.Success(booksResponse);
    }
}
