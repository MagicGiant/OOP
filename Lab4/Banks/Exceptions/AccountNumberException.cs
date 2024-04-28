namespace Banks.Exceptions;

public class AccountNumberException : Exception
{
    public AccountNumberException(string message)
        : base(message)
    { }

    public static AccountNumberException InvalidNumber(string number)
    {
        return new AccountNumberException(
            $"Account number must have 12 number. Now is \"{number}\" (length = {number.Length})");
    }
}