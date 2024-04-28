namespace Isu.Models;

public interface IReadOnlyGroupTimetable
{
    public IReadOnlyCollection<DayLessons> Days { get; }

    public bool IsCompatibility(Lesson lesson, DateOnly dateOnly);
    public bool IsLesson(LessonName name);
}