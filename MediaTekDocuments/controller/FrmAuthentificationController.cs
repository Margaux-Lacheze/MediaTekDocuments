using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Contrôleur lié à FrmAuthentification
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère un utilisateur en base de données si le couple login/password est correct
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Un objet de type Utilisateur</returns>
        public Utilisateur CheckUtilisateur(string login, string password)
        {
            return access.CheckUtilisateur(login, password);
        }
    }
}
