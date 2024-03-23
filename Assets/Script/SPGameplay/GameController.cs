using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    public int totalItems; // Total number of items in the collection
    public int itemsCollected; // Current number of items collected by the player
    

    // Update UI to reflect current collection progress
    void UpdateUI()
    {
        // Update UI elements such as progress bars, text displays, etc.
    }

    // Call this method whenever an item is collected
    public void CollectItem()
    {
        itemsCollected++;
        UpdateUI();

        // Check if all items are collected
        if (itemsCollected >= totalItems)
        {
            Credit();
        }
    }

    // Trigger end game sequence
    void Credit()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
