using Noname.Worldless.Combat;
using UnityEngine;

namespace WorldlessLibs.Combat;

public class FighterManager
{
    public Fighter fighter { get; private set; }

    public FighterManager(Fighter fighter)
    {
        this.fighter = fighter;
    }

    public GameObject getModel(String modelName)
    {
        GameObject fighterGameObject = fighter.gameObject;
        GameObject models = fighterGameObject.transform.FindChild("Model").gameObject;
        GameObject modelGameObject = models.transform.FindChild( modelName).gameObject;
        return modelGameObject;
    }
}