# MediatekDocuments
Cette application permet de gérer les documents (livres, DVD, revues) d'une médiathèque. Elle a été codée en C# sous Visual Studio 2022. C'est une application de bureau, prévue d'être installée sur plusieurs postes accédant à la même base de données.<br>
L'application exploite une API REST pour accéder à la BDD MySQL. Seules des fonctionnalités ajoutées sont présentées ici (gestion des commandes et authentification), pour retrouver les explications sur l'application de base, veuillez vous rendre sur ce dépôt : https://github.com/CNED-SLAM/MediaTekDocuments
## Installation
Pour utiliser cette application, il vous suffit de télécharger le fichier Mediatek.msi présent dans ce dépôt. Une fois le fichier téléchargé, double cliquez dessus et l'installateur se lancera. Suivez les étapes jusqu'à la fin et accédez à l'application via votre dossier Programmes ou par le raccourci créé sur votre bureau<br>
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
<br>
## Gestion des commandes
### Accéder à la gestion des commandes
Sur chaque onglet du catalogue, un bouton "gérer les commandes" est présent en haut à droite. Il suffit de cliquer dessus pour accéder à la fenêtre de gestion des commandes. Selon l'onglet du catalogue où vous vous trouvez, l'application vous positionnera directement sur l'onglet de commandes correspondant. Par exemple, si vous êtes sur l'onglet livres du catalogue, le clic sur le bouton vous positionnera sur l'onglet de gestion des commandes de livres. Pour vous rendre sur les commandes d'un autre type de document, vous n'êtes pas obligés de repasser par le catalogue.<br>
<img width="655" alt="image" src="https://github.com/user-attachments/assets/50def763-d41b-4e1b-883b-b8097a812a97" />
<br>
En effet, la gestion du commande est une seule fenêtre séparée en trois onglets distincts :<br>
. livres<br>
. dvd<br>
. revues<br>
<br>
<img width="399" alt="image" src="https://github.com/user-attachments/assets/15013521-11fb-4335-a263-a0f69f178bd2" />
<br>

### Gestion des commandes de documents type livres ou dvd
![image](https://github.com/user-attachments/assets/e4f67008-670b-435d-9a4b-94c190210a5a)
<br>
Les commandes de livres ou de DVD suivent le même principe. Dans un souci de simplification, nous ne présenterons donc pas les commandes de dvd.<br>
<img width="396" alt="image" src="https://github.com/user-attachments/assets/8a170dc7-b0ae-4425-9fc4-3936146be4c0" /><br>
La page de commande de document est constituée de 4 parties :<br>
. La zone de saisie du numéro du document<br>
. Les informations détaillées du document<br>
. La liste des commandes liées à ce documents<br>
. La zone d'édition d'une commande (ajout ou modification)<br>

#### Recherche du document dont on veut gérer les commandes
Pour cela, il suffit de renseigner le numéro de document dans la zone de saisie puis de cliquer sur le bouton Rechercher. Si le document existe, alors ses informations détaillées s'affichent ainsi que les éventuelles commandes liées.

#### Ajout, modification et suppression d'une commande

##### Ajout
Afin d'ajouter une commande, il suffit de cliquer sur le bouton "Créer une commande". Ce dernier est disponible dès lors qu'un document valide a été saisi. Suite à cela, la zone d'édition de commande (tout en bas) est débloquée. Il faut alors inscrire les informations de la commande : date, montant et nombre d'exemplaire. Par défaut, une nouvelle commande est initialisée à "En cours". Une fois que toutes les informations sont saisies, il faut cliquer sur le bouton "Valider la modification". Alors, la nouvelle commande apparaît dans la grille dédiée à cette effet. Si vous avez commis une erreur, alors vous pouvez cliquer sur "Annuler la modification". La zone d'édition se fermera et aucun changement ne sera pris en compte.

##### Modification 
Pour modifier une commande, il faut d'abord la sélectionner dans la grille des commandes puis cliquer sur le bouton "Modifier la commande". Vous pouvez alors changer les informations de celle-ci ainsi que l'étape de la commande.<br>
La modification de l'étape d'une commande suit des règles précises. Il existe plusieurs statuts possibles :<br>
. En cours<br>
. Relancée <br>
. Livrée <br>
. Réglée <br>
Une fois la commande livrée, elle ne peut plus passer à l'étape "en cours" ou à l'étape "relancée". De la même façon, une fois que la commande est réglée, cela est définitif et on ne peut retourner en arrière.<br>
Lorsque vous avez terminé les modifications, il suffit de les valider ou de les annuler comme dans l'étape d'ajout de commande.<br>

##### Suppression
Il est possible de supprimer une commande en la sélectionnant dans la grille et en cliquant sur le bouton "Supprimer cette commande". Cependant, une fois livrée, la commande est définitive et ne peut-être supprimée.

### Gestion des commandes de revues (abonnements)
<img width="401" alt="image" src="https://github.com/user-attachments/assets/9b9d4f4e-1906-4952-83a2-1745f7a3ce03" /><br>

La gestion des abonnements est accessible via l'onglet "Revues" de la fenêtre de gestion des commandes.<br>
Cet onglet est constitué de 4 parties :<br>
. La zone de recherche d'une revue<br>
. Les informations détaillées de la revue recherché<br>
. La liste des abonnements liés<br>
. La zone d'édition des abonnements<br>

#### Recherche de la revue dont on veut gérer les abonnements
Pour trouver une revue, il faut saisir son numéro dans la zone de saisie puis cliquer sur le bouton "Rechercher". Si le numéro est trouvé alors les informations détaillées de cette dernière s'affichent ainsi que les parutions qui lui sont liées (numéro, date d'achat et photo éventuelle). Si la revue a des abonnements, ils s'affichent dans la grille juste en dessous de celle des exemplaires.<br>

