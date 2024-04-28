namespace Backups.Exceptions;

public class RestorePointException : Exception
{
    public RestorePointException(string message)
        : base(message)
    { }

    public static RestorePointException TrackedException()
    {
        return new RestorePointException($"This object already tracked");
    }
}