using HarmonyLib;
using Noname.Worldless.Navigation;

namespace Nebula.Navigation.Hooks;

[HarmonyPatch(typeof(NavigationSystem))]
internal class NavigationSystemHooks
{
    [HarmonyPatch("InitInstance")]
    [HarmonyPostfix]
    private static void CombatSystemInit(NavigationSystem __instance)
    {
        Plugin.logger.LogDebug("Loading Navigation system instance");
        GameInfo.NavigationSystem = __instance;
    }
}