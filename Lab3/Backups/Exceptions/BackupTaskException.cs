namespace Backups.Exceptions;

public class BackupTaskException : Exception
{
    public BackupTaskException(string message)
        : base()
    { }

    public static BackupTaskException CreatePointWithExistingNameException(string name)
    {
        return new BackupTaskException($"Restore Point '{name}' already exist");
    }

    public static BackupTaskException RemoveException(string name)
    {
        return new BackupTaskException($"Can't remove not existing restore point with name ({name})");
    }
}