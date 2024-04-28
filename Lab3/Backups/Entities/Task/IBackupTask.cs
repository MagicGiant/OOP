using System.Dynamic;
using Zio;

namespace Backups.Entities;

public interface IBackupTask
{
    IBackupObject AddObject(IBackupObject backupObject);

    IBackupObject DeleteObject(IBackupObject backupObject);
}