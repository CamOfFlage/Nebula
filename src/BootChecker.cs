using HarmonyLib;
using Nebula.Combat;
using Noname.Worldless.Combat;
using Noname.Worldless.Navigation;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebula;

/// <summary>
/// The system for detecting the stages of the boot process
/// </summary>
public class BootChecker : MonoBehaviour
{
    public BootChecker(IntPtr intPtr) : base(intPtr) { }

    private bool _hasStartedBootScene = false;
    private void Update()
    {
        if (!_hasStartedBootScene && SceneManager.GetActiveScene().name == "Boot")
        {
            _hasStartedBootScene = true;
            Plugin.logger.LogMessage("Booting...");
        }
        if (SceneManager.GetActiveScene().name != "Boot" && !GameInfo.IsBooted && _hasStartedBootScene)
        {
            Plugin.logger.LogMessage("Boot finished");
            GameInfo.IsBooted = true;
            Plugin.Harmony.PatchAll();
            Plugin.logger.LogMessage("Harmony Patched");
            try
            {
                GameInfo.CombatSystem = GameObject.Find("CombatSystem(Clone)").GetComponent<CombatSystem>();
                CombatSystemPatchHandler.Instance.PatchCombat();
                GameInfo.NavigationSystem = GameObject.Find("NavigationSystem(Clone)").GetComponent<NavigationSystem>();
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