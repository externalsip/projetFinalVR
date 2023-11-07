using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private Vector3 relativeDifference;
    private ControllerVelocity controllerVelocity = null;
    public GameObject hookHint;
    public bool hasJoint = true;
    public bool isThrown = false;
    public float maxSpeed = 10f;
    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerVelocity = args.interactorObject.transform.GetComponent<ControllerVelocity>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerVelocity = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if(isSelected)
        {
            if(updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
            {

                HookThrow();
            }
        }
    }

    private void HookThrow()
    {                            

        Vector3 velocity = controllerVelocity ? controllerVelocity.Velocity : Vector3.zero;
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        
        foreach (var device in inputDevices)
        {
            bool triggerValue;
            bool primBtnValue;
            bool secBtnValue;

            relativeDifference = hook.transform.position - rod.transform.position;

            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {        

                    if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secBtnValue) && secBtnValue)
                {
                    if(isThrown == false)
                    {
                        var hookBody = hook.GetComponent<Rigidbody>();
                        var hookJoint = hook.GetComponent<HingeJoint>();
                        Destroy(hookJoint);
                        Debug.Log(velocity);
                        hookBody.AddForce(velocity*50, ForceMode.Impulse);
                        hookBody.velocity = Vector3.ClampMagnitude(velocity*50, maxSpeed);
                        hasJoint = false;
                        isThrown = true;

                    }

                }

                }
                if(device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primBtnValue) && primBtnValue)
            {   Debug.Log("status: " + hasJoint);
                if (hasJoint == false)
                {

                    Debug.Log("called");
                    var hookBody = hook.GetComponent<Rigidbody>();
                    hookBody.velocity = Vector3.zero;
                    hookBody.angularVelocity = Vector3.zero;
                    hook.transform.position = hookHint.transform.position;
                    hook.AddComponent<HingeJoint>();
                    hook.GetComponent<HingeJoint>().connectedBody = rod.GetComponent<Rigidbody>();

                    hasJoint = true;
                    isThrown = false;
                }

            }

        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
