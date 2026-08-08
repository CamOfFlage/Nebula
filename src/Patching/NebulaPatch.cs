using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract String patchId { get; }
    public abstract PatchHandler patchHandler { get; }
    
    public void Register()
    {
        Plugin.logger.LogMessage($"Nebula Patch {patchId} registered");
        patchHandler.patches.Add(this);
    }
}