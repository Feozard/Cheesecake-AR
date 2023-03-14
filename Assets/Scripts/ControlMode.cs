using TMPro;
using UnityEngine;

/* Avec ce script, on va pouvoir changer de mode de jeu.
 * � l'aide d'un bouton, on va basculer l'activation des scripts
 * servant � placer Cheesecake et � lancer une balle.
 * Par d�faut, c'est le script de placement de Cheesecake qui est activ�. 
 * Si l'utilisateur appuie sur le bouton, le placement de Cheesecake
 * sera d�sactiv� et c'est le script servant � lancer une balle
 * qui est activ�. */

public class ControlMode : MonoBehaviour
{
    [SerializeField] private ARTapToThrow arTapScript;
    [SerializeField] private PlaceCheesecake placeScript;
    [SerializeField] private TMP_Text displayText; // Ce texte va nous servir � afficher quel est le mode actuel
    private int selector;

    private void Start()
    {
        selector = 1; // Par d�faut, le selector est initialis� � 1 
        arTapScript.enabled = false;
        placeScript.enabled = true;
        SwitchMode();
    }

    public void SwitchMode()
    {
        switch(selector)
        {
            case 0: // Si le selector est � 0, on d�sactive le placement de Cheesecake et active le lancement de balle
                arTapScript.enabled = true;
                placeScript.enabled = false;
                selector = 1; // On change le selector � 1 pour la prochaine fois
                displayText.text = "Mode : Lancer une balle";
                break;
            case 1: // Si le selector est � 1, on active le placement de Cheesecake et d�sactive le lancement de balle
                arTapScript.enabled = false;
                placeScript.enabled = true;
                selector = 0; // On change le selector � 0 pour la prochaine fois
                displayText.text = "Mode : Placer Cheesecake";
                break;
        }
    }
}
