using HarmonyLib;
using Noname.Worldless.Combat;

namespace Nebula.Combat.Hooks;

[HarmonyPatch(typeof(CombatSystem))]
internal class CombatSystemHooks
{
    [HarmonyPatch("InitInstance")]
    [HarmonyPostfix]
    private static void CombatSystemInit(CombatSystem __instance)
    {
        Plugin.logger.LogDebug("Loading combat system instance");
        GameInfo.CombatSystem = __instance;
        CombatSystemPatchHandler.Instance.PatchCombat();
    }
}