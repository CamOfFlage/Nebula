using UnityEngine;
using Nebula.Combat;

namespace Nebula;

public static class GameInfo
{
    public static bool IsBooted = false;
    public static bool CombatTemplatesLoaded = false;
    public static GameObject GameManager;
    public static GameObject CombatSystem;
    public static GameObject NavigationSystem;
    public static LoadedTemplates LoadedTemplates;
}