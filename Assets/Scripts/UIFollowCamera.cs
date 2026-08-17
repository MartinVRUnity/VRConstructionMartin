using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform targetCamera;
    
    // Updated: X is negative (left), Y is positive (top/up), Z is distance forward
    [SerializeField] private Vector3 offset = new Vector3(-0.1f, 0.2f, 1.2f); 
    [SerializeField] private float smoothSpeed = 5f;

    private void Start()
    {
        if (targetCamera == null && Camera.main != null)
        {
            targetCamera = Camera.main.transform;
        }
    }

    private void LateUpdate()
    {
        if (targetCamera == null) return;

        // Position floating UI ahead of player camera
        Vector3 targetPosition = targetCamera.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);

        // Face player camera
        transform.rotation = Quaternion.LookRotation(transform.position - targetCamera.position);
    }
}