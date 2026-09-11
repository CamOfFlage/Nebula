using Noname.Worldless.Combat;

namespace Nebula.Combat;

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