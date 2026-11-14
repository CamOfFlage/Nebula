using System.Reflection;
using Nebula.Combat;

namespace Nebula.Reflection;

internal static class NebulaReflectionTypes
{
    public static HashSet<Type> reflectionTypes = new HashSet<Type>
    {
        typeof(Nebula.Combat.FighterComponent)
    };

    public static Dictionary<Type, MethodInfo> ManagementActions = new Dictionary<Type, MethodInfo>
    {
        {typeof(Nebula.Combat.FighterComponent), 
            typeof(FighterComponent).GetMethod(nameof(FighterComponent.RegisterFighterComponent)
                , new Type[] {typeof(Type)})}
    };
}