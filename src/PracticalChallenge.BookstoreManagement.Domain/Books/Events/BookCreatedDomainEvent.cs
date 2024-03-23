using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Domain.Books.Events;

public sealed record BookCreatedDomainEvent(Guid BookId) : IDomainEvent;
