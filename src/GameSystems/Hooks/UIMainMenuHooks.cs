using HarmonyLib;
using Noname.Worldless.UI;

namespace Nebula.GameSystems.Hooks;

[HarmonyPatch(typeof(UI_MainMenu))]
internal static class UIMainMenuHooks
{
    [HarmonyPatch(nameof(UI_MainMenu.Activate))]
    [HarmonyPostfix]
    internal static void ActivatePostfix(UI_MainMenu __instance)
    {
        NebulaLoadManager.MainMenuStart();
    }
}