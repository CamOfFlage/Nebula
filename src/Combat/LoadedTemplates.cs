using Il2CppInterop.Runtime;
using Noname.Worldless.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nebula.Combat;

public class LoadedTemplates
{
    public List<CombatTemplate> CombatTemplates = new List<CombatTemplate>();

    public List<CombatTemplate> GetLoadedTemplates()
    {
        var resources = Resources.FindObjectsOfTypeAll(Il2CppType.Of<CombatTemplate>());
        List<CombatTemplate> combatTemplates = new List<CombatTemplate>();
        for (int i = 0; i < resources.Length; i++)
        {
            CombatTemplate template = resources[i].TryCast<CombatTemplate>();
            if (template.gameObject.scene.IsValid() && template.gameObject.name.EndsWith("(Clone)"))
            {
                combatTemplates.Add(template);
            }
        }
        return combatTemplates;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Plugin.logger.LogMessage("OnSceneLoad");
        List<CombatTemplate> newCombatTemplates = new List<CombatTemplate>();
        List<CombatTemplate> currentCombatTemplates = GetLoadedTemplates();

        newCombatTemplates = currentCombatTemplates.Except(CombatTemplates).ToList();
        if (newCombatTemplates.Count > 0)
        {
            this.CombatTemplates = currentCombatTemplates;
            NewTemplatesLoadedArgs args = new NewTemplatesLoadedArgs(newCombatTemplates);
            NewTemplatesLoaded?.Invoke(this, args);
        }

    }
    
    public void OnSceneLoad(System.Object? obj, EventArgs? eventArgs)
    {
        Plugin.logger.LogMessage("OnSceneLoad");
        List<CombatTemplate> newCombatTemplates = new List<CombatTemplate>();
        List<CombatTemplate> currentCombatTemplates = GetLoadedTemplates();

        newCombatTemplates = currentCombatTemplates.Except(CombatTemplates).ToList();
        if (newCombatTemplates.Count > 0)
        {
            foreach (CombatTemplate template in newCombatTemplates)
            {
                Plugin.logger.LogMessage(template.gameObject.name);
            }
            this.CombatTemplates = currentCombatTemplates;
            NewTemplatesLoadedArgs args = new NewTemplatesLoadedArgs(newCombatTemplates);
            NewTemplatesLoaded?.Invoke(this, args);
        }

    }

    public class NewTemplatesLoadedArgs : EventArgs
    {
        public List<CombatTemplate> newTemplates;

        public NewTemplatesLoadedArgs(List<CombatTemplate> newTemplates)
        {
            this.newTemplates = newTemplates;
        }
    }
    
    public event EventHandler<NewTemplatesLoadedArgs> NewTemplatesLoaded;
}