using Noname;
using UnityEngine;

namespace Nebula.Combat;

public static class ProjectileFetcher
{
    /// <summary>
    /// Searches the global Effects manager for the given projectile
    /// </summary>
    /// <param name="addressableKey">The corresponding key</param>
    /// <returns>The found effect, or null if no matching effect was found</returns>
    public static GameObject? FetchProjectile(AddressableKey addressableKey)
    {
        Effects effects = MainSystem.effects;
        GameObject effectObject = effects.GetEffectInstance(addressableKey).gameObject;
        return effectObject;
    }
}