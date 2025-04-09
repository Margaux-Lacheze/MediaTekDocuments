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
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;

        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
        }

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
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    FrmMediatek frmMediatek = new FrmMediatek();
                    frmMediatek.Show();
                    
                }
            }
            else
            {
                MessageBox.Show("Les identifiants saisis sont incorrects", "Avertissement");
            }
        }
    } 
}