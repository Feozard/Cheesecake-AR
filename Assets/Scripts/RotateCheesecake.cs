using UnityEngine;

/* L'objectif de ce script est de faire en sorte
 * que Cheesecake soit constamment orient� vers le joueur. */

public class RotateCheesecake : MonoBehaviour
{
    private Camera player;

    private void Start()
    {
        player = Camera.main; // On r�cup�re la main camera, repr�sentant le joueur 
    }

    void Update()
    {
        /* On utilise la fonction LookAt pour dire au GameObject
         * vers quoi celui-ci doit regarder, en sp�cifiant que
         * seul l'axe Y doit �tre modifi�. De cette mani�re, Cheesecake
         * conservera une position horizontal peu importe o� se place le joueur. */
        this.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z)); 
    }
}
