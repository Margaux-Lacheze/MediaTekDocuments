# MediatekDocuments
Cette application permet de gérer les documents (livres, DVD, revues) d'une médiathèque. Elle a été codée en C# sous Visual Studio 2022. C'est une application de bureau, prévue d'être installée sur plusieurs postes accédant à la même base de données.<br>
L'application exploite une API REST pour accéder à la BDD MySQL. Seules des fonctionnalités ajoutées sont présentées ici (gestion des commandes et authentification), pour retrouver les explications sur l'application de base, veuillez vous rendre sur ce dépôt : https://github.com/CNED-SLAM/MediaTekDocuments
## Installation
Pour utiliser cette application, il vous suffit de télécharger le fichier Mediatek Documents.msi présent dans ce dépôt. Une fois le fichier téléchargé, double cliquez dessus et l'installateur se lancera. Suivez les étapes jusqu'à la fin et accédez à l'application via votre dossier Programmes ou par le raccourci créé sur votre bureau<br>
<img width="59" alt="icone bureau" src="https://github.com/user-attachments/assets/265ba243-db16-4aff-867d-c2ed34655a9c" />

## Connexion
Pour pouvoir accéder aux fonctionnalités de l'application, vous devez d'abord vous connecter en saisissant vos identifiants (login et mot de passe). Certains accès sont bridés selon le service d'appartenance de l'utilisateur :<br>
. L'administrateur et le personnel du service administratif ont accès à toutes les fonctionnalités (catalogue et commandes)<br>
. Le personnel du service prêts n'a accès qu'à la partie catalogue<br>
. Le personnel du service culture n'a pas accès à l'application<br>

<img width="233" alt="image" src="https://github.com/user-attachments/assets/7e841242-78a6-41ff-ac3a-74e6ca7b47bb" />

## Abonnements arrivant à expiration
Une fois connecté, si vous avez les droits du service administratifs alors une fenêtre vous informant des abonnements aux revues arrivant à expiration s'ouvrira. Une fois cette fenêtre fermée vous arriverez sur le catalogue. Pour le personnel n'ayant pas les droits, le catalogue seul s'affichera et les fonctionnalités décrites plus loin ne seront pas disponibles. Pour ceux-là, le fonctionnement de l'application est expliquée dans le readme du dépôt github d'origine dont le lien est donné plus haut.<br>

<img width="653" alt="image" src="https://github.com/user-attachments/assets/0b87df38-3d2a-4004-b5ba-c9eafb18aee1" />

## Gestion des commandes
### Accéder à la gestion des commandes
Sur chaque onglet du catalogue, un bouton "gérer les commandes" est présent en haut à droite. Il suffit de cliquer dessus pour accéder à la fenêtre de gestion des commandes. Selon l'onglet du catalogue où vous vous trouvez, l'application vous positionnera directement sur l'onglet de commandes correspondant. Par exemple, si vous êtes sur l'onglet livres du catalogue, le clic sur le bouton vous positionnera sur l'onglet de gestion des commandes de livres. Pour vous rendre sur les commandes d'un autre type de document, vous n'êtes pas obligés de repasser par le catalogue.<br>
<img width="655" alt="image" src="https://github.com/user-attachments/assets/50def763-d41b-4e1b-883b-b8097a812a97" />
<br>
En effet, la gestion du commande est une seule fenêtre séparée en trois onglets distincts :<br>
. livres<br>
. dvd<br>
. revues<br>

<img width="399" alt="image" src="https://github.com/user-attachments/assets/15013521-11fb-4335-a263-a0f69f178bd2" />


