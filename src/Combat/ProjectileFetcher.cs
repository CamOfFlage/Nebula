using Noname;
using UnityEngine;

namespace Nebula.Combat;

public static class ProjectileFetcher
{
    public static GameObject FetchProjectile(AddressableKey addressableKey)
    {
        Effects effects = MainSystem.effects;
        GameObject effectObject = effects.GetEffectInstance(addressableKey).gameObject;
        return effectObject;
    }
}