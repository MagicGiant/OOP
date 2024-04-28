using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models;

public class PassportID
{
    public static readonly Regex PassportRegex = new ("^d{10}$", RegexOptions.Compiled);

    private string _id;

    public PassportID(string id)
    {
        if (!PassportRegex.IsMatch(id))
            PassportIDException.InvalidIdException();
        _id = id;
    }

    public override string ToString()
    {
        return _id;
    }
}