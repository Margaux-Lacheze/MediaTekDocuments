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
    public class DvdTests
    {
        [TestMethod()]
        public void DvdTest()
        {
            string id = "1";
            string titre = "le titre du dvd";
            string image = "le chemin de l'image";
            int duree = 160;
            string realisateur = "le real";
            string synopsis = "le syno";
            string idGenre = "1";
            string genre = "le genre";
            string idPublic = "1";
            string lePublic = "le public";
            string idRayon = "4";
            string rayon = "le rayon";

            Dvd leDvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);

            Assert.AreEqual(titre, leDvd.Titre);
        }
    }
}