namespace Nebula.Patching;

public abstract class PatchHandler
{
    public List<NebulaPatch> Patches { get; } = new List<NebulaPatch>();
    public abstract Type GetPatchType();
    
    private readonly List<object> _storedItems = new List<object>();

    public void RegisterPatch(NebulaPatch patch)
    {
        Plugin.logger.LogDebug($"Registering {patch.PatchId}");
        if (patch.GetType() != GetPatchType())
        {
            Plugin.logger.LogError("Patch " + patch.PatchId + " does not match required patch type " + GetPatchType());
        }
        else
        {
            Patches.Add(patch);
        }
    }
    
    protected virtual void PatchEarlyItems(object[] itemsToPatch) {}

    protected void OutputError(NebulaPatch patch, Exception e, Type patchHandler)
    {
        Plugin.logger.LogError($"[{nameof(patchHandler)}] Encountered an error trying to patch {patch.PatchId}\n {e}");
    }

    /// <summary>
    /// If the patch handler relies on triggers that happen before the loading screen finishes, this makes sure that the patch still gets called
    /// </summary>
    /// <param name="itemToPatch">The item that will be saved until ready</param>
    /// <returns>True: Is early, and should not continue normal patching <br/> False: Is not early, and can continue normally</returns>
    protected bool HandleEarly(object itemToPatch)
    {
        if (GameInfo.NebulaLoaded) return false;
        _storedItems.Add(itemToPatch);
        return true;
    }

    public void ReleaseEarly()
    {
        if (_storedItems.Count > 0)
        {
            PatchEarlyItems(_storedItems.ToArray());
        }
    }
}