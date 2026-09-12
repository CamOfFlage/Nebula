using UnityEngine;

namespace Nebula.ResourceManager;

public static class ResourceFetcher
{
    /// <summary>
    /// Retrieves first game object with the given parameters
    /// </summary>
    /// <param name="name">The name of the target game object</param>
    /// <typeparam name="T">The filter for components</typeparam>
    /// <returns>The first game object found in the resources, not the live assets</returns>
    /// <exception cref="Exception">Thrown when no resource was found</exception>
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