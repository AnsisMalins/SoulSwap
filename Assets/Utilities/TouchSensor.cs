using UnityEngine;

// Took me a while to figure out a simple and reusable solution to detecting when we're on the
// ground.

/// <summary>Doesn't cover the case where we might be touching multiple things at once.</summary>
public class TouchSensor : MonoBehaviour
{
    private void Start()
    {
        if (collider == null) throw new UnityException(
            "GameObject '" + gameObject.name + "' does not have a Collider.");
        if (!collider.isTrigger) throw new UnityException(
            "Collider of GameObject '" + gameObject.name + "' must be a trigger.");
    }

    public Collider other { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        // We want to ignore collisions between e.g. the leg and the torso.
        if (gameObject.Parent<Collider>() != other) this.other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        if (this.other == other) this.other = null;
    }
}