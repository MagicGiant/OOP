using Isu.Exceptions;

namespace Isu.Models;

public class Ognp
{
    public Ognp(Lesson firstLesson, DateOnly firstLessonDay, Lesson secondLesson, DateOnly secondLessonDay)
    {
        ArgumentNullException.ThrowIfNull(firstLesson);
        ArgumentNullException.ThrowIfNull(firstLesson);
        ArgumentNullException.ThrowIfNull(secondLesson);
        ArgumentNullException.ThrowIfNull(secondLessonDay);

        if (firstLessonDay == secondLessonDay && !firstLesson.IsCompatibility(secondLesson))
            throw OgnpException.CompabilityException();

        FirstLesson = firstLesson;
        FirstLessonDay = firstLessonDay;
        SecondLesson = secondLesson;
        SecondLessonDay = secondLessonDay;
    }

    public Lesson FirstLesson { get; }
    public DateOnly FirstLessonDay { get; }

    public Lesson SecondLesson { get; }
    public DateOnly SecondLessonDay { get; }
}