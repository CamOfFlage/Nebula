using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract string PatchId { get; }
    protected abstract PatchHandler GetPatchHandler();
    
    internal static void Register(Type patch)
    {
        NebulaPatch patchInstance = Activator.CreateInstance(patch) as NebulaPatch;
        if (patchInstance == null)
        {
            Plugin.logger.LogError($"Encountered issues trying to register type {patch.Name} as a Nebula patch.");
            return;
        }
        patchInstance.GetPatchHandler().Patches.Add(patchInstance);
        Plugin.logger.LogMessage($"Nebula Patch {patchInstance.PatchId} registered");
    }
}