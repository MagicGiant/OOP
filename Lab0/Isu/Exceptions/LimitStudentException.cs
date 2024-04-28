using System;
using Isu.Entities;

namespace Isu.Exceptions;

public class LimitStudentException : Exception
{
    public LimitStudentException(int studentNumber)
        : base($"Max student in group should be {Group.MaxStudentInGroup}. Your student number {studentNumber}")
    { }
}