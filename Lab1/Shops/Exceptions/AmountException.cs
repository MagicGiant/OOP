namespace Shops.Exceptions;

public class AmountException : Exception
{
    public AmountException(int amount)
        : base($"Amount should be more 0. Now is {amount}")
    { }
}