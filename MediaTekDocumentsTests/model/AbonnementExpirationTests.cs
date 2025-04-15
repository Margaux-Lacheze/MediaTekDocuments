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
    public class AbonnementExpirationTests
    {
        [TestMethod()]
        public void AbonnementExpirationTest()
        {
            string titre = "Abonnement expiré";
            DateTime dateFinAbonnement = DateTime.Parse("2025-04-10");

            AbonnementExpiration abonnement = new AbonnementExpiration(titre, dateFinAbonnement);

            Assert.AreEqual(titre, abonnement.Titre);
            Assert.AreEqual(dateFinAbonnement, abonnement.DateFinAbonnement);
        }
    }
}