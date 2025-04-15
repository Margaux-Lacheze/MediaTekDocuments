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
    public class UtilisateurTests
    {
        [TestMethod()]
        public void UtilisateurTest()
        {
            string login = "login";
            string password = "password";
            int idService = 1;
            string libelle = "administratif";

            Utilisateur utilisateur = new Utilisateur(login, password, idService, libelle);

            Assert.AreEqual(login, utilisateur.Login);
        }
    }
}