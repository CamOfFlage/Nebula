using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class GlobalCombatTemplatePatchHandler : PatchHandler
{
    public static GlobalCombatTemplatePatchHandler instance;
    public override Type GetPatchType() => typeof(GlobalCombatTemplatePatch);

    public void PatchTemplate(CombatTemplate combatTemplate)
    {
        Plugin.logger.LogDebug($"Patcher loading {combatTemplate.id}");
        foreach (NebulaPatch nebulaPatch in Patches)
        {
            GlobalCombatTemplatePatch patch = nebulaPatch as GlobalCombatTemplatePatch;
            if (patch != null)
            {
                if (patch.TemplateId.Equals(combatTemplate.id) || patch.TemplateId.Equals("Any"))
                {
                    patch.Patch(combatTemplate);
                }
            }
        }
    }
}