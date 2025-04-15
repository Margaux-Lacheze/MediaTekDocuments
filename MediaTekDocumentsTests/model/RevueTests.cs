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
    public class RevueTests
    {
        [TestMethod()]
        public void RevueTest()
        {
            string id = "1";
            string titre = "le titre";
            string image = "";
            string idGenre = "2";
            string genre = "le genre";
            string idPublic = "3";
            string lePublic = "le public";
            string idRayon = "4";
            string rayon = "le rayon";
            string periodicite = "MS";
            int delaiMiseADispo = 12;

            Revue laRevue = new Revue (id, titre, image, idGenre, genre, idPublic, lePublic, idRayon,rayon,periodicite, delaiMiseADispo);

            Assert.AreEqual(periodicite, laRevue.Periodicite);
        }
    }
}