using Noname.Worldless.Combat;

namespace Nebula.Combat;

/// <summary>
/// Wrapper class to manage a single <see cref="T:Noname.Worldless.Combat.OffensiveAction"/>
/// </summary>
/// <remarks>
/// A single OffensiveAction contains one "Kind" of attack.
/// For enemies, this almost always is just the one attack.
/// For the player, one OffensiveAction contains each attack in a combo.
/// </remarks>
public class OffensiveActionManager
{
    /// <summary>
    /// The base OffensiveAction
    /// </summary>
    /// <remarks>
    /// You shouldn't need this unless you are doing something more advanced, most functionality is covered already
    /// </remarks>
    public OffensiveAction OffensiveAction { get; }
    /// <summary>
    /// All the attacks linked to the <see cref="OffensiveAction"/>
    /// </summary>
    public AttackManager[] Attacks;
    private Fighter Fighter { get; }
    
    /// <summary>
    /// Initialises a new OffensiveActionManager from the provided action
    /// </summary>
    /// <param name="offensiveAction">The action to create the wrapper from</param>
    public OffensiveActionManager(OffensiveAction offensiveAction)
    {
        this.OffensiveAction = offensiveAction;
        Fighter = OffensiveAction._fighter;
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }
    
    /// <summary>
    /// Initialises a new OffensiveActionManager by searching for an internal name
    /// </summary>
    /// <param name="fighter">The fighter that has the desired OffensiveAction</param>
    /// <param name="attackName">The exact internal name of the desired OffensiveAction</param>
    /// <exception cref="Exception">
    /// Will throw an exception if no attack is found
    /// </exception>
    public OffensiveActionManager(Fighter fighter, String attackName) //Attack name is the one from the relevant "OffensiveAction"
    {
        this.Fighter = fighter;
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }

    /// <summary>
    /// Initialises a new OffensiveActionManager using a FighterManager by searching for an internal name
    /// </summary>
    /// <param name="fighterManager">The </param>
    /// <param name="attackName"></param>
    /// <exception cref="Exception">
    /// Will throw an exception if no attack is found
    /// </exception>
    public OffensiveActionManager(FighterManager fighterManager, String attackName)
    {
        Fighter = fighterManager.Fighter;
        fighterManager = new FighterManager(Fighter);
        OffensiveAction = FindOffensiveActionByName(attackName);
        Attacks = AttackManager.GetAttacks(OffensiveAction);
    }

    private OffensiveAction FindOffensiveActionByName(string name)
    {
        OffensiveHandler offensiveHandler = Fighter.offensiveHandler;
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

    /// <summary>
    /// Creates an OffensiveActionManager for every <see cref="OffensiveAction"/> on the Fighter.
    /// </summary>
    /// <returns>
    /// OffensiveActionManagers for every <see cref="T:Noname.Worldless.Combat.OffensiveAction"/>
    /// possessed by the given <see cref="T:Noname.Worldless.Combat.Fighter"/>
    /// </returns>
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