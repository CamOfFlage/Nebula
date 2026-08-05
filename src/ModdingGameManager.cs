using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;
using Noname.Worldless.Combat;
using Nebula.ResourceManager;

namespace Nebula;

public class ModdingGameManager : MonoBehaviour
{
    /*
     * Check if templates are loaded and invoke event
     * OnSceneLoad, find all new templates and modify
     * OnSceneUnload, remove all old templates
     */
    public ModdingGameManager(IntPtr intPtr) : base(intPtr) { }
}