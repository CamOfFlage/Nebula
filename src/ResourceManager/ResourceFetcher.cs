using Il2CppInterop.Runtime;
using UnityEngine;

namespace Nebula.ResourceManager;

public class ResourceFetcher<T>
{
    //Future scope of having this return the actual instance of T instead of its gameObject
    private Type _type
    {
        get
        {
            return typeof(T);
        }
    }

    public GameObject? Fetch(string name)
    {
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameObject go in objects)
        {
            if (go.name == name && go.GetComponent<T>() != null && 
                (go.hideFlags.HasFlag(HideFlags.HideInHierarchy) || !go.scene.IsValid()))
            {
                gameObjects.Add(go);
            }
        }

        if (gameObjects.Count == 0)
        {
            return null;
        }
        return gameObjects.FirstOrDefault();
    }
}