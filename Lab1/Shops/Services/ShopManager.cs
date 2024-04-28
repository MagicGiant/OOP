using System.Collections.ObjectModel;
using Shops.Entities;
using Shops.Exceptions;
using Shops.Models;
using Shops.User;

namespace Shops.Services;

public class ShopManager
{
    private Dictionary<string, Shop> _shops = new ();

    public Shop CreateShop(ShopName name)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (_shops.ContainsKey(name.ToString()))
            throw new UsingShopNameException(name.ToString());

        Shop shop = new Shop(name);

        _shops[name.ToString()] = shop;

        return shop;
    }

    public IReadOnlyCollection<Shop> Shops() => _shops.Values;

    public Shop FindShop(string name)
    {
        return _shops[name];
    }

    public Shop GetCheapestShop(Person person, ICollection<Package> packages)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (packages == null) throw new ArgumentNullException(nameof(packages));

        if (!CheckProductAvailability(person, packages))
            throw new ProductPresenceException();

        Shop priorityShop = _shops.Values.First();
        decimal minCost = _shops.Values.First().GetPriceProductCollections(packages);

        if (_shops.Count == 1)
            return priorityShop;

        foreach (var shop in _shops.Values)
        {
            decimal price = shop.GetPriceProductCollections(packages);

            if (price < minCost)
            {
                priorityShop = shop;
                minCost = price;
            }
        }

        return priorityShop;
    }

    public bool CheckProductAvailability(Person person, ICollection<Package> packages)
    {
        if (person == null) throw new ArgumentNullException(nameof(person));
        if (packages == null) throw new ArgumentNullException(nameof(packages));

        foreach (Package package in packages)
        {
            int remainingProducts = package.Amount;

            foreach (Shop shop in _shops.Values)
                remainingProducts -= shop.GetProductNumber(package.Product);

            if (remainingProducts > 0)
                return false;
        }

        return true;
    }
}