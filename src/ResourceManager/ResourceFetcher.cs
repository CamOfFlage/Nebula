using UnityEngine;

namespace Nebula.ResourceManager;

public static class ResourceFetcher
{
    public static GameObject Fetch<T>(string name)
    {
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (GameObject go in objects)
        {
            if (!go.scene.IsValid())
            {
                Plugin.logger.LogMessage(go.name);
            }
            if (go.name.Equals(name) && go.GetComponent<T>() != null && !go.scene.IsValid())
            {
                gameObjects.Add(go);
            }
        }

        if (gameObjects.Count == 0)
        {
            throw new Exception("No Resources found");
        }
        return gameObjects.FirstOrDefault();
    }
}