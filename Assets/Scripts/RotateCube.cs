using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    private float spinForce;        //facteur de la vitesse de rotation de l'instance sur elle-même

    private float rotateForce;      //facteur de la vitesse de rotation de l'instance autour du centre
    private GameObject target;  //variable dans laquelle on va mettre le cube central pour pouvoir définir le centre de rotation

    //********************** Initialisation ******************************************************************
    void Start()
    {
        spinForce = Random.Range(0, 200);        //Choisi une valeur aléatoire entre 0 et 200 pour la vitesse de rotation sur soi-même

        rotateForce = Random.Range(0, 200);     //choisi une valeur aléatoire entre 0 et 200 pour la vitesse de rotation autour du centre
        target = GameObject.Find("CentralCube"); //Recherche le cube central qui définit le centre autour duquel les instances tournent
    }

    //********************** Rotations de l'instance frame par frame ******************************************
    void Update()
    {
        transform.Rotate(new Vector3(0f, spinForce * Time.deltaTime, 0f)); //Fait tourner l'instance sur elle-même autour de l'axe y à chaque frame à la vitesse choisie

                
        transform.RotateAround(target.transform.position, Vector3.up, rotateForce * Time.deltaTime);
                //Fait tourner l'instance autour du cube central à chaque frame à la vitesse choisie
    }
}
