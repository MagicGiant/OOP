using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models;

public class AccountNumber
{
    public static readonly Regex NumberRegex = new (@"^\d{12}$", RegexOptions.Compiled);

    private string _number;

    public AccountNumber(string number)
    {
        if (!NumberRegex.IsMatch(number))
            throw AccountNumberException.InvalidNumber(number);
        _number = number;
    }

    public override string ToString()
    {
        return _number;
    }
}