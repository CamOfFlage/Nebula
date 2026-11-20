using HarmonyLib;
using Noname;

namespace Nebula.GameSystems.Hooks;

[HarmonyPatch(typeof(MainSystem))]
internal static class MainSystemHooks
{
    [HarmonyPatch(nameof(MainSystem.InitInstance))]
    [HarmonyPostfix]
    internal static void InitInstancePostfix(MainSystem __instance)
    {
        Plugin.logger.LogDebug("MainSystemInit");
        GameInfo.MainSystem =  __instance;
        LoadingScreenController._loadingIcon = __instance._loadingIcon;
        LoadingScreenController._fadeHandler = __instance._fullScreenFader.activeFullScreenFadeHandler;
    }
}