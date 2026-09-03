namespace Nebula.Patching;

public abstract class PatchHandler
{
    public List<NebulaPatch> patches = new List<NebulaPatch>();
    public abstract Type GetPatchType();

    public void RegisterPatch(NebulaPatch patch)
    {
        Plugin.logger.LogDebug($"Registering {patch.patchId}");
        if (patch.GetType() != GetPatchType())
        {
            Plugin.logger.LogError("Patch " + patch.patchId + " not not match required patch type " + GetPatchType());
        }
        else
        {
            patches.Add(patch);
        }
    }
}