using Zio.FileSystems;

namespace Backups.Models;

public class MemoryRepository : Repository
{
    public MemoryRepository()
    {
        FileSystem = new MemoryFileSystem();
    }
}