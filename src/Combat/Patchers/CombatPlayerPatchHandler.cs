using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatSystemPatchHandler : PatchHandler
{
    public static CombatSystemPatchHandler Instance = new CombatSystemPatchHandler();
    public override Type GetPatchType() => typeof(CombatSystemPatch);

    public void PatchCombat()
    {
        foreach (NebulaPatch nebulaPatch in this.patches)
        {
            CombatSystemPatch patch = nebulaPatch as CombatSystemPatch;
            patch.Patch(GameInfo.CombatSystem.GetComponent<CombatSystem>());
        }
    }
}