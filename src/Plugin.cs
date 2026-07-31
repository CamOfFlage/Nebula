using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Logger = BepInEx.Logging.Logger;

namespace WorldlessLibs
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        static internal ManualLogSource logger;
        
        public override void Load()
        {
            logger = Log;
            Log.LogMessage($"{PluginInfo.PLUGIN_NAME} loaded!");
            ClassInjector.RegisterTypeInIl2Cpp<ModdingGameManager>();
            ClassInjector.RegisterTypeInIl2Cpp<BootChecker>();
            
            GameObject manager = new GameObject("WorldlessLibGameManager");
            GameInfo.GameManager = manager;
            manager.hideFlags = HideFlags.HideAndDontSave;
            manager.AddComponent<ModdingGameManager>();
            manager.AddComponent<BootChecker>();
        }
    }
    
    public static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.CamOfFlage.WorldlessLib";
        public const string PLUGIN_NAME = "WorldlessLib";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}