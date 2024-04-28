using Backups.Entities;

namespace Backups.Extra.Models;

public interface IClearAlgorithm
{
    IReadOnlyCollection<RestorePoint> GetClearedPoints(Backup backup);
}