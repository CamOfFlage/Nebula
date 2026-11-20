using System.Reflection;
using Nebula.Combat;

namespace Nebula.Reflection;

internal static class NebulaReflectionTypes
{
    public static HashSet<Type> reflectionTypes = new HashSet<Type>
    {
        typeof(Nebula.Combat.FighterComponent),
        typeof(NebulaPatch)
    };

    /*
     * Rules for setting these:
     * The type must be abstract
     * The method must only take one argument of Type
     */
    public static Dictionary<Type, MethodInfo> ManagementActions = new Dictionary<Type, MethodInfo>
    {
        {
            typeof(FighterComponent), 
            typeof(FighterComponent).GetMethod(nameof(FighterComponent.RegisterFighterComponent)
                , new Type[] {typeof(Type)})
        },
        {
            typeof(NebulaPatch),
            typeof(NebulaPatch).GetMethod(nameof(NebulaPatch.Register)
                , BindingFlags.Static | BindingFlags.NonPublic)
        }
    };
    
    internal static Type[] GetAllRelevantParents(Type type)
    {
        List<Type> parents = new List<Type>();
        if (type.BaseType == null)
        {
            return parents.ToArray();
        }

        Type lastType = type.BaseType;
        while (lastType != null && lastType != typeof(object))
        {
            parents.Add(lastType);
            lastType =  lastType.BaseType;
        }
        return parents.ToArray();
    }
}