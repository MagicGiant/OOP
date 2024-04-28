using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;
public class Group
{
    public const int MaxStudentInGroup = 23;

    private Dictionary<int, Student> _students = new ();

    public Group(GroupData data)
    {
        Data = data ?? throw new ArgumentNullException(nameof(data));
    }

    public GroupData Data { get; }

    public IReadOnlyCollection<Student> Students => _students.Values;

    public void AddStudent(Student student)
    {
        _ = student ?? throw new ArgumentNullException(nameof(student));
        if (_students.ContainsKey(student.Id))
            throw new IdWasUsingException(student.Id);
        if (_students.Count() + 1 > MaxStudentInGroup)
            throw new LimitStudentException(student.Id);

        _students.Add(student.Id, student);
    }

    public bool RemoveStudent(int id)
    {
        return _students.Remove(id);
    }
}
