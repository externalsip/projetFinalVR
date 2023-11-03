using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
//public XR.DirectInteractor rHand;
//public XR.DirectInteractor lHand;


public class fishingRodTests : XRGrabInteractable
{
    public GameObject rod;
    public GameObject hook;
    bool isHoldingItem = false;

    private ControllerVelocity controllerVelocity = null;
    private MeshRenderer meshRenderer = null;

    protected override void Awake()
    {
        base.Awake();
        meshRenderer = GetComponent<MeshRenderer>();

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerVelocity = args.interactor.GetComponent<ControllerVelocity>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        { 
            if(isHoldingItem)
            {
                bool triggerValue;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {

                }
            }
        }

    }

    public void SelectTest()
    {
        isHoldingItem=true;

    }
    public void StopHolding()
    {
        isHoldingItem=false;
    }
}
