using Backups.Entities;

namespace Backups.Extra.Models;

public class MergeAlgorithm
{
    public static RestorePoint Merge(RestorePoint firstPoint, RestorePoint secondPoint, string newPointName)
    {
        ArgumentNullException.ThrowIfNull(firstPoint);
        ArgumentNullException.ThrowIfNull(secondPoint);
        if (string.IsNullOrEmpty(newPointName))
            ArgumentNullException.ThrowIfNull(newPointName);

        RestorePoint restorePoint = new RestorePoint(newPointName, DateTime.Now);

        if (secondPoint.DateTimeCreating < firstPoint.DateTimeCreating)
        {
            restorePoint.SetObjects(secondPoint.Objects);
            restorePoint.SetObjects(firstPoint.Objects.Where(backupObject => !secondPoint.Is_Exist(backupObject)).ToList());
        }
        else
        {
            restorePoint.SetObjects(firstPoint.Objects);
            restorePoint.SetObjects(secondPoint.Objects.Where(backupObject => !firstPoint.Is_Exist(backupObject)).ToList());
        }

        return restorePoint;
    }
}