using Zio.FileSystems;

namespace Backups.Models;

public class PhysicalRepository : Repository
{
    public PhysicalRepository()
    {
        FileSystem = new PhysicalFileSystem();
    }
}