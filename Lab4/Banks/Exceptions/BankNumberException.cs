using Banks.Models.Datas;

namespace Banks.Exceptions;

public class BankNumberException : Exception
{
    public BankNumberException(string message)
        : base(message)
    { }

    public static BankNumberException InvalidNumberException(string name)
    {
        return new BankNumberException($"Invalid bank number \"{name}\"");
    }
}