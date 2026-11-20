using System.Collections.Immutable;

namespace Nebula.GameSystems;

public static class NebulaLoadManager
{
    internal static bool NebulaLoadingComplete {get; private set;}
    internal static bool isFreezingMenu = false;
    private static readonly List<Loadable> WaitingOn =  new List<Loadable>();

    internal static void AddLoadable(Loadable loadable)
    {
        WaitingOn.Add(loadable);
        NebulaLoadingComplete = false;
    }

    internal static void FinishLoadable(Loadable loadable)
    {
        WaitingOn.Remove(loadable);
        if (IsLoadingComplete())
        {
            NebulaLoadFinish();
        }
    }

    private static bool IsLoadingComplete()
    {
        if (WaitingOn.Count == 0) return true;
        return WaitingOn.Count == 0;
    }

    private static void NebulaLoadFinish()
    {
        if (isFreezingMenu)
        {
            LoadingScreenController.SetNebulaLoad(false);
        }
        NebulaLoadingComplete = true;
        GameInfo.NebulaLoaded = true;
        NebulaGameEvents.InvokeNebulaLoadFinished();
    }
    
    internal static void MainMenuStart()
    {
        Plugin.logger.LogDebug("MainMenuStart");
        if (NebulaLoadingComplete) return;
        LoadingScreenController._ui = GameInfo.NavigationSystem._uiManager;
        LoadingScreenController.SetNebulaLoad(true);
        isFreezingMenu = true;
    }
}