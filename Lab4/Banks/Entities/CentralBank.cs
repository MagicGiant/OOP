using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Datas;

namespace Banks.Entities;

public class CentralBank
{
    private Dictionary<string, Bank> _byBanksNumber = new ();
    private List<Transaction> _transactions = new ();
    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    public IReadOnlyCollection<Bank> Banks => _byBanksNumber.Values;

    public Bank CreateBank(AccountsData accountsData, BankNumber number)
    {
        ArgumentNullException.ThrowIfNull(accountsData);
        ArgumentNullException.ThrowIfNull(number);
        if (_byBanksNumber.ContainsKey(number.ToString()))
            throw CentralBankException.NumberExistException(number.ToString());
        Bank bank = new Bank(accountsData, number);
        _byBanksNumber[number.ToString()] = bank;
        return bank;
    }

    public Bank FindBank(BankNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!_byBanksNumber.ContainsKey(number.ToString()))
            return null;
        return _byBanksNumber[number.ToString()];
    }

    public Bank GetBank(BankNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        Bank bank = FindBank(number);
        if (bank is null)
            throw CentralBankException.FindBankException(number.ToString());
        return bank;
    }

    public void RemoveBank(BankNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!_byBanksNumber.ContainsKey(number.ToString()))
            throw CentralBankException.FindBankException(number.ToString());
        _byBanksNumber.Remove(number.ToString());
    }

    public void SetNewDate(BankNumber number, DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!_byBanksNumber.ContainsKey(number.ToString()))
            throw CentralBankException.FindBankException(number.ToString());
        foreach (Bank bank in _byBanksNumber.Values)
            bank.NotifyDateChanged(date);
    }

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}