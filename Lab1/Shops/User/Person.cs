using Shops.Exceptions;

namespace Shops.User;

public class Person
{
    private decimal _cash;

    public Person(decimal cash)
    {
        if (cash < 0) throw new PersonCashException(cash);
        _cash = cash;
    }

    public decimal Cash
    {
        get => _cash;
        set
        {
            if (value < 0) throw new PersonCashException(value);
            _cash = value;
        }
    }
}