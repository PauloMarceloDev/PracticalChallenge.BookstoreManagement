using Microsoft.EntityFrameworkCore;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Infrastructure.Repositories;

internal sealed class BookRepository(ApplicationDbContext dbContext) : Repository<Book>(dbContext), IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default) => 
        await DbContext.Set<Book>()
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);

    public async Task<Book?> GetByIdOrDefaultAsync(Guid id, CancellationToken cancellationToken = default) => 
        await DbContext.Set<Book>().FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<Book?> GetByTitleOrDefaultAsync(Title title, CancellationToken cancellationToken = default) => 
        await DbContext.Set<Book>().FirstOrDefaultAsync(b => b.Title == title, cancellationToken);

    public async Task<IEnumerable<Book>?> GetByGenresOrDefaultAsync(IEnumerable<Genre> genres, CancellationToken cancellationToken = default) => 
        await DbContext.Set<Book>()
            .Where(b => b.Genres.Any(genres.Contains))
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Book>?> GetByAuthorsOrDefaultAsync(IEnumerable<Author> authors, CancellationToken cancellationToken = default) => 
        await DbContext.Set<Book>()
            .Where(b => b.Authors.Any(authors.Contains))
            .OrderBy(b => b.Title)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Book>?> GetByMoneyRangeOrDefaultAsync(MoneyRange moneyRange, CancellationToken cancellationToken = default) =>
        await DbContext.Set<Book>().Where(b =>
                b.Price.Currency.Code == moneyRange.Start.Currency.Code &&
                b.Price.Amount >= moneyRange.Start.Amount &&
                b.Price.Amount <= moneyRange.End.Amount)
            .ToListAsync(cancellationToken);
}
