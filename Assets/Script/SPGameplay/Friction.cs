using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friction : MonoBehaviour
{
    public static float friction;
    public static bool nonStickyCheck;

    [SerializeField] private float mu;
    [SerializeField] private Rigidbody player;

    void Start()
    {
        nonStickyCheck = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //friction formular f= mu * mg
            friction = player.mass * mu * 10;
                    
            //if firction less than zero that mean for slippery...
            //but in real life it close to zero but I make it more relate ;) 
            if (friction > 0) 
            {
                nonStickyCheck = false;
            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            friction = 0;
            nonStickyCheck = true;
        }
        
    }
}
