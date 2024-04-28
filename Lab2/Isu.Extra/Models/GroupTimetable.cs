using Isu.Exceptions;

namespace Isu.Models;

public class GroupTimetable : IReadOnlyGroupTimetable
{
    private Dictionary<string, DayLessons> _days = new ();

    public IReadOnlyCollection<DayLessons> Days => _days.Values;

    public void AddLesson(Lesson lesson, DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        if (_days.ContainsKey(date.ToString()))
        {
            _days[date.ToString()].AddLesson(lesson);
        }
        else
        {
            DayLessons day = new DayLessons(date);
            day.AddLesson(lesson);
            _days[date.ToString()] = day;
        }
    }

    public void RemoveLesson(LessonName name, DateTime startLesson)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(startLesson);

        DateOnly date = new (startLesson.Year, startLesson.Month, startLesson.Day);
        TimeOnly timeStart = new (startLesson.Hour, startLesson.Minute);

        if (!_days.ContainsKey(startLesson.Date.ToString()))
            throw TimetableException.RemoveLessonException();

        if (_days[date.ToString()].FindLesson(name, timeStart) is null)
            throw TimetableException.RemoveLessonException();

        _days[date.ToString()].RemoveLesson(name, timeStart);

        if (_days[date.ToString()].Lessons.Count == 0)
            _days.Remove(date.ToString());
    }

    public bool IsCompatibility(Lesson lesson, DateOnly dateOnly)
    {
        ArgumentNullException.ThrowIfNull(lesson);
        ArgumentNullException.ThrowIfNull(dateOnly);

        if (!_days.ContainsKey(dateOnly.ToString()))
            return true;
        return _days[dateOnly.ToString()].IsCompatibility(lesson);
    }

    public bool IsLesson(LessonName name)
    {
        return _days.Values.FirstOrDefault(day => day.IsLesson(name)) is not null;
    }
}