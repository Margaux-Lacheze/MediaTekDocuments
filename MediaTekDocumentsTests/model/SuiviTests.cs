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
    public class SuiviTests
    {
        [TestMethod()]
        public void SuiviTest()
        {
            string id = "1";
            string libelle = "en cours";

            Suivi leSuivi = new Suivi(id, libelle);

            Assert.AreEqual(libelle, leSuivi.Libelle);
        }
    }
}