using Nebula.Patching;
using UnityEngine;
using Noname;

namespace Nebula.Combat;

public class ProjectilePatchHandler : PatchHandler
{
    public override Type GetPatchType() => typeof(ProjectilePatch);
    public static ProjectilePatchHandler Instance;

    public void PatchProjectile(GameObject effect, String projectileId)
    {
        foreach (NebulaPatch patch in this.patches)
        {
            ProjectilePatch projectilePatch = patch as ProjectilePatch;
            if (projectilePatch != null)
            {
                if (projectilePatch.projectileId == projectileId)
                {
                    projectilePatch.Patch(effect);
                }
            }
        }
    }
}