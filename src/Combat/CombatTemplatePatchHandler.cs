using Nebula.Patching;
using Nebula.PatchTemplates;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatTemplatePatchHandler : PatchHandler
{
    public static CombatTemplatePatchHandler instance;

    public override List<NebulaPatch> patches { get; set; }

    public void PatchTemplate(CombatTemplate combatTemplate)
    {
        foreach (NebulaPatch nebulaPatch in patches)
        {
            GlobalCombatTemplatePatch patch = nebulaPatch as GlobalCombatTemplatePatch;
            if (patch != null)
            {
                patches.Remove(patch);
                Plugin.logger.LogError($"Patch {nebulaPatch.patchId} is not a GlobalCombatTemplatePatch");
            }

            if (patch.templateId == combatTemplate.id)
            {
                patch.Patch(combatTemplate);
            }
        }
    }
}