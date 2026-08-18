using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class Attack
{
    private EvaTrack[] evaTracks;
    public Attack(EvaListener evaListener, AnimationClip animationClip)
    {
        evaTracks = GetEvaTracks(evaListener, animationClip);
    }

    public static Attack[] GetAttacks(OffensiveAction offensiveAction)
    {
        Fighter fighter = offensiveAction._fighter;
        FighterManager fighterManager = new FighterManager(fighter);
        EvaListener evaListener = fighter._evaListener;
        List<Attack> attacks = new List<Attack>();
        AnimationClip[] animationClips = GetAnimClipsFromOffensiveAction(offensiveAction, fighterManager);
        foreach (AnimationClip animationClip in animationClips)
        {
            attacks.Add(new Attack(evaListener, animationClip));
        }
        return attacks.ToArray();
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

    private static AnimationClip[] GetAnimClipsFromOffensiveAction(OffensiveAction offensiveAction, FighterManager fighter)
    {
        StyleHandler styleHandler = fighter.GetComponent<StyleHandler>();
        List<AnimationClip> clips = new List<AnimationClip>();
        if (styleHandler != null) //Only players have StyleHandler
        {
            Plugin.logger.LogMessage("StyleHandler found");
            foreach (AttackAbsorbParams attackAbsorbParams in styleHandler._attackAbsorbParams)
            {
                foreach (AnimationClip animationClip in attackAbsorbParams.attacks)
                {
                    clips.Add(animationClip);
                }
            }
        }
        else //Enemy naming is more straight-forward
        {
            foreach (EvaTracks evaTracks in fighter.EvaListener.evaTracks)
            {
                foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
                {
                    if (animationTracks.clip.name.Equals(offensiveAction.name,
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        clips.Add(animationTracks.clip);
                    }
                }
            }
        }
        return clips.ToArray();
    }
}