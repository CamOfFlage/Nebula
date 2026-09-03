using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class AttackManager
{
    private EvaTrack[] evaTracks;
    public EvaClip[] evaClips;
    public string attackName;
    public AnimationClip animationClip;
    public List<HitClip> hitClips = new List<HitClip>();
    public List<EffectClip> warnings = new List<EffectClip>();
    public List<EffectClip> effects = new List<EffectClip>();
    public List<SfxClip> sfxClips = new List<SfxClip>();
    public List<ProjectileClip> projectileClips =  new List<ProjectileClip>();
    
    public AttackManager(EvaListener evaListener, AnimationClip animationClip)
    {
        evaTracks = GetEvaTracks(evaListener, animationClip);
        
        List<EvaClip> _evaClips = new List<EvaClip>();
        foreach (EvaTrack evaTrack in evaTracks)
        {
            foreach (EvaClip evaClip in evaTrack.clips)
            {
                _evaClips.Add(evaClip);
            }
        }
        evaClips = _evaClips.ToArray();
        
        this.animationClip = animationClip;
        attackName = animationClip.name;
        Array.Sort(evaClips, (x, y) => x.start.CompareTo(y.start));

        foreach (EvaClip clip in evaClips)
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
                warnings.Add(effectClip);
            }
            else
            {
                effects.Add(effectClip);
            }
            return;
        }
        
        if (clip.TryCast<SfxClip>() != null)
        {
            SfxClip sfxClip = clip.TryCast<SfxClip>();
            sfxClips.Add(sfxClip);
            return;
        }
        
        if (clip.TryCast<HitClip>() != null)
        {
            HitClip hitClip = clip.TryCast<HitClip>();
            hitClips.Add(hitClip);
        }
        
        if (clip.TryCast<ProjectileClip>() != null)
        {
            ProjectileClip projectileClip = clip.TryCast<ProjectileClip>();
            projectileClips.Add(projectileClip);
        }
    }
}