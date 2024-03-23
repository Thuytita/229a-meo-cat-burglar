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
    
    [Header("Unlock Force Limit In Air")]
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
        //for set time to unlock Force limit for a bit for make knock back feel forceful
        if (check == true)
        { 
            t -= Time.deltaTime;
            if (t < 0)
            {
                check = false;
                t = setTimer;
            }
        }
        
        //check ground by using Raycast
        RaycastHit hit;
        groundCheck = Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance);
        
        //F = ma - f
        force = (mass * acceleration) - Friction.friction;

        Jump();
        SpeedControl();
        
        //detect direction from keyboard or arrow (Horizontal = A or D , Vertical = W or S)
        Vector3 moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        moveDirection.Normalize();
        
        //make Character rotate along with input direction (moveDirection)
        if (moveDirection.magnitude >= 0.1f)
        {
            float Angle = Mathf.Atan2(moveDirection.x , moveDirection.z) * Mathf.Rad2Deg; 
            float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y , Angle , ref Myfloat , 0.1f); 
            transform.rotation = Quaternion.Euler(0,Smooth,0);
        }
        
        //add Force along with direction
        rb.AddForce(moveDirection * force, ForceMode.Force);
        Debug.Log(Friction.friction);



    }
    
    
    private void Jump()
    { 
        //if press spce = jump whenever player grounded or not in sticky floor
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck && Friction.nonStickyCheck)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //limit velocity
        //if hit Loomba(Enemy) ,not using limit but using force from enemy knockback
        if (flatvel.magnitude > force && check != true)
        {
            limitVel = flatvel.normalized * force;
            rb.velocity = new Vector3(limitVel.x, rb.velocity.y, limitVel.z);
        }
        
        
        Debug.Log(rb.velocity); // show direction and force limit in Console
        

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            check = true; //if Player hit GameObject with "Enemy" Tag send check = true
                          //and unlock Force limit for shot because it would make Knock Back feel forceful
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fish")
        {
            Destroy(other.gameObject); 
        }
    }*/

    
}
