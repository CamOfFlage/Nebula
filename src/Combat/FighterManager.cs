using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula.Combat;

public class FighterManager
{
    public Fighter fighter { get; private set; }

    public FighterManager(Fighter fighter)
    {
        this.fighter = fighter;
    }

    public GameObject getModel()
    {
        /*
        GameObject fighterGameObject = fighter.gameObject;
        Transform modelsTransform = fighterGameObject.transform.FindChild("Model");
        if (modelsTransform == null)
        {
            Plugin.logger.LogError("Model transform not found");
        }
        GameObject models = modelsTransform.gameObject;
        if (models == null)
        {
            Plugin.logger.LogError("Model not found");
        }
        GameObject modelGameObject = models.transform.FindChild(modelName).gameObject;
        if (modelGameObject == null)
        {
            Plugin.logger.LogError("Model is null");
        }
        */
        Model model = fighter.model;
        return model.gameObject;
    }
}