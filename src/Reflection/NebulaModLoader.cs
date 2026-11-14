using System.Reflection;
using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace Nebula.Reflection;

internal class NebulaModLoader
{
    public Assembly Assembly {get; private set;}
    public BepInEx.PluginInfo PluginInfo {get; private set;}
    private BasePlugin _basePlugin;

    public NebulaModLoader(PluginInfo pluginInfo, Assembly assembly, BasePlugin basePlugin)
    {
        PluginInfo = pluginInfo;
        Assembly = assembly;
        _basePlugin = basePlugin;
        Plugin.logger.LogDebug($"Nebula Mod Loader: Trying to load {pluginInfo.Metadata.Name}");
    }

    public void Start()
    {
        UsesNebula();
    }

    private async void UsesNebula()
    {
        if (!NebulaDependent())
        {
            Plugin.logger.LogDebug($"Nebula Mod Loader: Resolved {PluginInfo.Metadata.Name} as not dependent");
            NebulaModsManager.RemoveModLoader(this);
            return;
        }
        Plugin.logger.LogDebug($"Nebula Mod Loader: Resolved {PluginInfo.Metadata.Name} as dependent");

        HashSet<Type> usingNebulaReflection = (
            from type in Assembly.GetTypes()
            where NebulaReflectionTypes.reflectionTypes.Any(type.IsSubclassOf)
            where !Attribute.IsDefined(type.Assembly, typeof(DisabledAttribute))
            select type).ToHashSet();

        if (usingNebulaReflection.Any())
        {
            Plugin.logger.LogDebug($"Nebula Mod Loader: Resolved {PluginInfo.Metadata.Name} as using Nebula Reflection");
            NebulaModsManager.SuccessModLoader(new NebulaMod(Assembly, _basePlugin, PluginInfo, usingNebulaReflection), this);
        }
        else
        {
            Plugin.logger.LogDebug($"Nebula Mod Loader: Resolved {PluginInfo.Metadata.Name} as not using Nebula Reflection");
            NebulaModsManager.RemoveModLoader(this);
        }
    }

    private bool NebulaDependent()
    {
        foreach (BepInDependency dependency in PluginInfo.Dependencies)
        {
            if (dependency.DependencyGUID == NebulaPluginInfo.PLUGIN_GUID)
            {
                return true;
            }
        }
        return false;
    }
}