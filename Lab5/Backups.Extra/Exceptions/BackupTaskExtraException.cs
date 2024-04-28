namespace Backups.Extra.Exceptions;

public class BackupTaskExtraException : Exception
{
    public BackupTaskExtraException(string message)
        : base(message)
    { }

    public static BackupTaskExtraException DirectoryExistException(string path)
    {
        return new BackupTaskExtraException($"No exist directory \"{path}\"");
    }
}