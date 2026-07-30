using BepInEx;
using BepInEx.Unity.IL2CPP;

namespace WorldlessLibs
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        public override void Load()
        {
            Log.LogMessage($"{PluginInfo.PLUGIN_NAME} loaded!");
        }
    }
    
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.CamOfFlage.WorldlessLib";
        public const string PLUGIN_NAME = "WorldlessLib";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}