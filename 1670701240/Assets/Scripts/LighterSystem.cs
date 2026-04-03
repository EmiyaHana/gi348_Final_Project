using UnityEngine;

public class LighterSystem : MonoBehaviour
{
    [Header("Lighter Settings")]
    public Light lighterLight;
    public bool isLighterOn = false;

    [Header("Flicker Effect")]  
    public float minIntensity = 1.5f;
    public float maxIntensity = 3.0f;

    public float flickerSpeed = 10f; 
    private float randomOffset;

    private PlayerInteract playerInteract;

    void Start()
    {
        lighterLight.enabled = isLighterOn;
        randomOffset = Random.Range(0f, 100f);

        playerInteract = GetComponent<PlayerInteract>();
    }

    void Update()
    {
        if (playerInteract != null && playerInteract.isHiding)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isLighterOn = !isLighterOn;
            lighterLight.enabled = isLighterOn;
        }

        if (isLighterOn)
        {
            float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, randomOffset);
            lighterLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        }
    }
}