using Isu.Exceptions;

namespace Isu.Models;

public class LessonName : IEquatable<LessonName>
{
    public const int MinLessonLength = 3;

    private string _name;

    public LessonName(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (name.Length < MinLessonLength)
            throw LessonException.LessonNameException(name);

        _name = name;
    }

    public bool Equals(LessonName other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _name == other._name;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LessonName)obj);
    }

    public override int GetHashCode()
    {
        return _name != null ? _name.GetHashCode() : 0;
    }
}