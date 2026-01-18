using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    private bool isSprinting;
    private bool isMoving;
    public Transform orientation;

    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private float yVelocity;
    private bool onPlatform;

    
    [SerializeField] private Image StaminaBar;
    [SerializeField] private Image OverchargeBar;
    private bool staminaEmpty;
    [SerializeField] private float Stamina = 100f;
    private float MaxStamina = 100f;
    private float MaxOverchargeStamina = 130f;
    
    private CharacterController characterController;

    [SerializeField] private AK.Wwise.RTPC speedRtpc;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        PickupSensor.PickupCollected += PickedUp;
        OverchargeBar.fillAmount = 0f;
    }
    
    void OnDestroy()
    {
        PickupSensor.PickupCollected -= PickedUp;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
        
        // Movement Input Logic
        Vector3 movementVector = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementVector += orientation.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector += orientation.forward * -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector += orientation.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movementVector += orientation.right * -1;
        }
        movementVector.Normalize();
        isMoving = movementVector.sqrMagnitude > 0;

        
        //Handle Gravity and Platforms

        if (characterController.isGrounded && yVelocity < 0)
        { 
            yVelocity = -2f;
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        
        
        
        // Stamina Bar Logic
        // Regeneration
        if (Stamina < MaxStamina && (!isSprinting || !isMoving))
        {
            Stamina += 50f * Time.deltaTime;
            Stamina = Mathf.Min(Stamina, MaxStamina);
            StaminaBar.fillAmount = Stamina / MaxStamina;
            if (jumpSpeed <= 10f)
            {
                jumpSpeed += 5f * Time.deltaTime;
            } 
        }
        
        // Sprint Drain
        if (isSprinting && isMoving)
        {
            Stamina -= 20f * Time.deltaTime;
            jumpSpeed -= 2f * Time.deltaTime;
            StaminaBar.fillAmount = Stamina / MaxStamina;
        }
        
        // Stamina Empty Check
        if (Stamina <= 0)
        {
            Stamina = 0;
            jumpSpeed = Mathf.Max(jumpSpeed, 0f);
            staminaEmpty = true;
        }
        else
        {
            staminaEmpty = false;
        }
        
        // Jumping Logic
        if (characterController.isGrounded)
        {
            if (Stamina < MaxStamina)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    yVelocity = jumpSpeed;
                    jumpSpeed = 0;
                    Stamina = 0;
                    StaminaBar.fillAmount = Stamina / MaxStamina;
                }
            }
            else if (Stamina >= MaxStamina)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    if (Stamina < MaxOverchargeStamina)
                    {
                        Stamina += 15f * Time.deltaTime;
                        jumpSpeed += 5f * Time.deltaTime;
                        float result = Map(Stamina, 100, 130, 0, 100);
                        OverchargeBar.fillAmount = result / MaxStamina;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    yVelocity = jumpSpeed;
                    jumpSpeed = 0;
                    Stamina = 0;
                    OverchargeBar.fillAmount = 0f;
                }
            }
        }
        else
        {
            if (Stamina >= MaxStamina)
            { 
                jumpSpeed = 10f;
                Stamina = MaxStamina;
                OverchargeBar.fillAmount = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        
        if (isSprinting && staminaEmpty == false || isSprinting && characterController.isGrounded == false) speed = sprintSpeed;
        else speed = walkSpeed;
        
        if(!isMoving)
        {
            speed = 0f;
        }
        
        // Apply Movement
        movementVector *= speed;
        movementVector.y = yVelocity;
        
        speedRtpc.SetValue(gameObject, speed);
        characterController.Move(movementVector * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        
    }

    void PickedUp(Pickup pickup)
    {
        walkSpeed += 1f;
        sprintSpeed += 1f;
        pickup.PickedUp();
    }
    
    private static float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) * (toTarget - fromTarget) / (toSource - fromSource) + fromTarget;
    }
}
