namespace Isu.Exceptions;

public class StudentNameException : Exception
{
    public StudentNameException(string groupName)
        : base($"Student has invalid name: {groupName}")
    { }
}