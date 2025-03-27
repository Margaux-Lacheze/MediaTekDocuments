using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.model;

namespace MediaTekDocuments.dal.Tests
{
    [TestClass()]
    public class AccessTests
    {

        [TestMethod()]
        public void GetInstanceTest()
        {

        }

        [TestMethod()]
        public void GetAllGenresTest()
        {

        }

        [TestMethod()]
        public void GetAllRayonsTest()
        {

        }

        [TestMethod()]
        public void GetAllPublicsTest()
        {

        }

        [TestMethod()]
        public void GetAllLivresTest()
        {

        }

        [TestMethod()]
        public void GetAllDvdTest()
        {

        }

        [TestMethod()]
        public void GetAllRevuesTest()
        {

        }

        [TestMethod()]
        public void GetExemplairesRevueTest()
        {

        }

        [TestMethod()]
        public void CreerExemplaireTest()
        {

        }

        [TestMethod()]
        public void GetAllSuiviTest()
        {

        }

        [TestMethod()]
        public void GetAllCommandeDocumentTest()
        {

        }

        [TestMethod()]
        public void CreerNouvelIdCommandeTest()
        {

        }

        [TestMethod()]
        public void CreerNouvelleCommandeDocumentTest()
        {

        }

        [TestMethod()]
        public void ModifierCommandeDocumentTest()
        {

        }

        [TestMethod()]
        public void SupprimerCommandeTest()
        {

        }

        [TestMethod()]
        public void CreerNouvelAbonnementTest()
        {

        }

        [TestMethod()]
        public void GetAllAbonnementTest()
        {

        }

        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            Access access = Access.GetInstance();

            DateTime dateCommande = DateTime.Parse("2025-03-01");
            DateTime dateFinAbonnement = DateTime.Parse("2025-06-30");

            DateTime dateParution = DateTime.Parse("2025-04-21");
            bool resultatTrue = access.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParution);
            Assert.AreEqual(true, resultatTrue);


            DateTime dateParutionAnterieure = DateTime.Parse("2025-01-01");
            bool resultatFalse = access.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParutionAnterieure);
            Assert.AreEqual(false, resultatFalse);


            DateTime dateParutionPosterieure = DateTime.Parse("2025-07-01");
            bool resultatFalse2 = access.ParutionDansAbonnement(dateCommande, dateFinAbonnement, dateParutionPosterieure);
            Assert.AreEqual(false, resultatFalse2);
        }
    }
}