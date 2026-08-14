using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [Header("Task Tracking")]
    private int collectedGarbageCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trash bin has the "Garbage" tag
        if (other.CompareTag("Garbage"))
        {
            CollectGarbage(other.gameObject);
        }
    }

    private void CollectGarbage(GameObject garbage)
{
    collectedGarbageCount++;
    
    // Update objective UI
    if (ObjectiveManager.Instance != null)
    {
        ObjectiveManager.Instance.AddGarbageCollected();
    }

    // Hide visual & collider immediately
    if (garbage.TryGetComponent<Renderer>(out var renderer)) renderer.enabled = false;
    foreach (var col in garbage.GetComponentsInChildren<Collider>()) col.enabled = false;

    // Delayed destruction for sound completion
    Destroy(garbage, 1.5f);
}
}