using Backups.Entities;

namespace Backups.Models;

public class SingleStorage : Algo
{
    public override Storage Save(RestorePoint point)
    {
        System.CreateDirectory($"/{point.Name}");
        string newPath = $"/{point.Name}" + $"/{point.Objects.First().GetObjectName()}";

        if (point.Objects.First().Type == ObjectType.Directory)
            System.CreateDirectory(newPath);
        else
            System.CreateFile(newPath);
        return System;
    }
}