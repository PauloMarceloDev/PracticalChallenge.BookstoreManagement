using PracticalChallenge.BookstoreManagement.Application.Abstractions.Clock;
using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Application.Books.UpdateBook;

internal sealed class UpdateBookCommandHandler(
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<UpdateBookCommand>
{
    public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        Book? bookById = await bookRepository.GetByIdOrDefaultAsync(request.BookId, cancellationToken);

        if (bookById is null)
        {
            return Result.Failure<Guid>(BookErrors.NotFound);
        }

        Book? bookByTitle = await bookRepository.GetByTitleOrDefaultAsync(new Title(request.Title), cancellationToken);

        if (bookByTitle is not null && bookByTitle.Id != request.BookId)
        {
            return Result.Failure<Guid>(BookErrors.TitleAlreadyExists);
        }
        
        Result<Book> updatedBookResult = Book.Update(
            bookById,
            new Title(request.Title),
            request.Authors.Select(value => new Author(value)),
            request.Genres.Select(value => new Genre(value)),
            new Money(request.MoneyValue, Currency.FromCode(request.MoneyCurrency)),
            new QuantityInStock(request.QuantityInStock),
            dateTimeProvider.UtcNow);
        
        if (updatedBookResult.IsFailure)
        {
            return Result.Failure<Guid>(updatedBookResult.Error);
        }

        bookRepository.Update(bookById);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
