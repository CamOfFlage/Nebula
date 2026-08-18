using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class OffensiveActionManager
{
    public OffensiveAction OffensiveAction;
    public Attack[] Attacks;
    private Fighter fighter;
    private FighterManager fighterManager;
    
    public OffensiveActionManager(OffensiveAction offensiveAction)
    {
        this.OffensiveAction = offensiveAction;
        fighter = OffensiveAction._fighter;
        fighterManager = new FighterManager(fighter);
        Attacks = Attack.GetAttacks(OffensiveAction);
    }

    public OffensiveActionManager(Fighter fighter, AnimationTracks animationTracks)
    {
        this.fighter = fighter;
        fighterManager = new FighterManager(fighter);
        OffensiveAction = GetOffensiveActionFromAnim(animationTracks.clip);
        Attacks = Attack.GetAttacks(OffensiveAction);
    }

    public OffensiveActionManager(Fighter fighter, String attackName) //Attack name is the one from the relevant "OffensiveAction"
    {
        this.fighter = fighter;
        fighterManager = new FighterManager(fighter);
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = Attack.GetAttacks(OffensiveAction);
    }

    public OffensiveActionManager(FighterManager fighterManager, AnimationTracks animationTracks)
    {
        fighter = fighterManager.fighter;
        fighterManager = new FighterManager(fighter);
        OffensiveAction = GetOffensiveActionFromAnim(animationTracks.clip);
        Attacks = Attack.GetAttacks(OffensiveAction);
    }

    public OffensiveActionManager(FighterManager fighterManager, String attackName)
    {
        fighter = fighterManager.fighter;
        fighterManager = new FighterManager(fighter);
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = Attack.GetAttacks(OffensiveAction);
    }

    private OffensiveAction FindOffensiveActionByName(string name)
    {
        OffensiveHandler offensiveHandler = fighter.offensiveHandler;
        OffensiveAction[] offensiveActions = new OffensiveAction[offensiveHandler.offensiveActions.Length];
        for (int i = 0; i < offensiveActions.Length; i++)
        {
            offensiveActions[i] = offensiveHandler.offensiveActions[i].TryCast<OffensiveAction>();
        }

        foreach (OffensiveAction offensiveAction in offensiveActions)
        {
            if (offensiveAction.name == name)
            {
                return offensiveAction;
            }
        }
        throw new Exception("No Offensive Action found with given name: " + name);
    }
    
    //All of this part is a very strange way of getting these references, if a better way is found: fix this
    private OffensiveAction GetOffensiveActionFromAnim(AnimationClip animClip)
    {
        string animName = animClip.name;
        StyleHandler styleHandler = fighterManager.GetComponent<StyleHandler>();
        if (styleHandler != null) //Only players have StyleHandler
        {
            Plugin.logger.LogMessage("StyleHandler found");
            foreach (AttackAbsorbParams attackAbsorbParams in styleHandler._attackAbsorbParams)
            {
                foreach (AnimationClip animationClip in attackAbsorbParams.attacks)
                {
                    if (animationClip.name == animName)
                    {
                        return FindOffensiveActionByName(attackAbsorbParams.name);
                    }
                }
            }

            throw new Exception("No Offensive Action matches given name: " + animName);
        }
        else //Enemy naming is more straight-forward
        {
            Plugin.logger.LogMessage("Using enemy pathway");
            return FindOffensiveActionByName(animName);
        }
    }

}