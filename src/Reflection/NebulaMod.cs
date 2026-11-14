using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using Nebula.Combat;
using UnityEngine;

namespace Nebula.Reflection;

internal class NebulaMod
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

    public void ExecuteAllReflectionUsages()
    {
        foreach (Type type in _usingNebulaReflection)
        {
            Plugin.logger.LogDebug($"Managing type {type.Name}");
            if (NebulaReflectionTypes.ManagementActions.ContainsKey(type.BaseType))
            {
                Plugin.logger.LogDebug($"{type.Name} has valid key");

                //GameObject go = new GameObject("temp");
                //FighterComponent fighterComponent = go.AddComponent<FighterComponent>();
                NebulaReflectionTypes.ManagementActions[type.BaseType].Invoke(null, new  object[] {type});
                //Destroy(go);
            }
        }
    }
}