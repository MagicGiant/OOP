using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private IsuService _test = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        string name = "Pasha";

        GroupData data = new GroupData("M13207");
        _test.AddGroup(data);

        _test.AddStudent(_test.FindGroup(data), name);

        Assert.Equal(name, _test.FindStudent(0)?.Name);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        GroupData data = new GroupData("M3207");

        Group group = _test.AddGroup(data);

        for (int i = 0; i < Group.MaxStudentInGroup; i++)
            _test.AddStudent(group, "Pasha");

        Assert.Throws<LimitStudentException>(() =>
        {
            _test.AddStudent(group, "Sherka");
        });
    }

    [Theory]
    [InlineData("alik")]
    [InlineData("Alisher obidganov")]
    [InlineData("A32987")]
    [InlineData("Ali@")]
    public void SetInvalidStudentName_ThrowException(string name)
    {
        Assert.Throws<StudentNameException>(() =>
        {
            Student student = new Student(name, 123);
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        GroupData firstData = new GroupData("M3207");
        GroupData secondData = new GroupData("M3807");

        Group firstGroup = _test.AddGroup(firstData);
        Group secondGroup = _test.AddGroup(secondData);

        Student student = _test.AddStudent(firstGroup, "Igor");

        _test.ChangeStudentGroup(student, secondGroup);

        var targetGroup = _test.FindGroup(secondData);

        Assert.NotNull(targetGroup);
        Assert.Equal(secondGroup, targetGroup);
        Assert.Equal(1, targetGroup.Students.Count);
    }
}