using Banks.Exceptions;

namespace Banks.Models;

public class AddMoney : Transaction
{
    private Account _account;
    private decimal addedMoney;

    public AddMoney(Account account, decimal money)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (money < 0)
            throw TransactionException.NegativeCashException();
        _account.Cash += money;
        _account = account;
        addedMoney = money;
    }

    public override void CancelTransaction()
    {
        if (IsCanceled)
            throw TransactionException.CancelTransactionException();
        IsCanceled = true;
        _account.MoneyReturnCashStrategy(-addedMoney);
    }
}