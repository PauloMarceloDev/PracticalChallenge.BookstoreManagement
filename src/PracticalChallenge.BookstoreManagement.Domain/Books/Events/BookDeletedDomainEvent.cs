using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Domain.Books.Events;

public sealed record BookDeletedDomainEvent(Guid BookId) : IDomainEvent;
