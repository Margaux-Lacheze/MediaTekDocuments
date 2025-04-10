using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument hérite de Commande
    /// </summary>
    public class CommandeDocument : Commande
    {
        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaire, string idLivreDvd, string idSuivi, string suivi)
            :base(id, dateCommande, montant)
        {
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.Suivi = suivi;
        }

        public int NbExemplaire { get; }
        public string IdLivreDvd { get; }
        public string IdSuivi { get; }
        public string Suivi { get; }

        private static readonly Dictionary<string, List<string>> TransitionsAutorisees = new Dictionary<string, List<string>>()
        {
            { "00001", new List<string> { "00004", "00002" } }, // En cours peut être relancée ou livrée
            { "00002", new List<string> { "00003" } }, // Livrée peut être réglée
            { "00003", new List<string>() }, // Réglée ne change plus
            { "00004", new List<string> { "00001", "00002" } } // Relancée peut être livrée ou remise en cours
        };

        /// <summary>
        /// Vérifier si la transition entre une étape précédente et une nouvelle étape est autorisée
        /// </summary>
        /// <param name="nouvelleEtape">Nouvelle étape de la commande</param>
        /// <returns>True si la transition est autorisée</returns>
        public bool EstTransitionAutorisee(string nouvelleEtape)
        {
            if (IdSuivi == nouvelleEtape)
                return true;

            return TransitionsAutorisees.ContainsKey(IdSuivi) &&
                   TransitionsAutorisees[IdSuivi].Contains(nouvelleEtape);
        }

    }
}
