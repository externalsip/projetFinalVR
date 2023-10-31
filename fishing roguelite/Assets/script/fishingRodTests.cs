using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
//public XR.DirectInteractor rHand;
//public XR.DirectInteractor lHand;


public class fishingRodTests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("kindaworks");
    }

    // Update is called once per frame
    void Update()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);


        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.characteristics.ToString()));
        }
    }
}
