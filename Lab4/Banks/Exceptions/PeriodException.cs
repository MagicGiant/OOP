namespace Banks.Exceptions;

public class PeriodException : Exception
{
    public PeriodException(string message)
        : base(message)
    { }

    public static PeriodException DateDiffereceException()
    {
        return new PeriodException("First date must be more second");
    }
}