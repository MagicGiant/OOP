using Isu.Entities;

namespace Isu.Exceptions;

public class OgnpException : Exception
{
    public OgnpException(string message)
        : base(message)
    { }

    public static OgnpException SettingAdditionalLessonException()
    {
        return new OgnpException("OGNP lessons should not be in the scoring of the faculty of this student");
    }

    public static OgnpException CompabilityWithBaseTimetableException()
    {
        return new OgnpException("OGNP lesson doesn't compability with base timetable");
    }

    public static OgnpException CompabilityException()
    {
        return new OgnpException("OGNP lesson doesn't compability together");
    }

    public static OgnpException RemoveException()
    {
        return new OgnpException("Can't remove OGNP lesson which doesn't exist");
    }
}