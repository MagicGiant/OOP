namespace Banks.Exceptions;

public class AccountsException : Exception
{
    public AccountsException(string message)
        : base(message)
    { }

    public static AccountsException NegativeCacheInDebitAccountException()
    {
        return new AccountsException("Cash can't be low 0");
    }

    public static AccountsException DepositSetterException()
    {
        return new AccountsException("The client cannot take the money during the contract period");
    }
}