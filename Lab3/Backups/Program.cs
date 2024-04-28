using Backups.Entities;
using Backups.Models;

namespace Backups;

public static class Program
{
    private static PhysicalRepository _repository = new ();

    private static string dirPath = "/mnt/c/work/Working/testing";
    private static string savePath = "/mnt/c/work/Working/save";

    private static void Main()
    {
        BackupTask task = new (_repository, savePath, new SplitStorage());
        BackupObject backupObject = new (_repository, dirPath);
        task.AddObject(backupObject);
        task.Save("1");
        _repository.CreateFile("/mnt/c/work/Working/testing" + "/q.txt");
        task.Save("2");
    }
}