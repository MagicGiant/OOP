namespace Backups.Exceptions;

public class PathException : Exception
{
    public PathException(string message)
        : base(message)
    { }

    public static PathException PathNameException(string name)
    {
        return new PathException($"Invalid path name ({name}) exception");
    }
}