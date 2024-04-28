namespace Shops.Exceptions;

public class NameAndPriceException : Exception
{
    public NameAndPriceException()
        : base("If the products have the same name, then the price must be the same")
    { }
}