using PracticalChallenge.BookstoreManagement.Application.Abstractions.Clock;

namespace PracticalChallenge.BookstoreManagement.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
