using HarmonyLib;
using Noname.Worldless.UI;

namespace Nebula.GameSystems.Hooks;

[HarmonyPatch(typeof(UI_Manager))]
internal static class UIManagerHooks
{
    [HarmonyPatch(nameof(UI_Manager.Start))]
    [HarmonyPostfix]
    public static void StartPostfix(UI_Manager __instance)
    {
        LoadingScreenController._ui = __instance;
    }
}