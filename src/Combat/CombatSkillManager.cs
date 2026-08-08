using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class CombatSkillManager
{
    private SkillsProgression progression;
    
    public CombatSkillManager(SkillsProgression progression)
    {
        this.progression = progression;
    }

    public void SetSkillUnlockValue(CombatSkill.Skill skill, bool unlocked)
    {
        progression.saveData.skills[skill] = unlocked;
    }
}