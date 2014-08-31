using System.Collections;
using UnityEngine;

// Your basic living creature (Pawn in UE4) that can be possessed by a soul (Controller in UE4).
// Provides a basic API to souls and translates that to forces and rotation in the world.

public class Creature : MonoBehaviour
{
    public float jumpHeight = 1;
    // Movement acts like a joystick. A vector of length one is full throttle in that direction.
    public Vector2 movement;
    public float movementForce = 300;
    // To rotate, just add the delta (e.g. from your mouse).
    public float rotation;
    public float rotationSpeed = 1;
    public Vector3 thoughtBubblePosition = new Vector3(0, 2, 0);

    private TouchSensor _groundSensor;
    private bool _jump;

    private void Start()
    {
        _groundSensor = gameObject.Require<TouchSensor>("GroundTouchSensor");
    }

    public bool isTouchingGround { get { return _groundSensor.other != null; } }

    public void Jump()
    {
        if (isTouchingGround) _jump = true;
    }

    // This is the core mechanic of the game. I engineered everything to make it as simple as
    // possible. Swapping souls requires just swapping their parents (using extension methods to
    // negate some Unity sillines. The method is a bit ugly as it assumes a single camera, etc.
    public void SwapSouls(GameObject mySoul)
    {
        var cameraTransform = Scene.current.mainCamera.transform;
        RaycastHit hitInfo;
        if (!Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hitInfo))
            return;
        var otherObject = hitInfo.transform.gameObject;
        var otherSoul = otherObject.gameObject.Child("Soul");
        if (otherSoul == null) return;
        mySoul.SetParent(otherObject);
        otherSoul.SetParent(gameObject);
    }

    public IEnumerator Think(string text)
    {
        return ThoughtBubble.Show(gameObject, thoughtBubblePosition, text);
    }

    private void FixedUpdate()
    {
        if (isTouchingGround)
        {
            var movement = Vector2.ClampMagnitude(this.movement, 1);

            var force = movement * movementForce * Time.fixedDeltaTime;
            rigidbody.AddRelativeForce(force.x, 0, force.y);

            if (_jump)
            {
                rigidbody.AddRelativeForce(0, Mathf.Sqrt(9.81f * jumpHeight), 0,
                    ForceMode.VelocityChange);
                _jump = false;
            }
        }
    }

    // I decided to put rotation in Update because it's instantaneous and the rotation member is
    // only going to be set during Update.
    private void Update()
    {
        transform.localEulerAngles = new Vector3(
            0, transform.localEulerAngles.y + rotation * rotationSpeed, 0);
        rotation = 0;
    }
}