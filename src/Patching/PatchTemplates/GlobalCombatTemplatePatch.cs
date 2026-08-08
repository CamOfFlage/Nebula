using Nebula.Combat;
using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public abstract class GlobalCombatTemplatePatch : NebulaPatch
{
    public abstract String templateId { get; }
    public override PatchHandler patchHandler => CombatTemplatePatchHandler.instance;

    public abstract void Patch(CombatTemplate combatTemplate);
}