using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class AttackManager
{
    private EvaTrack[] _evaTracks { get; }
    public EvaClip[] EvaClips { get; private set; }
    public string AttackName { get; private set; }
    public AnimationClip AnimationClip { get; private set; }
    public List<HitClip> HitClips { get; } = new List<HitClip>();
    public List<EffectClip> Warnings { get; } = new List<EffectClip>();
    public List<EffectClip> Effects { get; } = new List<EffectClip>();
    public List<SfxClip> SfxClips { get; } = new List<SfxClip>();
    public List<ProjectileClip> ProjectileClips { get; } =  new List<ProjectileClip>();
    
    public AttackManager(EvaListener evaListener, AnimationClip animationClip)
    {
        _evaTracks = GetEvaTracks(evaListener, animationClip);
        
        List<EvaClip> _evaClips = new List<EvaClip>();
        foreach (EvaTrack evaTrack in _evaTracks)
        {
            foreach (EvaClip evaClip in evaTrack.clips)
            {
                _evaClips.Add(evaClip);
            }
        }
        EvaClips = _evaClips.ToArray();
        
        this.AnimationClip = animationClip;
        AttackName = animationClip.name;
        Array.Sort(EvaClips, (x, y) => x.start.CompareTo(y.start));

        foreach (EvaClip clip in EvaClips)
        {
            DistributeClip(clip);
        }
    }

    public static AttackManager[] GetAttacks(OffensiveAction offensiveAction)
    {
        Fighter fighter = offensiveAction._fighter;
        EvaListener evaListener = fighter._evaListener;
        List<AttackManager> attacks = new List<AttackManager>();
        AnimationClip[] animationClips = GetAnimClipsFromOffensiveAction(offensiveAction, fighter);
        animationClips = animationClips.Distinct().ToArray();
        foreach (AnimationClip animationClip in animationClips)
        {
            attacks.Add(new AttackManager(evaListener, animationClip));
        }
        return attacks.ToArray();
    }
    
    private EvaTrack[] GetEvaTracks(EvaListener evaListener,  AnimationClip animClip)
    {
        List<EvaTrack> tracks = new List<EvaTrack>();
        foreach (EvaTracks _evaTracks in evaListener.evaTracks)
        {
            foreach (AnimationTracks animationTracks in _evaTracks.animationTracks)
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
        foreach (EvaTrack evaTrack in _evaTracks)
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
    
    private static AnimationClip[] GetAnimClipsFromOffensiveAction(OffensiveAction offensiveAction, Fighter fighter)
    {
        StyleHandler styleHandler = fighter.transform.FindChild("Offensive").gameObject.GetComponent<StyleHandler>();
        List<AnimationClip> clips = new List<AnimationClip>();
        if (styleHandler != null) //Only players have StyleHandler
        {
            Plugin.logger.LogMessage("Style handler used");
            foreach (AttackAbsorbParams attackAbsorbParams in styleHandler._attackAbsorbParams)
            {
                Plugin.logger.LogMessage(attackAbsorbParams.name);
                if (attackAbsorbParams.name.Equals(offensiveAction.name, StringComparison.InvariantCultureIgnoreCase))
                {
                    Plugin.logger.LogMessage(offensiveAction.name + " matches");
                    foreach (AnimationClip animationClip in attackAbsorbParams.attacks)
                    {
                        Plugin.logger.LogMessage(animationClip.name + " included");
                        clips.Add(animationClip);
                    }
                }
            }
        }
        else //Enemy naming is more straight-forward
        {
            foreach (EvaTracks evaTracks in fighter.evaListener.evaTracks)
            {
                foreach (AnimationTracks animationTracks in evaTracks.animationTracks)
                {
                    if (animationTracks.clip.name.Equals(offensiveAction.name,
                            StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (!clips.Any(clip => clip.name.Equals(animationTracks.clip.name)))
                        {
                            clips.Add(animationTracks.clip);
                        }
                    }
                }
            }
        }
        return clips.Distinct().ToArray();
    }

    private void DistributeClip(EvaClip clip)
    {
        if (clip.TryCast<EffectClip>() != null)
        {
            EffectClip effectClip = clip.TryCast<EffectClip>();
            if (GameInfo.WarningKeys.Contains(effectClip.addressableKey.key))
            {
                Warnings.Add(effectClip);
            }
            else
            {
                Effects.Add(effectClip);
            }
            return;
        }
        
        if (clip.TryCast<SfxClip>() != null)
        {
            SfxClip sfxClip = clip.TryCast<SfxClip>();
            SfxClips.Add(sfxClip);
            return;
        }
        
        if (clip.TryCast<HitClip>() != null)
        {
            HitClip hitClip = clip.TryCast<HitClip>();
            HitClips.Add(hitClip);
        }
        
        if (clip.TryCast<ProjectileClip>() != null)
        {
            ProjectileClip projectileClip = clip.TryCast<ProjectileClip>();
            ProjectileClips.Add(projectileClip);
        }
    }
}