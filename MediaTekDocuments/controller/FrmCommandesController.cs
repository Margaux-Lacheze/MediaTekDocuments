using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmCommandes
    /// </summary>
    class FrmCommandesController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        public FrmCommandesController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// getter sur les suivis
        /// </summary>
        /// <returns>Liste d'Objets Suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivi();
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        /// <summary>
        /// Récupère toutes les commandes liées à un document
        /// </summary>
        /// <param name="idDocument"></param>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetCommandesDocument(string idDocument)
        {
            return access.GetAllCommandeDocument(idDocument);
        }

        /// <summary>
        /// Incrémente le dernier id de commande de la bdd
        /// </summary>
        /// <returns>id formaté</returns>
        public string CreerNouvelIdCommande()
        {
            return access.CreerNouvelIdCommande();
        }

        /// <summary>
        /// Nouvel objet de type commandedocument en bdd
        /// </summary>
        /// <param name="commandeDocument"></param>
        /// <returns>true si réussi</returns>
        public bool CreerNouvelleCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.CreerNouvelleCommandeDocument(commandeDocument);
        }

        /// <summary>
        /// Modifie un objet CommandeDocument
        /// </summary>
        /// <param name="commandeDocument"></param>
        /// <returns>true si réussi</returns>
        public bool ModifierCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.ModifierCommandeDocument(commandeDocument) ;
        }

        /// <summary>
        /// Suppression d'une commande en base de données
        /// </summary>
        /// <param name="idCommande">id de la Commande à supprimer</param>
        /// <returns>True si la suppression a réussi</returns>
        public bool SupprimerCommande(string idCommande)
        {
            return access.SupprimerCommande(idCommande) ;
        }

        /// <summary>
        /// Création d'un abonnement en base de données
        /// </summary>
        /// <param name="abonnement"></param>
        /// <returns>True si l'opération a réussi</returns>
        public bool CreerNouvelAbonnement(Abonnement abonnement)
        {
            return access.CreerNouvelAbonnement(abonnement);
        }

        /// <summary>
        /// Récupère tous les abonnements d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue</param>
        /// <returns>Les abonnements de la revue</returns>
        public List<Abonnement> GetAllAbonnements(string idDocument)
        {
            return access.GetAllAbonnement(idDocument);
        }
    }
}
