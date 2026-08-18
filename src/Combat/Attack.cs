using Noname;
using UnityEngine;

namespace Nebula.Combat;

public class Attack
{
    public Attack(EvaListener evaListener, AnimationClip animationClip)
    {
        
    }
    
    private EvaTrack[] GetEvaTracks(EvaListener evaListener,  AnimationClip animClip)
    {
        List<EvaTrack> tracks = new List<EvaTrack>();
        foreach (EvaTracks evaTracks in evaListener.evaTracks)
        {
            foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
            {
                if (animationTracks.clip.name == animClip.name)
                {
                    foreach (EvaTrack track in animationTracks.tracks)
                    {
                        tracks.Add(track);
                    }
                }
            }
        }
        return tracks.ToArray();
    }
}