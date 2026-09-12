using Il2CppInterop.Runtime.InteropTypes;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

/// <summary>
/// Wrapper class to manage a single <see cref="Noname.Worldless.Combat.Fighter"/> instance
/// </summary>
public class FighterManager
{
    /// <summary>
    /// The base <see cref="Fighter"/>
    /// </summary>
    /// <remarks>
    /// You shouldn't need this unless you are doing something more advanced, most functionality is covered already
    /// </remarks>
    public Fighter Fighter { get; private set; }
    /// <summary>
    /// The <see cref="Noname.EvaListener"/> of the fighter
    /// </summary>
    public EvaListener EvaListener;
    /// <summary>
    /// Get the all the <see cref="OffensiveActionManager"/> by their name
    /// </summary>
    /// <remarks>
    /// The name is the exact internal name for the <see cref="Noname.Worldless.Combat.OffensiveAction"/>
    /// </remarks>
    public Dictionary<string, OffensiveActionManager> OffensiveActions = new Dictionary<string, OffensiveActionManager>();

    /// <summary>
    /// Initialises a new <see cref="FighterManager"/> from a base <see cref="Fighter"/>
    /// </summary>
    /// <param name="fighter">The Fighter to wrap</param>
    public FighterManager(Fighter fighter)
    {
        this.Fighter = fighter;
        EvaListener = fighter.evaListener;
        OffensiveActionManager[] offensiveActions = OffensiveActionManager.GetAllOffensiveActions(fighter);
        foreach (OffensiveActionManager offensiveAction in offensiveActions)
        {
            OffensiveActions.Add(offensiveAction.OffensiveAction.name, offensiveAction);
        }
    }

    /// <summary>
    /// Retrieves the primary model for the fighter
    /// </summary>
    /// <returns>The GameObject of the model</returns>
    public GameObject GetModel()
    {
        Model model = Fighter.model;
        return model.gameObject;
    }

    /// <summary>
    /// Searches through the fighter's components for one of the given type
    /// </summary>
    /// <typeparam name="T">The type of fighter component to search for</typeparam>
    /// <returns>Instance of the given type, or null if the fighter does not possess the given type</returns>
    public T GetComponent<T>() where T : Il2CppObjectBase
    {
        foreach (IFighterComponent component in Fighter._fighterComponents)
        {
            T componentType = component.TryCast<T>();
            if (componentType != null)
            {
                return componentType;
            }
        }

        return null;
    }
}