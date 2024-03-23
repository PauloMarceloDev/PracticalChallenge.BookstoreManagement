using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Application.Books.GetBookById;

internal sealed class GetBookByIdQueryHandler(IBookRepository repository)
    : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    public async Task<Result<BookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        Book? book = await repository.GetByIdOrDefaultAsync(request.Id, cancellationToken);
        
        if (book is null)
        {
            return Result.Failure<BookResponse>(BookErrors.NotFound);
        }

        return new BookResponse(book.Id,
            book.Title.Value,
            book.Authors.Select(a => a.Value),
            book.Genres.Select(a => a.Value),
            book.Price.Currency.Code,
            book.Price.Amount,
            book.QuantityInStock.Value,
            book.CreatedOnUtc,
            book.UpdatedOnUtc);
    }
}
