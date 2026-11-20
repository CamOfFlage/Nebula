using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatSystemPatchHandler : PatchHandler
{
    public static CombatSystemPatchHandler Instance { get; } = new CombatSystemPatchHandler();
    public override Type GetPatchType() => typeof(CombatSystemPatch);

    public void PatchCombat()
    {
        if (HandleEarly(null))
        {
            Plugin.logger.LogDebug("CombatSystem patcher called before all patches loaded, waiting!");
        }
        Plugin.logger.LogDebug("CombatSystem patcher triggered");
        foreach (NebulaPatch nebulaPatch in this.Patches)
        {
            CombatSystemPatch patch = nebulaPatch as CombatSystemPatch;
            if (patch == null) return;
            try
            {
                patch.Patch(GameInfo.CombatSystem);
            }
            catch (Exception e)
            {
                OutputError(patch, e, typeof(CombatSystemPatchHandler));
            }
        }
    }

    protected override void PatchEarlyItems(object[] itemsToPatch)
    {
        PatchCombat();
    }
}