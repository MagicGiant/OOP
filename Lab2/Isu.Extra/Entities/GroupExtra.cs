using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class GroupExtra : IReadOnlyGroupExtra
{
    public const int MaxStudentInGroup = 23;

    private Dictionary<int, StudentExtra> _students = new ();

    private Group _group;

    public GroupExtra(GroupData data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
        Group = new Group(data);
    }

    public Group Group
    {
        get => _group;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _group = value;
        }
    }

    public GroupData Data { get; }
    public GroupTimetable ChangedTimetable { get; set; }

    public IReadOnlyGroupTimetable Timetable => ChangedTimetable;
    public IReadOnlyCollection<IReadOnlyStudentExtra> Students => _students.Values;

    public void AddStudent(StudentExtra student)
    {
        _ = student ?? throw new ArgumentNullException(nameof(student));
        if (_students.ContainsKey(student.Id))
            throw new IdWasUsingException(student.Id);
        if (_students.Count() + 1 > MaxStudentInGroup)
            throw new LimitStudentException(student.Id);

        _students.Add(student.Id, student);
    }

    public IReadOnlyStudentExtra FindStudent(int id)
    {
        if (!_students.ContainsKey(id))
            return null;
        return _students[id];
    }

    public bool RemoveStudent(int id)
    {
        return _students.Remove(id);
    }
}