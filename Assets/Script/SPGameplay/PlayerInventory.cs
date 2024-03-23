using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    public int NumberOfDiamonds { get; private set; }
    public UnityEvent<PlayerInventory> OnDiamondCollected;
    public int totalItems;

    public void DiamondCollected()
    {
        NumberOfDiamonds++;
        OnDiamondCollected.Invoke(this);
        
        // Check if all items are collected
        if (NumberOfDiamonds == totalItems)
        {
            Credit();
        }
    }
    void Credit()
    {
        SceneManager.LoadSceneAsync(2);
    }
    
    
}
