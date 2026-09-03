using Noname;
using UnityEngine;

namespace Nebula.Combat;

public class ProjectileFetcher
{
    public GameObject fetchProjectile(AddressableKey addressableKey)
    {
        Effects effects = MainSystem.effects;
        GameObject effectObject = effects.GetEffectInstance(addressableKey).gameObject;
        return effectObject;
    }
}