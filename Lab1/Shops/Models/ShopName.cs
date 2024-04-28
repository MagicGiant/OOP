using System.Text.RegularExpressions;
using Shops.Exceptions;

namespace Shops.Models;

public class ShopName
{
    private static readonly Regex _regex = new Regex(@"^[A-Z][A-Za-z]{2,10}$", RegexOptions.Compiled);

    private string _name;

    public ShopName(string name)
    {
        if (!_regex.IsMatch(name))
            throw new ShopNameException(name);

        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}