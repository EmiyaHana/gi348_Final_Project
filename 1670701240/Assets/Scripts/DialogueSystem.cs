using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance;

    [Header("UI Elements")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI promptText;

    private List<string> lines = new List<string>();
    private int index;
    private bool isDialogueActive = false;
    private string missionAfterDialogue = "";

    void Awake()
    {
        Instance = this;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartDialogue(string[] newLines, string missionTrigger = "")
    {
        if (isDialogueActive) return;

        lines.Clear();
        lines.AddRange(newLines);
        index = 0;
        missionAfterDialogue = missionTrigger;

        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;

        ShowLine();
    }

    void Update()
    {
        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
            {
                NextLine();
            }
        }
    }

    void ShowLine()
    {
        dialogueText.text = lines[index];
    }

    void NextLine()
    {
        if (index < lines.Count - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        if (!string.IsNullOrEmpty(missionAfterDialogue))
        {
            ObjectiveManager.Instance.SetObjective(missionAfterDialogue);
        }
    }
}