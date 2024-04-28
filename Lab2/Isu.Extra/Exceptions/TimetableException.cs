using Isu.Models;

namespace Isu.Exceptions;

public class TimetableException : Exception
{
    public TimetableException(string message)
        : base(message)
    { }

    public static TimetableException RemoveLessonException()
    {
        return new TimetableException($"Can't remove lesson which non-except");
    }
}