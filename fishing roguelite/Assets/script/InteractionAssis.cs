using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionAssis : MonoBehaviour
{
    public XRGrabInteractable bancInteractable;  // R�f�rence � l'interactable du banc
    public GameObject rameGauchePrefab;  // R�f�rence � la rame gauche


    private bool isSitting = false;

    void Start()
    {
        // S'assure que l'interactable est configur� correctement
        if (bancInteractable == null)
        {
            Debug.LogError("Veuillez attribuer l'interactable du banc dans l'inspecteur Unity.");
        }
        else
        {
            // Ajoute un gestionnaire pour l'�v�nement OnSelectEntered
            bancInteractable.selectEntered.AddListener(OnSelectEntered);
        }
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Le joueur a s�lectionn� l'interactable (banc)
        if (!isSitting)
        {
            SitOnBench();
        }
    }

    void SitOnBench()
    {
        // Place le joueur sur le banc de la barque (� adapter selon votre setup VR)
        Debug.Log("Le joueur s'assoit sur le banc.");


        // Fait appara�tre la rame gauche dans la main gauche du joueur
        //GameObject rameGauche = Instantiate(rameGauchePrefab, transform.position, transform.rotation);

        GameObject rameGauche = null;

        Debug.Log("Position apr�s l'instantiation : " + rameGauche.transform.position);


        XRController leftController = FindObjectOfType<XRController>(); // Trouve le contr�leur (� ajuster selon votre configuration)


        if (leftController != null)
        {
            // Obtient la position de la main gauche du joueur
            Vector3 handPosition = leftController.transform.position;

            // Debug : Affiche la position avant l'instantiation
            Debug.Log("Position avant l'instantiation : " + handPosition);

            // Fait appara�tre la rame gauche � la position de la main gauche du joueur
            rameGauche = Instantiate(rameGauchePrefab, handPosition, Quaternion.identity);

            // Debug : Affiche la position apr�s l'instantiation
            Debug.Log("Position apr�s l'instantiation : " + rameGauche.transform.position);

            // R�initialise la rotation de la rame avant de l'attacher � la main
            rameGauche.transform.rotation = Quaternion.identity;

            // Attache la rame gauche au contr�leur de la main gauche
            rameGauche.transform.parent = leftController.gameObject.transform;
        }

        // Marque le joueur comme assis pour �viter de r�p�ter l'interaction
        isSitting = true;
    }
}