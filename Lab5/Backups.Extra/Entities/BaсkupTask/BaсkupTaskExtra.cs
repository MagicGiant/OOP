using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Extra.Models;
using Backups.Models;
using Newtonsoft.Json;
using Zio;

namespace Backups.Extra.Entities.BakupTask;

public class BackupTaskExtra
{
    public const string MetaDataFileName = "MetaData.json";

    private BackupTask _backupTask;

    public BackupTaskExtra(IRepository repository, string savePath, Algo algo, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _backupTask = new (repository, savePath, algo);
        string metaDataPath = $@"{savePath}\{MetaDataFileName}";
        if (!_backupTask.Repository.FileExists(metaDataPath))
        {
            _backupTask.Repository.CreateFile(metaDataPath);
        }
        else
        {
            string jMetaData = _backupTask.Repository.ReadAllText(metaDataPath);
            _backupTask = JsonConvert.DeserializeObject<BackupTask>(jMetaData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                CheckAdditionalContent = false,
            });
        }

        Logger = logger;
    }

    public ILogger Logger { get; }

    public Backup Backup => _backupTask.Backup;

    public void CleanPoints(IClearAlgorithm algorithm)
    {
        ArgumentNullException.ThrowIfNull(algorithm);
        foreach (var point in algorithm.GetClearedPoints(_backupTask.Backup))
        {
            _backupTask.Backup.RemoveRestorePoint(point.Name);
            _backupTask.Repository.DeleteDirectory($@"{_backupTask.SavePath}\{point.Name}");
        }

        Logger.AddLog("Clean points");
    }

    public void UpBack(RestorePoint point)
    {
        ArgumentNullException.ThrowIfNull(point);
        foreach (IBackupObject backupObject in point.Objects)
        {
            string objectDataPath = $@"{_backupTask.SavePath}/{point.Name}/{backupObject.GetObjectName()}";
            if (backupObject.Type == ObjectType.File)
            {
                _backupTask.Repository.DeleteFile(backupObject.ObjectPath.ToString());
                _backupTask.Repository.CopyFile(objectDataPath, backupObject.GetDirectoryName());
            }
            else
            {
                _backupTask.Repository.DeleteDirectory(backupObject.ObjectPath.ToString());
                _backupTask.Repository.CopyDirectory(objectDataPath, backupObject.ObjectPath.ToString());
            }
        }

        Logger.AddLog("UpBack");
    }

    public void UpBackDifferentLocation(RestorePoint point, string directoryPath)
    {
        if (!_backupTask.Repository.DirectoryExists(directoryPath))
            throw BackupTaskExtraException.DirectoryExistException(directoryPath);
        _backupTask.Repository.CopyDirectory($@"{_backupTask.SavePath}\{point.Name}", directoryPath);

        Logger.AddLog("UpBank different location");
    }

    public void Save(string name)
    {
        _backupTask.Save(name);

        Logger.AddLog("Save");
    }

    public void AddObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupTask.AddObject(backupObject);

        Logger.AddLog("AddObject");
    }

    public void DeleteObject(IBackupObject backupObject)
    {
        ArgumentNullException.ThrowIfNull(backupObject);
        _backupTask.DeleteObject(backupObject);

        Logger.AddLog("Delete object");
    }

    public void Merge(string firstPointName, string secondPointName, string newPointName)
    {
        if (string.IsNullOrEmpty(firstPointName))
            throw new ArgumentNullException(nameof(firstPointName));
        if (string.IsNullOrEmpty(secondPointName))
            throw new ArgumentNullException(nameof(secondPointName));
        if (string.IsNullOrEmpty(newPointName))
            throw new ArgumentNullException(nameof(newPointName));

        RestorePoint firstPoint = _backupTask.Backup.FindRestorePoint(firstPointName);
        RestorePoint secondPoint = _backupTask.Backup.FindRestorePoint(secondPointName);

        ArgumentNullException.ThrowIfNull(firstPoint);
        ArgumentNullException.ThrowIfNull(secondPoint);

        MergeAlgorithm.Merge(firstPoint, secondPoint, newPointName);

        Logger.AddLog("Merging");
    }

    public void SaveMetadata()
    {
        string jData = JsonConvert.SerializeObject(_backupTask, Formatting.Indented, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        });
        string jPath = $@"{_backupTask.SavePath}\{MetaDataFileName}";
        _backupTask.Repository.WrightAllText(jPath, jData);

        Logger.AddLog("Save metadata");
    }
}