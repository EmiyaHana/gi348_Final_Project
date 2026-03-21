using UnityEngine;
using UnityEngine.UI;

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

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(x, 0, 0); 

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        if (isRunning && move.magnitude > 0)
        {
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegen * Time.deltaTime;
        }

        if(staminaBar != null) staminaBar.value = currentStamina;

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }
}