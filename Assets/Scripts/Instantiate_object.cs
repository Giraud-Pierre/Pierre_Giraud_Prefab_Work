using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_object: MonoBehaviour
{
    //************************* Variables accessibles sur l'interface Unity  ****************************

    [SerializeField] private GameObject prefabAInstantier;  //initialise le préfab
    [SerializeField] private int NombreDePrefab =1; //permet de changer le nombre de copies souhaitées
    [SerializeField] private float Radius = 10; //permet de modifier le rayon du cercle sur lequel se trouve les copies

    [SerializeField] private float xCentral = 0;    //Permettent de changer la position du cube central et du centre de rotation des autres cubes
    [SerializeField] private float yCentral = 0;         
    [SerializeField] private float zCentral = 0;

    //*************************** Variables privées internes non accessibles ********************************

    private float CurrentXCentral;  //Sauvegarde la position du centre avant modification 
    private float CurrentYCentral;    
    private float CurrentZCentral;

    private float CurrentRadius;    //Sauvegarde le rayon avant modification

    private float angle;    //variable qui recevra l'angle sur le cercle où se trouvera la nouvelle copie à créer quand on crée une copie

    private System.Random rnd = new System.Random(); //permet d'avoir accès à des valeurs aléatoires

    private List<GameObject> Instances = new List<GameObject>();    //créee une liste qui va garder en mémoire toutes les copies créées

    private int InstanceToBeRemoved;  //variable qui va donner l'indice de la prochaine copie à être supprimer dans la listes "Instances" 
                                      //quand on veut supprimer des copies


    //************************ initialisation *************************************************************
    void Start()
    {
        CurrentRadius = Radius;             //initialise le rayon "actuel"

        CurrentXCentral = xCentral;         //initialise la position "actuelle" du centre du cercle
        CurrentYCentral = yCentral;         
        CurrentZCentral = zCentral;
    }

    //************************ Modification à chaque frame ***********************************************
    void Update()
    {
        //********************** Construction de nouvelles instances *************************************
        
        while(NombreDePrefab> Instances.Count)      //quand on veut créer des copies = Le nombre de copies souhaitées > nombre de copies créées
        {
            angle = (float)rnd.NextDouble() * 2 * Mathf.PI;     //choisi un angle au hasard

            GameObject newInstance = Instantiate(prefabAInstantier, position: new Vector3(CurrentRadius*Mathf.Cos(angle) + CurrentXCentral, 0f + CurrentYCentral, CurrentRadius*Mathf.Sin(angle)+ CurrentZCentral), Quaternion.identity);
            //génère la copie à la position souhaité (angle pris au hasard juste-au dessus et sur un cercle de rayon précisé par la variable "CurrentRadius")

            newInstance.GetComponent<Renderer>().material.color = UnityEngine.Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //génère une couleur aléatoire pour chaque copie

            Instances.Add(newInstance); //ajoute la copie à la fin de la liste
        }

        //********************** Destruction d'instances *************************************************

        while (NombreDePrefab < Instances.Count)    //quand on veut supprimer des copies = Le nombre de copies souhaitées < nombre de copies créées
        {
            InstanceToBeRemoved = rnd.Next(0, Instances.Count); //choisi un indice aléatoire dans la liste des copies
            Destroy(Instances[InstanceToBeRemoved]); //supprime la copie
            Instances.RemoveAt(InstanceToBeRemoved); //supprime l'emplacement dans la liste où se trouvait la copie, ce qui met à jour le nombre de copies ("Instances.Count")
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
                    /*On remarque ici que si le nouveau rayon "Radius" est nul, alors toutes les positions de tous les cubes vont être égal à (0,0,0)
                    ce qui 1) n'est pas très intéressant, 2) nous fait perdre l'information sur l'angle de chaque cube et 
                    3) va provoquer une divsion par 0 au prochain changment de rayon donc ça casse tout (d'où le fait que le nouveau rayon ne doit pas être nul) */
                
            }
            CurrentRadius = Radius;     //Remet à jour le rayon "actuel" du cercle
        }

        //********************** Modification du centre *****************************************************

        if (CurrentXCentral != xCentral || CurrentYCentral != yCentral || CurrentZCentral != zCentral) //Quand on modifie une coordonnée du centre
        {
            for (int i = 0; i < Instances.Count; i++)       //Change le centre de toutes les instances en les translatant de la même translation que le centre
            {
                Instances[i].transform.position += new Vector3(xCentral - CurrentXCentral, yCentral - CurrentYCentral, zCentral - CurrentZCentral);
            }

            transform.position = new Vector3(xCentral, yCentral, zCentral); //déplace le cube central sur le nouveau centre

            CurrentXCentral = xCentral;     //Remet à jour les positions "actuelles" du centre 
            CurrentYCentral = yCentral;
            CurrentZCentral = zCentral;
        }
    }
}
