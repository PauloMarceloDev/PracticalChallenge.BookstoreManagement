using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PracticalChallenge.BookstoreManagement.Domain.Books;

namespace PracticalChallenge.BookstoreManagement.Infrastructure.Configurations;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("Books");

        builder.HasKey(book => book.Id);

        builder.Property(book => book.Title)
            .HasConversion(title => title.Value, value => new Title(value))
            .HasMaxLength(255);

        builder.OwnsOne(book => book.Price, priceBuilder => priceBuilder.Property(money => money.Currency)
            .HasConversion(currency => currency.Code, code => Currency.FromCode(code)));

        builder.Property(book => book.QuantityInStock)
            .HasConversion(quantityInStock => quantityInStock.Value, value => new QuantityInStock(value));

        builder
            .Property(e => e.Authors)
            .HasConversion(
                authors => string.Join(';', authors.OrderBy(a => a.Value).Select(author => author.Value)),
                authorAsString => authorAsString
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(author => new Author(author))
                .ToList());

        builder
            .Property(e => e.Genres)
            .HasConversion(
                genres => string.Join(';', genres.OrderBy(genre => genre.Value).Select(genre => genre.Value)),
                genreAsString => genreAsString
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Select(genre => new Genre(genre))
                    .ToList());
    }
}
