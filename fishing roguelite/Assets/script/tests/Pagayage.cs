using UnityEngine;

public class Pagayage : MonoBehaviour
{
    public GameObject rameGauche;
    public GameObject rameDroite;
    public float vitesseDePagayage = 1.0f;

    void Update()
    {
        // Assurez-vous d'avoir configuré correctement les contrôleurs VR pour détecter le mouvement de pagayage
        float mouvementRameGauche = rameGauche.transform.position.y; // ajustez en fonction de votre configuration
        float mouvementRameDroite = rameDroite.transform.position.y; // ajustez en fonction de votre configuration

        // Calcul de la vitesse de la barque en fonction du mouvement des rames
        float vitesseBarque = (mouvementRameGauche + mouvementRameDroite) * vitesseDePagayage;

        // Déplacez votre barque en fonction de la vitesse calculée
        // Assurez-vous d'avoir un Rigidbody attaché à votre barque pour déplacer correctement en physique
        // Vous pouvez ajuster la direction en fonction de l'orientation des rames par rapport à votre barque
        transform.Translate(Vector3.forward * vitesseBarque * Time.deltaTime);
    }
}
