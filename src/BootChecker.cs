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
            Destroy(this);
        }
    }
}