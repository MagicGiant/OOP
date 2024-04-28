namespace Isu.Exceptions;

public class IsuServiceExtraException : Exception
{
    public IsuServiceExtraException(string message)
        : base(message)
    { }

    public static IsuServiceExtraException SetOgnpWithNonExceptStudentException()
    {
        return new IsuServiceExtraException("Injected student must to be present in injected group");
    }

    public static IsuServiceExtraException FindGroupException()
    {
        return new IsuServiceExtraException($"Not found group");
    }
}