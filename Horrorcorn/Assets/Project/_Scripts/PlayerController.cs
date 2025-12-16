using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    public Transform orientation;

    [SerializeField] private float jumpSpeed = 8f;
    [SerializeField] private float gravity = -30f;
    private float yVelocity;
    
    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        
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
            }
        }
        else
        {
            yVelocity += gravity * Time.deltaTime;
        }
        
        movementVector *= speed;
        movementVector.y = yVelocity;
        
        characterController.Move(movementVector * Time.deltaTime);
    }
}
