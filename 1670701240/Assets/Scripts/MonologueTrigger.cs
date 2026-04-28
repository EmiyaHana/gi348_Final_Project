using UnityEngine;
using TMPro;
using System.Collections;

public class MonologueTrigger : MonoBehaviour
{
    [Header("DetailSubtitle")]
    [TextArea(3, 5)]
    public string monologueText = "ฉันต้องหาทางออก...";
    
    [Header("ShowTime")]
    public float displayTime = 3f;
    
    [Header("PlayTimes")]
    public bool playOnlyOnce = true;

    [Header("SubtitleUIText")]
    public TextMeshProUGUI subtitleTextUI;

    private bool hasPlayed = false;

    void Start()
    {
        if (subtitleTextUI != null)
        {
            subtitleTextUI.text = "";
            subtitleTextUI.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            if (playOnlyOnce)
            {
                hasPlayed = true;
            }
            
            StopAllCoroutines();
            StartCoroutine(ShowMonologue());
        }
    }

    IEnumerator ShowMonologue()
    {
        if (subtitleTextUI != null)
        {
            subtitleTextUI.gameObject.SetActive(true);
            subtitleTextUI.text = monologueText;

            yield return new WaitForSeconds(displayTime);

            subtitleTextUI.text = "";
            subtitleTextUI.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("-ERROR-");
        }
    }
}