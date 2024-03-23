namespace PracticalChallenge.BookstoreManagement.Domain.Books;

public sealed record MoneyRange
{
    private MoneyRange()
    {
    }

    public Money Start { get; init; }

    public Money End { get; init; }

    public static MoneyRange Create(Money start, Money end)
    {
        if (start.Currency != end.Currency)
        {
            throw new ApplicationException("Money currencies must be the same.");
        }

        if (start.Amount > end.Amount)
        {
            throw new ApplicationException("Start value must be less than or equal to the end value.");
        }

        return new MoneyRange
        {
            Start = start,
            End = end
        };
    }
}
