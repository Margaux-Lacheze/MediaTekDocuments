using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier utilisateur
    /// </summary>
    public class Utilisateur
    {
        public string Login { get; }
        public string Password { get; }
        public int IdService { get; }
        public string Libelle { get; }

        public Utilisateur(string login, string password, int idService, string libelle)
        {
            this.Login = login;
            this.Password = password;
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
