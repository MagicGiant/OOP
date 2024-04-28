namespace BusinessLayer.Account;

public class AccountData
{
    public string PasswodHash { get; set; }
    
    public string Login { get; set; }
    
    public int Id { get; set; }

    public string BossLogin { get; set; }

    public List<Subordinate> Subordinates { get; set; }

    public List<Message> SentMessages { get; set; }

    public List<Message> ReceivedMessages { get; set; }
}