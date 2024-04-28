using Backups.Entities;

namespace Backups.Extra.Models;

public class CrossingClearAlgorithm
{
    public CrossingClearAlgorithm(ISpecificClearAlgorithm firstAlgorithm, ISpecificClearAlgorithm secondAlgorithm)
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
        return FirstAlgorithm.GetClearedPoints(backup)
            .Where(fPoint => SecondAlgorithm.GetClearedPoints(backup)
                .FirstOrDefault(sPoint => sPoint == fPoint) is not null)
            .ToList();
    }
}