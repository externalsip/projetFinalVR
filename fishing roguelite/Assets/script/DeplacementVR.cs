using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DeplacementVR : MonoBehaviour
{
    public XRController leftController; // Faites glisser votre contr�leur VR gauche ici dans l'�diteur Unity
    public float vitesseDeplacement = 1.0f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        if (leftController == null)
        {
            Debug.LogError("Veuillez assigner le contr�leur VR gauche dans l'inspecteur Unity.");
        }
    }

    void Update()
    {
        if (leftController != null && leftController.enableInputTracking)
        {
            InputDevice device = leftController.inputDevice;

            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 thumbstickValue))
            {
                // D�placement horizontal et vertical bas� sur le joystick gauche
                Vector3 deplacement = new Vector3(thumbstickValue.x, 0, thumbstickValue.y);
                characterController.Move(deplacement * vitesseDeplacement * Time.deltaTime);
            }
        }
    }
}
