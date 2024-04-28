using Banks.Entities;
using Banks.Models;
using Banks.Models.Datas;
using Newtonsoft.Json.Linq;

namespace BankConsole;
public class BankConsole
{
    private static JObject _jBankData;
    private static DateOnly _dateOnly;
    public static CentralBank CentralBank { get; } = new ();

    public static void RunConsole()
    {
        Console.WriteLine(_jBankData["commands"]["help"]);
        string commandData = string.Empty;
        while (commandData != "exit")
        {
            DateTime dateTime = DateTime.Today;
            _dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
            Console.WriteLine("#Menu");
            commandData = Console.ReadLine();
            List<string> commands = new (commandData.Split(' '));
            try
            {
                ParseCommands(commands);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

    public static void GetInfo(List<string> commands)
    {
        JObject jObject = new JObject(_jBankData);
        foreach (string command in commands)
        {
            string jData = string.Empty;
            if (ValidJson.IsExistCommand(jObject, command))
            {
                jData = jObject[command].ToString();
            }
            else
            {
                Console.WriteLine($"invalid command \"{command}\"");
            }

            if (ValidJson.IsValid(jData))
            {
                jObject = JObject.Parse(jData);
            }
            else
            {
                Console.WriteLine(jData);
                return;
            }
        }

        Console.WriteLine(jObject);
    }

    public static void CreateJBank(Bank bank)
    {
        var bankObject = new
        {
            bankData = new
            {
                commissionPercentage = Convert.ToString(bank.AccountsData.CommissionPercentage.Value),
                creditPercentage = Convert.ToString(bank.AccountsData.CreditPercentage.Value),
                depositMonthInaccessibility = Convert.ToString(bank.AccountsData.DepositMonthlyInaccessibility),
                debitRemainderPercent = Convert.ToString(bank.AccountsData.DebitRemainderPercent.Value),
                depositResidualPercentage = Convert.ToString(bank.AccountsData.DepositResidualPercentage.Value),
                depositCachePerMonthlyPercentage = Convert.ToString(bank.AccountsData.DepositCachePerMonthlyPercentage),
            },
            clients = new { },
        };
        JObject jBankNumbers = _jBankData["banks"] as JObject;
        jBankNumbers.Add(bank.BankNumber.ToString(), JObject.FromObject(bankObject));
    }

    public static void RemoveJBankInformation(BankNumber number)
    {
        ArgumentNullException.ThrowIfNull(number);
        if (!ValidJson.IsExistCommand(_jBankData["banks"] as JObject, number.ToString()))
            throw new Exception($"No found bank with number {number}");
        JObject banks = _jBankData["banks"] as JObject;
        banks.Remove(number.ToString());
    }

    public static void CreateBankAndJBank()
    {
        Console.WriteLine("#Bank creator:");
        AccountsDataBuilder dataBuilder = new ();
        Console.Write("Commission Percentage: ");
        dataBuilder.CommissionPercentage = new (Convert.ToDouble(Console.ReadLine()));
        Console.Write("Credit Percentage: ");
        dataBuilder.CreditPercentage = new (Convert.ToDouble(Console.ReadLine()));
        Console.Write("Debit Monthly Inaccessibility: ");
        dataBuilder.DebitMonthlyInaccessibility = Convert.ToInt32(Console.ReadLine());
        Console.Write("Debit Remainder Percent: ");
        dataBuilder.DebitRemainderPercent = new (Convert.ToDouble(Console.ReadLine()));
        Console.Write("Deposit Residual Percentage: ");
        dataBuilder.DepositResidualPercentage = new (Convert.ToDouble(Console.ReadLine()));
        Console.Write("Deposit Cache Per Monthly Percentage: ");
        dataBuilder.DepositCachePerMonthlyPercentage = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Bank Number: ");
        BankNumber number = new (Console.ReadLine());
        CreateJBank(CentralBank.CreateBank(dataBuilder.CreateAccountData(), number));
    }

    public static void CreateJClient(Client client, Bank bank)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(bank);
        var clientObject = new
        {
            passportData = new
            {
                name = $"{client.PassportData.Name.FirstName} {client.PassportData.Name.LastName}",
                address = $"{client.PassportData.Address.Street} {client.PassportData.Address.ApartmentNumber}",
                passportID = $"{client.PassportData.Id}",
            },
            accounts = new { },
        };
        JObject clients = _jBankData["banks"][bank.BankNumber.ToString()]["clients"] as JObject;
        clients.Add(client.PassportData.Id.ToString(), JObject.FromObject(clientObject));
    }

    public static void CreateClientAndJClient()
    {
        Console.WriteLine("#Client Creator:");

        Console.Write("Street: ");
        string street = Console.ReadLine();
        Console.Write("Apartment Number: ");
        Address address = new (street, Console.ReadLine());

        Console.Write("Passport ID: ");
        PassportID passportId = new (Console.ReadLine());

        Console.Write("Name: ");
        string name = Console.ReadLine();
        Console.Write("Last Name: ");
        ClientName clientName = new (name, Console.ReadLine());

        Console.Write("Bunk Number: ");
        BankNumber bankNumber = new (Console.ReadLine());

        Client client = new (new PassportData(address, passportId, clientName));
        CentralBank.GetBank(bankNumber).SetClient(client);
        CreateJClient(client, CentralBank.GetBank(bankNumber));
    }

    public static void RemoveClientAndJClient(List<string> commands)
    {
        Bank bank = CentralBank.GetBank(new BankNumber(commands.First()));
        commands.Remove(commands.First());
        bank.RemoveClient(new PassportID(commands.First()));

        JObject jClients = _jBankData["banks"][bank.BankNumber.ToString()]["clients"] as JObject;
        jClients.Remove(commands.First());
    }

    public static void CreateJAccount(Bank bank, PassportID id, Account account)
    {
        ArgumentNullException.ThrowIfNull(bank);
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(account);
        JObject jAccounts =
            (JObject)_jBankData["banks"][bank.BankNumber.ToString()]["clients"][id.ToString()]["accounts"];
        jAccounts.Add(account.Number.ToString(), JObject.FromObject(new { }));
        JObject jAccount = jAccounts[account.Number.ToString()] as JObject;
        jAccount.Add("cash", account.Cash);
    }

    public static void CreateAccountAndJAccount()
    {
        Console.WriteLine("#Account creator:");
        Console.Write("Bank number: ");
        Bank bank = CentralBank.GetBank(new BankNumber(Console.ReadLine()));
        Console.Write("Passport ID: ");
        Client client = bank.GetClient(new PassportID(Console.ReadLine()));
        Console.Write("Account number: ");
        AccountNumber number = new (Console.ReadLine());
        Console.Write("Account type: ");
        string accountType = new (Console.ReadLine());

        AccountFactory factory = new (bank.AccountsData);
        Account account;
        switch (accountType)
        {
            case "deposit":
                account = factory.CreateDepositAccount(_dateOnly, number);
                break;
            case "credit":
                account = factory.CreateCreditAccount(_dateOnly, number);
                break;
            case "debit":
                account = factory.CreateDebitAccount(_dateOnly, number);
                break;
            default:
                throw new Exception($"Invalid account type {accountType}");
        }

        bank.AddAccount(client.PassportData.Id, account);
        CreateJAccount(bank, client.PassportData.Id, account);
    }

    public static void RemoveAccountAndJAccount(List<string> commands)
    {
        BankNumber bankNumber = new BankNumber(commands.First());
        commands.Remove(commands.First());
        AccountNumber accountNumber = new AccountNumber(commands.First());
        Bank bank = CentralBank.GetBank(bankNumber);
        Client client = bank.Clients
            .FirstOrDefault(client => client.FindAccount(accountNumber) is not null);
        Account account = client.GetAccount(accountNumber);
        bank.RemoveAccount(client.PassportData, account);

        JObject jAccounts =
            (JObject)_jBankData["banks"][bank.BankNumber.ToString()]["clients"][client.ToString()]["accounts"];
        jAccounts.Remove(accountNumber.ToString());
    }

    public static void AddCashInAccountAndJAccount(List<string> commands)
    {
        BankNumber bankNumber = new (commands.First());
        commands.Remove(commands.First());
        AccountNumber accountNumber = new (commands.First());
        commands.Remove(commands.First());
        decimal cash = Convert.ToDecimal(commands.First());

        Client client = CentralBank.GetBank(bankNumber).Clients
            .FirstOrDefault(client => client.FindAccount(accountNumber) is not null);
        Account account = client.GetAccount(accountNumber);
        account.Cash += cash;

        JObject jAccount = (JObject)_jBankData["banks"][bankNumber.ToString()]["clients"][
            client.PassportData.Id.ToString()]["accounts"][accountNumber.ToString()];
        jAccount["cash"] = account.Cash;
    }

    public static void UpdateJAccount(BankNumber bankNumber, Client client, AccountNumber accountNumber)
    {
        JObject jAccount = (JObject)_jBankData["banks"][bankNumber.ToString()]["clients"][
            client.PassportData.Id.ToString()]["accounts"][accountNumber.ToString()];
        jAccount["cash"] = client.GetAccount(accountNumber).Cash;
    }

    public static void WithdrawMoney(List<string> commands)
    {
        BankNumber bankNumber = new (commands.First());
        commands.Remove(commands.First());
        AccountNumber accountNumber = new (commands.First());
        commands.Remove(commands.First());
        decimal cash = Convert.ToDecimal(commands.First());

        Client client = CentralBank.GetBank(bankNumber).Clients
            .FirstOrDefault(client => client.FindAccount(accountNumber) is not null);
        Account account = client.GetAccount(accountNumber);
        account.WithdrawMoney(cash);

        UpdateJAccount(bankNumber, client, accountNumber);
    }

    public static void TransferMoney(List<string> commands)
    {
        BankNumber firstBankNumber = new (commands.First());
        commands.Remove(commands.First());
        AccountNumber firstAccountNumber = new (commands.First());
        commands.Remove(commands.First());
        BankNumber secondBankNumber = new (commands.First());
        commands.Remove(commands.First());
        AccountNumber secondAccountNumber = new (commands.First());
        commands.Remove(commands.First());
        decimal cash = Convert.ToDecimal(commands.First());

        Bank firstBank = CentralBank.GetBank(firstBankNumber);
        Client firstClient = firstBank.Clients
            .First(client => client.FindAccount(firstAccountNumber) is not null);
        Account firstAccount = firstClient.GetAccount(firstAccountNumber);
        Bank secondBank = CentralBank.GetBank(secondBankNumber);
        Client secondClient = secondBank.Clients
            .First(client => client.FindAccount(secondAccountNumber) is not null);
        Account secondAccount = secondClient.GetAccount(secondAccountNumber);
        if (firstBankNumber == secondBankNumber)
            firstBank.AddTransaction(new TransactionBetweenClients(firstAccount, secondAccount, cash));
        else
            CentralBank.AddTransaction(new TransactionBetweenClients(firstAccount, secondAccount, cash));

        UpdateJAccount(firstBankNumber, firstClient, firstAccountNumber);
        UpdateJAccount(secondBankNumber, secondClient, secondAccountNumber);
    }

    public static void ParseCommands(List<string> commands)
    {
        string commandType = commands[0];
        commands.Remove(commandType);
        switch (commandType)
        {
            case "exit":
                return;
            case "help":
                Console.WriteLine(_jBankData["commands"]);
                break;
            case "getInfo":
                GetInfo(commands);
                break;
            case "createBank":
                CreateBankAndJBank();
                break;
            case "removeBank":
                CentralBank.RemoveBank(new BankNumber(commands.First()));
                RemoveJBankInformation(new BankNumber(commands.First()));
                break;
            case "createClient":
                CreateClientAndJClient();
                break;
            case "removeClient":
                RemoveClientAndJClient(commands);
                break;
            case "createAccount":
                CreateAccountAndJAccount();
                break;
            case "removeAccount":
                RemoveAccountAndJAccount(commands);
                break;
            case "addCash":
                AddCashInAccountAndJAccount(commands);
                break;
            case "withdrawMoney":
                WithdrawMoney(commands);
                break;
            case "transferMoney":
                TransferMoney(commands);
                break;
            default:
                Console.WriteLine("Invalid command");
                break;
        }
    }

    public static void Main()
    {
        DateTime dateTime = DateTime.Today;
        _dateOnly = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        _jBankData = JObject.Parse(File.ReadAllText(Path.GetFullPath("BanksData.json")));
        RunConsole();
    }
}
