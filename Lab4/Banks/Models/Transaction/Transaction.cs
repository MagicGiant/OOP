namespace Banks.Models;

public abstract class Transaction
{
    public bool IsCanceled { get; internal set; } = false;
    public abstract void CancelTransaction();
}