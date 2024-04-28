namespace Shops.Exceptions;

public class ProductsNumberPurchasedException : Exception
{
    public ProductsNumberPurchasedException()
        : base("The store is out of stock")
    { }
}