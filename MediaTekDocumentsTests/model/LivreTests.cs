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
    public class LivreTests
    {
        [TestMethod()]
        public void LivreTest()
        {
            string id = "1";
            string titre = "le titre du livre";
            string image = "l'image";
            string isbn = "234";
            string auteur = "l'auteur";
            string collection = "la collection";
            string idGenre = "3";
            string genre = "le genre";
            string idPublic = "4";
            string lePublic = "le public";
            string idRayon = "5";
            string rayon = "le rayon";

            Livre leLivre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon);

            Assert.AreEqual(isbn, leLivre.Isbn);
        }
    }
}