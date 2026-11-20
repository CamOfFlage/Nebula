using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public abstract class CombatSystemPatch : NebulaPatch
{
    protected override PatchHandler GetPatchHandler() => CombatSystemPatchHandler.Instance;
    
    public abstract void Patch(CombatSystem combatSystem);
}