namespace Backups.Extra.Models;

public class ConsoleLogger : ILogger
{
    private List<string> _messages = new ();

    public IReadOnlyCollection<string> Messages => _messages;

    public override void AddLog(string message)
    {
        _messages.Add(message);
    }

    public override void OutLog()
    {
        foreach (string message in _messages)
            Console.WriteLine(message);
    }
}