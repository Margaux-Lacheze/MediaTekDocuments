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
    public class ExemplaireTests
    {
        [TestMethod()]
        public void ExemplaireTest()
        {
            int numero = 1;
            DateTime dateAchat = new DateTime(2025, 04, 10);
            string photo = "la photo";
            string idEtat = "1";
            string idDocument = "2";

            Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);

            Assert.AreEqual(numero, exemplaire.Numero);
        }
    }
}