namespace Shops.Exceptions;

public class ShopNameException : Exception
{
    public ShopNameException(string name)
        : base($"Invalid name ({name})")
    { }
}