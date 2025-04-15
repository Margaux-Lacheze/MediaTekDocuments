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
    public class AbonnementTests
    {
        [TestMethod()]
        public void AbonnementTest()
        {
            string id = "00001";
            DateTime dateCommande = new DateTime(2025, 1, 1);
            double montant = 50.0;
            DateTime dateFinAbonnement = new DateTime(2025, 12, 31);
            string idRevue = "10001";

            Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);

            Assert.AreEqual(id, abonnement.Id);
            Assert.AreEqual(dateCommande, abonnement.DateCommande);
            Assert.AreEqual(montant, abonnement.Montant);
            Assert.AreEqual(dateFinAbonnement, abonnement.DateFinAbonnement);
            Assert.AreEqual(idRevue, abonnement.IdRevue);
        }

        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            string id = "00001";
            DateTime dateCommande = DateTime.Parse("2025,1,1");
            double montant = 50.0;
            DateTime dateFinAbonnement = DateTime.Parse("2025-12-31");
            string idRevue = "10001";

            Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);

            // 1. Dans la période d'abonnement
            DateTime dateParutionDansAbonnement = DateTime.Parse("2025-6-15");
            Assert.IsTrue(abonnement.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParutionDansAbonnement));

            // 2. Au début de l'abonnement
            Assert.IsTrue(abonnement.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateCommande));

            // 3. Date exacte de fin de l'abonnement
            Assert.IsTrue(abonnement.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateFinAbonnement ));

            // 4. Avant la période d'abonnement
            DateTime dateParutionAvantAbonnement = DateTime.Parse("2023,12,31");
            Assert.IsFalse(abonnement.ParutionDansAbonnement(dateCommande, dateFinAbonnement,dateParutionAvantAbonnement));

            // 5. Après la période d'abonnement
            DateTime dateParutionApresAbonnement = DateTime.Parse("2026-1-1");
            Assert.IsFalse(abonnement.ParutionDansAbonnement(dateCommande, dateFinAbonnement,dateParutionApresAbonnement));

        }
    }
}