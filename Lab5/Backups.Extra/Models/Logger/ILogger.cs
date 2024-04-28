namespace Backups.Extra.Models;

public abstract class ILogger
{
    public abstract void AddLog(string message);
    public abstract void OutLog();
}