using Backups.Exceptions;
using Backups.Models;
using Zio;

namespace Backups.Entities;

public class BackupObject : IBackupObject
{
    public BackupObject(IRepository repository, string objectPath)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrEmpty(objectPath))
            ArgumentNullException.ThrowIfNull(objectPath);
        BackupObjectTypeDefinition(repository, objectPath);
        ObjectPath = objectPath;
    }

    public ObjectType Type { get; private set; }

    public UPath ObjectPath { get; }

    public string GetObjectName()
    {
        return Path.GetFileName(ObjectPath.ToString());
    }

    public string GetDirectoryName()
    {
        return Path.GetDirectoryName(ObjectPath.ToString());
    }

    public override string ToString()
    {
        return ObjectPath.ToString();
    }

    public bool Equals(IBackupObject other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return ObjectPath.Equals(other.ObjectPath);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BackupObject)obj);
    }

    public override int GetHashCode()
    {
        return ObjectPath.GetHashCode();
    }

    private void BackupObjectTypeDefinition(IRepository repository, string path)
    {
        ArgumentNullException.ThrowIfNull(repository);
        if (string.IsNullOrEmpty(path))
            ArgumentNullException.ThrowIfNull(path);
        if (repository.FileExists(path))
        {
            if (repository.DirectoryExists(path))
                throw BackupObjectException.SamePathDirectoryAndFile(path);
            Type = ObjectType.File;
        }
        else if (repository.DirectoryExists(path))
        {
            Type = ObjectType.Directory;
        }
        else
        {
            throw BackupObjectException.ExistException(path);
        }
    }
}