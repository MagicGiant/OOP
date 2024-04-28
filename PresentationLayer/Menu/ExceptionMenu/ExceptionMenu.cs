using Spectre.Console;

namespace PresentationLayer.Menu;

class ExceptionMenu : IMenu
{
    private Exception _exception;
    
    public ExceptionMenu(Exception exception)
    {
        _exception = exception;
    }

    public void RunMenu()
    {
        string message = _exception.Message;
        ExceptionMenuMode mode = AnsiConsole.Prompt(
            new SelectionPrompt<ExceptionMenuMode>()
                .Title($"[yellow]Error: {message}[/]")
                .AddChoices(ExceptionMenuMode.Exit));
    }
}

