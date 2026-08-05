using Nebula.Combat;
using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.PatchTemplates;

public abstract class GlobalCombatTemplatePatch : NebulaPatch
{
    public virtual String templateId { get; }
    public override PatchHandler patchHandler { get; } = CombatTemplatePatchHandler.instance;

    public abstract void Patch(CombatTemplate combatTemplate);
}