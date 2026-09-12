using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public abstract class GlobalCombatTemplatePatch : NebulaPatch
{
    public abstract string TemplateId { get; }
    public override PatchHandler GetPatchHandler() => GlobalCombatTemplatePatchHandler.instance;

    public abstract void Patch(CombatTemplate combatTemplate);
}