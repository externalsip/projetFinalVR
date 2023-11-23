using UnityEngine;

public class Pagayage : MonoBehaviour
{
    public GameObject rameGauche;
    public GameObject rameDroite;
    public float vitesseDePagayage = 1.0f;

    void Update()
    {
        // Assurez-vous d'avoir configur� correctement les contr�leurs VR pour d�tecter le mouvement de pagayage
        float mouvementRameGauche = rameGauche.transform.position.y; // ajustez en fonction de votre configuration
        float mouvementRameDroite = rameDroite.transform.position.y; // ajustez en fonction de votre configuration

        // Calcul de la vitesse de la barque en fonction du mouvement des rames
        float vitesseBarque = (mouvementRameGauche + mouvementRameDroite) * vitesseDePagayage;

        // D�placez votre barque en fonction de la vitesse calcul�e
        // Assurez-vous d'avoir un Rigidbody attach� � votre barque pour d�placer correctement en physique
        // Vous pouvez ajuster la direction en fonction de l'orientation des rames par rapport � votre barque
        transform.Translate(Vector3.forward * vitesseBarque * Time.deltaTime);
    }
}
