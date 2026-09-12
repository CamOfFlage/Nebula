using System.Collections;
using Il2CppInterop.Runtime.Attributes;
using Nebula.Combat;
using Noname;
using Noname.Worldless.Combat;
using UnityEngine;

namespace Nebula;

internal class NebulaGameManager : MonoBehaviour
{
    public NebulaGameManager(IntPtr intPtr) : base(intPtr) { }
    
    [HideFromIl2Cpp]
    internal IEnumerator WaitForEffectsLoader(EffectsLoader loader)
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
            AddressableKey effectKey = new AddressableKey
            {
                _key = gameObjectReference.name,
                _guid = gameObjectReference.guid
            };
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