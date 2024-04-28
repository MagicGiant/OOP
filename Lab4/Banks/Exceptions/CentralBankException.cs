namespace Banks.Exceptions;

public class CentralBankException : Exception
{
    public CentralBankException(string message)
        : base(message)
    { }

    public static CentralBankException NumberExistException(string number)
    {
        return new CentralBankException($"This bank number \"{number}\" already exist");
    }

    public static CentralBankException FindBankException(string number)
    {
        return new CentralBankException($"Bank with number \"{number}\" doesn't exist");
    }

    public static CentralBankException TransactionException()
    {
        return new CentralBankException("Cash can't be low 0");
    }
}