using System;
using System.Collections;
using Isu.Models;

namespace Isu.Exceptions;

public class LimitCourseNumberException : Exception
{
    public LimitCourseNumberException(int courseNumber)
        : base($"CourseNumber can be also low {GroupData.MaxCourse}. Your data: {courseNumber}")
    { }
}