using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    private bool isSprinting;
    private bool isMoving;
    public Transform orientation;

    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private float yVelocity;
    
    [SerializeField] private Image StaminaBar;
    private bool staminaEmpty;
    [SerializeField] private float Stamina = 100f;
    [SerializeField] private float MaxStamina = 100f;
    
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        PickupSensor.PickupCollected += PickedUp;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
                jumpSpeed = 0;
                Stamina = 0;
                StaminaBar.fillAmount = Stamina / MaxStamina;
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
        
        if (isSprinting && staminaEmpty == false || isSprinting && characterController.isGrounded == false) speed = 10f;
        else speed = 5f;
        
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
        pickup.PickedUp();
    }
}
