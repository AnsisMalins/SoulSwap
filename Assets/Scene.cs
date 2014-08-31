using System;
using UnityEngine;

// Turns out you can't grab stuff from the prefab store. So I created this singleton class to
// hold all prototypes and other stuff that needs to be accessed from anywhere in the scene.
public class Scene : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject mainMenu;
    public GameObject thoughtBubble;

    private static WeakReference _current;

    // This is basically the scene constructor. No other behavior should define Awake.
    private void Awake()
    {
        _current = new WeakReference(this);
    }

    public static Scene current
    {
        get
        {
            var result = _current.Target as Scene;
            if (result == null) throw new UnityException("Scene singleton missing!");
            return result;
        }
    }
 }