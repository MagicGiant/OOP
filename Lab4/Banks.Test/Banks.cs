using Banks.Entities;
using Banks.Models;
using Banks.Models.Datas;
using Xunit;
using Xunit.Abstractions;

namespace Banks.Test;

public class Banks
{
    private ITestOutputHelper _output;
    private CentralBank _centralBank = new ();

    public Banks(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void OpenAndSetCashInDepositAccount_CheckMoneyAfterMonth()
    {
        AccountsData accountsData = CreateAccountsData();
        BankNumber bankNumber = new ("1000");
        Bank bank = _centralBank.CreateBank(accountsData, bankNumber);

        PassportData passportData = CreatePassportData();
        Client client = new (passportData);

        AccountFactory factory = new (accountsData);
        AccountNumber accountNumber = new ("123456789012");
        DebitAccount debitAccount = factory.CreateDebitAccount(new DateOnly(2000, 1, 1), accountNumber);

        bank.SetClient(client);
        bank.AddAccount(passportData.Id, debitAccount);
        debitAccount.Cash += 1000;
        bank.AddTransaction(new WithdrawMoney(debitAccount, 20));
        Assert.True(debitAccount.Cash == 978);
    }

    private AccountsData CreateAccountsData()
    {
        AccountsDataBuilder accountsDataBuilder = new ();
        accountsDataBuilder.CommissionPercentage = new Percent(10);
        accountsDataBuilder.CreditPercentage = new (10);
        accountsDataBuilder.DebitMonthlyInaccessibility = 2;
        accountsDataBuilder.DebitRemainderPercent = new (5);
        accountsDataBuilder.DepositResidualPercentage = new (5);
        accountsDataBuilder.DepositCachePerMonthlyPercentage = 1000;
        return accountsDataBuilder.CreateAccountData();
    }

    private PassportData CreatePassportData()
    {
        Address address = new ("Nevski", "100");
        ClientName name = new ("Pasha", "Pashkin");
        PassportID id = new ("1234576890");
        return new (address, id, name);
    }
}