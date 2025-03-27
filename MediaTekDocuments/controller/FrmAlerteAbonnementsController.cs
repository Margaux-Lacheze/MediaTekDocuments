using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    class FrmAlerteAbonnementsController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAlerteAbonnementsController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère les abonnements arrivant à expiration d'ici 30 jours
        /// </summary>
        /// <returns>liste des abonnements arrivant à expiration</returns>
        public List<AbonnementExpiration> GetAllAbonnementExpiration()
        {
            return access.GetAllAbonnementExpiration();
        }
    }
}
