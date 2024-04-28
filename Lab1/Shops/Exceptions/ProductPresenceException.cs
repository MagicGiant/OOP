namespace Shops.Exceptions;

public class ProductPresenceException : Exception
{
    public ProductPresenceException()
        : base("There are not enough products in the shops")
    { }
}