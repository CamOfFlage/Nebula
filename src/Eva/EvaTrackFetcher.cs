using UnityEngine.Animations;

using Noname;

namespace WorldlessLibs.Eva;

public class EvaTrackFetcher
{
    public EvaTrack FetchTrack(EvaListener evaListener, String evaTrackType, String animationName, int _evaTrack)
    {
        EvaTracks evaTracks = null;
        foreach (EvaTracks _evaTracks in evaListener.evaTracks)
        {
            if (_evaTracks.name == evaTrackType)
            {
                evaTracks = _evaTracks;
                break;
            }
        }

        if (evaTracks == null)
        {
            throw new NullReferenceException(); //Todo: Add a custom exception here later
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
        return evaTrack;
    }
}