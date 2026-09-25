using HarmonyLib;
using Noname.Worldless.UI;
using UnityEngine.Rendering;

namespace Nebula.GameSystem.Hooks;

[HarmonyPatch(typeof(SplashScreen))]
internal class SplashScreenRemoveHooks
{
    [HarmonyPatch("Begin")]
    [HarmonyPostfix]
    static void BeginHook()
    {
        Plugin.logger.LogMessage("SplashScreen Begin");
        SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
    }
}

[HarmonyPatch(typeof(UI_TitleSlides))]
internal class TitleSlidesRemoveHooks
{
    [HarmonyPatch("Awake")]
    [HarmonyPostfix]
    static void AwakeHook(UI_TitleSlides __instance)
    {
        Plugin.logger.LogMessage("SplashScreen Awake");
        __instance._slideTime = 0.1f;
        __instance._timeBetweenSlides = 0.1f;
    }
}