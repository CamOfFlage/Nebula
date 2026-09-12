using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Noname;

namespace Nebula.GameSystem.Hooks;

[HarmonyPatch(typeof(EffectsLoader), "LoadEffects")]
internal class EffectsLoaderHooks
{
    [HarmonyPostfix]
    private static void LoadEffects(EffectsLoader __instance)
    {
        Plugin.logger.LogMessage("Loading effects for " + __instance.name);

        NebulaGameManager gameManager = GameInfo.GameManager;
        gameManager.StartCoroutine(gameManager.WaitForEffectsLoader(__instance).WrapToIl2Cpp());
    }
}