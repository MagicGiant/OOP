using System.Collections.ObjectModel;
using Microsoft.VisualBasic;
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.User;

namespace Shops.Services;

public class Shop
{
    private Dictionary<string, ProductData> _packages = new ();
    public Shop(ShopName name)
    {
        Name = name;
    }

    public ShopName Name { get; }

    public void SetProduct(ProductData productData)
    {
        if (productData == null) throw new ArgumentNullException(nameof(productData));

        string name = productData.Product.Name;

        if (_packages.ContainsKey(name))
        {
            if (_packages[name].Product != productData.Product)
                throw new NameAndPriceException();

            _packages[name].Amount += productData.Amount;
        }
        else
        {
            _packages[name] = productData;
        }
    }

    public decimal GetPrice(Package package)
    {
        if (package == null) throw new ArgumentNullException(nameof(package));
        if (!_packages.ContainsKey(package.Product.ToString()))
            throw new SeekProductException(package.Product.ToString());

        return _packages[package.Product.ToString()].Price * package.Amount;
    }

    public decimal GetPriceProductCollections(ICollection<Package> packages)
    {
        decimal price = 0;

        foreach (Package package in packages)
            price += GetPrice(package);

        return price;
    }

    public int GetProductNumber(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        if (!_packages.ContainsKey(product.ToString()))
            return 0;
        return _packages[product.ToString()].Amount;
    }

    public void BuyProduct(Person person, Package package)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (package == null) throw new ArgumentNullException(nameof(package));

        if (package.Amount > GetProductNumber(package.Product))
            throw new ProductPresenceException();

        if (person.Cash < GetPrice(package))
            throw new SufficiencyMoneyException();

        person.Cash -= GetPrice(package);
        SupplyProduct(package);
    }

    public void BuyProducts(Person person, ICollection<Package> packages)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (packages == null) throw new ArgumentNullException(nameof(packages));

        if (!CheckProductAvailability(packages))
            throw new ProductPresenceException();
        if (!IsEnoughMoney(person, packages))
            throw new SufficiencyMoneyException();

        foreach (Package package in packages)
            SupplyProduct(package);

        person.Cash -= GetPriceProductCollections(packages);
    }

    public bool CheckProductAvailability(ICollection<Package> packages)
    {
        if (packages == null) throw new ArgumentNullException(nameof(packages));

        foreach (var package in packages)
        {
            string productName = package.Product.Name;
            if (package.Amount > _packages[productName].Amount)
                return false;
        }

        return true;
    }

    public bool IsEnoughMoney(Person person, ICollection<Package> packages)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (packages == null) throw new ArgumentNullException(nameof(packages));

        decimal personCash = person.Cash;
        foreach (var package in packages)
        {
            personCash -= _packages[package.Product.Name].Price;
            if (personCash < 0)
                return false;
        }

        return true;
    }

    public IReadOnlyCollection<ProductData> GetPackages() => _packages.Values;

    private void SupplyProduct(Package package)
    {
        if (package == null) throw new ArgumentNullException(nameof(package));

        string name = package.Product.Name;

        if (package.Amount > _packages[name].Amount)
            throw new ProductPresenceException();

        if (package.Amount == _packages[name].Amount)
            _packages.Remove(name);
        else
            _packages[name].Amount -= package.Amount;
    }
}