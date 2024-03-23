using System.Collections;

namespace PracticalChallenge.BookstoreManagement.Domain.Books;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Book?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Book?> GetByTitleOrDefaultAsync(Title title, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByGenresOrDefaultAsync(IEnumerable<Genre> genres, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByAuthorsOrDefaultAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>?> GetByMoneyRangeOrDefaultAsync(MoneyRange moneyRange, CancellationToken cancellationToken = default);
    void Add(Book book);
    void Update(Book book);
    void Delete(Book bookId);
}
