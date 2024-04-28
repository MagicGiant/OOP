namespace Isu.Exceptions;

public class UsingGroupNameException : Exception
{
    public UsingGroupNameException(string name)
        : base($"This name ({name}) was using")
    { }
}