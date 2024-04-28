using Banks.Exceptions;

namespace Banks.Models;

public class TransactionBetweenClients : Transaction
{
    private Account _sendingAccount;
    private Account _receivedAccount;
    private decimal _money;

    public TransactionBetweenClients(Account sendingAccount, Account receivedAccount, decimal money)
    {
        ArgumentNullException.ThrowIfNull(sendingAccount);
        ArgumentNullException.ThrowIfNull(receivedAccount);
        if (money < 0)
            throw TransactionException.NegativeCashException();
        sendingAccount.WithdrawMoney(money);
        receivedAccount.Cash += money;
        _money = money;
        _sendingAccount = sendingAccount;
        _receivedAccount = receivedAccount;
    }

    public override void CancelTransaction()
    {
        if (IsCanceled)
            throw TransactionException.CancelTransactionException();
        IsCanceled = true;
        _receivedAccount.MoneyReturnCashStrategy(-_money);
        _sendingAccount.MoneyReturnCashStrategy(_money);
    }
}