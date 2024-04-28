using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class DepositAccount : Account
{
    private DateOnly _dateInvestment;

    private decimal _cash;

    public DepositAccount(AccountsData data, DateOnly realDate, AccountNumber number)
        : base(data, realDate, number)
    { }

    public Percent ByMoneyPercent { get; internal set; }

    public override decimal Cash
    {
        get => _cash;
        set
        {
            if (value < _cash)
            {
                if (!CanTakeMoney())
                    throw AccountsException.DepositSetterException();
            }
            else
            {
                _dateInvestment = RealDate;
                ByMoneyPercent = new ((double)(value / Data.DepositCachePerMonthlyPercentage) *
                                     Data.DepositResidualPercentage.Value);
            }

            _cash = value;
        }
    }

    public override decimal CheckCashAfterTime(DateOnly dateOnly)
    {
        int monthNumber = Period.ByDateDifferenceMonth(dateOnly, _dateInvestment);
        return Cash * (decimal)Math.Pow(1 + (ByMoneyPercent.Value / 100), monthNumber);
    }

    public bool CanTakeMoney()
    {
        return Data.DepositMonthlyInaccessibility <= Period.ByDateDifferenceMonth(_dateInvestment, RealDate);
    }
}