using Shops.Models;

namespace Shops.Exceptions;

public class ProductNameException : Exception
{
    public ProductNameException(string name)
        : base($"Invalid product name ({name}")
    { }
}