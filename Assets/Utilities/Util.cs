using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// This part I'm the most proud of. :) The Swiss army knife to cut through the stupidity of Unity.

public static class Util
{
    /// <summary>Creates objects. Use only when loading.</summary>
    public static IEnumerable<GameObject> Ancestors(this GameObject gameObject)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        while (true)
        {
            gameObject = gameObject.Parent();
            if (gameObject == null) break;
            yield return gameObject;
        }
    }

    public static GameObject Child(this GameObject gameObject, string name)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        if (gameObject.transform == null) return null;
        var transform = gameObject.transform.Find(name);
        return transform != null ? transform.gameObject : null;
    }

    public static T Child<T>(this GameObject gameObject, string name) where T : Component
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        var found = Child(gameObject, name);
        return found != null ? found.GetComponent<T>() : null;
    }

    /// <summary>Creates objects. Use only when loading.</summary>
    public static IEnumerable<GameObject> Children(this GameObject gameObject)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        if (gameObject.transform == null) yield break;
        for (int i = 0; i < gameObject.transform.childCount; i++)
            yield return gameObject.transform.GetChild(i).gameObject;
    }

    public static T Clone<T>(this T original) where T : UnityEngine.Object
    {
        if (original == null) throw new ArgumentNullException("original");
        return UnityEngine.Object.Instantiate(original) as T;
    }

    public static readonly IEnumerator emptyCoroutine = EmptyCoroutine();
    private static IEnumerator EmptyCoroutine() { yield break; }

    public static GameObject Parent(this GameObject gameObject)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        return gameObject.transform != null ? gameObject.transform.parent.gameObject : null;
    }

    public static T Parent<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        return gameObject.transform != null
            ? gameObject.transform.parent.gameObject.GetComponent<T>()
            : null;
    }

    /// <summary>Uses UnityEngine.Random.</summary>
    public static float Random(Vector2 range)
    {
        return UnityEngine.Random.Range(range.x, range.y);
    }

    /// <summary>Uses UnityEngine.Random. Creates objects. Use only when loading.</summary>
    public static TSource Random<TSource>(this IEnumerable<TSource> source)
    {
        if (!source.Any()) throw new ArgumentException("The source sequence is empty.", "source");
        return source.ElementAt(UnityEngine.Random.Range(0, source.Count()));
    }

    /// <summary>Uses UnityEngine.Random.</summary>
    public static TSource Random<TSource>(this IList<TSource> source)
    {
        if (source.Count == 0)
            throw new ArgumentException("The source sequence is empty.", "source");
        return source[UnityEngine.Random.Range(0, source.Count)];
    }

    public static GameObject Require(this GameObject gameObject, string name)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        var result = Child(gameObject, name);
        if (result == null) throw new UnityException("GameObject '" + name + "' not found.");
        return result;
    }

    public static T Require<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        var result = gameObject.GetComponent<T>();
        if (result == null) throw new UnityException("GameObject '" + gameObject.name
            + "' does not have Component '" + typeof(T).Name + "'.");
        return result;
    }

    public static T Require<T>(this GameObject gameObject, string name) where T : Component
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        return Require<T>(Require(gameObject, name));
    }

    // The need for this function proves that Unity is retarded.
    public static void SetParent(this GameObject gameObject, GameObject parent)
    {
        if (gameObject == null) throw new ArgumentNullException("gameObject");
        var transform = gameObject.transform;
        var position = transform.localPosition;
        var rotation = transform.localRotation;
        var scale = transform.localScale;
        transform.parent = parent.transform;
        transform.localPosition = position;
        transform.localRotation = rotation;
        transform.localScale = scale;
    }
}