using Isu.Models;
namespace Isu.Entities;

public class StudentExtra : Student, IReadOnlyStudentExtra
{
    private Ognp _ognp;
    public StudentExtra(string name, int id)
        : base(name, id)
    { }

    public Ognp Ognp
    {
        get => _ognp;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _ognp = value;
        }
    }
}