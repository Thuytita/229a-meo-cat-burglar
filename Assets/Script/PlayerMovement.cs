using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] Rigidbody rb;
    [SerializeField] private float mass;
    [SerializeField] private float acceleration;
    [SerializeField] private float force;
    [SerializeField] private float jumpForce;
    
    [Header("Force Limit")]
    [SerializeField] public Vector3 limitVel;

    [Header("Ground Check")]
    public bool groundCheck = false;
    public float groundCheckDistance;
    public float Myfloat;
    
    [Header("Timer Unlock Force Limit In Air")]
    public bool check;
    public float setTimer;
    public float t;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mass = rb.mass;
        
        check = false;
        t = setTimer;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (check == true)
        { 
            t -= Time.deltaTime;
            if (t < 0)
            {
                check = false;
                t = setTimer;
            }
        }
        
        RaycastHit hit;
        groundCheck = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance);
        
        
        force = mass * acceleration;

        Jump();
        SpeedControl();
        
        
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        
        
        if (moveDirection.magnitude >= 0.1f)
        {
            float Angle = Mathf.Atan2(moveDirection.x , moveDirection.z) * Mathf.Rad2Deg; 
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y , Angle , ref Myfloat , 0.1f); 
            transform.rotation = Quaternion.Euler(0,Smooth,0);
        }
        
        rb.AddForce(moveDirection * force, ForceMode.Force);
        

        
    }
    private void Jump()
    { 
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //limit velocity 
        if (flatvel.magnitude > force && check != true)
        {
            limitVel = flatvel.normalized * force;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
        //if hit enemy,not using limit but using force from enemy knockback
        if (check == true)
        {
            
            Debug.Log("Triggered by player");
            Debug.Log("ZDFVGBNM<");
        }
        Debug.Log(rb.velocity);
        //Debug.Log("out = "+ Knockback.check );

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            check = true;
        }
    }
}
