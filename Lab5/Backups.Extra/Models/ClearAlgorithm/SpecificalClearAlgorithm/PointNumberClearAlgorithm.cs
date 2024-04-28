using System.Buffers;
using Backups.Entities;

namespace Backups.Extra.Models;

public class PointNumberClearAlgorithm : ISpecificClearAlgorithm
{
    private int _maxPointNumber;

    public PointNumberClearAlgorithm(int maxPointNumber)
    {
        _maxPointNumber = maxPointNumber;
    }

    public IReadOnlyCollection<RestorePoint> GetClearedPoints(Backup backup)
    {
        List<RestorePoint> restorePoints = backup.RestorePoints.ToList();
        restorePoints.OrderBy(point => point.DateTimeCreating);
        restorePoints.Take(_maxPointNumber);
        return restorePoints;
    }
}