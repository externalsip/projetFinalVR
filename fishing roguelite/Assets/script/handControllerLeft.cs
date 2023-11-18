using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handControllerLeft : MonoBehaviour
{
    public bool isLeftHoldingRod;
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
        switch(other.tag)
        {
            case "RodTest":

                isLeftHoldingRod = true;
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch(other.tag)
        {
            case "RodTest":

                isLeftHoldingRod = false;
                break;
        }
    }
}
