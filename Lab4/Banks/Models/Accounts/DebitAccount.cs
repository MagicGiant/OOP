using Banks.Exceptions;

namespace Banks.Models;

public class DebitAccount : Account
{
    private decimal _cash = 0;

    public DebitAccount(AccountsData data, DateOnly realDate, AccountNumber number)
        : base(data, realDate, number)
    { }

    public override decimal Cash
    {
        get => _cash;
        set
        {
            if (value < 0)
                throw AccountsException.NegativeCacheInDebitAccountException();
            _cash = value;
        }
    }

    public override decimal CheckCashAfterTime(DateOnly dateOnly)
    {
        int monthNumber = Period.ByDateDifferenceMonth(dateOnly, RealDate);
        return Cash * (decimal)Math.Pow(1 + (Data.DebitRemainderPercent.Value / 100), monthNumber);
    }
}