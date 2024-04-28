using System.ComponentModel.Design;
using BusinessLayer.Account;
using BusinessLayer.Entities;
using Spectre.Console;

namespace PresentationLayer.Menu;

public class MessageMenu : IMenu
{
    private Account _account;

    public MessageMenu(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _account = account;
    }


    public void RunMenu()
    {
        MessageMenuMode mode = new ();
        while (mode != MessageMenuMode.Exit)
        {
            mode = AnsiConsole.Prompt(
                new SelectionPrompt<MessageMenuMode>()
                    .Title($"[green]MessageMenu ({_account}):[/]")
                    .AddChoices(MessageMenuMode.SendMessage,
                        MessageMenuMode.ViewMessage,
                        MessageMenuMode.GetAllReceivedMessages,
                        MessageMenuMode.GetAllSenderMessages));

            switch (mode)
            {
                case MessageMenuMode.GetAllReceivedMessages:
                    GetAllReceivedMessage();
                    break;
                case MessageMenuMode.SendMessage:
                    SendMessage();
                    break;
                case MessageMenuMode.GetAllSenderMessages:
                    break;
            }
        }
    }

    public void SendMessage()
    {
        Source source = AnsiConsole.Prompt(new SelectionPrompt<Source>()
            .Title($"[green]TypeOfSource:[/]")
            .AddChoices(Source.Email,
                Source.Messenger,
                Source.Sms));

        Message message;
        string data= 
            AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter text of message[/])"));
        switch (source)
        {
            case Source.Messenger:
                message = new Messenger(_account.Data.Login, data);
                message.DateOfCreation = DateTime.Now;
                break;
            case Source.Email:
                string email = 
                    AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter your email[/])"));
                message = new MailMassage(email, data);
                message.DateOfCreation = DateTime.Now;
                break;
            case Source.Sms:
                string phoneNumber =
                    AnsiConsole.Prompt(new TextPrompt<string>("[blue]Enter your phone number[/])"));
                message = new SMS(phoneNumber, data);
                message.DateOfCreation = DateTime.Now;
                break;
        }
    }
    
    public void GetAllReceivedMessage()
    {
        Table table = new Table();
        table.AddColumn("[green]Message ID[/]");
        table.AddColumn("[green]Sender login[/]");
        table.AddColumn("[green]Processing progress[/]");
        table.AddColumn("[green]Date of send[/]");
        table.AddColumn("[green]Source[/]");

        foreach (var message in _account.Data.ReceivedMessages)
        {
            string progress = string.Empty;
            if (!message.Viewed)
                progress = "Not viewed";
            if (message.Viewed)
                progress = "Viewed";
            if (message.Processed)
                progress = "Processed";

            table.AddRow(message.Id.ToString()
                , message.Sender
                , progress
                , message.Data
                , message.Source.ToString());
        }
    }
}