using Il2CppInterop.Runtime.InteropTypes;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class FighterManager
{
    public Fighter Fighter { get; private set; }
    public EvaListener EvaListener;
    public Dictionary<string, OffensiveActionManager> OffensiveActions = new Dictionary<string, OffensiveActionManager>();

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

    public GameObject GetModel()
    {
        Model model = Fighter.model;
        return model.gameObject;
    }

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