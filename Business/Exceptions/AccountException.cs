namespace BusinessLayer.Exceptions;

public class AccountException : Exception
{
    public AccountException(string message)
        : base(message)
    { }

    public static AccountException AddSubordinateException()
    {
        return new AccountException("No access to add subordinates");
    }

    public static AccountException SetChiefException(string login)
    {
        return new AccountException($"Subordinate {login} can't be chief");
    }

    public static AccountException FindAccountException(string login)
    {
        return new AccountException($"Account with login {login} not exist");
    }

    public static AccountException FindMessageException(int id)
    {
        return new AccountException($"Can't find Exception with id '{id}'");
    }
}