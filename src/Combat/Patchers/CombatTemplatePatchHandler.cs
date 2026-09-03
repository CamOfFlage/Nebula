using Nebula.Patching;
using Nebula.Combat;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatTemplatePatchHandler : PatchHandler
{
    public static CombatTemplatePatchHandler instance;
    public override Type GetPatchType() => typeof(GlobalCombatTemplatePatch);

    public void PatchTemplate(CombatTemplate combatTemplate)
    {
        Plugin.logger.LogDebug($"Patcher loading {combatTemplate.id}");
        foreach (NebulaPatch nebulaPatch in patches)
        {
            GlobalCombatTemplatePatch patch = nebulaPatch as GlobalCombatTemplatePatch;
            if (patch.templateId.Equals(combatTemplate.id))
            {
                patch.Patch(combatTemplate);
            }
        }
    }
}