using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using Il2CppInterop.Runtime.Injection;
using Nebula.Combat;

namespace Nebula
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BasePlugin
    {
        internal static ManualLogSource logger;
        
        public override void Load()
        {
            logger = Log;
            Log.LogMessage($"{PluginInfo.PLUGIN_NAME} loading...");
            
            ClassInjector.RegisterTypeInIl2Cpp<NebulaGameManager>();
            ClassInjector.RegisterTypeInIl2Cpp<BootChecker>();
            
            NebulaGameManager manager = AddComponent<NebulaGameManager>();
            GameInfo.GameManager = manager;
            BootChecker bootChecker = AddComponent<BootChecker>();
            GameInfo.BootChecker = bootChecker;
            
            GlobalCombatTemplatePatchHandler.instance = new GlobalCombatTemplatePatchHandler();
            ProjectilePatchHandler.Instance = new ProjectilePatchHandler();
            Log.LogMessage($"{PluginInfo.PLUGIN_NAME} loaded!");
        }
    }

    internal static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.CamOfFlage.Nebula";
        public const string PLUGIN_NAME = "Nebula";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}