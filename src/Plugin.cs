using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Nebula.Combat;
using Nebula.GameSystem.Hooks;
using Nebula.Reflection;
using UnityEngine.Rendering;

namespace Nebula
{
    [BepInPlugin(NebulaPluginInfo.PLUGIN_GUID, NebulaPluginInfo.PLUGIN_NAME, NebulaPluginInfo.PLUGIN_VERSION)]
    internal class Plugin : BasePlugin
    {
        internal static ManualLogSource logger;
        internal static Harmony Harmony;
        
        public override void Load()
        {
            logger = Log;
            Log.LogMessage($"{NebulaPluginInfo.PLUGIN_NAME} loading...");
            
            ClassInjector.RegisterTypeInIl2Cpp<NebulaGameManager>();
            ClassInjector.RegisterTypeInIl2Cpp<BootChecker>();
            
            NebulaGameManager manager = AddComponent<NebulaGameManager>();
            GameInfo.GameManager = manager;
            BootChecker bootChecker = AddComponent<BootChecker>();
            GameInfo.BootChecker = bootChecker;
            
            GlobalCombatTemplatePatchHandler.instance = new GlobalCombatTemplatePatchHandler();
            ProjectilePatchHandler.Instance = new ProjectilePatchHandler();

            IL2CPPChainloader.Instance.PluginLoad += NebulaModsManager.OnNewPluginLoad;
            
            SplashScreen.Stop(SplashScreen.StopBehavior.StopImmediate);
            
            Harmony harmony = new Harmony(NebulaPluginInfo.PLUGIN_GUID);
            Harmony = harmony;
            harmony.PatchAll(typeof(SplashScreenRemoveHooks));
            harmony.PatchAll(typeof(TitleSlidesRemoveHooks));
            
            Log.LogMessage($"{NebulaPluginInfo.PLUGIN_NAME} loaded!");
        }
    }

    internal static class NebulaPluginInfo
    {
        public const string PLUGIN_GUID = "com.CamOfFlage.Nebula";
        public const string PLUGIN_NAME = "Nebula";
        public const string PLUGIN_VERSION = "0.1.1";
    }
}