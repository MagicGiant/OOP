using System.Transactions;
using Banks.Exceptions;
using Banks.Models;
using Banks.Models.Datas;
using Transaction = Banks.Models.Transaction;

namespace Banks.Entities;

public class Bank
{
    private Dictionary<string, Client> _byClientsPassportId = new ();

    private AccountsData _accountsData;
    private List<Transaction> _transactions = new ();
    public Bank(AccountsData accountsData, BankNumber bankNumber)
    {
        ArgumentNullException.ThrowIfNull(accountsData);
        ArgumentNullException.ThrowIfNull(bankNumber);
        _accountsData = accountsData;
        BankNumber = bankNumber;
    }

    public IReadOnlyCollection<Client> Clients => _byClientsPassportId.Values;

    public IReadOnlyCollection<Transaction> Transactions => _transactions;
    public BankNumber BankNumber { get; }

    public AccountsData AccountsData
    {
        get => _accountsData;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _accountsData = value;
        }
    }

    public IReadOnlyCollection<Account> GetAccounts(PassportID passportId)
    {
        ArgumentNullException.ThrowIfNull(passportId);
        if (!_byClientsPassportId.ContainsKey(passportId.ToString()))
            throw BankException.AccountException(passportId.ToString());
        return _byClientsPassportId[passportId.ToString()].Accounts;
    }

    public void AddTransaction(Transaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);
        _transactions.Add(transaction);
    }

    public void AddAccount(PassportID passportID, Account account)
    {
        ArgumentNullException.ThrowIfNull(passportID);
        ArgumentNullException.ThrowIfNull(account);
        if (!_byClientsPassportId.ContainsKey(passportID.ToString()))
            throw BankException.AccountException(passportID.ToString());
        _byClientsPassportId[passportID.ToString()].SetAccount(account);
    }

    public void RemoveAccount(PassportData passportData, Account account)
    {
        if (!_byClientsPassportId.ContainsKey(passportData.Id.ToString()))
            throw BankException.AccountException(passportData.Id.ToString());
        _byClientsPassportId[passportData.Id.ToString()].RemoveAccount(account);
    }

    public Client FindClient(PassportID passportId)
    {
        ArgumentNullException.ThrowIfNull(passportId);
        if (!_byClientsPassportId.ContainsKey(passportId.ToString()))
            return null;
        return _byClientsPassportId[passportId.ToString()];
    }

    public Client GetClient(PassportID passportId)
    {
        ArgumentNullException.ThrowIfNull(passportId);
        Client client = FindClient(passportId);
        if (client is null)
            throw BankException.FindClientException(passportId.ToString());
        return client;
    }

    public void SetClient(Client client)
    {
        ArgumentNullException.ThrowIfNull(client);
        if (_byClientsPassportId.ContainsKey(client.ToString()))
            throw BankException.SetClientExistingException(client.ToString());
        _byClientsPassportId[client.PassportData.Id.ToString()] = client;
    }

    public void RemoveClient(PassportID passportId)
    {
        ArgumentNullException.ThrowIfNull(passportId);
        if (!_byClientsPassportId.ContainsKey(passportId.ToString()))
            throw BankException.RemoveClientException(passportId.ToString());
        _byClientsPassportId.Remove(passportId.ToString());
    }

    public void NotifyDateChanged(DateOnly date)
    {
        foreach (Client client in _byClientsPassportId.Values)
            client.NotifyDateChanged(date);
    }
}