using Nebula.Patching;

namespace Nebula;

public abstract class NebulaPatch
{
    public abstract void Patch();

    public abstract String patchId { get; }
    public abstract PatchHandler patchHandler { get; }

    public void Register()
    {
        patchHandler.patches.Add(this);
    }
}