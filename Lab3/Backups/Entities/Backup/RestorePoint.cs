using Backups.Exceptions;
using Newtonsoft.Json;

namespace Backups.Entities;

public class RestorePoint
{
    [JsonProperty]
    private List<IBackupObject> _objects = new ();

    public RestorePoint(string name, DateTime dateTimeCreating)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        Name = name;
        DateTimeCreating = dateTimeCreating;
    }

    public string Name { get; }

    public DateTime DateTimeCreating { get; } = new ();

    [JsonIgnore]
    public ICollection<IBackupObject> Objects => _objects;

    public void SetObject(IBackupObject trackedObject)
    {
        ArgumentNullException.ThrowIfNull(trackedObject);
        if (Is_Exist(trackedObject))
            throw RestorePointException.TrackedException();

        _objects.Add(trackedObject);
        DateTimeCreating.ToLocalTime();
    }

    public void SetObjects(ICollection<IBackupObject> trackedObjects)
    {
        ArgumentNullException.ThrowIfNull(trackedObjects);
        if (Is_Exist(trackedObjects))
            throw RestorePointException.TrackedException();

        _objects.AddRange(trackedObjects);
    }

    public bool Is_Exist(ICollection<IBackupObject> trackedObjects)
    {
        return trackedObjects.FirstOrDefault(trackedObject => Is_Exist(trackedObject)) is not null;
    }

    public bool Is_Exist(IBackupObject trackedObject)
    {
        return _objects.FirstOrDefault(theseObject => trackedObject.Equals(theseObject)) is not null;
    }

    public override string ToString()
    {
        return DateTimeCreating.ToString();
    }
}