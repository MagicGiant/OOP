using System.Text.RegularExpressions;
using DataAccessLayer.Exceptions;

namespace BusinessLayer.Account;

public class SMS : Message
{
    public static readonly Regex Regex = new("^[+][0-9]{11}$", RegexOptions.Compiled);
    
    public SMS(string PhoneNumber, string data)
    {
        if (!Regex.IsMatch(PhoneNumber))
            throw  MessageException.InvalidPhoneNumber(PhoneNumber);
        Sender = PhoneNumber;
        Source = Source.Sms;
        Data = data;
    }
}