using Noname;
using UnityEngine;

namespace Nebula.Combat;

public class ProjectileFetcher
{
    public GameObject fetchProjectile(AddressableKey addressableKey)
    {
        Effects effects = GameObject.Find("Effects").GetComponent<Effects>();
        GameObject effectObject = effects.GetEffectInstance(addressableKey).gameObject;
        return effectObject;
    }
}