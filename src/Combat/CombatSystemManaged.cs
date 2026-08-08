using Noname.Worldless.Combat;
using Noname.Worldless;

namespace Nebula.Combat;

public class CombatSystemManaged
{
    public CombatSystem combatSystem;
    public FighterAven aven;
    public FighterEdda edda;
    public Dictionary<CombatSkill.Skill, CombatSkill> skillTreeSkills;

    public int darkEssence
    {
        get
        {
            return combatSystem.progression.skills.GetNumEssences(Faction.Dark);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > darkEssence)
            {
                int addAmount = value - darkEssence;
                combatSystem.progression.skills.IncreaseEssences(Faction.Dark, addAmount);
            }

            if (value < darkEssence)
            {
                int removeAmount = darkEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    combatSystem.progression.skills.DecreaseEssences(Faction.Dark);
                }
            }
        }
    }
    
    public int lightEssence
    {
        get
        {
            return combatSystem.progression.skills.GetNumEssences(Faction.Light);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > lightEssence)
            {
                int addAmount = value - lightEssence;
                combatSystem.progression.skills.IncreaseEssences(Faction.Light, addAmount);
            }

            if (value < lightEssence)
            {
                int removeAmount = lightEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    combatSystem.progression.skills.DecreaseEssences(Faction.Light);
                }
            }
        }
    }
    
    public int hybridEssence
    {
        get
        {
            return combatSystem.progression.skills.GetNumEssences(Faction.Hybrid);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > hybridEssence)
            {
                int addAmount = value - hybridEssence;
                combatSystem.progression.skills.IncreaseEssences(Faction.Hybrid, addAmount);
            }

            if (value < hybridEssence)
            {
                int removeAmount = hybridEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    combatSystem.progression.skills.DecreaseEssences(Faction.Hybrid);
                }
            }
        }
    }
    
    

    public CombatSystemManaged(CombatSystem combatSystem)
    {
        this.combatSystem = combatSystem;
        aven = combatSystem.transform.FindChild("FighterAven").GetComponent<FighterAven>();
        edda = combatSystem.transform.FindChild("FighterEdda").GetComponent<FighterEdda>();
        skillTreeSkills = combatSystem.progression.skills._combatSkillsDictionary._dict;
    }
}