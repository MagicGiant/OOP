using Isu.Exceptions;

namespace Isu.Models;

public class Lesson
{
    public static readonly TimeOnly MaxLessonDuration = new (3, 0);

    public Lesson(TimeOnly startTime, TimeOnly endTime, LessonName name)
    {
        ArgumentNullException.ThrowIfNull(startTime);
        ArgumentNullException.ThrowIfNull(endTime);
        ArgumentNullException.ThrowIfNull(name);

        if (endTime < startTime)
            throw LessonException.StartEndTimeException();
        if (endTime - startTime > MaxLessonDuration.ToTimeSpan())
            throw LessonException.DurationException();

        StartTime = startTime;
        EndTime = endTime;
        Name = name;
    }

    public LessonName Name { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    public bool IsCompatibility(Lesson other)
    {
        ArgumentNullException.ThrowIfNull(other);
        return EndTime < other.StartTime || StartTime > other.EndTime;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}