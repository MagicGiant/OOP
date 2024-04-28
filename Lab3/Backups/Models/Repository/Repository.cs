using Newtonsoft.Json;
using Zio;

namespace Backups.Models;

public abstract class Repository : IRepository
{
    public IFileSystem FileSystem { get; protected set; }

    public void CreateDirectory(string path)
    {
        FileSystem.CreateDirectory(path);
    }

    public void DeleteDirectory(string path)
    {
        FileSystem.DeleteDirectory(path, isRecursive: true);
    }

    public void CreateFile(string path)
    {
        using (Stream zio = FileSystem.CreateFile(path))
        {
        }
    }

    public Stream GetFile(string path, FileMode mode, FileAccess access, FileShare share)
    {
        return FileSystem.OpenFile(path, mode, access, share);
    }

    public bool FileExists(string path)
    {
        return FileSystem.FileExists(path);
    }

    public bool DirectoryExists(string path)
    {
        return FileSystem.DirectoryExists(path);
    }

    public void CopyFile(string srcPath, string destPath)
    {
        using (FileSystem.CreateFile(destPath + "/" + Path.GetFileName(srcPath)))
        {
        }

        FileSystem.CopyFile(srcPath, destPath + "/" + Path.GetFileName(srcPath), true);
    }

    public void CopyDirectory(string srcPath, string destPath)
    {
        string newDirectoryPath = destPath + "/" + Path.GetFileName(srcPath);
        FileSystem.CreateDirectory(newDirectoryPath);
        var files = FileSystem.EnumerateFiles(srcPath);
        foreach (UPath uPath in files)
            CopyFile(uPath.ToString(), newDirectoryPath);
        var directories = FileSystem.EnumerateDirectories(srcPath);
        foreach (UPath directory in directories)
        {
            FileSystem.CreateDirectory(newDirectoryPath + "/" + Path.GetFileName(directory.ToString()));
            CopyDirectory(directory.ToString(), newDirectoryPath + "/" + Path.GetFileName(directory.ToString()));
        }
    }

    public void DeleteFile(string path)
    {
        FileSystem.DeleteFile(path);
    }

    public IEnumerable<string> EnumerateFiles(string path)
    {
        return FileSystem.EnumerateFiles(path).Select(uPath => uPath.ToString());
    }

    public IEnumerable<string> EnumerableDirectories(string path)
    {
        return FileSystem.EnumerateDirectories(path).Select(uPath => uPath.ToString());
    }

    public IEnumerable<string> EnumerablePaths(string path)
    {
        return FileSystem.EnumeratePaths(path).Select(uPath => uPath.ToString());
    }

    public string ReadAllText(string path)
    {
        return FileSystem.ReadAllText(path);
    }

    public void WrightAllText(string path, string text)
    {
        FileSystem.WriteAllText(path, text);
    }

    public void MoveFile(string srcPath, string destPath)
    {
        FileSystem.MoveFile(srcPath, destPath);
    }

    public void MoveDirectory(string srcPath, string destPath)
    {
        FileSystem.MoveDirectory(srcPath, destPath);
    }
}