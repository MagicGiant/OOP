namespace DataAccessLayer.Exceptions;

public class MessageException : Exception
{
    public MessageException(string message)
        : base(message)
    { }

    public static MessageException InvalidPhoneNumber (string number)
    {
        return new MessageException($"Invalid phone number '{number}'");
    }
    
    public static MessageException InvalidEmailAddress (string address)
    {
        return new MessageException($"Invalid email address'{address}'");
    }
}