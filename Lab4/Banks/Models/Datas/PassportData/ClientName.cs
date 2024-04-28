using System.Text.RegularExpressions;
using Banks.Exceptions;

namespace Banks.Models;

public class ClientName
{
    public static readonly Regex NameRegex = new ("^[A-Z][a-z]{3,20}$");

    public ClientName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName))
            throw new ArgumentNullException(nameof(firstName));
        if (string.IsNullOrEmpty(lastName))
            throw new ArgumentNullException(nameof(lastName));
        if (!NameRegex.IsMatch(firstName))
            throw ClientNameException.InvalidNameException(firstName);
        if (!NameRegex.IsMatch(lastName))
            throw ClientNameException.InvalidNameException(lastName);
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }

    public string LastName { get; }
}