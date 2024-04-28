using BusinessLayer.Entities;
using Spectre.Console;

namespace PresentationLayer.Menu;

public class AccountMenu : IMenu
{
    private Account _account;

    public AccountMenu(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _account = account;
    }
    
    public void RunMenu()
    {
        AccountMenuMode mode = new ();

        while (mode != AccountMenuMode.Exit)
        {
            mode = AnsiConsole.Prompt(
                new SelectionPrompt<AccountMenuMode>()
                    .Title($"[green]AccountMenu ({_account}):[/]")
                    .AddChoices(AccountMenuMode.MessageMenu
                        ,AccountMenuMode.Exit));

            switch (mode)
            {
                case AccountMenuMode.MessageMenu:
                    SendMessage();
                    break;
            }
        }
        
    }

    public void SendMessage()
    {
        try
        {
            
        }
        catch (Exception exception)
        {
            ExceptionMenu menu = new (exception);
            menu.RunMenu();
        }
    }
}