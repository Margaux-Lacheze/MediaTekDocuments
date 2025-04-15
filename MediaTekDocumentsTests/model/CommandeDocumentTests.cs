using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class CommandeDocumentTests
    {
        private string id;
        private DateTime dateCommande;
        private double montant;
        private int nbExemplaire;
        private string idLivreDvd;
        private string idSuivi;
        private string libelle;
        private CommandeDocument commande;

        [TestInitialize]
        public void Initialize()
        {
            id = "1";
            dateCommande = DateTime.Parse("2025-04-10");
            montant = 10.0;
            nbExemplaire = 2;
            idLivreDvd = "2";
            idSuivi = "00001";
            libelle = "commande";
            commande = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);
        }

        [TestMethod()]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual(idSuivi, commande.IdSuivi);
        }

        [TestMethod()]
        public void EstTransitionAutoriseeTest()
        {
            // Commande en cours peut être relancée
            Assert.IsTrue(commande.EstTransitionAutorisee("00004"));
            // Commande en cours ne peut pas être tout de suite réglée
            Assert.IsFalse(commande.EstTransitionAutorisee("00003"));
        }
    }
}