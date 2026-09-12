using Nebula.Patching;
using UnityEngine;

namespace Nebula.Combat;

public abstract class ProjectilePatch : NebulaPatch
{
    public override PatchHandler GetPatchHandler() => ProjectilePatchHandler.Instance;
    
    public abstract void Patch(GameObject effect);
    public abstract string ProjectileKey { get; }
    public abstract string ProjectileGuid { get; }
}