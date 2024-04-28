using BusinessLayer.Account;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities;

public class ReportBuilder
{
    public AccountData AccountData;

    public ReportBuilder(AccountData accountData)
    {
        ArgumentNullException.ThrowIfNull(accountData);
        AccountData = accountData;
    }
    
    public int MessageProcessed { get; set; }
    
    public int ViewedMessagesByTheDevice { get; set; }
    
    public int MessagesInThePeriod { get; set; }

    public void AddMessageProcessed()
    {
        foreach (Message message in AccountData.SentMessages)
        {
            if (message.Processed)
                MessageProcessed++;
        }
    }

    public void AddMessagesInPeriod(DateTime startPeriod, DateTime endPeriod)
    {
        if (startPeriod > endPeriod)
            throw ReportBuilderException.PeriodException();
        foreach (Message message in AccountData.SentMessages)
        {
            if (message.DateOfCreation >= startPeriod && message.DateOfCreation <= endPeriod)
                MessagesInThePeriod++;
        }
    }

    public void AddViewedMessagesByTheDevice(Source source)
    {
        foreach (Message message in AccountData.SentMessages)
        {
            if (message.Source == source && message.Viewed)
                ViewedMessagesByTheDevice++;
        }
    }

    public Report CreateReport()
    {
        Report report = new ();
        report.MessageProcessed = MessageProcessed;
        report.MessagesInThePeriod = MessagesInThePeriod;
        report.ViewedMessagesByTheDevice = ViewedMessagesByTheDevice;
        return report;
    }
}