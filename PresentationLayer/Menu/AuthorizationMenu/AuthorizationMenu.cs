using BusinessLayer.Account;
using BusinessLayer.Entities;
using Spectre.Console;

namespace PresentationLayer.Menu;

public class AuthorizationMenu : IMenu
{
    public void RunMenu()
    {
        AuthorizationMenuMode menuMode = new ();

        while (menuMode != AuthorizationMenuMode.Exit)
        {
            menuMode = AnsiConsole.Prompt(
                new SelectionPrompt<AuthorizationMenuMode>()
                    .Title("[green]Authorization:[/]")
                    .AddChoices(AuthorizationMenuMode.Login,
                        AuthorizationMenuMode.Registration,
                        AuthorizationMenuMode.GetAllLogins,
                        AuthorizationMenuMode.Exit));

            switch (menuMode)
            {
                case AuthorizationMenuMode.Login:
                    Login();
                    break;
                case AuthorizationMenuMode.Registration:
                    Registration();
                    break;
                case AuthorizationMenuMode.GetAllLogins:
                    GetAllLogins();
                    break;
            }
        }
    }

    private void Login()
    {
        string login = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter login:[/]"));
        string password = AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter password:[/]").Secret());

        try
        {
            AccountMenu menu = new(AccountsManager.GetAccount(login, password));
            menu.RunMenu();
        }
        catch(Exception exception)
        {
            ExceptionMenu menu = new(exception);
            menu.RunMenu();
        }
    }

    private void Registration()
    {
        string login = AnsiConsole.Prompt(new TextPrompt<string>("Create login:"));
        string password = AnsiConsole.Prompt(new TextPrompt<string>("Create password:").Secret());

        try
        {
            AccountMenu menu = new(AccountsManager.CreateAccount(login, password));
            menu.RunMenu();
        }
        catch(Exception exception)
        {
            ExceptionMenu menu = new (exception);
            menu.RunMenu();;
        }
    }

    private void GetAllLogins()
    {
        Table table = new ();
        table.AddColumn(new TableColumn("[green]Id[/]"));
        table.AddColumn(new TableColumn("[green]Logins[/]"));

        using (DataBaseContext context = new())
        {
            foreach (AccountData accountData in context.AccountsData)
                table.AddRow(accountData.Id.ToString() ,accountData.Login);
        }
        
        AnsiConsole.Write(table);
    }
}