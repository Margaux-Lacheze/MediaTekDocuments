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
    public class RayonTests
    {
        [TestMethod()]
        public void RayonTest()
        {
            string id = "1";
            string libelle = "le rayon";

            Rayon leRayon = new Rayon(id, libelle);

            Assert.AreEqual(id, leRayon.Id);
        }
    }
}