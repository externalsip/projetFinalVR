using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookScript : MonoBehaviour

{
    public timingGamePrototype timingGamePrototype;

    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(6, 3, true);
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
                timingGamePrototype.hookInWater();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var zone = other;
        switch (zone.tag) 
        {
            case "water":
                Debug.Log("outWater");
                timingGamePrototype.hookOutwater();
                break;
        }
    }

}
