using UnityEngine;
using UnityEngine.SceneManagement;

namespace WorldlessLibs;

public class BootChecker : MonoBehaviour
{
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "Boot")
        {
            GameInfo.IsBooted = true;
            Destroy(this);
        }
    }
}