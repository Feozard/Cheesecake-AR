using TMPro;
using UnityEngine;

/* Avec ce script, on va pouvoir changer de mode de jeu.
 * À l'aide d'un bouton, on va basculer l'activation des scripts
 * servant à placer Cheesecake et à lancer une balle.
 * Par défaut, c'est le script de placement de Cheesecake qui est activé. 
 * Si l'utilisateur appuie sur le bouton, le placement de Cheesecake
 * sera désactivé et c'est le script servant à lancer une balle
 * qui est activé. */

public class ControlMode : MonoBehaviour
{
    [SerializeField] private ARTapToThrow arTapScript;
    [SerializeField] private PlaceCheesecake placeScript;
    [SerializeField] private TMP_Text displayText; // Ce texte va nous servir à afficher quel est le mode actuel
    private int selector;

    private void Start()
    {
        selector = 1; // Par défaut, le selector est initialisé à 1 
        arTapScript.enabled = false;
        placeScript.enabled = true;
        SwitchMode();
    }

    public void SwitchMode()
    {
        switch(selector)
        {
            case 0: // Si le selector est à 0, on désactive le placement de Cheesecake et active le lancement de balle
                arTapScript.enabled = true;
                placeScript.enabled = false;
                selector = 1; // On change le selector à 1 pour la prochaine fois
                displayText.text = "Mode : Lancer une balle";
                break;
            case 1: // Si le selector est à 1, on active le placement de Cheesecake et désactive le lancement de balle
                arTapScript.enabled = false;
                placeScript.enabled = true;
                selector = 0; // On change le selector à 0 pour la prochaine fois
                displayText.text = "Mode : Placer Cheesecake";
                break;
        }
    }
}
