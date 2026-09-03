using UnityEngine;
using Nebula.Combat;
using Noname;

namespace Nebula;

public static class GameInfo
{
    public static bool IsBooted = false;
    public static bool CombatTemplatesLoaded = false;
    public static GameObject GameManager;
    public static GameObject CombatSystem;
    public static GameObject NavigationSystem;
    
    public static List<String> WarningKeys = new List<string>
    {
        "light_physical_warning", "light_physical_warning_2", "light_physical_warning_3", "light_physical_warning_4",
        "light_magical_warning", "light_magical_warning_2", "light_magical_warning_3", "light_magical_warning_4",
        "light_fusion_warning",
        "dark_physical_warning", "dark_physical_warning_2", "dark_physical_warning_3", "dark_physical_warning_4",
        "dark_magical_warning", "dark_magical_warning_2", "dark_magical_warning_3",  "dark_magical_warning_4",
        "dark_magical_warning_6",
        "dark_fusion_warning",
        "hybrid_physical_warning", "hybrid_physical_warning_2",  "hybrid_physical_warning_3",
        "hybrid_magical_warning", "hybrid_magical_warning_2", "hybrid_magical_warning_3",
        "hybrid_fusion_warning"
    };
}