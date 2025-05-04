using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float acceleration = 10f;
    public float turnSpeed = 500f;

    public float maxStamina = 2f;           // max sprint time
    public float staminaRegenRate = 1f;     // regen per second
    public float staminaDrainRate = 1f;     // drain per second while sprinting
    public float cooldownTime = 3f;         // cooldown after stamina is depleted
    public float staminaRegenDelay = 1.5f;

    private float currentStamina;
    private float cooldownTimer = 0f;
    private float regenDelayTimer = 0f;
    private bool isSprinting = false;

    public Slider sprintMeterUI;

    private float currentSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;


    //shooting things 
    public Weapon weapon;
    
    Vector2 mousePosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        currentSpeed = walkSpeed;
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontalInput, verticalInput).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (weapon != null)
                weapon.Fire();

            if (animator != null)
                animator.SetTrigger("Shoot");
        }

        // Handle cooldown
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Handle sprinting
        bool holdingSprint = Input.GetKey(KeyCode.LeftShift);
        bool canSprint = holdingSprint && currentStamina > 0f && cooldownTimer <= 0f;

        if (canSprint)
        {
            isSprinting = true;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            regenDelayTimer = staminaRegenDelay; // ?? restart regen delay

            if (currentStamina <= 0f)
            {
                currentStamina = 0f;
                isSprinting = false;
                cooldownTimer = cooldownTime;
            }
        }
        else
        {
            if (isSprinting)
            {
                // just stopped sprinting
                regenDelayTimer = staminaRegenDelay;
            }

            isSprinting = false;

            // countdown regen delay
            if (regenDelayTimer > 0f)
            {
                regenDelayTimer -= Time.deltaTime;
            }

            // Regen only after delay + cooldown are done
            if (cooldownTimer <= 0f && regenDelayTimer <= 0f && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
            }
        }


        // Set movement speed
        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        /* Rotate toward movement direction
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle + 90f);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        } */

        // Update UI meter
        if (sprintMeterUI != null)
        {
            sprintMeterUI.value = currentStamina / maxStamina;
        }
    }

 



    void FixedUpdate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        rb.MovePosition(rb.position + moveInput * currentSpeed * Time.fixedDeltaTime);
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90f;
        //rb.rotation = aimAngle;

    }
}