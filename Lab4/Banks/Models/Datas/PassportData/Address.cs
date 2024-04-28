using System.Text.RegularExpressions;

namespace Banks.Models;

public class Address
{
    public Address(string street, string apartmentNumber)
    {
        if (string.IsNullOrEmpty(street))
            throw new ArgumentNullException(nameof(street));
        if (string.IsNullOrEmpty(apartmentNumber))
            throw new ArgumentNullException(apartmentNumber);
        Street = street;
        ApartmentNumber = apartmentNumber;
    }

    public string Street { get; }

    public string ApartmentNumber { get; }
}