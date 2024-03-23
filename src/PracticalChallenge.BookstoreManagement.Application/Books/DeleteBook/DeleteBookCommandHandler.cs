using PracticalChallenge.BookstoreManagement.Application.Abstractions.Messaging;
using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Application.Books.DeleteBook;

internal sealed class DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteBookCommand>
{
    public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        Book? bookById = await bookRepository.GetByIdOrDefaultAsync(request.BookId, cancellationToken);

        if (bookById is null)
        {
            return Result.Failure<Guid>(BookErrors.NotFound);
        }
        
        bookRepository.Delete(bookById);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}
