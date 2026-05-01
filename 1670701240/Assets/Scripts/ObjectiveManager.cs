using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI objectiveTextUI;
    public GameObject objectivePanel;

    public string currentObjective = "Exploration";

    void Awake()
    {
        Instance = this;
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