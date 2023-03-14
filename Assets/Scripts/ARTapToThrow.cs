using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

/* Ce script a pour but de gérer le lancement des balles
 * à l'intention de Cheesecake. Si on appuie sur l'écran, une
 * balle sera lancé avec une vitesse proportionnelle au temps
 * d'appui. Celle-ci aura toujours son origine au milieu de l'écran. */

public class ARTapToThrow : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private int speed;
    private GameObject spawnedObject;
    private Vector2 touchPosition;
    private float holdTime = 0;

    private void Update()
    {
        holdTime += Time.deltaTime; // On va incrémenter une variable à chaque frame servant à connaître le temps d'appui du joueur sur l'écran
    }

    private void OnEnable()
    {
        EnhancedTouch.TouchSimulation.Enable();
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += FingerDown;
        EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        EnhancedTouch.TouchSimulation.Disable();
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= FingerDown;
        EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }

    private void FingerUp(EnhancedTouch.Finger finger)
    {
        if (finger.index != 0) return;
        touchPosition = Input.GetTouch(index: 0).position;
        /* Pour éviter que l'intéraction ne se déclenche alors qu'on clique
        * sur un bouton de l'interface, on va définir une zone acceptée 
        * qui exclue la zone correspondant au bouton. */
        if (touchPosition.y > Screen.height * 0.2) ThrowBall(holdTime); // Si l'appui est dans la zone, on déclenche la fonction
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        holdTime = 0; // Si le joueur commence à toucher l'écran, le temps d'appui est réinitialisé
    }

    private void ThrowBall(float timeHeld)
    {
        GameObject thrownBall = Instantiate(ball, transform.position, transform.rotation); // On commence par instancier la balle a lancer dans un nouveau GameObject
        Rigidbody rbBall = thrownBall.GetComponent<Rigidbody>();
        var addForce = Mathf.Min(timeHeld, 3);
        if (timeHeld < 0.2) addForce = 1;
        rbBall.velocity = transform.TransformDirection(Vector3.forward * speed * addForce); // En fonction du temps d'appui et de la vitesse choisit, on va lancer la balle instanciée
        Destroy(thrownBall,5f); // Au bout d'un certain temps, la balle est détruite 
        holdTime = 0; // On re-initialise le temps d'appui pour préparer un nouveau lancement
    }
}
