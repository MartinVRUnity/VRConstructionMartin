using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI generatorText;
    [SerializeField] private TextMeshProUGUI garbageText;
    [SerializeField] private TextMeshProUGUI helmetText; // Add this

    [Header("Garbage Settings")]
    [SerializeField] private int totalGarbageToCollect = 3;

    private bool isGeneratorOn = false;
    private bool isHelmetEquipped = false; // Add this
    private int currentGarbageCollected = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
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

    public void SetHelmetEquipped() // Add this
    {
        isHelmetEquipped = true;
        UpdateUI();
    }

    public void AddGarbageCollected()
    {
        currentGarbageCollected++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (generatorText != null)
        {
            generatorText.text = isGeneratorOn 
                ? "<color=green>[✓] Generator Powered ON</color>" 
                : "[ ] Turn on Generator";
        }

        if (garbageText != null)
        {
            garbageText.text = (currentGarbageCollected >= totalGarbageToCollect)
                ? $"<color=green>[✓] Trash Cleared ({currentGarbageCollected}/{totalGarbageToCollect})</color>"
                : $"[ ] Trash Thrown ({currentGarbageCollected}/{totalGarbageToCollect})";
        }

        // Add this for helmet checklist line
        if (helmetText != null)
        {
            helmetText.text = isHelmetEquipped
                ? "<color=green>[✓] Helmet Put on Worker</color>"
                : "[ ] Put Helmet on Worker";
        }
    }
}