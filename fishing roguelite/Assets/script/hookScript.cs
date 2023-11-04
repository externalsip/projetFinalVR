using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookScript : MonoBehaviour
{
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
        var zone = other;
        switch (zone.tag)
        {
            case "water":
                Debug.Log("enteredWater");
                break;
        }
    }
}
