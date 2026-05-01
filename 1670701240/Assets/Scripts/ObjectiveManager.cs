using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI objectiveTextUI;
    public GameObject objectivePanel;

    public string currentObjective = "Exploration";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateObjectiveUI();
    }

    public void SetObjective(string newObjective)
    {
        currentObjective = newObjective;
        UpdateObjectiveUI();
        Debug.Log("New Objective: " + newObjective);
    }

    void UpdateObjectiveUI()
    {
        if (objectiveTextUI != null)
        {
            objectiveTextUI.text = "What to do : " + currentObjective;
        }
    }
}