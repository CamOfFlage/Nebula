using Noname.Worldless.Combat;
using Noname.Worldless.Navigation;
using UnityEngine;

namespace Nebula;

/// <summary>
/// Contains basic information of the state of the game, along with references to basic game systems
/// </summary>
public static class GameInfo
{
    /// <summary>
    /// Has the game bypassed the initial boot sequence
    /// </summary>
    /// <remarks>
    /// This is showing the core boot sequence, not the game specific boot sequence. It changes upon the title cards ending, not the main loading screen.
    /// </remarks>
    public static bool IsBooted { get; set; } = false;
    /// <summary>
    /// The base <see cref="BootChecker"/> for Nebula
    /// </summary>
    /// <remarks>
    /// This gets destroyed shortly after the game loads, do not rely on this persisting
    /// </remarks>
    internal static BootChecker? BootChecker;
    internal static NebulaGameManager GameManager { get; set; }
    /// <summary>
    /// The base CombatSystem game object
    /// </summary>
    /// <remarks>
    /// May not be loaded immediately 
    /// </remarks>
    public static CombatSystem? CombatSystem { get; set; }
    /// <summary>
    /// The base NavigationSystem game object
    /// </summary>
    /// <remarks>
    /// May not be loaded immediately 
    /// </remarks>
    public static NavigationSystem? NavigationSystem { get; set; }
    
    /// <summary>
    /// Contains the keys for all the attack warnings in the game
    /// </summary>
    public static List<string> WarningKeys = new List<string>
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