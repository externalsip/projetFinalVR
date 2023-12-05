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
    public bool isFishing = false;
    public float maxSpeed = 10f;

    public bool rodHand = false;
    public int controllerNum;

    public bool rodToggle = false;
    public AudioClip castLine;      // Son de canne à pêche
    public AudioClip castLineLong;      // Son de canne à pêche long
    public AudioClip click;
    public AudioClip fishSound;
    public AudioClip whipSound;
    private AudioSource audioSource;
    public float forceMultiplier = 5f;

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        controllerVelocity = args.interactorObject.transform.GetComponent<ControllerVelocity>();

        if(args.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor != null)
        {
            var controller = controllerInteractor.xrController;
            switch(controller.name)
            {
                case "Left Controller":
                    controllerNum = 0;
                    break;
                case "Right Controller":
                    controllerNum = 1; 
                    break;
            }

            //Determines which hand is holding the object
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        controllerVelocity = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);


        if (isSelected)
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
        var controllers = new List<UnityEngine.XR.InputDevice>();

        switch (controllerNum)
        {
            case 0:
                var desiredCharacteristicsLeft = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller;
                UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsLeft, controllers);

                break;
            case 1:
                var desiredCharacteristicsRight = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
                UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristicsRight, controllers);

                break;
        }

        foreach (var device in controllers)
        {
            bool triggerValue;
            bool primBtnValue;
            bool secBtnValue;

            relativeDifference = hook.transform.position - rod.transform.position;

            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {

                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out secBtnValue) && secBtnValue)
                {
                    if (isThrown == false)
                    {
                        PlayCastingLineLong();
                        WhipSound();
                        var hookBody = hook.GetComponent<Rigidbody>();
                        var hookJoint = hook.GetComponent<CharacterJoint>();
                        Destroy(hookJoint);
                        Debug.Log(velocity);
                        hookBody.AddForce(velocity * forceMultiplier, ForceMode.Impulse);
                        hasJoint = false;
                        isThrown = true;

                    }

                }

            }
            if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out primBtnValue) && primBtnValue)
            {


                if (hasJoint == false && isFishing == false)
                {
                    PlayCastingLine();

                    var hookBody = hook.GetComponent<Rigidbody>();
                    hookBody.velocity = Vector3.zero;
                    hookBody.angularVelocity = Vector3.zero;
                    hook.transform.position = hookHint.transform.position;
                    hook.AddComponent<CharacterJoint>();
                    hook.GetComponent<CharacterJoint>().connectedBody = rod.GetComponent<Rigidbody>();
                    hook.GetComponent<CharacterJoint>().anchor = new Vector3(0, 0.23f, 0);
                    hook.GetComponent<CharacterJoint>().axis = new Vector3(1, 0, 0);
                    hook.GetComponent<CharacterJoint>().enableProjection = true;
                    hasJoint = true;
                    isThrown = false;
                }

            }
        }
        

        }

    public void reAttachHook()
    {
        if(!hasJoint && !isFishing)
        {
            FishCaught();
            var hookBody = hook.GetComponent<Rigidbody>();
            hookBody.velocity = Vector3.zero;
            hookBody.angularVelocity = Vector3.zero;
            hook.transform.position = hookHint.transform.position;
            hook.AddComponent<CharacterJoint>();
            hook.GetComponent<CharacterJoint>().connectedBody = rod.GetComponent<Rigidbody>();
            hook.GetComponent<CharacterJoint>().anchor = new Vector3(0, 0.23f, 0);
            hook.GetComponent<CharacterJoint>().axis = new Vector3(1, 0, 0);
            hook.GetComponent<CharacterJoint>().enableProjection = true;
            hasJoint = true;
            isThrown = false;
        }
    }

    public void PlayCastingLine()
    {
        audioSource.clip = castLine;

        audioSource.Play();
    }
    public void PlayCastingLineLong()
    {
        audioSource.clip = whipSound;
        audioSource.clip = castLineLong;
        audioSource.Play();
    }
    public void PlayClick()
    {
        audioSource.clip = click;
        audioSource.Play();
    }
    public void FishCaught()
    {
        audioSource.clip = castLineLong;
        audioSource.Play();
    }
    public void WhipSound()
    {
        audioSource.clip = whipSound;
        audioSource.Play();
    }


    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = false;

    }

    // Update is called once per frame
    void Update()
    {
       // bool joystickVal;
      //  bool firstExecution = true;
       /* if (inputDevices[controllerNum].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxisClick, out joystickVal) && joystickVal)
        {
            if (firstExecution)
            {
                if (rodToggle)
                {
                    rodToggle = false;
                   // spawnRod();
                }
                else
                {
                    rodToggle = true;
                }
                firstExecution = false;
            }
        }
        else
        {
            firstExecution = true;
        }*/
    }


   /* void spawnRod()
    {
        switch (rodHand)
        {
            case true:
                leftHand.GetComponent<XRRayInteractor>().IsSelecting(this);
                break;
            case false:
                rightHand.GetComponent<XRRayInteractor>().IsSelecting(this);
                break;
        }
    }*/
}
