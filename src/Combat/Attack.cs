using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class AttackManager
{
    private EvaTrack[] evaTracks;
    public string attackName;
    public AnimationClip animationClip;
    public HitClip[] hitClips;
    public EffectClip[] warnings;
    public EffectClip[] effects;
    //SFX
    //Projectiles
    
    public AttackManager(EvaListener evaListener, AnimationClip animationClip)
    {
        evaTracks = GetEvaTracks(evaListener, animationClip);
        this.animationClip = animationClip;
        hitClips = getAllTracksOfType<HitClip>();
        attackName = animationClip.name;
    }

    public static AttackManager[] GetAttacks(OffensiveAction offensiveAction)
    {
        Fighter fighter = offensiveAction._fighter;
        FighterManager fighterManager = new FighterManager(fighter);
        EvaListener evaListener = fighter._evaListener;
        List<AttackManager> attacks = new List<AttackManager>();
        AnimationClip[] animationClips = GetAnimClipsFromOffensiveAction(offensiveAction, fighterManager);
        foreach (AnimationClip animationClip in animationClips)
        {
            attacks.Add(new AttackManager(evaListener, animationClip));
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
                if (animationTracks.clip.name.Equals(animClip.name))
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

    private T[] getAllTracksOfType<T>() where T : EvaClip<T>
    {
        List<T> tracks = new List<T>();
        foreach (EvaTrack evaTrack in evaTracks)
        {
            foreach (EvaClip evaClip in evaTrack.clips)
            {
                if (evaClip.TryCast<T>() != null)
                {
                    tracks.Add(evaClip.TryCast<T>());
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
            foreach (AttackAbsorbParams attackAbsorbParams in styleHandler._attackAbsorbParams)
            {
                if (attackAbsorbParams.name.Equals(offensiveAction.name, StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (AnimationClip animationClip in attackAbsorbParams.attacks)
                    {
                        clips.Add(animationClip);
                    }
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