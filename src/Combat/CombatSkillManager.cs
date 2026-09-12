using Noname.Worldless.Combat;

namespace Nebula.Combat;

/// <summary>
/// Allows for the adding and revoking of combat abilities
/// </summary>
public class CombatSkillManager
{
    private SkillsProgression Progression { get; }
    
    public CombatSkillManager(SkillsProgression progression)
    {
        this.Progression = progression;
    }

    public void SetSkillUnlockValue(CombatSkill.Skill skill, bool unlocked)
    {
        Progression.saveData.skills[skill] = unlocked;
    }
}