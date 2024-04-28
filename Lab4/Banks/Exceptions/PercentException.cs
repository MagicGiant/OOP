using Banks.Models;

namespace Banks.Exceptions;

public class PercentException : Exception
{
    public PercentException(string message)
        : base(message)
    { }

    public static PercentException ValueException()
    {
        return new PercentException("Percent can't be low 0");
    }
}