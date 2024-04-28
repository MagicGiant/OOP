using Backups.Entities;
using Backups.Extra.Entities.BakupTask;
using Backups.Extra.Models;
using Backups.Models;
using Xunit;
using Xunit.Abstractions;

namespace Backups.Extra.Test;

public class BackupExtraTest
{
    private ITestOutputHelper _output;
    private MemoryRepository _repository = new ();

    private string _savePath = "/mnt/c/saved";

    public BackupExtraTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void UpBack_outputRightTextFromFile()
    {
        _repository.CreateDirectory("/mnt/c");
        _repository.CreateFile("/mnt/c/1.txt");
        string firstText = "First text";
        string secondText = "Second text";
        _repository.WrightAllText("/mnt/c/1.txt", firstText);
        BackupTaskExtra backupTaskExtra = new (_repository, _savePath, new SplitStorage(), new ConsoleLogger());
        BackupObject backupObject = new (_repository, "/mnt/c/1.txt");
        backupTaskExtra.AddObject(backupObject);
        backupTaskExtra.Save("first");

        _repository.WrightAllText("/mnt/c/1.txt", secondText);
        backupTaskExtra.UpBack(backupTaskExtra.Backup.RestorePoints.First());

        Assert.Equal(_repository.ReadAllText("/mnt/c/1.txt"), firstText);
    }
}