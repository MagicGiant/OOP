using Shops.Exceptions;

namespace Shops.Entities;

public class ProductData
{
    private int _amount;
    private decimal _price;

    public ProductData(int amount, decimal price, Product product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        Price = price;
        Amount = amount;
    }

    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
                throw new PriceException(value);

            _price = value;
        }
    }

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

    public Product Product { get; }

    public override bool Equals(object obj)
    {
        if ((obj == null) || GetType() != obj.GetType())
            return false;
        return Product == ((ProductData)obj).Product;
    }

    public override int GetHashCode()
    {
        return Product.GetHashCode();
    }
}