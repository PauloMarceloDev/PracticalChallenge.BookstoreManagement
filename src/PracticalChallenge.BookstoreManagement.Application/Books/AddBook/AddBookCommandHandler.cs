using PracticalChallenge.BookstoreManagement.Application.Abstractions.Clock;
using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Application.Books.AddBook;

internal sealed class AddBookCommandHandler(
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<AddBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        Book? book = await bookRepository.GetByTitleOrDefaultAsync(new Title(request.Title), cancellationToken);

        if (book is not null)
        {
            return Result.Failure<Guid>(BookErrors.TitleAlreadyExists);
        }

        Result<Book> bookResult = Book.Create(
            new Title(request.Title),
            request.Authors.Select(value => new Author(value)),
            request.Genres.Select(value => new Genre(value)),
            new Money(request.MoneyValue, Currency.FromCode(request.MoneyCurrency)),
            new QuantityInStock(request.QuantityInStock),
            dateTimeProvider.UtcNow);

        if (bookResult.IsFailure)
        {
            return Result.Failure<Guid>(bookResult.Error);
        }
        
        bookRepository.Add(bookResult.Value);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(bookResult.Value.Id);
    }
}
