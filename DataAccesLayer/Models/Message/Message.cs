namespace BusinessLayer.Account;

public class Message
{
    public int Id { get; set; }
    
    public DateTime DateOfCreation { get; set; }

    public Source Source { get; protected set; }
    
    public string Sender { get; protected set; }

    public bool Viewed { get; set; } = false;

    public bool Processed { get; set; } = false;
    
    public string Data { get; protected set; }
}