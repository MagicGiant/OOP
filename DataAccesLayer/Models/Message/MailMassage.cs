using System.Text.RegularExpressions;
using DataAccessLayer.Exceptions;

namespace BusinessLayer.Account;

public class MailMassage : Message
{
    public static readonly Regex Regex = 
        new("^.{1,40}@[a-z]{4}[.][a-z]{1,5}$", RegexOptions.Compiled);

    public MailMassage(string mailAddress, string data)
    {
        if (Regex.IsMatch(mailAddress))
            throw MessageException.InvalidEmailAddress(mailAddress);
        Sender = mailAddress;
        Source = Source.Email;
        Data = data;
    }
}