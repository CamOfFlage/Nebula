using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Nebula.Combat;
using Nebula.Patching;
using UnityEngine;
using Nebula.ResourceManager;
using UnityEngine.SceneManagement;

namespace Nebula
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        static internal ManualLogSource logger;
        
        public override void Load()
        {
            ResourceEvents.Instance = new ResourceEvents();
            
            logger = Log;
            Log.LogMessage($"{PluginInfo.PLUGIN_NAME} loaded!");
            
            ClassInjector.RegisterTypeInIl2Cpp<ModdingGameManager>();
            ClassInjector.RegisterTypeInIl2Cpp<BootChecker>();
            
            GameObject manager = new GameObject("NebulaGameManager");
            GameInfo.GameManager = manager;
            manager.hideFlags = HideFlags.HideAndDontSave;
            manager.AddComponent<ModdingGameManager>();
            manager.AddComponent<BootChecker>();
            
            CombatTemplatePatchHandler.instance = new CombatTemplatePatchHandler();
        }
    }

    internal static class PluginInfo
    {
        public const string PLUGIN_GUID = "com.CamOfFlage.Nebula";
        public const string PLUGIN_NAME = "Nebula";
        public const string PLUGIN_VERSION = "0.0.1";
    }
}