using BepInEx.Unity.IL2CPP.Utils.Collections;
using HarmonyLib;
using Nebula.Combat;
using Noname;
using Noname.Worldless.Combat;

namespace Nebula.GameSystem.Hooks;

[HarmonyPatch(typeof(EffectsLoader), "LoadEffects")]
public class EffectsLoaderHooks
{
    [HarmonyPostfix]
    static void LoadEffects(EffectsLoader __instance)
    {
        Plugin.logger.LogMessage("Loading effects for " + __instance.name);

        ModdingGameManager gameManager = GameInfo.GameManager;
        gameManager.StartCoroutine(gameManager.WaitForEffectsLoader(__instance).WrapToIl2Cpp());
    }
}