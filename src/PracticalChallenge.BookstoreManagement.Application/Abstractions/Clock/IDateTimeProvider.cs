namespace PracticalChallenge.BookstoreManagement.Application.Abstractions.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
