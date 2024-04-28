using System.Collections.ObjectModel;
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.Services;
using Shops.User;
using Xunit;

namespace Shops.Test;

public class Shop
{
    private ShopManager _shopManager = new ();

    [Fact]
    public void CreateShop_FindThisShopInShopManager()
    {
        string name = "Gman";
        _shopManager.CreateShop(new ShopName(name));
        Assert.NotNull(_shopManager.FindShop(name));
    }

    [Theory]
    [InlineData("1239234")]
    [InlineData("gordon")]
    [InlineData("Alix@")]
    [InlineData("Sherhan obirhanov")]
    public void SetInvalidShopName_ThrowException(string name)
    {
        Assert.Throws<ShopNameException>(() =>
        {
            ShopName shopName = new (name);
        });
    }

    [Theory]
    [InlineData("R129 8")]
    [InlineData("ojsdk")]
    [InlineData("J@kjsf")]
    public void SetInvalidProductName_ThrowException(string name)
    {
        Assert.Throws<ProductNameException>(() =>
        {
            Product product = new Product(name);
        });
    }

    [Fact]
    public void GetProductNumber_CheckNumber()
    {
        Services.Shop shop = _shopManager.CreateShop(new ShopName("Mac"));
        Product product = new Product("Chocolate");
        shop.SetProduct(new ProductData(1, 120, product));

        Assert.Equal(1, shop.GetProductNumber(product));
    }

    [Fact]
    public void BuyProducts_CheckMinimumCost()
    {
        Person person = new Person(1000);

        string fName = "Magnit";
        string sName = "Piterochka";

        Services.Shop firstShop = _shopManager.CreateShop(new ShopName(fName));
        Services.Shop secondShop = _shopManager.CreateShop(new ShopName(sName));

        Product fProduct = new Product("Chocolate");
        Product sProduct = new Product("Laptop");

        firstShop.SetProduct(new ProductData(15, 134, fProduct));
        firstShop.SetProduct(new ProductData(13, 123, sProduct));

        secondShop.SetProduct(new ProductData(15, 122, fProduct));
        secondShop.SetProduct(new ProductData(15, 140, sProduct));

        Collection<Package> products = new ();
        products.Add(new Package(fProduct, 1));
        products.Add(new Package(sProduct, 1));

        Services.Shop shop = _shopManager.GetCheapestShop(person, products);
        shop.BuyProducts(person, products);

        int expectedCost = 1000 - (134 + 123);
        Assert.Equal(expectedCost, person.Cash);
    }

    [Fact]
    public void CheckEquals_FirstProductEqualsSecondProduct()
    {
        string name = "Chokolade";

        Product firstProduct = new (name);
        Product secondProduct = new (name);

        bool a = firstProduct == secondProduct;
        Assert.True(a);
        Assert.Equal(firstProduct, secondProduct);
    }
}