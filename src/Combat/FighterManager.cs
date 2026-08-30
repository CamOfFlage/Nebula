using Il2CppInterop.Runtime.InteropTypes;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class FighterManager
{
    public Fighter fighter { get; private set; }
    public EvaListener EvaListener;
    public Dictionary<string, OffensiveActionManager> offensiveActions = new Dictionary<string, OffensiveActionManager>();

    public FighterManager(Fighter fighter)
    {
        this.fighter = fighter;
        EvaListener = fighter.evaListener;
        OffensiveActionManager[] _offensiveActions = OffensiveActionManager.GetAllOffensiveActions(fighter);
        foreach (OffensiveActionManager offensiveAction in _offensiveActions)
        {
            offensiveActions.Add(offensiveAction.OffensiveAction.name, offensiveAction);
        }
    }

    public GameObject getModel()
    {
        Model model = fighter.model;
        return model.gameObject;
    }

    public T GetComponent<T>() where T : Il2CppObjectBase
    {
        foreach (IFighterComponent component in fighter._fighterComponents)
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