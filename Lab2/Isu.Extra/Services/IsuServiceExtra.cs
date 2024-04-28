using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services;

public class IsuServiceExtra
{
     private Dictionary<char, FacultyTimetable> _faculties = new ();
     private Dictionary<string, GroupExtra> _groups = new ();

     private IsuService _isuService = new ();

     public IReadOnlyCollection<FacultyTimetable> FacultyTimetables => _faculties.Values;

     public IReadOnlyGroupExtra AddGroup(GroupData data)
     {
          ArgumentNullException.ThrowIfNull(data);
          Group group = _isuService.AddGroup(data);
          _groups[data.GroupName] = new GroupExtra(data);
          _groups[data.GroupName].Group = group;
          return _groups[data.GroupName];
     }

     public IReadOnlyStudentExtra AddStudent(IReadOnlyGroupExtra group, string name)
     {
          ArgumentNullException.ThrowIfNull(group);
          if (string.IsNullOrEmpty(name))
               throw new ArgumentNullException();

          Student sudent = _isuService.AddStudent(group.Group, name);
          StudentExtra studentExtra = new (name, sudent.Id);
          _groups[group.Data.GroupName].AddStudent(studentExtra);
          return studentExtra;
     }

     public IReadOnlyStudentExtra GetStudent(int id)
     {
          IReadOnlyStudentExtra student = FindStudent(id);
          if (student is null)
               throw IsuServiceExtraException.FindGroupException();

          return student;
     }

     public IReadOnlyStudentExtra FindStudent(int id)
     {
          GroupExtra group = _groups.Values
               .SingleOrDefault(group => group.FindStudent(id) is not null);
          if (group is null)
               return null;

          return group.FindStudent(id);
     }

     public IReadOnlyCollection<IReadOnlyStudentExtra> FindStudents(GroupData groupData)
     {
          ArgumentNullException.ThrowIfNull(groupData);
          if (!_groups.ContainsKey(groupData.GroupName))
               throw IsuServiceExtraException.FindGroupException();

          return _groups[groupData.GroupName].Students;
     }

     public void AddLesson(IReadOnlyGroupExtra group, Lesson lesson, DateOnly date)
     {
          ArgumentNullException.ThrowIfNull(group);
          ArgumentNullException.ThrowIfNull(lesson);
          ArgumentNullException.ThrowIfNull(date);

          if (!_faculties.ContainsKey(group.Data.FacultyLiteral))
               _faculties[group.Data.FacultyLiteral] = new ();

          _faculties[group.Data.FacultyLiteral].SetLesson(group, lesson, date);
     }

     public void RemoveLesson(GroupData data, LessonName name, DateTime date)
     {
          ArgumentNullException.ThrowIfNull(data);
          ArgumentNullException.ThrowIfNull(name);
          ArgumentNullException.ThrowIfNull(data);
          _faculties[data.FacultyLiteral].RemoveLesson(data, name, date);
     }

     public IReadOnlyGroupTimetable FindGroupTimetable(GroupData data)
     {
          ArgumentNullException.ThrowIfNull(data);
          if (!_faculties.ContainsKey(data.FacultyLiteral))
               return null;
          return _faculties[data.FacultyLiteral].FindTimetable(data);
     }

     public IReadOnlyFacultyTimetable FindFacultyTimetable(GroupData data)
     {
          ArgumentNullException.ThrowIfNull(data);
          if (!_faculties.ContainsKey(data.FacultyLiteral))
               return null;
          return _faculties[data.FacultyLiteral];
     }

     public void SetOgnp(IReadOnlyGroupExtra group, IReadOnlyStudentExtra student, Ognp ognp)
     {
          ArgumentNullException.ThrowIfNull(group);
          ArgumentNullException.ThrowIfNull(student);
          ArgumentNullException.ThrowIfNull(ognp);

          if (group.Students.SingleOrDefault(student) is null)
               throw IsuServiceExtraException.SetOgnpWithNonExceptStudentException();

          _faculties[group.Data.FacultyLiteral].SetOGNP(group.Data, student, ognp);
     }

     public void RemoveOgnp(GroupData data, IReadOnlyStudentExtra student, Ognp ognp)
     {
          ArgumentNullException.ThrowIfNull(data);
          ArgumentNullException.ThrowIfNull(student);
          ArgumentNullException.ThrowIfNull(ognp);

          _faculties[data.FacultyLiteral].SetOGNP(data, student, ognp);
     }
}