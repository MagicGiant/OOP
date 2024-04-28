using Backups.Entities;
using Xunit.Sdk;

namespace Backups.Extra.Models;

public class AvailableClearAlgorithm : IClearAlgorithm
{
    public AvailableClearAlgorithm(ISpecificClearAlgorithm firstAlgorithm, ISpecificClearAlgorithm secondAlgorithm)
    {
        ArgumentNullException.ThrowIfNull(firstAlgorithm);
        ArgumentNullException.ThrowIfNull(secondAlgorithm);
        FirstAlgorithm = firstAlgorithm;
        SecondAlgorithm = secondAlgorithm;
    }

    public ISpecificClearAlgorithm FirstAlgorithm { get; }

    public ISpecificClearAlgorithm SecondAlgorithm { get; }

    public IReadOnlyCollection<RestorePoint> GetClearedPoints(Backup backup)
    {
        List<RestorePoint> clearedPoints = FirstAlgorithm.GetClearedPoints(backup)
            .Where(fPoint => SecondAlgorithm.GetClearedPoints(backup)
                .FirstOrDefault(sPoint => sPoint == fPoint) is null)
            .ToList();
        clearedPoints.AddRange(SecondAlgorithm.GetClearedPoints(backup));
        return clearedPoints;
    }
}