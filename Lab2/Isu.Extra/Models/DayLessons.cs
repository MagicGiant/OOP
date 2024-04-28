using Isu.Exceptions;

namespace Isu.Models;

public class DayLessons
{
    private List<Lesson> _lessons = new ();

    public DayLessons(DateOnly date)
    {
        Date = date;
    }

    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public DateOnly Date { get; }

    public bool IsCompatibility(Lesson checkingLesson)
    {
        ArgumentNullException.ThrowIfNull(checkingLesson);
        foreach (Lesson lesson in _lessons)
        {
            if (!lesson.IsCompatibility(checkingLesson))
                return false;
        }

        return true;
    }

    public void AddLesson(Lesson lesson)
    {
        if (!IsCompatibility(lesson))
            throw LessonException.CompatibilityException();
        _lessons.Add(lesson);
    }

    public Lesson FindLesson(LessonName name, TimeOnly startTime)
    {
        return _lessons
            .SingleOrDefault(lesson => lesson.Name.Equals(name) && lesson.StartTime == startTime);
    }

    public void RemoveLesson(LessonName name, TimeOnly startTime)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(startTime);
        Lesson lesson = _lessons
            .SingleOrDefault(lesson => lesson.Name.Equals(name) && lesson.StartTime == startTime);
        if (lesson is null)
            throw LessonException.RemoveException();
        _lessons.Remove(lesson);
    }

    public bool IsLesson(LessonName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _lessons.FirstOrDefault(lesson => lesson.Name.Equals(name)) is not null;
    }
}