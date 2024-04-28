using Backups.Exceptions;
using Backups.Models;
using Zio;
namespace Backups.Entities;

public interface IBackupObject : IEquatable<IBackupObject>
{
    ObjectType Type { get; }

    UPath ObjectPath { get; }

    string GetDirectoryName();

    string GetObjectName();

    string ToString();
}