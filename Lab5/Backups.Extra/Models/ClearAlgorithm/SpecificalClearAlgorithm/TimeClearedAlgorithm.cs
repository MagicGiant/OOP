using Backups.Entities;

namespace Backups.Extra.Models;

public class TimeClearedAlgorithm : ISpecificClearAlgorithm
{
    private DateTime _realDateTime;
    private TimeSpan _allowedTimeSpan;

    public TimeClearedAlgorithm(DateTime realDateTime, TimeSpan allowedTimeSpan)
    {
        _realDateTime = realDateTime;
        _allowedTimeSpan = allowedTimeSpan;
    }

    public IReadOnlyCollection<RestorePoint> GetClearedPoints(Backup backup)
    {
        return backup.RestorePoints
            .Where(point => _realDateTime - point.DateTimeCreating > _allowedTimeSpan)
            .ToList();
    }
}