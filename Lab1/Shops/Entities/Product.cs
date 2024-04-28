using System.Text.RegularExpressions;
using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Product
{
    private static readonly Regex _regex = new (@"[A-Z][a-z]{2,8}", RegexOptions.Compiled);
    public Product(string name)
    {
        if (!_regex.IsMatch(name))
            throw new ProductNameException(name);

        Name = name;
    }

    public string Name { get; }

    public static bool operator ==(Product first, Product second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(Product first, Product second)
    {
        return !(first == second);
    }

    public override bool Equals(object obj)
    {
        if ((obj == null) || !GetType().Equals(obj.GetType()))
            return false;
        return Name == ((Product)obj).Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}