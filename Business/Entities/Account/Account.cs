using BusinessLayer.Account;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities;

public class Account
{
    public AccountData Data { get;}
    
    public Account(AccountData data)
    {
        ArgumentNullException.ThrowIfNull(data);
        Data = data;
    }

    public void SetChief(string login)
    {
        if (Data.Subordinates.Exists(subordinate => subordinate.StrSubordinate == login))
            throw AccountException.SetChiefException(login);

        AccountData newChiefData = findData(login);

        if (newChiefData is null)
            throw AccountException.FindAccountException(login);
        if (!string.IsNullOrEmpty(Data.BossLogin))
        {
            AccountData chiefData = findData(Data.BossLogin);
            Subordinate subordinate = chiefData.Subordinates
                .Find(subordinate => subordinate.StrSubordinate == Data.Login);
            chiefData.Subordinates.Remove(subordinate);
        }
        
        newChiefData.Subordinates.Add(new (){StrSubordinate = Data.Login});
        Data.BossLogin = login;
    }

    public void SendMessage(Message message, string sendingLogin)
    {
        AccountData sendingData = findData(sendingLogin);
        sendingData.ReceivedMessages.Add(message);
        Data.SentMessages.Add(message);
        
        using (DataBaseContext context = new())
            context.SaveChanges();
    }

    public void MarkMessageAsViewed(int messageId)
    {
        Message message = findMessage(messageId);
        if (message is null)
            throw AccountException.FindMessageException(messageId);
        message.Viewed = true;
        
        using (DataBaseContext context = new())
            context.SaveChanges();
    }

    public void MarkMessageAsProcessed(int messageId)
    {
        Message message = findMessage(messageId);
        if (message is null)
            throw AccountException.FindMessageException(messageId);
        message.Viewed = true;
        message.Processed = true;
        
        using (DataBaseContext context = new())
            context.SaveChanges();
    }

    private AccountData findData(string login)
    {
        using (DataBaseContext context = new())
        {
            return context.AccountsData
                .ToList()
                .Find(data => data.Login == login);
        }
    }

    private Message findMessage(int messageId)
    {
        return Data.ReceivedMessages.FirstOrDefault(message => message.Id == messageId);
    }
}