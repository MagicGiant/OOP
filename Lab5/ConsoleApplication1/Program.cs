using Backups.Entities;
using Backups.Extra.Entities.BakupTask;
using Backups.Extra.Models;
using Backups.Models;

namespace Backups.Extra;

public static class Program
{
    private static PhysicalRepository _repository = new ();
    public static string ProgramLocation { get; } = @"/mnt/c/work/Alisher/programLocation";
    public static string SaveLocation { get; } = @$"{ProgramLocation}/saved";

    public static void Main()
    {
        BackupTaskExtra task = new BackupTaskExtra(_repository, ProgramLocation, new SplitStorage(), new ConsoleLogger());

        BackupObject backupObject = new BackupObject(_repository, $@"{ProgramLocation}/data/123.txt");
        task.AddObject(backupObject);

        task.Save("1");
    }
}