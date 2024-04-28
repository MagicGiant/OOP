using System.Collections.Generic;
using Isu.Entities;
using Isu.Models;

namespace Isu.Services;
public interface IIsuService
{
    Group AddGroup(GroupData data);
    Student AddStudent(Group group, string name);
    Student GetStudent(int id);
    Student FindStudent(int id);
    IReadOnlyCollection<Student> FindStudents(GroupData groupData);
    IReadOnlyCollection<Student> FindStudents(int courseNumber);

    Group FindGroup(GroupData groupData);
    IReadOnlyCollection<Group> FindGroups(int courseNumber);

    void ChangeStudentGroup(Student student, Group newGroup);
}