using Noname;
using UnityEngine;

namespace Nebula.Eva;

public class EvaManager
{
    public EvaListener EvaListener;
    public Dictionary<String, EvaTrack[]> TracksByName = new Dictionary<String, EvaTrack[]>();
    public Dictionary<AnimationClip, EvaTrack[]> TracksByClip = new Dictionary<AnimationClip, EvaTrack[]>();
    public AnimationClip[] AnimationClips;
    private Dictionary<EvaClip, EvaTrack> _clipLocations;

    public EvaManager(EvaListener evaListener)
    {
        EvaListener = evaListener;
        _clipLocations = GetClipLocations();
        AnimationClips = GetAllAnimClips();
        TracksByClip = GetTracksByAnim();
        foreach (KeyValuePair<AnimationClip, EvaTrack[]> kvp in TracksByClip)
        {
            TracksByName.Add(kvp.Key.name, kvp.Value);
        }
    }

    private AnimationClip[] GetAllAnimClips()
    {
        List<AnimationClip> animationClips = new List<AnimationClip>();
        List<AnimationClip> allClips = new List<AnimationClip>();

        foreach (EvaTracks evaTracks in EvaListener.evaTracks)
        {
            foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
            {
                allClips.Add(animationTracks.clip);
            }
        }
        
        animationClips = allClips.Distinct().ToList();
        return animationClips.ToArray();
    }

    private Dictionary<EvaClip, EvaTrack> GetClipLocations()
    {
        Dictionary<EvaClip, EvaTrack> clipLocations = new Dictionary<EvaClip, EvaTrack>();
        
        foreach (EvaTracks evaTracks in EvaListener.evaTracks)
        {
            foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
            {
                foreach (EvaTrack track in animationTracks.tracks)
                {
                    foreach (EvaClip clip in track.clips)
                    {
                        clipLocations.Add(clip, track);
                    }
                }
            }
        }
        return clipLocations;
    }

    private Dictionary<AnimationClip, EvaTrack[]> GetTracksByAnim()
    {
        Dictionary<AnimationClip, List<EvaTrack>> evaTracksMap = new Dictionary<AnimationClip, List<EvaTrack>>();
        foreach (EvaTracks evaTracks in EvaListener.evaTracks)
        {
            foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
            {
                if (!evaTracksMap.ContainsKey(animationTracks.clip))
                {
                    List<EvaTrack> tracks = new List<EvaTrack>();
                    foreach (EvaTrack track in animationTracks.tracks)
                    {
                        tracks.Add(track);
                    }
                    evaTracksMap.Add(animationTracks.clip, tracks);
                }
                else
                {
                    List<EvaTrack> tracks = new List<EvaTrack>();
                    foreach (EvaTrack track in animationTracks.tracks)
                    {
                        tracks.Add(track);
                    }
                    evaTracksMap[animationTracks.clip].AddRange(tracks);
                }
            }
        }

        Dictionary<AnimationClip, EvaTrack[]> tracksByClip;
        tracksByClip = evaTracksMap.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
        return tracksByClip;
    }

    private AnimationTracks[] GetAllAnimTracks()
    {
        List<AnimationTracks> animationTracksList = new List<AnimationTracks>();
        foreach (EvaTracks evaTracks in EvaListener.evaTracks)
        {
            foreach (AnimationTracks animationTrack in evaTracks.animationTracks)
            {
                animationTracksList.Add(animationTrack);
            }
        }
        return animationTracksList.ToArray();
    }
}