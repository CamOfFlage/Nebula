using Noname;
using UnityEngine;

namespace Nebula.Eva;

public class EvaManager
{
    public EvaListener EvaListener;
    public Dictionary<String, EvaTrack[]> TracksByName = new Dictionary<String, EvaTrack[]>();
    private Dictionary<AnimationClip, EvaTrack[]> TracksByClip = new Dictionary<AnimationClip, EvaTrack[]>();
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
                Plugin.logger.LogMessage(animationTracks.clip.name + " checking...");
                if (!evaTracksMap.Keys.Any(key =>  key.name.Equals(animationTracks.clip.name)))
                {
                    Plugin.logger.LogMessage(animationTracks.clip.name + " does not exist");
                    List<EvaTrack> tracks = new List<EvaTrack>();
                    foreach (EvaTrack track in animationTracks.tracks)
                    {
                        tracks.Add(track);
                    }
                    evaTracksMap.Add(animationTracks.clip, tracks);
                    Plugin.logger.LogMessage(animationTracks.clip.name + " added");
                }
                else
                {
                    Plugin.logger.LogMessage(animationTracks.clip.name + " already exists");
                    List<EvaTrack> tracks = new List<EvaTrack>();
                    foreach (EvaTrack track in animationTracks.tracks)
                    {
                        tracks.Add(track);
                    }

                    AnimationClip? keyClip = null;
                    foreach (KeyValuePair<AnimationClip, List<EvaTrack>> kvp in evaTracksMap)
                    {
                        if (animationTracks.clip.name.Equals(kvp.Key.name))
                        {
                            keyClip = kvp.Key;
                        }
                    }

                    if (keyClip != null)
                    {
                        evaTracksMap[keyClip]
                            .AddRange(
                                tracks); //This needs to be by name. It does not reconize them as the same clip unless they are EXACTLY the same
                        Plugin.logger.LogMessage("Added");
                    }
                    else
                    {
                        Plugin.logger.LogError("Failed to connect " + animationTracks.clip.name + " to the existing array");
                    }
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