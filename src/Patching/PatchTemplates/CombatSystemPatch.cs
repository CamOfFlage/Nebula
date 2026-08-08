using Nebula.Patching;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public abstract class CombatSystemPatch : NebulaPatch
{
    public override PatchHandler patchHandler => CombatSystemPatchHandler.Instance;
    
    public abstract void Patch(CombatSystem combatSystem);
}