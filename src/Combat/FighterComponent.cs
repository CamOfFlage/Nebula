using Il2CppInterop.Runtime.Injection;
using UnityEngine;
using Noname.Worldless.Combat;

namespace Nebula.Combat;

public class FighterComponent : MonoBehaviour
{
    public FighterComponent(IntPtr intPtr) : base(intPtr) { }
    public static void RegisterFighterComponent(Type fighterComponentType)
    {
        if (fighterComponentType == null)
        {
            Plugin.logger.LogError("Fighter component type is null");
            return;
        }

        Plugin.logger.LogDebug("Not null");

        if (fighterComponentType.BaseType != typeof(FighterComponent))
        {
            Plugin.logger.LogError("Fighter component type is not FighterComponent");
        }
        
        Plugin.logger.LogDebug("Base type not null");
        
        if (fighterComponentType.BaseType == typeof(FighterComponent))
        {
            Plugin.logger.LogMessage($"Registering Fighter Component {fighterComponentType.Name}");
            ClassInjector.RegisterTypeInIl2Cpp(fighterComponentType, new RegisterTypeOptions
            {
                Interfaces = new Il2CppInterfaceCollection(new[] { typeof(IFighterComponent) })
            });
        }
        else
        {
            Plugin.logger.LogError($"Given type  {fighterComponentType.Name} does not inherit from FighterComponent");
        }
    }
}