using Nebula.Patching;
using UnityEngine;

namespace Nebula.Combat;

public class ProjectilePatchHandler : PatchHandler
{
    public override Type GetPatchType() => typeof(ProjectilePatch);
    public static ProjectilePatchHandler Instance;

    public void PatchProjectile(GameObject effect, string projectileId)
    {
        foreach (NebulaPatch patch in this.Patches)
        {
            ProjectilePatch projectilePatch = patch as ProjectilePatch;
            if (projectilePatch != null)
            {
                if (projectilePatch.ProjectileId == projectileId)
                {
                    projectilePatch.Patch(effect);
                }
            }
        }
    }
}