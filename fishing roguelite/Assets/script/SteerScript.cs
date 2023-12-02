using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerScript : MonoBehaviour
{
    public float currentRotate = 0;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "rightZone":
                Debug.Log("hit");
                currentRotate = 2f;
                break;
            case "neutralZone":
                currentRotate = 0;
                break;
            case "leftZone":
                currentRotate = -2f;
                break;
        }
    }
}
