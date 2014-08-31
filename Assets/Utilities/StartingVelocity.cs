using UnityEngine;

/// <summary>Allows setting the initial velocity of rigibodies in the editor.</summary>
public class StartingVelocity : MonoBehaviour
{
    public Vector3 Velocity;
    public Vector3 AngularVelocity;

    private void Start()
    {
        if (rigidbody == null) throw new UnityException("Rigidbody required.");
        rigidbody.AddForce(Velocity, ForceMode.VelocityChange);
        rigidbody.AddTorque(AngularVelocity, ForceMode.VelocityChange);
        Destroy(this);
    }
}