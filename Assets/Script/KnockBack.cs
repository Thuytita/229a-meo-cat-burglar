using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] public float setTimeToTurn;
    [SerializeField] public float timer;
    [SerializeField] public bool turnCheck;
    public GameObject a;
    void Start()
    {
        timer = setTimeToTurn;
        turnCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (turnCheck == false)
        {
            transform.Translate(transform.forward *1*Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                turnCheck = true;
                timer = 0;
            }
            

        }
        if (turnCheck == true)
        {
            
            transform.Translate(transform.forward *-1*Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= setTimeToTurn)
            {
                turnCheck = false;
                timer = setTimeToTurn;
            }
        }
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            a.GetComponent<Rigidbody>().AddForce((a.transform.forward * -20) + (transform.up * 7), ForceMode.Impulse);
        }
    }
}
