using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    public Transform orientation;

    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private float yVelocity;
    
    [SerializeField] private Image StaminaBar;
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
        
        if (Stamina < MaxStamina)
        {
            Stamina += 0.1f;
            StaminaBar.fillAmount = Stamina / MaxStamina;
            jumpSpeed += 0.01f;
        }
        
        movementVector *= speed;
        movementVector.y = yVelocity;
        
        characterController.Move(movementVector * Time.deltaTime);
    }

    void PickedUp(Pickup pickup)
    {
        pickup.PickedUp();
    }
}
