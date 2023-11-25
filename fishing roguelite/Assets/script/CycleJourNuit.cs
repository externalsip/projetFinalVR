using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;


public class CycleJourNuit : MonoBehaviour
{
    public Transform sunTransform;
    public Transform moonTransform;
    public float vitesseRotation = 24.0f; // Vitesse de rotation en degrés par minute
    public GameObject Soleil;

    public TextMeshProUGUI jour;
    public int joursPasse = 0;



    private void Start()
    {

        if (sunTransform == null || moonTransform == null)
        {
            Debug.LogError("Les transformées pour Sun et Moon doivent être attribuées dans l'inspecteur.");
        }
        // Appeler la fonction MaFonction toutes les 15 minutes, en commençant après 0 secondes.
        InvokeRepeating("MaFonction", 0f, 15f * 60f);
    }

    private void Update()
    {
        SynchroniserRotation();
    }

    private void SynchroniserRotation()
    {
        float angleRotation = (Time.time * vitesseRotation) % 360.0f;

        // Synchroniser la rotation du soleil et de la lune
        sunTransform.rotation = Quaternion.Euler(new Vector3(angleRotation, 0, 0));
        moonTransform.rotation = Quaternion.Euler(new Vector3(angleRotation + 180.0f, 0, 0));

    }

    private void MaFonction()
    {
        joursPasse++;
        Debug.Log(joursPasse);
        jour.text = "Jour " + joursPasse.ToString();
    }
}