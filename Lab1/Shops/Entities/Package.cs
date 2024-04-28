using Shops.Exceptions;

namespace Shops.Entities;

public class Package
{
    private int _amount;
    public Package(Product product, int amount)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Amount = amount;
    }

    public Product Product { get; }

    public int Amount
    {
        get => _amount;
        set
        {
            if (value < 0)
                throw new AmountException(value);

            _amount = value;
        }
    }
}