namespace Banks.Models;

public class CreditAccount : Account
{
    public CreditAccount(AccountsData data, DateOnly realDate, AccountNumber number)
        : base(data, realDate, number)
    { }

    public override decimal CheckCashAfterTime(DateOnly dateOnly)
    {
        int monthNumber = Period.ByDateDifferenceMonth(dateOnly, RealDate);
        if (Cash < 0)
            return Cash * (decimal)Math.Pow(1 + (Data.CreditPercentage.Value / 100), monthNumber);
        return Cash;
    }
}