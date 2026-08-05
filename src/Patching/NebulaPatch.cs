using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract void Patch();

    public virtual String patchId { get; }
    public virtual PatchHandler patchHandler { get; }
    
    public void Register()
    {
        Plugin.logger.LogMessage($"Nebula Patch {patchId} registered");
        patchHandler.patches.Add(this);
    }
}