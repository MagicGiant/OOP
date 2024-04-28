using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public interface IReadOnlyGroupExtra
{
    public GroupData Data { get; }
    public Group Group { get; }

    public IReadOnlyCollection<IReadOnlyStudentExtra> Students { get; }

    public IReadOnlyGroupTimetable Timetable { get; }
}