using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_object: MonoBehaviour
{
    //************************* Variables accessibles sur l'interface Unity  ****************************

    [SerializeField] private GameObject prefabAInstantier;  //initialise le pr�fab
    [SerializeField] private int NombreDePrefab =1; //permet de changer le nombre de copies souhait�es
    [SerializeField] private float Radius = 10; //permet de modifier le rayon du cercle sur lequel se trouve les copies

    [SerializeField] private float xCentral = 0;    //Permettent de changer la position du cube central et du centre de rotation des autres cubes
    [SerializeField] private float yCentral = 0;         
    [SerializeField] private float zCentral = 0;

    //*************************** Variables priv�es internes non accessibles ********************************

    private float CurrentXCentral;  //Sauvegarde la position du centre avant modification 
    private float CurrentYCentral;    
    private float CurrentZCentral;

    private float CurrentRadius;    //Sauvegarde le rayon avant modification

    private float angle;    //variable qui recevra l'angle sur le cercle o� se trouvera la nouvelle copie � cr�er quand on cr�e une copie

    private System.Random rnd = new System.Random(); //permet d'avoir acc�s � des valeurs al�atoires

    private List<GameObject> Instances = new List<GameObject>();    //cr�ee une liste qui va garder en m�moire toutes les copies cr��es

    private int InstanceToBeRemoved;  //variable qui va donner l'indice de la prochaine copie � �tre supprimer dans la listes "Instances" 
                                      //quand on veut supprimer des copies


    //************************ initialisation *************************************************************
    void Start()
    {
        CurrentRadius = Radius;             //initialise le rayon "actuel"

        CurrentXCentral = xCentral;         //initialise la position "actuelle" du centre du cercle
        CurrentYCentral = yCentral;         
        CurrentZCentral = zCentral;
    }

    //************************ Modification � chaque frame ***********************************************
    void Update()
    {
        //********************** Construction de nouvelles instances *************************************
        
        while(NombreDePrefab> Instances.Count)      //quand on veut cr�er des copies = Le nombre de copies souhait�es > nombre de copies cr��es
        {
            angle = (float)rnd.NextDouble() * 2 * Mathf.PI;     //choisi un angle au hasard

            GameObject newInstance = Instantiate(prefabAInstantier, position: new Vector3(CurrentRadius*Mathf.Cos(angle) + CurrentXCentral, 0f + CurrentYCentral, CurrentRadius*Mathf.Sin(angle)+ CurrentZCentral), Quaternion.identity);
            //g�n�re la copie � la position souhait� (angle pris au hasard juste-au dessus et sur un cercle de rayon pr�cis� par la variable "CurrentRadius")

            newInstance.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //g�n�re une couleur al�atoire pour chaque copie

            Instances.Add(newInstance); //ajoute la copie � la fin de la liste
        }

        //********************** Destruction d'instances *************************************************

        while (NombreDePrefab < Instances.Count)    //quand on veut supprimer des copies = Le nombre de copies souhait�es < nombre de copies cr��es
        {
            InstanceToBeRemoved = rnd.Next(0, Instances.Count); //choisi un indice al�atoire dans la liste des copies
            Destroy(Instances[InstanceToBeRemoved]); //supprime la copie
            Instances.RemoveAt(InstanceToBeRemoved); //supprime l'emplacement dans la liste o� se trouvait la copie, ce qui met � jour le nombre de copies ("Instances.Count")
        }

        //********************** Modification du rayon ****************************************************

        if (CurrentRadius != Radius && Radius != 0) //si l'on modifie le rayon (et que le nouveau rayon n'est pas nul)
        {
            for (int i = 0; i < Instances.Count; i++) //change le rayon pour chaque copie
            {
                Instances[i].transform.position = new Vector3(
                                                                    Radius * (Instances[i].transform.position[0] / CurrentRadius), //on multiplie chaque position par le rapport
                                                                    0f,                                                            // (nouveau rayon / ancien rayon)     
                                                                    Radius * (Instances[i].transform.position[2] / CurrentRadius)
                                                             );
                    /*On remarque ici que si le nouveau rayon "Radius" est nul, alors toutes les positions de tous les cubes vont �tre �gal � (0,0,0)
                    ce qui 1) n'est pas tr�s int�ressant, 2) nous fait perdre l'information sur l'angle de chaque cube et 
                    3) va provoquer une divsion par 0 au prochain changment de rayon donc �a casse tout (d'o� le fait que le nouveau rayon ne doit pas �tre nul) */
                
            }
            CurrentRadius = Radius;     //Remet � jour le rayon "actuel" du cercle
        }

        //********************** Modification du centre *****************************************************

        if (CurrentXCentral != xCentral || CurrentYCentral != yCentral || CurrentZCentral != zCentral) //Quand on modifie une coordonn�e du centre
        {
            for (int i = 0; i < Instances.Count; i++)       //Change le centre de toutes les instances en les translatant de la m�me translation que le centre
            {
                Instances[i].transform.position += new Vector3(xCentral - CurrentXCentral, yCentral - CurrentYCentral, zCentral - CurrentZCentral);
            }

            transform.position = new Vector3(xCentral, yCentral, zCentral); //d�place le cube central sur le nouveau centre

            CurrentXCentral = xCentral;     //Remet � jour les positions "actuelles" du centre 
            CurrentYCentral = yCentral;
            CurrentZCentral = zCentral;
        }
    }
}
