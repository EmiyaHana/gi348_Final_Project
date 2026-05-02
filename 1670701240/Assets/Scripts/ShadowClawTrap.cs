using UnityEngine;

public class ShadowClawTrap : MonoBehaviour
{
    [Header("Trap Setting")]
    public int damageAmount = 1;
    public float detectRadius = 1f;
    public float attackCooldown = 2f;
    public LayerMask playerLayer;

    [Header("Effect")]
    public GameObject clawVisual;
    public AudioSource attackSound;

    private float cooldownTimer;
    private bool isAttacking;

    void Start()
    {
        if (clawVisual != null) clawVisual.SetActive(false);
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius, playerLayer);

        if (hitColliders.Length > 0 && cooldownTimer <= 0)
        {
            GameObject player = hitColliders[0].gameObject;
            TryAttackPlayer(player);
        }
    }

    void TryAttackPlayer(GameObject player)
    {
        if (attackSound != null)
        {
            attackSound.Play();
        }
        DoDamage(player);
    }

    void DoDamage(GameObject player)
    {
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        if (health != null && movement != null)
        {
            StartCoroutine(ShowClawVisual());
            if (attackSound != null) attackSound.Play();

            cooldownTimer = attackCooldown;

            if (!movement.isDodging)
            {
                health.TakeDamage(damageAmount);
                Debug.Log("You have been attacked!");
            }
            else
            {
                Debug.Log("Dodging success!");
            }
        }
    }

    System.Collections.IEnumerator ShowClawVisual()
    {
        if (clawVisual != null)
        {
            clawVisual.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            clawVisual.SetActive(false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}