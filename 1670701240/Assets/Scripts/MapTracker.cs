using UnityEngine;

public class MapTracker : MonoBehaviour
{
    [Header("Icon")]
    public Transform playerTransform;
    public RectTransform playerIconUI;

    [Header("World Bounds")]
    public Transform worldTopLeft;
    public Transform worldBottomRight;

    [Header("UIMap Bounds")]
    public RectTransform mapTopLeft;
    public RectTransform mapBottomRight;

    void Update()
    {
        if (playerTransform == null || playerIconUI == null || worldTopLeft == null || worldBottomRight == null)
            return;
        float percentX = Mathf.InverseLerp(worldTopLeft.position.x, worldBottomRight.position.x, playerTransform.position.x);
        float percentY = Mathf.InverseLerp(worldBottomRight.position.y, worldTopLeft.position.y, playerTransform.position.y); 

        float mappedX = Mathf.Lerp(mapTopLeft.anchoredPosition.x, mapBottomRight.anchoredPosition.x, percentX);
        float mappedY = Mathf.Lerp(mapBottomRight.anchoredPosition.y, mapTopLeft.anchoredPosition.y, percentY);

        playerIconUI.anchoredPosition = new Vector2(mappedX, mappedY);
    }
}