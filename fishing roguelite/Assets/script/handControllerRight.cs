using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handControllerRight : MonoBehaviour
{
    public bool isRightHoldingRod;
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
            case "RodTest":
                Debug.Log("rightHold");
                isRightHoldingRod = true;
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "RodTest":
                isRightHoldingRod = false;
                break;
        }
    }
}
