using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/* Le but de ce script est de placer la mascotte (Cheesecake)
 * sur une surface d�tect�e � l'aide de la cam�ra. � chaque clique 
 * sur une surface, la mascotte sera plac�e. Elle sera pr�sente une
 * seule fois � l'�cran. */

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceCheesecake: MonoBehaviour
{
    [SerializeField] private GameObject cheesecake;
    private GameObject spawnedObject;
    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    static List <ARRaycastHit> hits = new List<ARRaycastHit>();
    private void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // Si l'�cran est touch�, on va r�cup�rer sa position
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            /* Pour �viter que l'int�raction ne se d�clenche alors qu'on clique
             * sur un bouton de l'interface, on va d�finir une zone accept�e 
             * qui exclue la zone correspondant au bouton. */
            if (touchPosition.y > Screen.height * 0.2) return true; // Si l'appui est dans la zone, on renvoie True
            return false; // Sinon on renvoie false
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        // � chaque frame, on va voir si la zone choisit a �t� touch� par l'utilisateur 
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return; // Si aucune int�raction n'a �t� enregistr�, on termine la fonction
        if(_arRaycastManager.Raycast (touchPosition, hits, trackableTypes : TrackableType.PlaneWithinPolygon ))
        {
            var hitPose = hits[0].pose;
            if(spawnedObject == null) // Si Cheesecake n'a pas encore �t� plac�, on l'instantie une premi�re fois
            {
                spawnedObject = Instantiate(cheesecake, hitPose.position, hitPose.rotation);
            }
            else // Sinon, on le d�place � la nouvelle position
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}