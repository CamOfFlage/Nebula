using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class GlobalCombatTemplatePatchHandler : PatchHandler
{
    public static GlobalCombatTemplatePatchHandler instance;
    public override Type GetPatchType() => typeof(GlobalCombatTemplatePatch);

    public void PatchTemplate(CombatTemplate combatTemplate)
    {
        if (HandleEarly(combatTemplate))
        {
            Plugin.logger.LogDebug("CombatTemplate patcher called before all patches loaded, waiting!");
            return;
        }
        Plugin.logger.LogDebug($"Patcher loading {combatTemplate.id}");
        foreach (NebulaPatch nebulaPatch in Patches)
        {
            GlobalCombatTemplatePatch patch = nebulaPatch as GlobalCombatTemplatePatch;
            if (patch == null) return;
            if (patch.TemplateId.Equals(combatTemplate.id) || patch.TemplateId.Equals("Any"))
            {
                try
                {
                    patch.Patch(combatTemplate);
                }
                catch (Exception e)
                {
                    OutputError(patch, e, typeof(GlobalCombatTemplatePatchHandler));
                }
            }
        }
    }

    protected override void PatchEarlyItems(object[] itemsToPatch)
    {
        foreach (object itemToPatch in itemsToPatch)
        {
            PatchTemplate(itemToPatch as CombatTemplate);
        }
    }
}