using Backups.Entities;
using Zio;
using Zio.FileSystems;

namespace Backups.Models;

public class SplitStorage : Algo
{
    public override Storage Save(RestorePoint point)
    {
        System.CreateDirectory($"/{point.Name}");
        foreach (IBackupObject backupObject in point.Objects)
        {
            string newPath = $"/{point.Name}" + $"/{backupObject.GetObjectName()}";
            if (backupObject.Type == ObjectType.Directory)
                System.CreateDirectory(newPath);
            else
                System.CreateFile(newPath);
        }

        return System;
    }
}