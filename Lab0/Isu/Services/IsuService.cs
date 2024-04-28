using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;
public class IsuService : IIsuService
{
    private Dictionary<string, Group> _groups = new ();

    private int _lastStudentsId = 0;

    public Group AddGroup(GroupData data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));

        if (_groups.ContainsKey(data.GroupName))
            throw new UsingGroupNameException(data.GroupName);

        Group newGroup = new Group(data);
        _groups.Add(data.GroupName, newGroup);

        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        _ = group ?? throw new ArgumentNullException(nameof(group));
        Student student = new Student(name, _lastStudentsId);
        group.AddStudent(student);

        _lastStudentsId++;

        return student;
    }

    public Student GetStudent(int id)
    {
        return FindStudent(id) ?? throw new SearchIdException(id);
    }

    public Student FindStudent(int id)
    {
        return _groups.Values
            .SelectMany(group => group.Students)
            .SingleOrDefault(student => student.Id == id);
    }

    public IReadOnlyCollection<Student> FindStudents(GroupData groupData)
    {
        Group group = FindGroup(groupData);

        if (group != null)
            return group.Students.ToList();

        return new List<Student>();
    }

    public IReadOnlyCollection<Student> FindStudents(int courseNumber)
    {
        return _groups.Values
            .Where(group => group.Data.Course == courseNumber)
            .SelectMany(group => group.Students)
            .ToList();
    }

    public Group FindGroup(GroupData groupData)
    {
        return _groups.Values.SingleOrDefault(group => group.Data == groupData);
    }

    public IReadOnlyCollection<Group> FindGroups(int courseNumber)
    {
        return _groups.Values.Where(group => group.Data.Course == courseNumber).ToList();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (newGroup.Students.Contains(student))
            throw new ChangeGroupException(student.Id);

        var groups = _groups.Values.Where(group => group.Students.Contains(student));

        Group group = groups.SingleOrDefault();

        if (group is null)
            throw new SearchIdException(student.Id);

        group.RemoveStudent(student.Id);
        newGroup.AddStudent(student);
    }
}