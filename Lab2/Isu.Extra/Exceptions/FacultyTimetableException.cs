using Isu.Models;

namespace Isu.Exceptions;

public class FacultyTimetableException : Exception
{
    public FacultyTimetableException(string message)
        : base(message)
    { }

    public static FacultyTimetableException RemoveException()
    {
        return new FacultyTimetableException("Can't remove lesson in non-existent group");
    }
}