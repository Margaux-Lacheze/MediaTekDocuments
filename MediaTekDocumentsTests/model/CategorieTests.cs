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
    public class CategorieTests
    {
        [TestMethod()]
        public void CategorieTest()
        {
            string id = "1";
            string libelle = "Horreur";

            Categorie categorie = new Categorie(id, libelle);

            Assert.AreEqual(id, categorie.Id);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Categorie categorie = new Categorie("1", "Horreur");
             string result = categorie.ToString();
            Assert.AreEqual("Horreur", result);
        }
    }
}