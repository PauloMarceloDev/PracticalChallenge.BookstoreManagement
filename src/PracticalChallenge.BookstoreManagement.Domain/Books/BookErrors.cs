using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Domain.Books;

public static class BookErrors
{
    public static readonly Error InvalidPrice = new(
        "Book.InvalidPrice",
        "The book cannot be negative price");
    
    public static readonly Error InvalidQuantityInStock = new(
        "Book.InvalidQuantityInStock",
        "The book cannot be negative quantity in stock");
    
    public static readonly Error DifferentIds = new(
        "Book.DifferentIds",
        "Only book with the same id can be updated");
}
