using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmAlerteAbonnements : Form
    {
        private readonly FrmAlerteAbonnementsController controller;
        private readonly BindingSource bdgAbonnementsExpireListe = new BindingSource();
        private bool existenceAbonnementsExpire;

        /// <summary>
        /// Création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmAlerteAbonnements()
        {
            InitializeComponent();
            this.controller = new FrmAlerteAbonnementsController();
        }

        /// <summary>
        /// Rempli le datagridview s'il y a des abonnements prêts à expirer
        /// </summary>
        public void RemplirListeAbonnementsExpiration()
        {
            List<AbonnementExpiration> lesAbonnementsExpire = controller.GetAllAbonnementExpiration();
            if (lesAbonnementsExpire != null )
            {
                bdgAbonnementsExpireListe.DataSource = lesAbonnementsExpire;
                dgvAlerteAbonnements.DataSource = bdgAbonnementsExpireListe;
                dgvAlerteAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvAlerteAbonnements.Columns["titre"].HeaderText = "Titre de la revue";
                dgvAlerteAbonnements.Columns["dateFinAbonnement"].HeaderText = "Echéance de l'abonnement";
                existenceAbonnementsExpire = true;
            }
            else
            {
                dgvAlerteAbonnements.DataSource= null;
                existenceAbonnementsExpire=false;
            }
        }

        /// <summary>
        /// Gère les visuels selon s'il existe ou non des abonnements arrivant à expiration
        /// </summary>
        public void AffichageAbonnements()
        {
            if (existenceAbonnementsExpire)
            {
                lblAlerteAbonnements.Text = "Attention, ces abonnements vont arriver à expiration d'ici 30 jours :";
                dgvAlerteAbonnements.Visible = true;
            }
            else
            {
                lblAlerteAbonnements.Text = "Aucun abonnement ne va expirer dans les 30 jours";
                dgvAlerteAbonnements.Visible = false;
            }
        }

        /// <summary>
        /// Au chargement de la fenêtre, affiche les abonnements qui vont expirer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAlerteAbonnements_Load(object sender, EventArgs e)
        {
            RemplirListeAbonnementsExpiration();
            AffichageAbonnements();
        }

        /// <summary>
        /// Ferme la fenêtre et retourne sur FrmMediatek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlerteAbonnementsFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
