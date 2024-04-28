using Backups.Exceptions;
using Newtonsoft.Json;

namespace Backups.Entities;

public class Backup : IBackup
{
    [JsonProperty]
    private Dictionary<string, RestorePoint> _byRestorePointsName = new ();
    [JsonIgnore]
    public ICollection<RestorePoint> RestorePoints => _byRestorePointsName.Values;

    public void SetRestorePoint(RestorePoint point)
    {
        ArgumentNullException.ThrowIfNull(point);
        _byRestorePointsName[point.Name] = point;
    }

    public void RemoveRestorePoint(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (!_byRestorePointsName.ContainsKey(name))
            throw BackupException.RemoveException(name);
        _byRestorePointsName.Remove(name);
    }

    public RestorePoint GetRestorePoint(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (!_byRestorePointsName.ContainsKey(name))
            throw new BackupException(name);

        return _byRestorePointsName[name];
    }

    public bool CountainsName(string name)
    {
        return _byRestorePointsName.ContainsKey(name);
    }

    public RestorePoint FindRestorePoint(string name)
    {
        if (!_byRestorePointsName.ContainsKey(name))
            return null;
        return _byRestorePointsName[name];
    }
}