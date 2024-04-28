using Backups.Entities;
using Zio.FileSystems;

namespace Backups.Models;

public abstract class Algo
{
    public Storage System { get; } = new ();

    public abstract Storage Save(RestorePoint point);
}