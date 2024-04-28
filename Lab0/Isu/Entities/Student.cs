using System.Text.RegularExpressions;
using Isu.Exceptions;

namespace Isu.Entities;
public class Student
{
    public static readonly Regex Regex = new Regex(@"^[A-Z][a-z]{1,20}$", RegexOptions.Compiled);

    private string _name;
    public Student(string name, int id)
    {
        if (!Regex.IsMatch(name))
            throw new StudentNameException(name);

        _name = name;
        Id = id;
    }

    public int Id { get; }
    public string Name
    {
        get => _name;
        set
        {
            if (Regex.IsMatch(value))
                throw new StudentNameException(value);

            _name = value;
        }
    }
}
