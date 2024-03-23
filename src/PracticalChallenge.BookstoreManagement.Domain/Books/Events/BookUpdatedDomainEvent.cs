using PracticalChallenge.BookstoreManagement.Domain.Abstractions;

namespace PracticalChallenge.BookstoreManagement.Domain.Books.Events;

public sealed record BookUpdatedDomainEvent(Guid BookId) : IDomainEvent;
