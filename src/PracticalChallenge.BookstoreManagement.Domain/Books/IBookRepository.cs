namespace PracticalChallenge.BookstoreManagement.Domain.Books;

public interface IBookRepository
{
    Task<Book?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByTitleOrDefaultAsync(Title title, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByGenresOrDefaultAsync(IEnumerable<Genre> genres, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByAuthorsOrDefaultAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByMoneyRangeOrDefaultAsync(MoneyRange moneyRange, CancellationToken cancellationToken = default);
}
