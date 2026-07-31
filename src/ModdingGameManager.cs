using Il2CppInterop.Runtime;
using UnityEngine;
using Noname.Worldless.Combat;
using WorldlessLibs.ResourceManager;

namespace WorldlessLibs;

public class ModdingGameManager : MonoBehaviour
{
    /*
     * Check if templates are loaded and invoke event
     * OnSceneLoad, find all new templates and modify
     * OnSceneUnload, remove all old templates
     */
    public CombatTemplate[] combatTemplates { get; private set; } = null;

    private void Update()
    {
        if (!GameInfo.CombatTemplatesLoaded && GameInfo.IsBooted)
        {
            if (GetAllCombatTemplates().Length >= 35)
            {
                Plugin.logger.LogMessage("Templates loaded!");
                GameInfo.CombatTemplatesLoaded = true;
                ResourceEvents.Instance.OnAllTemplatesLoaded();
            }
        }

        
    }

    private CombatTemplate[] GetAllCombatTemplates()
    {
        var resources = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CombatTemplate>());
        List<CombatTemplate> combatTemplates = new List<CombatTemplate>();
        for (int i = 0; i < resources.Length; i++)
        {
            CombatTemplate template = resources[i].TryCast<CombatTemplate>();
            if (!template.gameObject.scene.IsValid() && !template.gameObject.name.EndsWith("(Clone)"))
            {
                combatTemplates.Add(template);
            }
        }
        
        //combatTemplates[i] = resources[i].TryCast<CombatTemplate>();
        this.combatTemplates = combatTemplates.ToArray();
        return this.combatTemplates;
    }
}