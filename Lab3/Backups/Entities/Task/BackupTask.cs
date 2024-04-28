using System.Collections;
using Backups.Exceptions;
using Backups.Models;
using Newtonsoft.Json;
using Zio;
using Zio.FileSystems;

namespace Backups.Entities;

public class BackupTask
{
    [JsonProperty]
    private List<IBackupObject> _objects = new ();
    [JsonProperty]
    private Backup _backup = new ();
    [JsonProperty]
    private IRepository _repository;
    [JsonProperty]
    private Algo _algo;

    public BackupTask(IRepository repository, string savePath, Algo algo)
    {
        if (string.IsNullOrEmpty(savePath))
            throw new ArgumentNullException(nameof(savePath));
        ArgumentNullException.ThrowIfNull(repository);
        ArgumentNullException.ThrowIfNull(algo);

        _repository = repository;
        SavePath = savePath;
        _algo = algo;
        if (!repository.DirectoryExists(savePath))
            _repository.CreateDirectory(savePath);
    }

    public string SavePath { get; }

    public Algo Algo
    {
        get => _algo;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _algo = value;
        }
    }

    [JsonIgnore]
    public Backup Backup => _backup;
    [JsonIgnore]
    public IRepository Repository => _repository;

    public void AddObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(_objects);
        _objects.Add(backupObject);
    }

    public void DeleteObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(_objects);
        _objects.Add(backupObject);
    }

    public void Save(string name)
    {
        RestorePoint point = CreateRestorePoint(name);
        _backup.SetRestorePoint(point);
        point.SetObjects(_objects);
        CloneSystem(point);
    }

    public IBackupObject GetObject(string name)
    {
        return _objects.SingleOrDefault(backupObject => backupObject.GetObjectName() == name);
    }

    protected void CloneSystem(RestorePoint point)
    {
        Storage storage = _algo.Save(point);
        string dir = SavePath + "/" + point.Name;
        _repository.CreateDirectory(dir);
        foreach (UPath enumeratePath in storage.EnumerablePaths("/" + point.Name))
        {
            string objectName = Path.GetFileName(enumeratePath.ToString());
            IBackupObject backupObject = GetObject(objectName);
            if (backupObject.Type == ObjectType.File)
                _repository.CopyFile(backupObject.ObjectPath.ToString(), dir);
            else
                _repository.CopyDirectory(backupObject.ObjectPath.ToString(), dir);
        }
    }

    private RestorePoint CreateRestorePoint(string name)
    {
        if (_backup.CountainsName(name))
            throw BackupTaskException.CreatePointWithExistingNameException(name);
        return new RestorePoint(name, DateTime.Now);
    }

    private void RemoveRestorePoint(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
        if (!_backup.CountainsName(name))
            throw BackupTaskException.RemoveException(name);
        _backup.RemoveRestorePoint(name);
    }
}