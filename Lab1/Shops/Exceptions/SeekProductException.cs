namespace Shops.Exceptions;

public class SeekProductException : Exception
{
    public SeekProductException(string productName)
        : base($"This product ({productName}) not found")
    { }
}