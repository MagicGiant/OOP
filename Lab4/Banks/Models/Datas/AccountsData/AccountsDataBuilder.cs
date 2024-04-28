using Banks.Entities;

namespace Banks.Models;

public class AccountsDataBuilder
{
    private Percent _commissionPercentage;

    private Percent _creditPercentage;

    private Percent _debitRemainderPercent;

    private Percent _depositResidualPercentage;

    private decimal _depositCachePerMonthlyPercentage;

    private int _debitMonthlyInaccessibility;

    public Percent CommissionPercentage
    {
        get => _commissionPercentage;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _commissionPercentage = value;
        }
    }

    public Percent CreditPercentage
    {
        get => _creditPercentage;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _creditPercentage = value;
        }
    }

    public Percent DebitRemainderPercent
    {
        get => _debitRemainderPercent;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _debitRemainderPercent = value;
        }
    }

    public Percent DepositResidualPercentage
    {
        get => _depositResidualPercentage;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _depositResidualPercentage = value;
        }
    }

    public decimal DepositCachePerMonthlyPercentage
    {
        get => _depositCachePerMonthlyPercentage;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _depositCachePerMonthlyPercentage = value;
        }
    }

    public int DebitMonthlyInaccessibility
    {
        get => _debitMonthlyInaccessibility;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _debitMonthlyInaccessibility = value;
        }
    }

    public AccountsData CreateAccountData()
    {
        AccountsData accountsData = new ();
        accountsData.CommissionPercentage = _commissionPercentage;
        accountsData.CreditPercentage = CreditPercentage;
        accountsData.DebitRemainderPercent = CreditPercentage;
        accountsData.DepositMonthlyInaccessibility = DebitMonthlyInaccessibility;
        accountsData.DepositResidualPercentage = DepositResidualPercentage;
        accountsData.DepositCachePerMonthlyPercentage = DepositCachePerMonthlyPercentage;
        return accountsData;
    }
}