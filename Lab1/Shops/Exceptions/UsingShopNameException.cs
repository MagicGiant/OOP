using Shops.Models;

namespace Shops.Exceptions;

public class UsingShopNameException : Exception
{
    public UsingShopNameException(string name)
        : base($"This name ({name}) was using")
    { }
}