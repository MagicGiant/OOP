using Backups.Entities;

namespace Backups.Exceptions;

public class BackupObjectException : Exception
{
    public BackupObjectException(string message)
        : base(message)
    { }

    public static BackupObjectException ExistException(string path)
    {
        return new BackupObjectException($"File or directory '{path}' doesn't exist");
    }

    public static BackupObjectException ExistFileException(string path)
    {
        return new BackupObjectException($"File '{path}' doesn't exist");
    }

    public static BackupObjectException ExistDirectoryException(string path)
    {
        return new BackupObjectException($"Directory '{path}' doesn't exist");
    }

    public static BackupObjectException SamePathDirectoryAndFile(string path)
    {
        return new BackupObjectException("Directory and File have same path");
    }
}