using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier abonnement
    /// </summary>
    public class Abonnement : Commande
    {
        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue):base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
        }

        public DateTime DateFinAbonnement { get; set; }
        public string IdRevue { get; set; }

        /// <summary>
        /// Vérifie si un exemplaire est rattaché à un abonnement
        /// </summary>
        /// <param name="dateCommande"></param>
        /// <param name="dateFinAbonnement"></param>
        /// <param name="dateParutionExemplaire"></param>
        /// <returns>True si un exemplaire est présent entre la date de commande et la date de fin d'abonnement</returns>
        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return dateParution >= dateCommande && dateParution <= dateFinAbonnement;
        }
    }
}
