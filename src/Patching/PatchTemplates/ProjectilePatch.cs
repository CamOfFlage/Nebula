using HarmonyLib;
using Nebula.Patching;
using Noname;
using UnityEngine;

namespace Nebula.Combat;

public abstract class ProjectilePatch : NebulaPatch
{
    public override PatchHandler GetPatchHandler() => ProjectilePatchHandler.Instance;
    
    public abstract void Patch(GameObject effect);
    public abstract string projectileId { get; }
    public abstract string projectileGuid { get; }
}