using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] private float mass;
    [SerializeField] private float acceleration;
    [SerializeField] private float force;
    [SerializeField] private float jumpForce;

    public bool groundCheck = false;
    public float groundCheckDistance;
    public float bufferCheckDistance = 0.1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mass = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        groundCheck = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance);
        
        
        force = mass * acceleration;

        Jump();
        
        
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        
        
        //rb.AddForce(move * 5,ForceMode.Force);
        //rb.MovePosition(transform.position+(move*0.1f));
        transform.Translate((moveDirection * force *Time.deltaTime));
        

        
    }
    private void Jump()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    
}
