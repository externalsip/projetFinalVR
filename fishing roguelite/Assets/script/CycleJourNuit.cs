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
    public AudioClip birdsAndWavesClip; // Son avec des oiseaux et des vagues
    public AudioClip wavesOnlyClip;      // Son avec juste les vagues

    private AudioSource audioSource;
    private float initialTime = 7 * 60 + 30;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.loop = true;
        // Démarrage du son avec des oiseaux et des vagues
        PlayBirdsAndWaves();


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
        if (Time.time < initialTime)
        {
            // Si le temps est inférieur à 7 minutes et 30 secondes, jouer les deux sons
            if (!audioSource.isPlaying || audioSource.clip != birdsAndWavesClip)
            {
                PlayBirdsAndWaves();
                PlayWavesOnly();
            }
        }
        else
        {
            // Si le temps est supérieur ou égal à 7 minutes et 30 secondes, jouer le son juste les vagues
            if (!audioSource.isPlaying || audioSource.clip != wavesOnlyClip)
            {
                PlayWavesOnly();
            }
        }
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
    void PlayBirdsAndWaves()
    {
        audioSource.clip = birdsAndWavesClip;
        audioSource.Play();
    }

    void PlayWavesOnly()
    {
        audioSource.clip = wavesOnlyClip;
        audioSource.Play();
    }

}
