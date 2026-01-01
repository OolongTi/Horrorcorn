using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed;
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 10.0f;
    private bool isSprinting;
    private bool isMoving;
    public Transform orientation;

    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private float yVelocity;
    
    [SerializeField] private Image StaminaBar;
    [SerializeField] private Image OverchargeBar;
    private bool staminaEmpty;
    [SerializeField] private float Stamina = 100f;
    private float MaxStamina = 100f;
    private float MaxOverchargeStamina = 130f;
    
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        PickupSensor.PickupCollected += PickedUp;
        OverchargeBar.fillAmount = 0f;
    }

    void Update()
    {
        if (Time.timeScale == 0) return;
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

        if (characterController.isGrounded)
        {
            yVelocity = -1f;
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
                        Stamina += 0.1f;
                        jumpSpeed += 0.01f;
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
            yVelocity += gravity * Time.deltaTime;
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
        
        movementVector *= speed;
        movementVector.y = yVelocity;
        
        characterController.Move(movementVector * Time.deltaTime);
    }
    
    void FixedUpdate()
    {
        if (Stamina < MaxStamina && (!isSprinting || !isMoving))
        {
            Stamina += 1f;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            if (jumpSpeed <= 10f)
            {
                jumpSpeed += 0.1f;
            } 
        }
        if (isSprinting && isMoving)
        {
            Stamina -= 0.1f;
            jumpSpeed -= 0.01f;
            StaminaBar.fillAmount = Stamina / MaxStamina;
        }
        if (Stamina <= 0)
        {
            Stamina = 0;
            jumpSpeed = 0;
            staminaEmpty = true;
        }
        else
        {
            staminaEmpty = false;
        }
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
