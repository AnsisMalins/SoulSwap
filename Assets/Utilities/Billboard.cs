using UnityEngine;

// I copypasted this code from the Internet after a short attempt at own solution.

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        var cameraTransform = Scene.current.mainCamera.transform;
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward,
            cameraTransform.rotation * Vector3.up);
    }
}