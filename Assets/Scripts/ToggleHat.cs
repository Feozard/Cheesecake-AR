using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ce script est la personnalisation de la mascotte.
 Il est associé à un bouton sur l'écran et va 
 permettre d'afficher ou non un accessoir sur Cheesecake.*/

public class ToggleHat : MonoBehaviour
{
    private GameObject _cheesecake;
    private GameObject _hat;

    private bool hat_enabled = false; // Par défaut, le chapeau n'est pas visible
    void Start()
    {
        _cheesecake = null;
        _hat = null;
    }

    private void FindCheesecake()
    {
        _cheesecake = GameObject.FindGameObjectWithTag("Cheesecake");
        _hat = _cheesecake.transform.Find("Hat").gameObject;
    }
    public void SwitchHat()
    {
        if (_cheesecake == null || _hat == null)
        {
            FindCheesecake();
        }
        
        _hat.SetActive(hat_enabled);
        hat_enabled = !hat_enabled; // On va inverser l'état de cette variable a chaque fois pour connaitre l'état du chapeau (affiché ou non)
    }
}
