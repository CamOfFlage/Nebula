using Noname.Worldless;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatSystemManaged
{
    public CombatSystem CombatSystem { get; }
    public FighterAven Aven { get; }
    public FighterEdda Edda { get; }
    public Il2CppSystem.Collections.Generic.Dictionary<CombatSkill.Skill, CombatSkill> SkillTreeSkills;
    public CombatSkillManager SkillManager;
    
    public int DarkEssence
    {
        get
        {
            return CombatSystem.progression.skills.GetNumEssences(Faction.Dark);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > DarkEssence)
            {
                int addAmount = value - DarkEssence;
                CombatSystem.progression.skills.IncreaseEssences(Faction.Dark, addAmount);
            }

            if (value < DarkEssence)
            {
                int removeAmount = DarkEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    CombatSystem.progression.skills.DecreaseEssences(Faction.Dark);
                }
            }
        }
    }
    
    public int LightEssence
    {
        get
        {
            return CombatSystem.progression.skills.GetNumEssences(Faction.Light);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > LightEssence)
            {
                int addAmount = value - LightEssence;
                CombatSystem.progression.skills.IncreaseEssences(Faction.Light, addAmount);
            }

            if (value < LightEssence)
            {
                int removeAmount = LightEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    CombatSystem.progression.skills.DecreaseEssences(Faction.Light);
                }
            }
        }
    }
    
    public int HybridEssence
    {
        get
        {
            return CombatSystem.progression.skills.GetNumEssences(Faction.Hybrid);
        }
        set
        {
            if (value < 0 || value > 99)
            {
                throw new IndexOutOfRangeException("Essence must be between 0 and 99");
            }

            if (value > HybridEssence)
            {
                int addAmount = value - HybridEssence;
                CombatSystem.progression.skills.IncreaseEssences(Faction.Hybrid, addAmount);
            }

            if (value < HybridEssence)
            {
                int removeAmount = HybridEssence - value;
                for (int i = 0; i < removeAmount; i++)
                {
                    CombatSystem.progression.skills.DecreaseEssences(Faction.Hybrid);
                }
            }
        }
    }
    
    

    public CombatSystemManaged(CombatSystem combatSystem)
    {
        this.CombatSystem = combatSystem;
        Aven = combatSystem.transform.FindChild("FighterAven").GetComponent<FighterAven>();
        Edda = combatSystem.transform.FindChild("FighterEdda").GetComponent<FighterEdda>();
        SkillTreeSkills = combatSystem.progression.skills._combatSkillsDictionary._dict;
        SkillManager = new CombatSkillManager(combatSystem.progression.skills);
    }
}