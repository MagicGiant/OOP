namespace BusinessLayer.Entities;

public class Report
{
    public int MessageProcessed { get; set; }
    
    public int ViewedMessagesByTheDevice { get; set; }
    
    public int MessagesInThePeriod { get; set; }
}