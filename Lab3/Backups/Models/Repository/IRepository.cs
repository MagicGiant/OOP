namespace Backups.Models;

public interface IRepository
{
    void CreateDirectory(string path);

    void DeleteDirectory(string path);

    void CreateFile(string path);

    Stream GetFile(string path, FileMode mode, FileAccess access, FileShare share);

    bool FileExists(string path);

    bool DirectoryExists(string path);

    void CopyFile(string srcPath, string destPath);

    void CopyDirectory(string srcPath, string destPath);

    void MoveFile(string srcPath, string destPath);

    void MoveDirectory(string srcPath, string destPath);

    void DeleteFile(string path);

    IEnumerable<string> EnumerateFiles(string path);

    IEnumerable<string> EnumerableDirectories(string path);

    IEnumerable<string> EnumerablePaths(string path);

    string ReadAllText(string path);

    void WrightAllText(string path, string text);
}