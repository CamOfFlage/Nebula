using Il2CppInterop.Runtime.Attributes;
using Nebula.Combat;
using UnityEngine;
using Noname.Worldless.Combat;
using Noname;

namespace Nebula;

public class ModdingGameManager : MonoBehaviour
{
    /*
     * Check if templates are loaded and invoke event
     * OnSceneLoad, find all new templates and modify
     * OnSceneUnload, remove all old templates
     */
    public ModdingGameManager(IntPtr intPtr) : base(intPtr) { }
    
    [HideFromIl2Cpp]
    public System.Collections.IEnumerator WaitForEffectsLoader(EffectsLoader loader)
    {
        bool isLoaded = false;
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (!isLoaded)
        {
            isLoaded = loader.IsEveryEffectLoaded();
            yield return wait;
        }
        
        foreach (GameObjectReference gameObjectReference in loader._effects)
        {
            //Plugin.logger.LogMessage("Processing effect: " + gameObjectReference.name);
            AddressableKey effectKey = new AddressableKey();
            effectKey._key = gameObjectReference.name;
            effectKey._guid = gameObjectReference.guid;
            Effect effect = MainSystem.effects.GetEffectInstance(effectKey);
            if (effect == null)
            {
                //Plugin.logger.LogError("No effect found for key " + effectKey.key);
            }
            else
            {
                
                if (effect.gameObject.GetComponent<Hitter>() != null)
                {
                    ProjectilePatchHandler.Instance.PatchProjectile(effect.gameObject, effectKey.key);
                }
            }
        }
    }
}