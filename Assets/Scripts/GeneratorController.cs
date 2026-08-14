using System.Collections;
using UnityEngine;

public class GeneratorController : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource startUpAudioSource;
    [SerializeField] private AudioSource loopAudioSource;

    private bool isRunning = false;

    // Call this via the 'Activated' event on XR Simple Interactable (triggered by R2 / Index Trigger)
    public void ToggleGenerator()
    {
        if (!isRunning)
        {
            StartCoroutine(StartGeneratorRoutine());
        }
    }

    private IEnumerator StartGeneratorRoutine()
    {
        isRunning = true;
        Debug.Log("Generator Turned On!");

        // Update the checklist UI
        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.SetGeneratorOn();
        }

        // 1. Play Startup Sound
        if (startUpAudioSource != null && startUpAudioSource.clip != null)
        {
            startUpAudioSource.Play();

            // Wait until the startup audio source actually finishes playing entirely
            while (startUpAudioSource.isPlaying)
            {
                yield return null; // Check again next frame
            }
        }

        // 2. Start Looping Engine Sound immediately after startup finishes
        if (loopAudioSource != null)
        {
            loopAudioSource.Play();
        }
    }
}