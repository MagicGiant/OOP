namespace Banks.Models;

public abstract class Account
{
    public Account(AccountsData data, DateOnly realDate, AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(realDate);
        RealDate = realDate;
        Number = number;
        Data = data;
    }

    public AccountsData Data { get; }

    public virtual decimal Cash { get; set; }

    public DateOnly RealDate { get; set; }

    public AccountNumber Number { get; }

    public virtual void MoneyReturnCashStrategy(decimal returnedCash)
    {
        Cash += Math.Abs((decimal)Data.CommissionPercentage.PercentageOfNumber((double)returnedCash)) +
                returnedCash;
    }

    public abstract decimal CheckCashAfterTime(DateOnly dateOnly);

    public virtual bool CanRemoved()
    {
        return Cash == 0;
    }

    public void SetDate(DateOnly dateOnly)
    {
        Cash = CheckCashAfterTime(dateOnly);
        RealDate = dateOnly;
    }

    public void WithdrawMoney(decimal cash)
    {
        Cash -= (decimal)Data.CommissionPercentage.PercentageOfNumber((double)cash) + cash;
    }
}