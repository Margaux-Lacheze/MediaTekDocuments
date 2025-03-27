using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier AbonnementExpiration
    /// </summary>
    public class AbonnementExpiration
    {
        public AbonnementExpiration(string titre, DateTime dateFinAbonnement)
        {
            this.Titre = titre;
            this.DateFinAbonnement = dateFinAbonnement;
        }

        public string Titre { get; set; }
        public DateTime DateFinAbonnement { get; set; }
    }
}
