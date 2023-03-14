using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

/* Ce script a pour but de g�rer le lancement des balles
 * � l'intention de Cheesecake. Si on appuie sur l'�cran, une
 * balle sera lanc� avec une vitesse proportionnelle au temps
 * d'appui. Celle-ci aura toujours son origine au milieu de l'�cran. */

public class ARTapToThrow : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private int speed;
    private GameObject spawnedObject;
    private Vector2 touchPosition;
    private float holdTime = 0;

    private void Update()
    {
        holdTime += Time.deltaTime; // On va incr�menter une variable � chaque frame servant � conna�tre le temps d'appui du joueur sur l'�cran
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
        /* Pour �viter que l'int�raction ne se d�clenche alors qu'on clique
        * sur un bouton de l'interface, on va d�finir une zone accept�e 
        * qui exclue la zone correspondant au bouton. */
        if (touchPosition.y > Screen.height * 0.2) ThrowBall(holdTime); // Si l'appui est dans la zone, on d�clenche la fonction
    }
    private void FingerDown(EnhancedTouch.Finger finger)
    {
        holdTime = 0; // Si le joueur commence � toucher l'�cran, le temps d'appui est r�initialis�
    }

    private void ThrowBall(float timeHeld)
    {
        GameObject thrownBall = Instantiate(ball, transform.position, transform.rotation); // On commence par instancier la balle a lancer dans un nouveau GameObject
        Rigidbody rbBall = thrownBall.GetComponent<Rigidbody>();
        var addForce = Mathf.Min(timeHeld, 3);
        if (timeHeld < 0.2) addForce = 1;
        rbBall.velocity = transform.TransformDirection(Vector3.forward * speed * addForce); // En fonction du temps d'appui et de la vitesse choisit, on va lancer la balle instanci�e
        Destroy(thrownBall,5f); // Au bout d'un certain temps, la balle est d�truite 
        holdTime = 0; // On re-initialise le temps d'appui pour pr�parer un nouveau lancement
    }
}
