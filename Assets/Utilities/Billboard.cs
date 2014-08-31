using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        var cameraTransform = Scene.current.mainCamera.transform;
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
            cameraTransform.rotation * Vector3.up);
    }
}