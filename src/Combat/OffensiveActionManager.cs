using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class OffensiveActionManager
{
    public OffensiveAction OffensiveAction;
    public AttackManager[] Attacks;
    private Fighter fighter;
    
    public OffensiveActionManager(OffensiveAction offensiveAction)
    {
        this.OffensiveAction = offensiveAction;
        fighter = OffensiveAction._fighter;
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }

    /*
    public OffensiveActionManager(Fighter fighter, AnimationTracks animationTracks)
    {
        this.fighter = fighter;
        OffensiveAction = GetOffensiveActionFromAnim(animationTracks.clip);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }
    */

    public OffensiveActionManager(Fighter fighter, String attackName) //Attack name is the one from the relevant "OffensiveAction"
    {
        this.fighter = fighter;
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }

    /*
    public OffensiveActionManager(FighterManager fighterManager, AnimationTracks animationTracks)
    {
        fighter = fighterManager.fighter;
        OffensiveAction = GetOffensiveActionFromAnim(animationTracks.clip);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }
    */

    public OffensiveActionManager(FighterManager fighterManager, String attackName)
    {
        fighter = fighterManager.fighter;
        fighterManager = new FighterManager(fighter);
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
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
            if (offensiveAction.name.Equals(name))
            {
                return offensiveAction;
            }
        }
        throw new Exception("No Offensive Action found with given name: " + name);
    }
    
    //All of this part is a very strange way of getting these references, if a better way is found: fix this
    /*
    private OffensiveAction GetOffensiveActionFromAnim(AnimationClip animClip, FighterManager fighterManager)
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
                    if (animationClip.name.Equals(animName))
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
    */

    public static OffensiveActionManager[] GetAllOffensiveActions(Fighter fighter)
    {
        List<OffensiveActionManager> offensiveActionManagers = new List<OffensiveActionManager>();
        foreach (OffensiveAction offensiveAction in fighter.offensiveHandler._actions.actions)
        {
            offensiveActionManagers.Add(new OffensiveActionManager(offensiveAction));
        }
        return offensiveActionManagers.ToArray();
    }
}