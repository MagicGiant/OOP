namespace BusinessLayer.Exceptions;

public class AccountManagerException : Exception
{
    public AccountManagerException(string message)
        : base(message)
    { }
    
    public static AccountManagerException PasswordException()
    {
        return new AccountManagerException("Wrong password");
    }

    public static AccountManagerException LoginException()
    {
        return new AccountManagerException("Wrong login");
    }
    
    public static AccountManagerException LoginTakingException(string login)
    {
        return new AccountManagerException($"This login '{login}' already taken");
    }
}