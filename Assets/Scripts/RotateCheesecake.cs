using UnityEngine;

/* L'objectif de ce script est de faire en sorte
 * que Cheesecake soit constamment orienté vers le joueur. */

public class RotateCheesecake : MonoBehaviour
{
    private Camera player;

    private void Start()
    {
        player = Camera.main; // On récupère la main camera, représentant le joueur 
    }

    void Update()
    {
        /* On utilise la fonction LookAt pour dire au GameObject
         * vers quoi celui-ci doit regarder, en spécifiant que
         * seul l'axe Y doit être modifié. De cette manière, Cheesecake
         * conservera une position horizontal peu importe où se place le joueur. */
        this.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)); 
    }
}
