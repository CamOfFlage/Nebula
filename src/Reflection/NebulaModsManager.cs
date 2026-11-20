using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Nebula.GameSystems;

namespace Nebula.Reflection;

internal static class NebulaModsManager
{
    private static List<NebulaMod> _nebulaMods = new List<NebulaMod>();
    private static List<NebulaModLoader> _activeLoaders =  new List<NebulaModLoader>();
    
    private static LoadInstance _loadInstance = new LoadInstance();
    
    public static void TryLoadMod(PluginInfo pluginInfo, Assembly assembly,  BasePlugin basePlugin)
    {
        NebulaModLoader loader = new NebulaModLoader(pluginInfo, assembly, basePlugin);
        _activeLoaders.Add(loader);
        loader.Start();
    }

    public static void RemoveModLoader(NebulaModLoader loader)
    {
        _activeLoaders.Remove(loader);
        IsFinished();
    }

    public static void SuccessModLoader(NebulaMod nebulaMod, NebulaModLoader loader)
    {
        RemoveModLoader(loader);
        _nebulaMods.Add(nebulaMod);
    }

    public static void OnNewPluginLoad(PluginInfo pluginInfo, Assembly assembly, BasePlugin basePlugin)
    {
        TryLoadMod(pluginInfo, assembly, basePlugin);
    }

    private static void ExecuteAllModUsages()
    {
        foreach (NebulaMod mod in _nebulaMods)
        {
            mod.ManageTypes();
        }
    }

    private static void IsFinished()
    {
        if (_activeLoaders.Count == 0 && GameInfo.IsBooted)
        {
            _loadInstance.Finish();
            Plugin.logger.LogDebug("Nebula Mods Manager is finished");
        }
    }

    internal static void BootStart()
    {
        IsFinished();
        ExecuteAllModUsages();
    }
}