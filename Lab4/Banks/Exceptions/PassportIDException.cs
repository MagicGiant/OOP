namespace Banks.Exceptions;

public class PassportIDException : Exception
{
    public PassportIDException(string message)
        : base(message)
    { }

    public static PassportIDException InvalidIdException()
    {
        return new PassportIDException("Invalid id exception");
    }
}