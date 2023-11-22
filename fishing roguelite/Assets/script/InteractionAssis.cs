using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionAssis : MonoBehaviour
{
    public XRGrabInteractable bancInteractable;  // Référence à l'interactable du banc
    public GameObject rameGauchePrefab;  // Référence à la rame gauche


    private bool isSitting = false;

    void Start()
    {
        // S'assure que l'interactable est configuré correctement
        if (bancInteractable == null)
        {
            Debug.LogError("Veuillez attribuer l'interactable du banc dans l'inspecteur Unity.");
        }
        else
        {
            // Ajoute un gestionnaire pour l'événement OnSelectEntered
            bancInteractable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Le joueur a sélectionné l'interactable (banc)
        if (!isSitting)
        {
            SitOnBench();
        }
    }

    void SitOnBench()
    {
        // Place le joueur sur le banc de la barque (à adapter selon votre setup VR)
        Debug.Log("Le joueur s'assoit sur le banc.");


        // Fait apparaître la rame gauche dans la main gauche du joueur
        //GameObject rameGauche = Instantiate(rameGauchePrefab, transform.position, transform.rotation);

        GameObject rameGauche = null;

        Debug.Log("Position après l'instantiation : " + rameGauche.transform.position);


        XRController leftController = FindObjectOfType<XRController>(); // Trouve le contrôleur (à ajuster selon votre configuration)


        if (leftController != null)
        {
            // Obtient la position de la main gauche du joueur
            Vector3 handPosition = leftController.transform.position;

            // Debug : Affiche la position avant l'instantiation
            Debug.Log("Position avant l'instantiation : " + handPosition);

            // Fait apparaître la rame gauche à la position de la main gauche du joueur
            rameGauche = Instantiate(rameGauchePrefab, handPosition, Quaternion.identity);

            // Debug : Affiche la position après l'instantiation
            Debug.Log("Position après l'instantiation : " + rameGauche.transform.position);

            // Réinitialise la rotation de la rame avant de l'attacher à la main
            rameGauche.transform.rotation = Quaternion.identity;

            // Attache la rame gauche au contrôleur de la main gauche
            rameGauche.transform.parent = leftController.gameObject.transform;
        }

        // Marque le joueur comme assis pour éviter de répéter l'interaction
        isSitting = true;
    }
}