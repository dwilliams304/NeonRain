using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// Get the component attached to a GameObject, and if it doesn't exist - add it.
    /// </summary>
    /// <typeparam name="T">The component we want</typeparam>
    /// <param name="gameObject">The GameObject we are accessing</param>
    /// <returns></returns>
    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (!component) component = gameObject.AddComponent<T>();

        return component;
    }
}