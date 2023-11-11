using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class handleBasket : MonoBehaviour
{

    public TextMeshPro fishNumber;
    public TextMeshPro fishValue;
    public playerInventory playerInventory;
    private int fishAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "fish")
        {
            fishAmount++;
            var fish = collision.gameObject;
            int fishWorth = fish.GetComponent<fishData>().Worth;
            Debug.Log(fishWorth);
            fishNumber.text = "Nombre de Poissons: " + fishAmount.ToString();
            fishValue.text = "Valeur du Poisson: " + fishWorth.ToString();
            playerInventory.addSlot();
            playerInventory.playerInventoryContent[playerInventory.playerInventoryContent.Length - 1] = fish;
            fish.SetActive(false);
        }
    }
}
