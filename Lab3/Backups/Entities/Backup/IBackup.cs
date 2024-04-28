namespace Backups.Entities;

public interface IBackup
{
    ICollection<RestorePoint> RestorePoints { get; }

    void SetRestorePoint(RestorePoint point);

    void RemoveRestorePoint(string name);

    RestorePoint FindRestorePoint(string name);
}