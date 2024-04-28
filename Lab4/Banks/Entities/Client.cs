using Banks.Exceptions;
using Banks.Models;

namespace Banks.Entities;

public class Client
{
    private Dictionary<string, Account> _byAccountsNumber = new ();

    public Client(PassportData passportData)
    {
        ArgumentNullException.ThrowIfNull(passportData);
        PassportData = passportData;
    }

    public IReadOnlyCollection<Account> Accounts => _byAccountsNumber.Values;

    public PassportData PassportData { get; }

    public Account GetAccount(AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!_byAccountsNumber.ContainsKey(number.ToString()))
            throw ClientException.AccountKeyException(number.ToString());
        return _byAccountsNumber[number.ToString()];
    }

    public Account FindAccount(AccountNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!_byAccountsNumber.ContainsKey(number.ToString()))
            return null;
        return _byAccountsNumber[number.ToString()];
    }

    public void SetAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (_byAccountsNumber.ContainsKey(account.ToString()))
            throw ClientException.AccountKeyExistException(account.Number.ToString());
        _byAccountsNumber[account.Number.ToString()] = account;
    }

    public void RemoveAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        if (!account.CanRemoved())
            throw BankException.RemoveAccountWithCashException();
        _byAccountsNumber.Remove(account.Number.ToString());
    }

    public void NotifyDateChanged(DateOnly date)
    {
        foreach (Account account in _byAccountsNumber.Values)
            account.SetDate(date);
    }

    public override string ToString()
    {
        return PassportData.ToString();
    }
}