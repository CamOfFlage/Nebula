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
            GameInfo.IsBooted = true;
            NebulaGameEvents.InvokeBootStarted();
            Plugin.logger.LogMessage("Boot finished");
            //NebulaGameEvents.InvokeBaseGameBootFinished();
            Destroy(this);
        }
    }
}