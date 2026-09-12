using Noname.Worldless;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

/// <summary>
/// Wrapper class for the <see cref="T:CombatSystem"/>
/// </summary>
public class CombatSystemManaged
{
    /// <summary>
    /// The base CombatSystem
    /// </summary>
    /// <remarks>
    /// You shouldn't need this unless you are doing something more advanced, most functionality is covered already
    /// </remarks>
    public CombatSystem CombatSystem { get; }
    /// <summary>
    /// The instance of Aven's <see cref="FighterAven"/>
    /// </summary>
    public FighterAven Aven { get; }
    /// <summary>
    /// The instance of Edda's <see cref="FighterEdda"/>
    /// </summary>
    public FighterEdda Edda { get; }
    /// <summary>
    /// Converts all skills to their corresponding skill tree information
    /// </summary>
    public Il2CppSystem.Collections.Generic.Dictionary<CombatSkill.Skill, CombatSkill> SkillTreeSkills;
    public CombatSkillManager SkillManager;
    
    /// <summary>
    /// Allows to change the amount of dark essence collected
    /// </summary>
    /// <remarks>
    /// Using this to increase essence count will permanently increase the number of acquired essence on the save slot
    /// </remarks>
    /// <exception cref="IndexOutOfRangeException">You cannot have an essence count below 0 or above 99</exception>
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
    
    /// <summary>
    /// Allows to change the amount of light essence collected
    /// </summary>
    /// <remarks>
    /// Using this to increase essence count will permanently increase the number of acquired essence on the save slot
    /// </remarks>
    /// <exception cref="IndexOutOfRangeException">You cannot have an essence count below 0 or above 99</exception>
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
    
    /// <summary>
    /// Allows to change the amount of hybrid essence collected
    /// </summary>
    /// <remarks>
    /// Using this to increase essence count will permanently increase the number of acquired essence on the save slot
    /// </remarks>
    /// <exception cref="IndexOutOfRangeException">You cannot have an essence count below 0 or above 99</exception>
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