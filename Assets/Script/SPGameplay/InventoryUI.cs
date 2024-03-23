using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    private TextMeshProUGUI dimondText;
    
    // Start is called before the first frame update
    void Start()
    {
        dimondText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDimondText(PlayerInventory playerInventory)
    {
        dimondText.text = playerInventory.NumberOfDiamonds.ToString();
    }
}