### Gestion des commandes de documents type livres ou dvd
![image](https://github.com/user-attachments/assets/e4f67008-670b-435d-9a4b-94c190210a5a)
<br>
Les commandes de livres ou de DVD suivent le même principe. Dans un souci de simplification, nous ne présenterons donc pas les commandes de dvd.<br>





## Les différents onglets
### Onglet 1 : Livres
Cet onglet présente la liste des livres, triée par défaut sur le titre.<br>
La liste comporte les informations suivantes : titre, auteur, collection, genre, public, rayon.
![img2](https://github.com/CNED-SLAM/MediaTekDocuments/assets/100127886/e3f31979-cf24-416d-afb1-a588356e8966)
#### Recherches
<strong>Par le titre :</strong> Il est possible de rechercher un ou plusieurs livres par le titre. La saisie dans la zone de recherche se fait en autocomplétions sans tenir compte de la casse. Seuls les livres concernés apparaissent dans la liste.<br>
<strong>Par le numéro :</strong> il est possible de saisir un numéro et, en cliquant sur "Rechercher", seul le livre concerné apparait dans la liste (ou un message d'erreur si le livre n'est pas trouvé, avec la liste remplie à nouveau).
#### Filtres
Il est possible d'appliquer un filtre (un seul à la fois) sur une de ces 3 catégories : genre, public, rayon.<br>
Un combo par catégorie permet de sélectionner un item. Seuls les livres correspondant à l'item sélectionné, apparaissent dans la liste (par exemple, en choisissant le genre "Policier", seuls les livres de genre "Policier" apparaissent).<br>
Le fait de sélectionner un autre filtre ou de faire une recherche, annule le filtre actuel.<br>
Il est possible aussi d'annuler le filtre en cliquant sur une des croix.
#### Tris
Le fait de cliquer sur le titre d'une des colonnes de la liste des livres, permet de trier la liste par rapport à la colonne choisie.
#### Affichage des informations détaillées
Si la liste des livres contient des éléments, par défaut il y en a toujours un de sélectionné. Il est aussi possible de sélectionner une ligne (donc un livre) en cliquant n'importe où sur la ligne.<br>
La partie basse de la fenêtre affiche les informations détaillées du livre sélectionné (numéro de document, code ISBN, titre, auteur(e), collection, genre, public, rayon, chemin de l'image) ainsi que l'image.
### Onglet 2 : DVD
Cet onglet présente la liste des DVD, triée par titre.<br>
La liste comporte les informations suivantes : titre, durée, réalisateur, genre, public, rayon.<br>
Le fonctionnement est identique à l'onglet des livres.<br>
La seule différence réside dans certaines informations détaillées, spécifiques aux DVD : durée (à la place de ISBN), réalisateur (à la place de l'auteur), synopsis (à la place de collection).
### Onglet 3 : Revues
Cet onglet présente la liste des revues, triées par titre.<br>
La liste comporte les informations suivantes : titre, périodicité, délai mise à dispo, genre, public, rayon.<br>
Le fonctionnement est identique à l'onglet des livres.<br>
La seule différence réside dans certaines informations détaillées, spécifiques aux revues : périodicité (à la place de l'auteur), délai mise à dispo (à la place de collection).
### Onglet 4 : Parutions des revues
Cet onglet permet d'enregistrer la réception de nouvelles parutions d'une revue.<br>
Il se décompose en 2 parties (groupbox).
#### Partie "Recherche revue"
Cette partie permet, à partir de la saisie d'un numéro de revue (puis en cliquant sur le bouton "Rechercher"), d'afficher toutes les informations de la revue (comme dans l'onglet précédent), ainsi que son image principale en petit, avec en plus la liste des parutions déjà reçues (numéro, date achat, chemin photo). Sur la sélection d'une ligne dans la liste des parutions, la photo de la parution correspondante s'affiche à droite.<br>
Dès qu'un numéro de revue est reconnu et ses informations affichées, la seconde partie ("Nouvelle parution réceptionnée pour cette revue") devient accessible.<br>
Si une modification est apportée au numéro de la revue, toutes les zones sont réinitialisées et la seconde partie est rendue inaccessible, tant que le bouton "Rechercher" n'est pas utilisé.
#### Partie "Nouvelle parution réceptionnée pour cette revue"
Cette partie n'est accessible que si une revue a bien été trouvée dans la première partie.<br>
Il est possible alors de réceptionner une nouvelle parution en saisissant son numéro, en sélectionnant une date (date du jour proposée par défaut) et en cherchant l'image correspondante (optionnel) qui doit alors s'afficher à droite.<br>
Le clic sur "Valider la réception" va permettre d'ajouter un tuple dans la table Exemplaire de la BDD. La parution correspondante apparaitra alors automatiquement dans la liste des parutions et les zones de la partie "Nouvelle parution réceptionnée pour cette revue" seront réinitialisées.<br>
Si le numéro de la parution existe déjà, il n’est pas ajouté et un message est affiché.
![img3](https://github.com/CNED-SLAM/MediaTekDocuments/assets/100127886/225e10f2-406a-4b5e-bfa9-368d45456056)
## La base de données
La base de données 'mediatek86 ' est au format MySQL.<br>
Voici sa structure :<br>
![img4](https://github.com/CNED-SLAM/MediaTekDocuments/assets/100127886/4314f083-ec8b-4d27-9746-fecd1387d77b)
<br>On distingue les documents "génériques" (ce sont les entités Document, Revue, Livres-DVD, Livre et DVD) des documents "physiques" qui sont les exemplaires de livres ou de DVD, ou bien les numéros d’une revue ou d’un journal.<br>
Chaque exemplaire est numéroté à l’intérieur du document correspondant, et a donc un identifiant relatif. Cet identifiant est réel : ce n'est pas un numéro automatique. <br>
Un exemplaire est caractérisé par :<br>
. un état d’usure, les différents états étant mémorisés dans la table Etat ;<br>
. sa date d’achat ou de parution dans le cas d’une revue ;<br>
. un lien vers le fichier contenant sa photo de couverture de l'exemplaire, renseigné uniquement pour les exemplaires des revues, donc les parutions (chemin complet) ;
<br>
Un document a un titre (titre de livre, titre de DVD ou titre de la revue), concerne une catégorie de public, possède un genre et est entreposé dans un rayon défini. Les genres, les catégories de public et les rayons sont gérés dans la base de données. Un document possède aussi une image dont le chemin complet est mémorisé. Même les revues peuvent avoir une image générique, en plus des photos liées à chaque exemplaire (parution).<br>
Une revue est un document, d’où le lien de spécialisation entre les 2 entités. Une revue est donc identifiée par son numéro de document. Elle a une périodicité (quotidien, hebdomadaire, etc.) et un délai de mise à disposition (temps pendant lequel chaque exemplaire est laissé en consultation). Chaque parution (exemplaire) d'une revue n'est disponible qu'en un seul "exemplaire".<br>
Un livre a aussi pour identifiant son numéro de document, possède un code ISBN, un auteur et peut faire partie d’une collection. Les auteurs et les collections ne sont pas gérés dans des tables séparées (ce sont de simples champs textes dans la table Livre).<br>
De même, un DVD est aussi identifié par son numéro de document, et possède un synopsis, un réalisateur et une durée. Les réalisateurs ne sont pas gérés dans une table séparée (c’est un simple champ texte dans la table DVD).
Enfin, 3 tables permettent de mémoriser les données concernant les commandes de livres ou DVD et les abonnements. Une commande est effectuée à une date pour un certain montant. Un abonnement est une commande qui a pour propriété complémentaire la date de fin de l’abonnement : il concerne une revue.  Une commande de livre ou DVD a comme caractéristique le nombre d’exemplaires commandé et concerne donc un livre ou un DVD.<br>
<br>
La base de données est remplie de quelques exemples pour pouvoir tester son application. Dans les champs image (de Document) et photo (de Exemplaire) doit normalement se trouver le chemin complet vers l'image correspondante. Pour les tests, vous devrez créer un dossier, le remplir de quelques images et mettre directement les chemins dans certains tuples de la base de données qui, pour le moment, ne contient aucune image.<br>
Lorsque l'application sera opérationnelle, c'est le personnel de la médiathèque qui sera en charge de saisir les informations des documents.
## L'API REST
L'accès à la BDD se fait à travers une API REST protégée par une authentification basique.<br>
Le code de l'API se trouve ici :<br>
https://github.com/CNED-SLAM/rest_mediatekdocuments<br>
avec toutes les explications pour l'utiliser (dans le readme).
## Installation de l'application
Ce mode opératoire permet d'installer l'application pour pouvoir travailler dessus.<br>
- Installer Visual Studio 2022 entreprise et l'extension newtonsoft.json (pour ce dernier, voir l'article "Accéder à une API REST à partir d'une application C#" dans le wiki de ce dépôt : consulter juste le début pour la configuration, car la suite permet de comprendre le code existant).<br>
- Télécharger le code et le dézipper puis renommer le dossier en "mediatekdocuments".<br>
- Récupérer et installer l'API REST nécessaire (https://github.com/CNED-SLAM/rest_mediatekdocuments) ainsi que la base de données (les explications sont données dans le readme correspondant).
