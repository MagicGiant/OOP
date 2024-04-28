namespace BusinessLayer.Account;

public class Messenger : Message
{
    public Messenger(string login, string data)
    {
        Sender = login;
        Data = data;
        Source = Source.Messenger;
    }
}