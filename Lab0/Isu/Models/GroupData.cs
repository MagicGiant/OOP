namespace Isu.Models;

using System.Text.RegularExpressions;
using Exceptions;

public class GroupData
{
    public const int MaxCourse = 5;

    private const int CourseNumberIndex = 1;
    private const int FacultyLiteralIndex = 0;

    private static readonly Regex NameRegex = new Regex(@"^[A-Z][1-9][0-9]{1,4}$",  RegexOptions.Compiled);

    public GroupData(string groupName)
    {
        if (!NameRegex.IsMatch(groupName))
            throw new GroupNameException(groupName);

        int checkCourse = int.Parse(groupName[CourseNumberIndex].ToString());

        if (checkCourse > MaxCourse || checkCourse < 0)
            throw new LimitCourseNumberException(checkCourse);

        Course = checkCourse;
        FacultyLiteral = groupName[FacultyLiteralIndex];
        GroupName = groupName;
    }

    public string GroupName { get; }
    public int Course { get; }
    public char FacultyLiteral { get; }
}