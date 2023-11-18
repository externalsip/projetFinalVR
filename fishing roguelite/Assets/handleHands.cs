using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleHands : MonoBehaviour
{

    public bool leftHandHeld;
    public bool rightHandHeld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "leftHand":
                Debug.Log("heldLeft");
                leftHandHeld = true;
                rightHandHeld = false;
                break;
            case "rightHand":
                Debug.Log("heldRight");
                leftHandHeld = false;
                rightHandHeld = true;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        leftHandHeld = false;
        rightHandHeld = false;
    }
}
