using Isu.Entities;
using Isu.Models;

namespace Isu.Exceptions;

public class LessonException : Exception
{
    public LessonException(string message)
        : base(message)
    { }

    public override string Message { get; }

    public static LessonException StartEndTimeException()
    {
        return new LessonException("End time of lesson can't be low start time");
    }

    public static LessonException DurationException()
    {
        return new LessonException(
            $"Duration of the lesson can't be more {Lesson.MaxLessonDuration.ToTimeSpan().TotalMinutes} minutes");
    }

    public static LessonException CompatibilityException()
    {
        return new LessonException("The time of one lesson should not overlap with the time of another.");
    }

    public static LessonException LessonNameException(string name)
    {
        return new LessonException(
            $"Invalid lesson name ({name}) .Lesson name length must to be more or equal {LessonName.MinLessonLength}");
    }

    public static LessonException RemoveException()
    {
        return new LessonException("Can't remove lesson which non-except");
    }
}