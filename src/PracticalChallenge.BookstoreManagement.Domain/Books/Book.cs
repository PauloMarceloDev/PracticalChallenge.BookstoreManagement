using PracticalChallenge.BookstoreManagement.Domain.Abstractions;
using PracticalChallenge.BookstoreManagement.Domain.Books.Events;

namespace PracticalChallenge.BookstoreManagement.Domain.Books;

public sealed class Book : Entity
{
    public Title Title { get; private set; }
    
    public List<Author> Authors { get; private set; } = [];
    
    public List<Genre> Genres { get; private set; } = [];
    
    public Money Price { get; private set; }
    
    public QuantityInStock QuantityInStock { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime? UpdatedOnUtc { get; private set; }

    private Book(Guid id, Title title, IEnumerable<Author> authors, IEnumerable<Genre> genres, Money price,
        QuantityInStock quantityInStock, DateTime createdOnUtc)
        : base (id)
    {
        Title = title;
        Authors = authors.ToList();
        Genres = genres.ToList();
        Price = price;
        QuantityInStock = quantityInStock;
        CreatedOnUtc = createdOnUtc;
    }

    private Book()
    {
    }
    
    public static Result<Book> Create(
        Title title,
        IEnumerable<Author> authors,
        IEnumerable<Genre> genres,
        Money price,
        QuantityInStock quantityInStock,
        DateTime createdOnUtc)
    {
        if (price.Amount <= 0)
        {
            return Result.Failure<Book>(BookErrors.InvalidPrice);
        }

        if (quantityInStock.Value <= 0)
        {
            return Result.Failure<Book>(BookErrors.InvalidQuantityInStock);
        }

        var book = new Book(
            Guid.NewGuid(),
            title,
            authors,
            genres,
            price,
            quantityInStock,
            createdOnUtc);
        
        book.RaiseDomainEvent(new BookCreatedDomainEvent(book.Id));

        return book;
    }

    public void Update(DateTime updatedOnUtc)
    {
        UpdatedOnUtc = updatedOnUtc;
        
        RaiseDomainEvent(new BookUpdatedDomainEvent(Id));
    }

    public void Delete()
    {
        RaiseDomainEvent(new BookDeletedDomainEvent(Id));
    }
}
