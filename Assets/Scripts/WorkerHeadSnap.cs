using UnityEngine;


public class WorkerHeadSnap : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform helmetSnapPoint; // Position on head where helmet lands

    private bool isEquipped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isEquipped) return;

        // Check if the touching object is tagged "Helmet"
        if (other.CompareTag("Helmet"))
        {
            SnapHelmet(other.gameObject);
        }
    }

    private void SnapHelmet(GameObject helmet)
    {
        isEquipped = true;

        // 1. Disable VR interaction so player drops it
        if (helmet.TryGetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(out var grab))
        {
            grab.enabled = false;
        }

        // 2. Disable physics
        if (helmet.TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 3. Parent and snap position/rotation
        Transform targetPoint = helmetSnapPoint != null ? helmetSnapPoint : transform;
        helmet.transform.SetParent(targetPoint);
        helmet.transform.localPosition = Vector3.zero;
        helmet.transform.localRotation = Quaternion.identity;

        // 4. Update Checklist
        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.SetHelmetEquipped();
        }

        Debug.Log("Helmet snapped onto worker!");
    }
}