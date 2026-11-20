using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Nebula.GameSystems;

namespace Nebula.Reflection;

internal class NebulaMod : Loadable
{
    private Assembly _assembly;
    private BasePlugin _plugin;
    private PluginInfo _pluginInfo;

    private readonly HashSet<Type> _usingNebulaReflection;

    public NebulaMod(Assembly assembly, BasePlugin plugin, PluginInfo pluginInfo, HashSet<Type> usingNebulaReflection)
    {
        _assembly = assembly;
        _plugin = plugin;
        _pluginInfo = pluginInfo;
        _usingNebulaReflection = usingNebulaReflection;
    }

    public void ManageTypes()
    {
        Plugin.logger.LogDebug("Managing types");
        ExecuteAllReflectionUsages();
    }

    private async void ExecuteAllReflectionUsages()
    {
        //TODO: Have it keep track of which one it failed on and have it still try the rest
        Plugin.logger.LogDebug("Executing all reflection usage");
        try
        {
            await Task.Run(() =>
            {
                foreach (Type type in _usingNebulaReflection)
                {
                    Plugin.logger.LogDebug($"Managing type {type.Name}");
                    if (NebulaReflectionTypes.ManagementActions.ContainsKey(type.BaseType))
                    {
                        Plugin.logger.LogDebug($"{type.Name} has valid key");
                        NebulaReflectionTypes.ManagementActions[type.BaseType].Invoke(null, new object[] { type });
                    }
                }
            });
        }
        catch (Exception e)
        {
            _plugin.Log.LogError("An exception occured while trying to load Nebula Reflection usages, " +
                                 "some features may not work correctly.");
            _plugin.Log.LogError(e.ToString());
        }

        FinishLoad();
    }
}