##### Ajout d'un abonnement ou d'un renouvellement
Un abonnement ou un renouvellement à une revue fonctionne selon le même principe et ne sont pas distingués.<br>
Pour ajouter un abonnement, il faut avoir recherché un numéro de revue valide. Le bouton "Nouvel abonnement/renouvellement" situé sous la liste des abonnements se débloque. Il suffit de cliquer dessus pour ouvrir la zone d'édition d'un abonnement.<br>
Il faut saisir les informations demandées : date, date de fin d'abonnement et montant. Puis, cliquer sur valider l'opération. La date de l'abonnement doit être inférieure à la date de fin de l'abonnement. Si toutes les informations sont correctes, le nouvel abonnement s'affiche dans la liste des abonnements.<br>
Si lors de l'ajout vous faites une erreur, vous pouvez tout simplement cliquer sur le bouton "Annuler l'opération". La zone d'édition se fermera et la création ne sera pas prise en compte.

##### Suppression d'un abonnement
Pour supprimer un abonnement, il faut le sélectionner dans la liste des abonnements. Si une parution existe est liée à cet abonnement, c'est-à-dire qu'un numéro a été reçu entre la date de début de l'abonnement et la date de fin, alors le bouton "Supprimer l'abonnement sélectionné" ne sera pas disponible.<br>

## La base de données
La base de données 'mediatek86 ' est au format MySQL.<br>
Vous pouvez retrouver la base de données d'origine en vous rendant sur le dépôt mentionné dans l'introduction de ce readme. Nous n'aborderons ici que les tables ajoutées lors de l'atelier.<br>
Voici sa structure :<br>
<img width="481" alt="image" src="https://github.com/user-attachments/assets/6b36c5c9-3370-437f-b55f-73e24f58d783" />
<br><br>

Chaque <strong>commande de document</strong> (entité commandedocument) est à une étape mémorisée dans la table suivi.<br>
Un tuple de suivi est caractérisé par un numéro (id) et un libelle (En cours, Relancée, Livrée ou Réglée).<br>
<br>
Les utilisateurs correspondent au personnel des médiathèques. Ils sont représentés dans l'entité <strong>utilisateur</strong>. Chaque utilisateur a un id, un login et un mot de passe. L'id correspond au service dont dépend l'utilisateur. L'entité <strong>service</service>, représente les services de la médiathèque. Un service est représenté par un id et un libelle (administrateur, administratif, prêts ou culture).<br>

## L'API REST
L'accès à la BDD se fait à travers une API REST protégée par une authentification basique.<br>
Le code de l'API se trouve ici :<br>
[https://github.com/CNED-SLAM/rest_mediatekdocuments](https://github.com/Margaux-Lacheze/rest_mediatekdocuments)<br>

## Installation de l'application
Ce mode opératoire permet d'installer l'application pour pouvoir travailler dessus.<br>
- Installer Visual Studio 2022 entreprise et l'extension newtonsoft.json (pour ce dernier, voir l'article "Accéder à une API REST à partir d'une application C#" dans le wiki de ce dépôt : consulter juste le début pour la configuration, car la suite permet de comprendre le code existant).<br>
- Télécharger le code et le dézipper puis renommer le dossier en "mediatekdocuments".<br>
- Récupérer et installer l'API REST nécessaire ([https://github.com/CNED-SLAM/rest_mediatekdocuments](https://github.com/Margaux-Lacheze/rest_mediatekdocuments)) ainsi que la base de données (les explications sont données dans le readme correspondant).
