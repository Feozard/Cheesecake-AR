using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/* Le but de ce script est de placer la mascotte (Cheesecake)
 * sur une surface détectée à l'aide de la caméra. À chaque clique 
 * sur une surface, la mascotte sera placée. Elle sera présente une
 * seule fois à l'écran. */

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
        // Si l'écran est touché, on va récupérer sa position
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(index: 0).position;
            /* Pour éviter que l'intéraction ne se déclenche alors qu'on clique
             * sur un bouton de l'interface, on va définir une zone acceptée 
             * qui exclue la zone correspondant au bouton. */
            if (touchPosition.y > Screen.height * 0.2) return true; // Si l'appui est dans la zone, on renvoie True
            return false; // Sinon on renvoie false
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        // À chaque frame, on va voir si la zone choisit a été touché par l'utilisateur 
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return; // Si aucune intéraction n'a été enregistré, on termine la fonction
        if(_arRaycastManager.Raycast (touchPosition, hits, trackableTypes : TrackableType.PlaneWithinPolygon ))
        {
            var hitPose = hits[0].pose;
            if(spawnedObject == null) // Si Cheesecake n'a pas encore été placé, on l'instantie une première fois
            {
                spawnedObject = Instantiate(cheesecake, hitPose.position, hitPose.rotation);
            }
            else // Sinon, on le déplace à la nouvelle position
            {
                spawnedObject.transform.position = hitPose.position;
            }
        }
    }
}