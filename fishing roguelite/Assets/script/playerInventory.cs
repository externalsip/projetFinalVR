using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    public GameObject[] playerInventoryContent;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addSlot()
    {
       var playerInventoryContentTransfer = new GameObject[playerInventoryContent.Length + 1];
        if (playerInventoryContent.Length > 0)
        {
            for (int i = 0; i < playerInventoryContent.Length; i++) {
                playerInventoryContentTransfer[i] = playerInventoryContent[i];
            }
            playerInventoryContent = playerInventoryContentTransfer;
        }
        else
        {
            playerInventoryContent = new GameObject[1];
        }

    }
}
