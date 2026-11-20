using Noname;
using Noname.Worldless.UI;
using UnityEngine;

namespace Nebula.GameSystems;

internal static class LoadingScreenController
{
    internal static LoadingIcon _loadingIcon;
    internal static FullScreenFadeHandler _fadeHandler;
    internal static UI_Manager _ui;

    public static void SetNebulaLoad(bool start)
    {
        _loadingIcon.SetLoadingIconActive(start);
        _ui.gameObject.SetActive(start);
        if (start)
        {
            _fadeHandler.FadeOut(1);
        }
        else
        {
            _fadeHandler.FadeIn(1);
        }
    }
}