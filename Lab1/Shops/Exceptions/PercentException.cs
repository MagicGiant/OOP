namespace Shops.Exceptions;

public class PercentException : Exception
{
    public PercentException(int percent)
        : base($"Percent should be more 0. Now is {percent}")
    { }
}