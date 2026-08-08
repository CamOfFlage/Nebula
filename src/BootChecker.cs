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
            GameInfo.IsBooted = true;
            GameInfo.CombatSystem = GameObject.Find("CombatSystem(Clone)");
            CombatSystemPatchHandler.Instance.PatchCombat();
            GameInfo.NavigationSystem = GameObject.Find("NavigationSystem(Clone)");
            Destroy(this);
        }
    }
}