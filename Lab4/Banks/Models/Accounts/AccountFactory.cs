using Banks.Exceptions;

namespace Banks.Models;

public class AccountFactory
{
    private AccountsData _accountsData;

    private List<AccountNumber> _accountNumbers = new ();

    public AccountFactory(AccountsData accountsData)
    {
        ArgumentNullException.ThrowIfNull(accountsData);
        AccountsData = accountsData;
    }

    public AccountsData AccountsData
    {
        get => _accountsData;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _accountsData = value;
        }
    }

    public CreditAccount CreateCreditAccount(DateOnly realDate, AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (_accountNumbers.Contains(number))
            throw AccountFactoryException.ExistingNumberException(number.ToString());
        return new CreditAccount(_accountsData, realDate, number);
    }

    public DebitAccount CreateDebitAccount(DateOnly realDate, AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (_accountNumbers.Contains(number))
            throw AccountFactoryException.ExistingNumberException(number.ToString());
        return new DebitAccount(_accountsData, realDate, number);
    }

    public DepositAccount CreateDepositAccount(DateOnly realDate, AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (_accountNumbers.Contains(number))
            throw AccountFactoryException.ExistingNumberException(number.ToString());
        return new DepositAccount(_accountsData, realDate, number);
    }
}
