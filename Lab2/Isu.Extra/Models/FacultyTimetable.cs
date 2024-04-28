using Isu.Entities;
using Isu.Exceptions;

namespace Isu.Models;

public class FacultyTimetable : IReadOnlyFacultyTimetable
{
    private Dictionary<string, GroupTimetable> _timetables = new ();
    private Dictionary<int, Ognp> _ognpLessens = new ();
    private List<LessonName> _facultyLessons = new ();

    public ICollection<GroupTimetable> Timetables => _timetables.Values;

    public void SetLesson(IReadOnlyGroupExtra group, Lesson lesson, DateOnly date)
    {
        ArgumentNullException.ThrowIfNull(group);
        ArgumentNullException.ThrowIfNull(lesson);
        ArgumentNullException.ThrowIfNull(date);

        foreach (Student student in group.Students)
        {
            if (_ognpLessens.ContainsKey(student.Id) && !IsCompabilityOgnp(_ognpLessens[student.Id], group.Data))
                throw OgnpException.CompabilityWithBaseTimetableException();
        }

        if (!_timetables.ContainsKey(group.Data.GroupName))
            _timetables[group.Data.GroupName] = new ();

        _timetables[group.Data.GroupName].AddLesson(lesson, date);
    }

    public void RemoveLesson(GroupData data, LessonName name, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(date);

        if (!_timetables.ContainsKey(data.GroupName))
            throw FacultyTimetableException.RemoveException();

        _timetables[data.GroupName].RemoveLesson(name, date);

        if (!IsLesson(name))
            _timetables.Remove(data.GroupName);
    }

    public GroupTimetable FindTimetable(GroupData data)
    {
        ArgumentNullException.ThrowIfNull(data);
        if (!_timetables.ContainsKey(data.GroupName))
            return null;
        return _timetables[data.GroupName];
    }

    public bool IsLesson(LessonName name)
    {
        ArgumentNullException.ThrowIfNull(name);
        return _timetables.Values.FirstOrDefault(timetable => timetable.IsLesson(name)) is not null;
    }

    public void SetOGNP(GroupData data, IReadOnlyStudentExtra student, Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(student);
        ArgumentNullException.ThrowIfNull(ognp);
        ArgumentNullException.ThrowIfNull(data);
        if (!IsSettingNotBaseOgnp(ognp))
            throw OgnpException.SettingAdditionalLessonException();
        if (!IsCompabilityOgnp(ognp, data))
            throw OgnpException.CompabilityWithBaseTimetableException();

        _ognpLessens[student.Id] = ognp;
    }

    public Ognp FindOgnp(IReadOnlyStudentExtra student)
    {
        if (!_ognpLessens.ContainsKey(student.Id))
            return null;
        return _ognpLessens[student.Id];
    }

    public void RemoveOgnp(IReadOnlyStudentExtra student)
    {
        ArgumentNullException.ThrowIfNull(student);
        if (_ognpLessens.ContainsKey(student.Id))
            throw OgnpException.RemoveException();

        _ognpLessens.Remove(student.Id);
    }

    public bool IsSettingNotBaseOgnp(Ognp ognp)
    {
        ArgumentNullException.ThrowIfNull(ognp);
        return !IsLesson(ognp.FirstLesson.Name) && !IsLesson(ognp.SecondLesson.Name);
    }

    public bool IsCompabilityOgnp(Ognp ognp, GroupData data)
    {
        GroupTimetable timetable = FindTimetable(data);
        return timetable.IsCompatibility(ognp.FirstLesson, ognp.FirstLessonDay)
               && timetable.IsCompatibility(ognp.FirstLesson, ognp.SecondLessonDay);
    }
}