using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using Newtonsoft.Json;
using MediaTekDocuments.utils;
using System.Windows.Forms.Design;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'Affichage
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Clic sur le bouton "Se connecter", vérifie les droits de l'utilisateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAuthConnect_Click(object sender, EventArgs e)
        {
            string login = txtAuthLogin.Text;
            string pwd = txtAuthPwd.Text;

            Utilisateur utilisateurEnCours = controller.CheckUtilisateur(login, pwd);
            if (utilisateurEnCours != null)
            {
                Service.IdService = utilisateurEnCours.IdService;
                if (Service.IdService == 3)
                {
                    MessageBox.Show("Vous n'avez pas les droits suffisants pour accéder à l'application", "Avertissement");
                    this.DialogResult = DialogResult.Cancel;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                MessageBox.Show("Les identifiants saisis sont incorrects", "Avertissement");
            }
        }
    } 
}