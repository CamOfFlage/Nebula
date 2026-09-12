using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract string PatchId { get; }
    public abstract PatchHandler GetPatchHandler();
    
    public void Register()
    {
        Plugin.logger.LogMessage($"Nebula Patch {PatchId} registered");
        GetPatchHandler().Patches.Add(this);
    }
}