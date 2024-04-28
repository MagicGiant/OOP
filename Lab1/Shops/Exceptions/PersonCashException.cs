namespace Shops.Exceptions;

public class PersonCashException : Exception
{
    public PersonCashException(decimal cash)
        : base($"Cash should be more 0. Now is {cash}")
    { }
}