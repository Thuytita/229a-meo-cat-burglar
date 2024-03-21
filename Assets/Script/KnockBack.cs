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
    public GameObject player;
    void Start()
    {
        timer = setTimeToTurn;
        turnCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if it not time to turnback, so it not.
        if (turnCheck == false)
        {
            transform.Translate(transform.forward *1*Time.deltaTime);
            timer -= Time.deltaTime;
            if (timer <= 0) //wait to 0 to turn back
            {
                turnCheck = true;
                timer = 0;
            }
            

        }
        //if it time to turnback, then do it.
        if (turnCheck == true)
        {
            
            transform.Translate(transform.forward *-1*Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= setTimeToTurn) //wait to setTimeToTurn to turn back
            {
                turnCheck = false;
                timer = setTimeToTurn;
            }
        }
        
        
    }

    //hit Gameobect with Tag "Player" and Knok Back it, with tag make it more accurate and less bugs
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Rigidbody>().AddForce((player.transform.forward * -20) + (transform.up * 7), ForceMode.Impulse);
        }
    }
}
