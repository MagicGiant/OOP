using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models.Datas;

public class BankNumber
{
    public static readonly Regex NumberRegex = new ("^d{4}$", RegexOptions.Compiled);

    private string _number;

    public BankNumber(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (NumberRegex.IsMatch(name))
            throw BankNumberException.InvalidNumberException(name);
        _number = name;
    }

    public override string ToString()
    {
        return _number;
    }
}