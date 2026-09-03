using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract String patchId { get; }
    public abstract PatchHandler GetPatchHandler();
    
    public void Register()
    {
        Plugin.logger.LogMessage($"Nebula Patch {patchId} registered");
        GetPatchHandler().patches.Add(this);
    }
}