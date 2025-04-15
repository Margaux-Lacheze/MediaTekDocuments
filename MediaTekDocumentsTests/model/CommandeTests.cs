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
    public class CommandeTests
    {
        [TestMethod()]
        public void CommandeTest()
        {
            string id = "1";
            DateTime dateCommande = DateTime.Parse("2025-04-10");
            double montant = 10.0;

            Commande commande = new Commande(id, dateCommande, montant);

            Assert.AreEqual(montant, commande.Montant);
        }
    }
}