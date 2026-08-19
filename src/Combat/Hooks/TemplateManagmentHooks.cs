using HarmonyLib;
using Noname.Worldless.Combat;

namespace Nebula.Combat.Hooks;

[HarmonyPatch(typeof(CombatTemplate), "Awake")]
public class AwakeHook
{
    [HarmonyPostfix]
    static void LoadTemplate(CombatTemplate __instance)
    {
        Plugin.logger.LogMessage("Hi");
        Plugin.logger.LogMessage($"Combat template {__instance.name} loaded");
        CombatTemplates.loadedTemplates.Add(__instance);
        CombatTemplatePatchHandler.instance.PatchTemplate(__instance);
    }
}