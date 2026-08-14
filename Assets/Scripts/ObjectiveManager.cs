using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    // THIS LINE CREATES THE 'Instance' THAT TRASHBIN IS LOOKING FOR
    public static ObjectiveManager Instance;

    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI generatorText;
    [SerializeField] private TextMeshProUGUI garbageText;

    [Header("Garbage Settings")]
    [SerializeField] private int totalGarbageToCollect = 3;

    private bool isGeneratorOn = false;
    private int currentGarbageCollected = 0;

    private void Awake()
    {
        // Singleton setup so other scripts can talk to ObjectiveManager easily
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void SetGeneratorOn()
    {
        isGeneratorOn = true;
        UpdateUI();
    }

    public void AddGarbageCollected()
    {
        currentGarbageCollected++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Update Generator Checklist line
        if (generatorText != null)
        {
            generatorText.text = isGeneratorOn 
                ? "<color=green>[✓] Generator Powered ON</color>" 
                : "[ ] Turn on Generator";
        }

        // Update Garbage Checklist line
        if (garbageText != null)
        {
            if (currentGarbageCollected >= totalGarbageToCollect)
            {
                garbageText.text = $"<color=green>[✓] Trash Cleared ({currentGarbageCollected}/{totalGarbageToCollect})</color>";
            }
            else
            {
                garbageText.text = $"[ ] Trash Thrown ({currentGarbageCollected}/{totalGarbageToCollect})";
            }
        }
    }
}