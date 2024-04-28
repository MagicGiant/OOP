namespace Backups.Exceptions;

public class BackupException : Exception
{
    public BackupException(string message)
        : base(message)
    { }

    public static BackupException RemoveException(string name)
    {
        return new BackupException($"Can't remove not existing restore point with name ({name})");
    }

    public static BackupException GetException(string name)
    {
        return new BackupException($"Can't find object with name {name}");
    }
}