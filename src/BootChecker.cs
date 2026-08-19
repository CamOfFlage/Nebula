using HarmonyLib;
using Nebula.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebula;

public class BootChecker : MonoBehaviour
{
    public BootChecker(IntPtr intPtr) : base(intPtr) { }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Boot")
        {
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
            try
            {
                GameInfo.IsBooted = true;
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