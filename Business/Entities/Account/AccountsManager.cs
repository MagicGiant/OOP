using BusinessLayer.Account;
using BusinessLayer.Exceptions;
using BusinessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Entities;

public static class AccountsManager
{
    public static Account GetAccount(string login, string password)
    {
        using (DataBaseContext context = new ())
        {
            AccountData data = context.AccountsData.FirstOrDefault(account => account.Login == login);
            if (data is null)
                throw AccountManagerException.LoginException();
            
            if (data.PasswodHash != PasswordHasher.GetHash(password))
                throw AccountManagerException.PasswordException();
    
            return new Account(data);
        }
        
    }

    public static Account CreateAccount(string login, string password)
    {
        using (DataBaseContext context = new())
        {
            if (context.AccountsData.ToList().Exists(account => account.Login == login))
                throw AccountManagerException.LoginTakingException(login);
            AccountData data = new ();
            data.Login = login;
            data.PasswodHash = PasswordHasher.GetHash(password);
            data.BossLogin = "NoBoss";
            context.AccountsData.Add(data);

            context.SaveChanges();
            
            return new Account(data);
        }
    }
}