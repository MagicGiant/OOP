using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Extra.Test;

public class IsuService
{
    private IsuServiceExtra _service = new ();
    private GroupData _data = new GroupData("M3107");

    private string _name = "Pasha";
    [Fact]
    private void SetNotBaseOgnp_ThrowException()
    {
        IReadOnlyGroupExtra group = _service.AddGroup(_data);
        Lesson lesson1 = new (new (10, 0), new (11, 30), new ("Math"));
        Lesson lesson2 = new (new (12, 30), new (13, 0), new ("OOP"));

        _service.AddStudent(group, _name);
        _service.AddLesson(group, lesson1, new DateOnly(2022, 1, 1));
        _service.AddLesson(group, lesson2, new DateOnly(2022, 1, 1));

        Ognp ognp = new (lesson1, new (2022, 1, 2), lesson2, new (2022, 1, 2));
        Assert.Throws<OgnpException>(() =>
        {
            _service.SetOgnp(group, group.Students.First(), ognp);
        });
    }

    [Fact]
    private void SetOgnp_FindOgnpFromStudent()
    {
        IReadOnlyGroupExtra group = _service.AddGroup(_data);
        Lesson lesson1 = new (new (10, 0), new (11, 30), new ("Math"));
        Lesson lesson2 = new (new (12, 30), new (13, 0), new ("OOP"));
        Lesson lesson3 = new (new (15, 0), new (16, 30), new ("English"));
        Lesson lesson4 = new (new (17, 0), new (18, 0), new ("BGD"));

        IReadOnlyStudentExtra student = _service.AddStudent(group, _name);
        _service.AddLesson(group, lesson1, new DateOnly(2022, 1, 1));
        _service.AddLesson(group, lesson2, new DateOnly(2022, 1, 1));

        Ognp ognp = new (lesson3, new (2022, 1, 2), lesson4, new (2022, 1, 2));
        _service.SetOgnp(group, student, ognp);

        Assert.NotNull(_service.FindFacultyTimetable(group.Data).FindOgnp(student));
        Assert.Equal(ognp, _service.FindFacultyTimetable(group.Data).FindOgnp(student));
    }

    [Fact]
    private void InvalidLessonName_ThrowException()
    {
        Assert.Throws<LessonException>(() =>
        {
            LessonName name = new ("IT");
        });
    }

    [Fact]
    private void CheckIsLessonInGroupTimetable_EqualBoolData()
    {
        IReadOnlyGroupExtra group = _service.AddGroup(_data);
        string lessonName = "math";
        Lesson lesson = new (new (10, 0), new (11, 0), new LessonName(lessonName));
        _service.AddLesson(group, lesson, new (2022, 1, 1));

        Assert.True(_service.FindGroupTimetable(group.Data).IsLesson(new LessonName(lessonName)));
    }

    [Fact]
    private void SetNotCompabilityLesson_ThrowException()
    {
        IReadOnlyGroupExtra group = _service.AddGroup(_data);
        Lesson lesson1 = new (new (10, 0), new (11, 30), new ("Math"));
        Lesson lesson2 = new (new (11, 00), new (12, 0), new ("OOP"));
        _service.AddLesson(group, lesson1, new (2022, 1, 1));

        Assert.Throws<LessonException>(() =>
            _service.AddLesson(group, lesson2, new (2022, 1, 1)));
    }
}