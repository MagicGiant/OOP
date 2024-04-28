using System.Diagnostics;

namespace Banks.Exceptions;

public class TransactionException : Exception
{
    public TransactionException(string message)
        : base(message)
    { }

    public static TransactionException NegativeCashException()
    {
        return new TransactionException("Cash can't be low 0");
    }

    public static TransactionException CancelTransactionException()
    {
        return new TransactionException("Can't cancel canceled transaction");
    }
}