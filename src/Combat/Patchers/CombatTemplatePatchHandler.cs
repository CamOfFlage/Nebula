using Nebula.Patching;
using Nebula.Combat;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatTemplatePatchHandler : PatchHandler
{
    public static CombatTemplatePatchHandler instance;

    public void PatchTemplate(CombatTemplate combatTemplate)
    {
        Plugin.logger.LogDebug($"Patcher loading {combatTemplate.id}");
        foreach (NebulaPatch nebulaPatch in patches)
        {
            GlobalCombatTemplatePatch patch = nebulaPatch as GlobalCombatTemplatePatch;
            if (patch == null)
            {
                patches.Remove(nebulaPatch);
                Plugin.logger.LogError($"Patch {nebulaPatch.patchId} is not a GlobalCombatTemplatePatch");
            }

            if (patch.templateId == combatTemplate.id)
            {
                patch.Patch(combatTemplate);
            }
        }
    }
}