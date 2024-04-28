using Isu.Entities;
namespace Isu.Models;

public interface IReadOnlyFacultyTimetable
{
    public ICollection<GroupTimetable> Timetables { get; }

    public GroupTimetable FindTimetable(GroupData data);

    public Ognp FindOgnp(IReadOnlyStudentExtra student);

    public bool IsLesson(LessonName name);

    public bool IsSettingNotBaseOgnp(Ognp ognp);

    public bool IsCompabilityOgnp(Ognp ognp, GroupData data);
}