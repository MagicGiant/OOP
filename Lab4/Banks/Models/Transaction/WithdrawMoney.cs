using Banks.Exceptions;

namespace Banks.Models;

public class WithdrawMoney : Transaction
{
    private Account _account;
    private decimal withdrownMoney;

    public WithdrawMoney(Account account, decimal money)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (money < 0)
            throw TransactionException.NegativeCashException();
        account.WithdrawMoney(money);
        _account = account;
        withdrownMoney = money;
    }

    public override void CancelTransaction()
    {
        if (IsCanceled)
            throw TransactionException.CancelTransactionException();
        IsCanceled = true;
        _account.MoneyReturnCashStrategy(withdrownMoney);
    }
}