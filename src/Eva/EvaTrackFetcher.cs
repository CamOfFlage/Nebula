using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine.Animations;

using Noname;

namespace Nebula.Eva;

public class EvaTrackFetcher
{
    public EvaTrack FetchTrack(EvaListener evaListener, String evaTrackType, String animationName, int _evaTrack)
    {
        EvaTracks evaTracks = null;
        Il2CppReferenceArray<EvaTracks> evaTracksArray = evaListener.evaTracks;
        for (int i = 0; i < evaTracksArray.Length; i++)
        {
            Plugin.logger.LogMessage(evaTracksArray[i].name);
            Plugin.logger.LogMessage(evaTrackType);
            if (evaTracksArray[i].name == evaTrackType)
            {
                evaTracks = evaTracksArray[i];
                break;
            }
        }

        if (evaTracks == null)
        {
            throw new Exception("No EVA tracks found");
        }

        AnimationTracks[] animationTracksArray = evaTracks.animationTracks.ToArray();
        Dictionary<String, AnimationTracks> animationTracksMap = new Dictionary<string, AnimationTracks>();
        foreach (AnimationTracks tracks in animationTracksArray)
        {
            String animName = tracks.clip.name;
            animationTracksMap.Add(animName, tracks);
        }

        AnimationTracks animationTracks = animationTracksMap[animationName];

        EvaTrack evaTrack = animationTracks.tracks[_evaTrack];
        Plugin.logger.LogMessage(evaTrack.name);
        return evaTrack;
    }
}