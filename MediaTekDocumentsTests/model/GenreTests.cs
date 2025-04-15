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
    public class GenreTests
    {
        [TestMethod()]
        public void GenreTest()
        {
            string id = "1";
            string libelle = "Enfants";

            Genre legenre = new Genre(id, libelle);

            Assert.AreEqual(id, legenre.Id);
        }
    }
}