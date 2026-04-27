using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    
    [Header("Stamina System")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrain = 15f;
    public float staminaRegen = 10f;
    public Slider staminaBar;

    [Header("Dodge System")]
    public bool isDodging = false;
    public float dodgeSpeed = 15f;
    public float dodgeDuration = 1f;
    public float dodgeStaminaCost = 25f;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x, 0, 0); 

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isDodging;

        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= dodgeStaminaCost && !isDodging && move.magnitude > 0)
        {
            StartCoroutine(DodgeRoutine());
        }

        float currentSpeed = walkSpeed;
        if (isDodging) 
            currentSpeed = dodgeSpeed;
        else if (isRunning) 
            currentSpeed = runSpeed;

        if (isRunning && move.magnitude > 0 && !isDodging)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else if (currentStamina < maxStamina && !isDodging)
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        if(staminaBar != null) staminaBar.value = currentStamina;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (move != Vector3.zero && !isDodging)
        {
            Quaternion targetRotation = Quaternion.Euler(0, x > 0 ? 90 : -90, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    IEnumerator DodgeRoutine()
    {
        isDodging = true;
        currentStamina -= dodgeStaminaCost;
        Debug.Log("Dodging!");
        
        yield return new WaitForSeconds(dodgeDuration);
        
        isDodging = false;
    }
}