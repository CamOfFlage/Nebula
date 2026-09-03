using HarmonyLib;
using Nebula.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebula;

public class BootChecker : MonoBehaviour
{
    public BootChecker(IntPtr intPtr) : base(intPtr) { }

    public bool HasStartedBootScene = false;
    private void Update()
    {
        if (!HasStartedBootScene && SceneManager.GetActiveScene().name == "Boot")
        {
            HasStartedBootScene = true;
            Plugin.logger.LogMessage("Booting...");
        }
        if (SceneManager.GetActiveScene().name != "Boot" && !GameInfo.IsBooted && HasStartedBootScene)
        {
            Plugin.logger.LogMessage("Boot finished");
            GameInfo.IsBooted = true;
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            Plugin.logger.LogMessage("Harmony Patched");
            try
            {
                GameInfo.CombatSystem = GameObject.Find("CombatSystem(Clone)");
                CombatSystemPatchHandler.Instance.PatchCombat();
                GameInfo.NavigationSystem = GameObject.Find("NavigationSystem(Clone)");
            }
            catch (Exception e)
            {
                Plugin.logger.LogError("Encountered errors whilst patching the combatSystem, this will likely impact gameplay. Some, or all, mods may not work");
                Plugin.logger.LogError($"Error: {e}");
            }

            Destroy(this);
        }
    }
}