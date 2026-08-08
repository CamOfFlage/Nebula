using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatSystemPatchHandler : PatchHandler
{
    public static CombatSystemPatchHandler Instance = new CombatSystemPatchHandler();

    public void PatchCombat()
    {
        foreach (NebulaPatch nebulaPatch in this.patches)
        {
            CombatSystemPatch patch = nebulaPatch as CombatSystemPatch;
            if (patch == null)
            {
                patches.Remove(nebulaPatch);
                Plugin.logger.LogError($"Patch {nebulaPatch.patchId} is not a CombatSystemPatch");
            }
            patch.Patch(GameInfo.CombatSystem.GetComponent<CombatSystem>());
        }
    }
}