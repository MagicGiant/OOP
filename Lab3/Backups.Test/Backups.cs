using Backups.Entities;
using Backups.Models;
using Xunit;
using Xunit.Abstractions;

namespace Backups.Test;

public class Backups
{
    private ITestOutputHelper _output;
    private MemoryRepository _repository = new ();

    private string dirPath = "/mnt/c/test/direct";
    private string filePath = "/mnt/c/test/1.txt";
    private string savePath = "/mnt/c/saves";

    public Backups(ITestOutputHelper output)
    {
        _output = output;
    }

    private void CreateObjects()
    {
        _repository.CreateDirectory(dirPath);
        _repository.CreateFile(filePath);
        _repository.CreateFile(dirPath + "/2.txt");
    }

    private void F(string path)
    {
        IEnumerable<string> fPaths = _repository.EnumerateFiles(path);
        foreach (string fPath in fPaths)
            _output.WriteLine(fPath);
        IEnumerable<string> dPaths = _repository.EnumerableDirectories(path);
        foreach (string dPath in dPaths)
        {
            _output.WriteLine($"|{dPath}|");
            F(dPath);
        }
    }

    [Fact]
    private void AddObjectAndSave_CheckCreateSaveObjects()
    {
        BackupTask task = new (_repository, savePath, new SplitStorage());
        CreateObjects();
        BackupObject backupObject1 = new (_repository, dirPath);
        BackupObject backupObject2 = new (_repository, filePath);
        task.AddObject(backupObject1);
        task.AddObject(backupObject2);
        task.Save("new");
        Assert.True(_repository.DirectoryExists("/mnt/c/saves/new/direct"));
    }

    [Fact]
    private void CreateObjects_CheckObjects()
    {
        CreateObjects();
        F("/");
    }
}