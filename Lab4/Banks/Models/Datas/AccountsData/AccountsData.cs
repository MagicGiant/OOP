using Banks.Entities;

namespace Banks.Models;

public class AccountsData
{
    public Percent CommissionPercentage { get; set; }

    public Percent CreditPercentage { get; set; }

    public Percent DebitRemainderPercent { get; set; }
    public Percent DepositResidualPercentage { get; set; }

    public decimal DepositCachePerMonthlyPercentage { get; set; }

    public int DepositMonthlyInaccessibility { get; set; }
}