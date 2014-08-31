using UnityEngine;

// I went for the simplest possible solution here. You can use the sensitivity setting to filter
// out axes. In this game I use two copies of this Component. X axis to control the box, and Y
// axis to tilt the camera.

public class MouseLook : MonoBehaviour
{
    public Vector2 sensitivity = new Vector2(1, 1);

    private void Update()
    {
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(
            angles.x - Input.GetAxis("Mouse Y") * sensitivity.y,
            angles.y + Input.GetAxis("Mouse X") * sensitivity.x,
            angles.z);
    }
}