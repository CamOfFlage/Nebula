using Noname.Worldless.Combat;
using Noname;
using UnityEngine;

namespace Nebula.Combat;

public class TemplateManager
{
    public CombatTemplate managedTemplate {get; private set;}

    public Fighter fighter
    {
        get
        {
            return managedTemplate.enemy.fighter;
        }
    }

    public FighterEnemy fighterEnemy
    {
        get
        {
            return managedTemplate.enemy;
        }
    }
    
    public EvaListener evaListener
    {
        get
        {
            return fighter.model.gameObject.GetComponent<EvaListener>();
        }
    }

    public String id
    {
        get
        {
            return managedTemplate.id;
        }
    }
    
    public TemplateManager(CombatTemplate managedTemplate)
    {
        this.managedTemplate = managedTemplate;
    }
}