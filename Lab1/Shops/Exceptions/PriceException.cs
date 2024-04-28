namespace Shops.Exceptions;

public class PriceException : Exception
{
    public PriceException(decimal price)
        : base($"Price should be more 0. Now is {price}")
    { }
}