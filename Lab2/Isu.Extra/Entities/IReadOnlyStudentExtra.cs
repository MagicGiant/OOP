using Isu.Models;

namespace Isu.Entities;

public interface IReadOnlyStudentExtra
{
    public int Id { get; }
    public string Name { get; }
    public Ognp Ognp { get; }
}