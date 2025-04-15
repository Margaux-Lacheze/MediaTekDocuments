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
    public class DocumentTests
    {
        [TestMethod()]
        public void DocumentTest()
        {
            string id = "1";
            string titre = "Le document";
            string image = "le chemin de l'image";
            string idGenre = "2";
            string genre = "le genre";
            string idPublic = "3";
            string lePublic = "le public";
            string idRayon = "4";
            string rayon = "le rayon";

            Document document = new Document(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon);

            Assert.AreEqual(image, document.Image);
        }
    }
}