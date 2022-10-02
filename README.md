# Pierre_Giraud_Prefabs_Work

L'objectif de ce projet est de générer des instances de couleur aléatoire d'un cube préfabriqué (Prefab1) sur un cercle centré sur un cube central (CentralCube), tournant à une vitesse aléatoire autour de CentralCube et à une autre vitesse aléatoire sur elles-même autour de l'axe y.

Le projet contient 2 cubes :

    * CubeCentral qui va représenté le centre du cercle sur lequel les instances vont se trouver
    
    * Prefab1 qui est le prefab qui va être copié pour former toutes les instances

Il contient aussi 2 scripts:

   -Instantiate_object.cs qui :
   
      * est lié au CentralCube;
      
      * gère la génération des instances du cube Prefab1;
      
      * gère la destruction des instances quand on réduit le nombre d'instances voulues (choisi aléatoirement des instances à supprimer jusqu'à ce que 
          l'on atteigne le nombre voulu);
          
      * donne une couleur aléatoire à chaque nouvelle instance;
      
      * permet de translater toutes les instances lorsque l'on change la position de CentralCube (et donc du centre du cercle);
      
      * permet d'adapter la position de toutes les instances lorsque l'on change le rayon (l'obite) de rotation autour de CentralCube;
      
      
   -RotateCube.cs qui :
   
      * est lié à Prefab1;
      
      * donne une vitesse de rotation aléatoire pour les deux rotations (sur elles-mêmes et autour de CentralCube);
      
      * gère les 2 rotations frame par frame.

*J'ai fait le choix de mettre la partie qui gère le rayon et la position du centre dans Instantiate_object.cs et pas dans un script directement lié au prefab (comme la rotation) car cela me permet de ne vérifier qu'une seule fois par frame si l'on a besoin ou pas de faire les changements alors que dans l'autre cas, il aurait fallu vérifier à chaque frame pour chaque instances si on avait changé le centre ou le rayon ou pas.*

Est modifiable sur l'interface Unity dans le composant "Instantiate_object.cs (script)" de CentralCube :

      * le nombre d'instances de Prefab1
      
      * le rayon du cercle sur lequel tourne les instances (centré sur CentralCube);
      
      * la position en x, y et z de CentralCube et donc du centre du cercle sur lequel se situe les instances
