using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Vector2 Sensitivity = new Vector2(1, 1);

    private void Update()
    {
        var angles = transform.localEulerAngles;
        transform.localEulerAngles = new Vector3(
            angles.x - Input.GetAxis("Mouse Y") * Sensitivity.y,
            angles.y + Input.GetAxis("Mouse X") * Sensitivity.x,
            angles.z);
    }
}