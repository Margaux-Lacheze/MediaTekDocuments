using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi
    /// </summary>
    public class Suivi
    {
        public Suivi(string idService, string libelle)
        {
            this.IdService = idService;
            this.Libelle = libelle;
        }

        public string IdService { get; set; }
        public string Libelle { get; set; }
    }
}
