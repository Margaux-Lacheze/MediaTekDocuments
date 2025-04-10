using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using System.Windows.Input;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmCommandes : Form
    {
        #region Commun
        private readonly FrmCommandesController controller;
        private readonly BindingSource bdgSuivis = new BindingSource();
        private readonly BindingSource bdgCommandesDocument = new BindingSource();
        private List<CommandeDocument> lesCommandesDocument = new List<CommandeDocument>();
        private Boolean modifCommande = false;
        const string ENCOURS = "00001";
        const string RELANCEE = "00004";

        /// <summary>
        /// Création du contrôleur lié à ce formulaire
        /// </summary>
        internal FrmCommandes()
        {
            InitializeComponent();
            this.controller = new FrmCommandesController();
        }

        /// <summary>
        /// Sélection du bon onglet de commandes à ouvrir
        /// </summary>
        /// <param name="index"></param>
        public void SelectionnerOnglet(int index)
        {
            if (index >= 0 && index < tabCommandes.TabCount)
            {
                tabCommandes.SelectedIndex = index;
            }
        }

        /// <summary>
        /// Rempli les combos d'étape de suivi d'une commande
        /// </summary>
        /// <param name="lesSuivis"></param>
        /// <param name="bdg"></param>
        /// <param name="cbx"></param>
        private static void RemplirComboSuivi(List<Suivi> lesSuivis, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesSuivis;
            cbx.DataSource = bdg;
            cbx.DisplayMember = "Libelle";
            cbx.ValueMember = "Id";
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Initialisation des onglets lors d'un changement de sélection d'onglet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCommandes.SelectedTab == tabCommandeLivre)
            {
                lesLivres = controller.GetAllLivres();
                RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCommandeLivre);
                CommandeLivreEnCoursModification(false);
                ViderInformationsLivre();
                CommandeLivreEnCoursEdition(false);
            }

            if (tabCommandes.SelectedTab == tabCommandeDvd)
            {
                lesDvd = controller.GetAllDvd();
                RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCommandeDvd);
                CommandeDvdEnCoursModification(false);
                ViderInformationsDvd();
                CommandeDvdEnCoursEdition(false);
                btnCommandeDvdModifier.Enabled = false;
                btnCommandeDvdSupprimer.Enabled = false;
                btnCommandeDvdAjouter.Enabled = false;
                modifCommande = false;
            }
            if (tabCommandes.SelectedTab == tabCommandeAbonnement)
            {
                lesRevues = controller.GetAllRevues();
                AbonnementEnCoursEdition(false);
                ViderInformationsRevue();
                GestionBoutonGestion();
            }

        }
        #endregion



        #region Commandes Livres
        private List<Livre> lesLivres = new List<Livre>();


        /// <summary>
        /// Ouverture de l'onglet de commande de livres 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeLivre_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCommandeLivre);
            CommandeLivreEnCoursModification(false);
            ViderInformationsLivre();
            CommandeLivreEnCoursEdition(false);
        }

        /// <summary>
        /// Recherche d'un livre par son numéro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreRechercher_Click(object sender, EventArgs e)
        {
            if (txtCommandeLivreRechercheNumero.Text != "")
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txtCommandeLivreRechercheNumero.Text));
                if (livre != null)
                {
                    RemplirInformationsDetailleesLivre(livre);
                    AfficherCommandesLivre();
                    btnCommandeLivreAjouter.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Numéro introuvable", "Information");
                    btnCommandeLivreAjouter.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Veuillez saisir un numéro de livre", "Information");
            }
        }


        /// <summary>
        /// Au focus sur le textbox de recherche, vide les informations d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCommandeLivreRechercheNumero_Enter(object sender, EventArgs e)
        {
            ViderInformationsLivre();
            btnCommandeLivreAjouter.Enabled = false;
            btnCommandeLivreModifier.Enabled = false;
            btnCommandeLivreSupprimer.Enabled = false;
        }

        ///// <summary>
        ///// Tri sur les colonnes
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        private void dgvCommandeLivre_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandeLivre.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Date de la commande":
                    sortedList = lesCommandesDocument.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandesDocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Nombre d'exemplaires commandés":
                    sortedList = lesCommandesDocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Stade de la commande":
                    sortedList = lesCommandesDocument.OrderBy(o => o.Suivi).ToList();
                    break;
            }
            RemplirCommandesLivre(sortedList);
        }

        /// <summary>
        /// Clic sur le bouton créer une commande. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreAjouter_Click(object sender, EventArgs e)
        {
            CommandeLivreEnCoursEdition(true);
            CommandeLivreEnCoursModification(false);
            gpbCommandeLivreNouvelleCommande.Enabled = true;
            cbxCommandeLivre.SelectedValue = ENCOURS;
            cbxCommandeLivre.Enabled = false;
        }

        /// <summary>
        /// Validation d'une nouvelle commande ou de la modification d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreValider_Click(object sender, EventArgs e)
        {
            if (!txtCommandeLivreRechercheNumero.Text.Equals(""))
            {
                if (modifCommande)
                {
                    // Modifier commande
                    try
                    {
                        CommandeDocument commandeLivre = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                        string id = commandeLivre.Id;
                        DateTime dateCommande = dtpCommandeLivre.Value;
                        double montant = int.Parse(txtCommandeLivreMontant.Text);
                        int nbExemplaire = int.Parse(txtCommandeLivreNbExemplaire.Text);
                        string idLivreDvd = txtCommandeLivreRechercheNumero.Text;
                        string idSuivi = cbxCommandeLivre.SelectedValue.ToString();
                        string libelle = "";
                        CommandeDocument commandeModif = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);
                        if (controller.ModifierCommandeDocument(commandeModif))
                        {
                            MessageBox.Show("Commande modifiée avec succès", "Succès", MessageBoxButtons.OK);
                            AfficherCommandesLivre();
                            CommandeLivreEnCoursEdition(false);
                        }
                        else
                        {
                            MessageBox.Show("Vérifiez les informations de votre commande et réessayez", "Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("La modification a échoué, veuillez ré-essayer", "Attention");

                    }

                }
                else
                {
                    // Ajouter commande
                    try
                    {
                        string id = controller.CreerNouvelIdCommande();
                        DateTime dateCommande = dtpCommandeLivre.Value;
                        double montant = double.Parse(txtCommandeLivreMontant.Text);
                        int nbExemplaire = int.Parse(txtCommandeLivreNbExemplaire.Text);
                        string idLivreDvd = txtCommandeLivreRechercheNumero.Text;
                        string idSuivi = ENCOURS;
                        string libelle = "";
                        CommandeDocument nouvelleCommande = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);
                        if (controller.CreerNouvelleCommandeDocument(nouvelleCommande))
                        {
                            MessageBox.Show("Nouvelle commande ajoutée", "Succès", MessageBoxButtons.OK);
                            AfficherCommandesLivre();
                            CommandeLivreEnCoursEdition(false);
                        }
                        else
                        {
                            MessageBox.Show("Vérifiez les informations de votre commande et réessayez", "Erreur");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("L'ajout a échoué, veuillez ré-essayer", "Attention");
                    }
                }

            }
            else
            {
                MessageBox.Show("Un numéro de livre doit être renseigné", "Avertissement");
                txtCommandeLivreRechercheNumero.Focus();
            }
            CommandeLivreEnCoursModification(false);
            CommandeLivreEnCoursEdition(false);
        }

        /// <summary>
        /// Annule la modification ou l'ajout d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler l'opération en cours ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CommandeLivreEnCoursModification(false);
                gpbCommandeLivreNouvelleCommande.Enabled = false;
                CommandeLivreEnCoursEdition(false);
            }
        }

        /// <summary>
        /// Clic sur le bouton modifier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreModifier_Click(object sender, EventArgs e)
        {
            if (dgvCommandeLivre.SelectedRows.Count > 0)
            {
                CommandeDocument commandeLivre = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                CommandeLivreEnCoursModification(true);
                CommandeLivreEnCoursEdition(true);
                dtpCommandeLivre.Value = commandeLivre.DateCommande;
                txtCommandeLivreNbExemplaire.Text = commandeLivre.NbExemplaire.ToString();
                txtCommandeLivreMontant.Text = commandeLivre.Montant.ToString();
                cbxCommandeLivre.SelectedValue = commandeLivre.IdSuivi;
            }
            else
            {
                MessageBox.Show("Une commande doit être sélectionnée dans la liste", "Information");
            }
        }

        /// <summary>
        /// Gère les contraintes de transition sur l'étape de la commande d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCommandeLivre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modifCommande)
            {
                CommandeDocument commandeLivre = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                string nouvelleEtape = cbxCommandeLivre.SelectedValue.ToString();

                if (!commandeLivre.EstTransitionAutorisee(nouvelleEtape))
                {
                    MessageBox.Show("Changement du stade de commande non autorisé.\n\n • A l'ajout une commande est en cours \n • Une commande peut ensuite être livrée ou relancée \n • Enfin, la commande sera réglée.", "Information");
                    // Ne pas écouter événement le temps du changement
                    cbxCommandeLivre.SelectedIndexChanged -= cbxCommandeLivre_SelectedIndexChanged;
                    cbxCommandeLivre.SelectedValue = commandeLivre.IdSuivi;
                    cbxCommandeLivre.SelectedIndexChanged += cbxCommandeLivre_SelectedIndexChanged;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandeLivre_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandeLivre.SelectedRows.Count > 0 && !modifCommande)
            {
                btnCommandeLivreModifier.Enabled = true;

                CommandeDocument commandeSelected = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                btnCommandeLivreSupprimer.Enabled = (commandeSelected.IdSuivi == ENCOURS || commandeSelected.IdSuivi == RELANCEE);
            }
            else if (dgvCommandeLivre.SelectedRows.Count == 0)
            {
                btnCommandeLivreModifier.Enabled = false;
                btnCommandeLivreSupprimer.Enabled = false;
            }
        }

        /// <summary>
        /// Clic sur le bouton pour supprimer un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreSupprimer_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeSelected = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
            if (commandeSelected.IdSuivi != ENCOURS || commandeSelected.IdSuivi != RELANCEE)
            {
                MessageBox.Show("Impossible de supprimer une commande déjà livrée", "Erreur", MessageBoxButtons.OK);
            }
            else
            {
                string idCommande = commandeSelected.Id;
                if (MessageBox.Show("Etes-vous sur de vouloir supprimer la commande sélectionnée ?", "Validation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (controller.SupprimerCommande(idCommande))
                    {
                        MessageBox.Show("La commande a été supprimée avec succès", "Succès");
                        AfficherCommandesLivre();
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue", "Echec");
                    }
                }
            }
        }

        /// <summary>
        /// Affichage des informations détaillées d'un livre
        /// </summary>
        /// <param name="livre"></param>
        private void RemplirInformationsDetailleesLivre(Livre livre)
        {
            txtCommandeLivreTitre.Text = livre.Titre;
            txtCommandeLivreAuteur.Text = livre.Auteur;
            txtCommandeLivreCollection.Text = livre.Collection;
            txtCommandeLivreGenre.Text = livre.Genre;
            txtCommandeLivrePublic.Text = livre.Public;
            txtCommandeLivreRayon.Text = livre.Rayon;
            txtCommandeLivreISBN.Text = livre.Isbn;
            txtCommandeLivreCheminImage.Text = livre.Image;
            string image = livre.Image;
            try
            {
                pcbLCommandeLivreImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLCommandeLivreImage.Image = null;
            }
        }

        /// <summary>
        /// Affiche les commandes d'un livre recherché
        /// </summary>
        private void AfficherCommandesLivre()
        {
            string idLivre = txtCommandeLivreRechercheNumero.Text;
            lesCommandesDocument = controller.GetCommandesDocument(idLivre);
            RemplirCommandesLivre(lesCommandesDocument);
        }

        /// <summary>
        /// Vide les informations d'un livre ainsi que ses commandes
        /// </summary>
        private void ViderInformationsLivre()
        {
            txtCommandeLivreRechercheNumero.Text = "";
            txtCommandeLivreTitre.Text = "";
            txtCommandeLivreAuteur.Text = "";
            txtCommandeLivreCollection.Text = "";
            txtCommandeLivreGenre.Text = "";
            txtCommandeLivrePublic.Text = "";
            txtCommandeLivreRayon.Text = "";
            txtCommandeLivreISBN.Text = "";
            txtCommandeLivreCheminImage.Text = "";
            pcbLCommandeLivreImage.Image = null;
            dgvCommandeLivre.DataSource = null;
            txtCommandeLivreRechercheNumero.Focus();
        }

        /// <summary>
        /// Rempli le datagridview contenant les commandes de livre
        /// </summary>
        /// <param name="commande"></param>
        private void RemplirCommandesLivre(List<CommandeDocument> commande)
        {
            if (commande != null && commande.Count > 0)
            {
                bdgCommandesDocument.DataSource = commande;
                dgvCommandeLivre.DataSource = bdgCommandesDocument;
                dgvCommandeLivre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCommandeLivre.Columns["dateCommande"].DisplayIndex = 0;
                dgvCommandeLivre.Columns["montant"].DisplayIndex = 2;
                dgvCommandeLivre.Columns["nbExemplaire"].DisplayIndex = 1;
                dgvCommandeLivre.Columns["Suivi"].DisplayIndex = 3;
                dgvCommandeLivre.Columns["dateCommande"].HeaderText = "Date de la commande";
                dgvCommandeLivre.Columns["montant"].HeaderText = "Montant";
                dgvCommandeLivre.Columns["nbExemplaire"].HeaderText = "Nombre d'exemplaires commandés";
                dgvCommandeLivre.Columns["Suivi"].HeaderText = "Stade de la commande";
                dgvCommandeLivre.Columns["id"].Visible = false;
                dgvCommandeLivre.Columns["idLivreDvd"].Visible = false;
                dgvCommandeLivre.Columns["idSuivi"].Visible = false;
                btnCommandeLivreModifier.Enabled = true;
                CommandeDocument commandeSelected = commande[0];
                btnCommandeLivreSupprimer.Enabled = (commandeSelected.IdSuivi == ENCOURS || commandeSelected.IdSuivi == RELANCEE);
            }
            else
            {
                bdgCommandesDocument.DataSource = null;
                btnCommandeLivreModifier.Enabled = false;
                btnCommandeLivreSupprimer.Enabled = false;
            }
        }

        /// <summary>
        /// Gère l'apparence du groupbox pour ajouter ou modifier une commande, selon l'action attendue
        /// </summary>
        /// <param name="modif"></param>
        private void CommandeLivreEnCoursModification(Boolean modif)
        {
            // lien avec le booléan modifCommande
            modifCommande = modif;
            dgvCommandeLivre.Enabled = !modif;
            if (modif)
            {
                gpbCommandeLivreNouvelleCommande.Text = "Modifier la commande :";
                gpbCommandeLivreNouvelleCommande.Enabled = true;
                cbxCommandeLivre.Enabled = true;
            }
            else
            {
                gpbCommandeLivreNouvelleCommande.Text = "Ajouter une nouvelle commande :";
                dtpCommandeLivre.Value = DateTime.Now;
                txtCommandeLivreNbExemplaire.Text = "";
                txtCommandeLivreMontant.Text = "";
            }
        }

        /// <summary>
        /// Gère les contrôles lorsqu'une commande est en train d'être ajoutée ou modifiée
        /// </summary>
        private void CommandeLivreEnCoursEdition(Boolean edition)
        {
            if (edition)
            {
                gpbCommandeLivreNouvelleCommande.Enabled = true;
                dgvCommandeLivre.Enabled = false;
                btnCommandeLivreSupprimer.Enabled = false;
                btnCommandeLivreModifier.Enabled = false;
                btnCommandeLivreAjouter.Enabled = false;
                gpbCommandeLivreInformations.Enabled = false;
                txtCommandeLivreRechercheNumero.Enabled = false;
                btnCommandeLivreRechercher.Enabled = false;
                tabCommandeDvd.Enabled = false;
                tabCommandeAbonnement.Enabled = false;
            }
            else
            {
                gpbCommandeLivreNouvelleCommande.Enabled = false;
                btnCommandeLivreAjouter.Enabled = true;
                txtCommandeLivreRechercheNumero.Enabled = true;
                btnCommandeLivreRechercher.Enabled = true;
                tabCommandeDvd.Enabled = true;
                tabCommandeAbonnement.Enabled = true;
            }
        }
        #endregion





        #region DVD
        private List<Dvd> lesDvd = new List<Dvd>();


        /// <summary>
        /// Gestion de l'entrée sur l'onglet commande de DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboSuivi(controller.GetAllSuivis(), bdgSuivis, cbxCommandeDvd);
            CommandeDvdEnCoursModification(false);
            ViderInformationsDvd();
            CommandeDvdEnCoursEdition(false);
            btnCommandeDvdModifier.Enabled = false;
            btnCommandeDvdSupprimer.Enabled = false;
            btnCommandeDvdAjouter.Enabled = false;
            modifCommande = false;
        }

        /// <summary>
        /// Vide les informations détaillées d'un dvd, son datagridview et remet le focus sur le numero
        /// </summary>
        private void ViderInformationsDvd()
        {
            txtCommandeDvdNumero.Text = "";
            txtCommandeDvdTitre.Text = "";
            txtCommandeDvdReal.Text = "";
            txtCommandeDvdSynopsis.Text = "";
            txtCommandeDvdGenre.Text = "";
            txtCommandeDvdPublic.Text = "";
            txtCommandeDvdRayon.Text = "";
            txtCommandeLivreCheminImage.Text = "";
            txtCommandeDvdDuree.Text = "";
            pcbCommandeDvdImage.Image = null;
            dgvCommandeDvd.DataSource = null;
            btnCommandeDvdModifier.Enabled = false;
            btnCommandeDvdSupprimer.Enabled= false;
            txtCommandeDvdNumero.Focus();
        }

        /// <summary>
        /// Rempli les informations du dvd passé en paramètre
        /// </summary>
        /// <param name="leDvd"></param>
        private void RemplirInformationsDetailleesDvd(Dvd leDvd)
        {
            txtCommandeDvdTitre.Text = leDvd.Titre;
            txtCommandeDvdReal.Text = leDvd.Realisateur;
            txtCommandeDvdSynopsis.Text = leDvd.Synopsis;
            txtCommandeDvdGenre.Text = leDvd.Genre;
            txtCommandeDvdPublic.Text= leDvd.Public;
            txtCommandeDvdRayon.Text = leDvd.Rayon;
            txtCommandeDvdCheminImage.Text = leDvd.Image;
            txtCommandeDvdDuree.Text = leDvd.Duree.ToString();
            string image = leDvd.Image;
            try
            {
                pcbCommandeDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandeDvdImage = null;
            }
        }

        /// <summary>
        /// Affiche les commandes du dvd sélectionné
        /// </summary>
        private void AfficherCommandesDvd()
        {
            string idDvd = txtCommandeDvdNumero.Text;
            lesCommandesDocument = controller.GetCommandesDocument(idDvd);
            RemplirCommandesDvd(lesCommandesDocument);
        }

        /// <summary>
        /// Rempli et organise le datagridview des commandes
        /// </summary>
        /// <param name="lesCommandesDvd"></param>
        private void RemplirCommandesDvd(List<CommandeDocument> lesCommandesDvd)
        {
            if (lesCommandesDvd != null && lesCommandesDvd.Count > 0)
            {
                bdgCommandesDocument.DataSource = lesCommandesDvd;
                dgvCommandeDvd.DataSource = bdgCommandesDocument;
                dgvCommandeDvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCommandeDvd.Columns["dateCommande"].DisplayIndex = 0;
                dgvCommandeDvd.Columns["montant"].DisplayIndex = 2;
                dgvCommandeDvd.Columns["nbExemplaire"].DisplayIndex = 1;
                dgvCommandeDvd.Columns["Suivi"].DisplayIndex = 3;
                dgvCommandeDvd.Columns["dateCommande"].HeaderText = "Date de la commande";
                dgvCommandeDvd.Columns["montant"].HeaderText = "Montant";
                dgvCommandeDvd.Columns["nbExemplaire"].HeaderText = "Nombre d'exemplaires commandés";
                dgvCommandeDvd.Columns["Suivi"].HeaderText = "Stade de la commande";
                dgvCommandeDvd.Columns["id"].Visible = false;
                dgvCommandeDvd.Columns["idLivreDvd"].Visible = false;
                dgvCommandeDvd.Columns["idSuivi"].Visible = false;
                btnCommandeDvdModifier.Enabled = true;
                CommandeDocument premiereCommande = lesCommandesDvd[0];
                btnCommandeDvdSupprimer.Enabled = (premiereCommande.IdSuivi == ENCOURS || premiereCommande.IdSuivi == RELANCEE);
            }
            else
            {
                bdgCommandesDocument.DataSource = null;
                btnCommandeDvdModifier.Enabled = false;
                btnCommandeDvdSupprimer.Enabled = false;
            }

        }

        /// <summary>
        /// Gère les contrôles selon si l'on est en train d'éditer ou non une commande
        /// </summary>
        /// <param name="edition"></param>
        private void CommandeDvdEnCoursEdition(Boolean edition)
                {
                    if (edition)
                    {
                        grbCommandeDvdEditerCommande.Enabled = true;
                        dgvCommandeDvd.Enabled = false;
                        btnCommandeDvdAjouter.Enabled = false;
                        btnCommandeDvdModifier.Enabled = false;
                        btnCommandeDvdSupprimer.Enabled = false;
                        grbCommandeDvdInformations.Enabled = false;
                        txtCommandeDvdNumero.ReadOnly = true;
                        btnCommandeDvdRechercher.Enabled = false;
                        tabCommandeLivre.Enabled = false;
                        tabCommandeAbonnement.Enabled = false;
                    }
                    else
                    {
                        grbCommandeDvdEditerCommande.Enabled = false;
                        btnCommandeDvdAjouter.Enabled = true;
                        txtCommandeDvdNumero.ReadOnly = false;
                        btnCommandeDvdRechercher.Enabled=true;
                        cbxCommandeDvd.SelectedIndex = -1;
                        tabCommandeLivre.Enabled = true;
                        tabCommandeAbonnement.Enabled = true;
            }
                }

        /// <summary>
        /// Gère les visuels d'édition de commande selon si on ajoute ou modifie une commande
        /// </summary>
        /// <param name="modif"></param>
        private void CommandeDvdEnCoursModification(Boolean modif)
        {
            // lien avec le booléan modifCommande
            modifCommande = modif;
            dgvCommandeDvd.Enabled = !modif;
            if (modif)
            {
                grbCommandeDvdEditerCommande.Text = "Modifier la commande :";
                grbCommandeDvdEditerCommande.Enabled = true;
                cbxCommandeDvd.Enabled = true;
            }
            else
            {
                grbCommandeDvdEditerCommande.Text = "Ajouter une nouvelle commande :";
                dtpCommandeDvd.Value = DateTime.Now;
                txtCommandeDvdNbExemplaire.Text = "";
                txtCommandeDvdMontant.Text = "";
                cbxCommandeDvd.SelectedValue = ENCOURS;
                cbxCommandeDvd.Enabled=false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCommandeDvdNumero_Enter(object sender, EventArgs e)
        {
            ViderInformationsDvd();
            CommandeDvdEnCoursEdition(false);
        }

        /// <summary>
        /// Clic sur le bouton Rechercher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdRechercher_Click(object sender, EventArgs e)
        {
            if(txtCommandeDvdNumero.Text != "")
            {
                Dvd leDvd = lesDvd.Find(x => x.Id.Equals(txtCommandeDvdNumero.Text));
                if (leDvd != null )
                {
                    RemplirInformationsDetailleesDvd(leDvd);
                    AfficherCommandesDvd();
                    btnCommandeDvdAjouter.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Numéro Introuvable", "Erreur");
                    btnCommandeDvdAjouter.Enabled = false;
                    txtCommandeDvdNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("Veuillez renseigner un numéro de Dvd", "Erreur");
                txtCommandeDvdNumero.Focus();
            }
        }

        /// <summary>
        /// Clic sur le bouton Créer une nouvelle commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdAjouter_Click(object sender, EventArgs e)
        {
            CommandeDvdEnCoursEdition(true);
            CommandeDvdEnCoursModification(false);
            grbCommandeDvdEditerCommande.Enabled = true;
            txtCommandeDvdNbExemplaire.Focus();
            cbxCommandeDvd.SelectedValue = ENCOURS;
        }

        /// <summary>
        /// Clic sur le bouton Modifier une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdModifier_Click(object sender, EventArgs e)
        {
            if (dgvCommandeDvd.SelectedRows.Count > 0)
            {
                CommandeDocument commandeDvd = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                CommandeDvdEnCoursEdition(true);
                CommandeDvdEnCoursModification(true);
                dtpCommandeDvd.Value = commandeDvd.DateCommande;
                txtCommandeDvdNbExemplaire.Text = commandeDvd.NbExemplaire.ToString();
                txtCommandeDvdMontant.Text = commandeDvd.Montant.ToString();
            }
            else
            {
                MessageBox.Show("Une commande doit être sélectionnée dans la liste", "Erreur");
            }
            
        }

        /// <summary>
        /// Clic sur le bouton Valider de l'éditeur de commande, envoie la commande en base de données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdValider_Click(object sender, EventArgs e)
        {
            if (!txtCommandeDvdNumero.Text.Equals(""))
            {
                if(modifCommande)
                {
                    try
                    {
                        CommandeDocument commandeDvd = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                        string id = commandeDvd.Id;
                        DateTime dateCommande = dtpCommandeDvd.Value;
                        double montant = double.Parse(txtCommandeDvdMontant.Text);
                        int nbExemplaire = int.Parse(txtCommandeDvdNbExemplaire.Text);
                        string idLivreDvd = txtCommandeDvdNumero.Text;
                        string idSuivi = cbxCommandeDvd.SelectedValue.ToString();
                        string libelle = "";
                        CommandeDocument commandeModif = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);
                        if (controller.ModifierCommandeDocument(commandeModif))
                        {
                            MessageBox.Show("Commande modifiée avec succès", "Succès", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vérifiez les informations de votre commande et réessayez", "Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("La modification a échoué, veuillez ré-essayer", "Attention");
                    }

                }
                else
                {
                    try
                    {
                        string id = controller.CreerNouvelIdCommande();
                        DateTime dateCommande = dtpCommandeDvd.Value;
                        double montant = double.Parse(txtCommandeDvdMontant.Text);
                        int nbExemplaire = int.Parse(txtCommandeDvdNbExemplaire.Text);
                        string idLivreDvd = txtCommandeDvdNumero.Text;
                        string idSuivi = ENCOURS;
                        string libelle = "";
                        CommandeDocument nouvelleCommande = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);


                        if (controller.CreerNouvelleCommandeDocument(nouvelleCommande))
                        {
                            MessageBox.Show("Nouvelle commande de dvd ajoutée", "Succès", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Vérifiez les informations de votre commande et réessayez", "Erreur");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("L'ajout de commande à échoué, veuillez ré-essayer", "Erreur");
                    }
                }
            }
            else
            {
                MessageBox.Show("Un numéro de DVD doit être renseigné", "Erreur");
            }
            AfficherCommandesDvd();
            CommandeDvdEnCoursEdition(false);
            CommandeDvdEnCoursModification(false);
        }

        /// <summary>
        /// Clic sur le bouton d'annulation de l'éditeur de commande, annule la modification en cours
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler l'opération en cours ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                CommandeDvdEnCoursEdition(false);
                CommandeDvdEnCoursModification(false);
                grbCommandeDvdEditerCommande.Enabled = false;
            }

        }

        /// <summary>
        /// Clic sur le bouton pour supprimer une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdSupprimer_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeSelected = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
            if (commandeSelected.IdSuivi != ENCOURS || commandeSelected.IdSuivi != RELANCEE)
            {
                MessageBox.Show("Impossible de supprimer une commande déjà livrée", "Erreur", MessageBoxButtons.OK);
            }
            else
            {
                string idCommande = commandeSelected.Id;
                if (MessageBox.Show("Etes-vous sur de vouloir supprimer la commande sélectionnée ?", "Validation", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (controller.SupprimerCommande(idCommande))
                    {
                        MessageBox.Show("La commande a été supprimée avec succès", "Succès");
                        AfficherCommandesDvd();
                    }
                    else
                    {
                        MessageBox.Show("Une erreur est survenue", "Echec");
                    }
                }
            }
        }

        /// <summary>
        /// Changement d'étape de suivi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCommandeDvd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modifCommande)
            {
                CommandeDocument commandeDvd = (CommandeDocument)bdgCommandesDocument.List[bdgCommandesDocument.Position];
                string nouvelleEtape = cbxCommandeDvd.SelectedValue.ToString();

                if (!commandeDvd.EstTransitionAutorisee(nouvelleEtape))
                {
                    MessageBox.Show("Changement du stade de commande non autorisé.\n\n • A l'ajout une commande est en cours \n • Une commande peut ensuite être livrée ou relancée \n • Enfin, la commande sera réglée.", "Information");
                    // Pour éviter que le messageBox se redéclenche
                    cbxCommandeDvd.SelectedIndexChanged -= cbxCommandeDvd_SelectedIndexChanged;
                    cbxCommandeDvd.SelectedValue = commandeDvd.IdSuivi;
                    cbxCommandeDvd.SelectedIndexChanged += cbxCommandeDvd_SelectedIndexChanged;
                }
            }
        }

        /// <summary>
        /// Clic sur le header du tableau, trie la liste des commandes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandeDvd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandeDvd.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Date de la commande":
                    sortedList = lesCommandesDocument.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandesDocument.OrderBy(o => o.Montant).ToList();
                    break;
                case "Nombre d'exemplaires commandés":
                    sortedList = lesCommandesDocument.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Stade de la commande":
                    sortedList = lesCommandesDocument.OrderBy(o => o.Suivi).ToList();
                    break;
            }
            RemplirCommandesLivre(sortedList);
        }

        #endregion



        #region Revues
        private List<Revue> lesRevues = new List<Revue>();
        private Boolean abonnementEnCoursEdition = false;
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private readonly BindingSource bdgAbonnementsListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        private List<Abonnement> lesAbonnements = new List<Abonnement>();

        /// <summary>
        /// Gère le visuel des boutons de gestion des commandes (ajout et suppression)
        /// </summary>
        public void GestionBoutonGestion()
        {
            GestionBoutonAjout();
            GestionBoutonSuppression();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true si l'ajout est possible</returns>
        public void GestionBoutonAjout()
        {
            if (txtAbonnementNumero != null && !abonnementEnCoursEdition)
            {
                btnAbonnementAjouter.Enabled = true;
            }
            else
            {
                btnAbonnementAjouter.Enabled=false;
            }
        }

        /// <summary>
        /// Gère la possibilité de cliquer ou non sur le bouton supprimer
        /// </summary>
        /// <returns>true si la suppression est possible</returns>
        public void GestionBoutonSuppression()
        {
            if (dgvAbonnementCommandes.SelectedRows.Count > 0 && !abonnementEnCoursEdition)
            {
                Abonnement abonnementSelection = (Abonnement)bdgAbonnementsListe.List[bdgAbonnementsListe.Position];

                bool exemplaireAbonnement = lesExemplaires.Any(exemplaire => abonnementSelection.ParutionDansAbonnement(exemplaire.DateAchat));

                btnAbonnementSupprimer.Enabled = !exemplaireAbonnement;
            }
            else
            {
                btnAbonnementSupprimer.Enabled = false;
            }
        }

        /// <summary>
        /// Vide toutes les informations liées à une revue
        /// </summary>
        public void ViderInformationsRevue()
        {
            txtAbonnementNumero.Text = "";
            txtAbonnementTitre.Text = "";
            txtAbonnementPeriodicite.Text = "";
            txtAbonnementDelaiMiseADispo.Text = "";
            txtAbonnementGenre.Text = "";
            txtAbonnementPublic.Text = "";
            txtAbonnementRayon.Text = "";
            txtAbonnementCheminImage.Text = "";
            pcbAbonnementImageRevue.Image = null;
            dgvAbonnementParutions.DataSource = null;
            pcbAbonnementImageExemplaire.Image = null;
            dgvAbonnementCommandes.DataSource = null;
            AbonnementEnCoursEdition(false);
            GestionBoutonGestion();
            txtAbonnementNumero.Focus();

        }

        /// <summary>
        /// Affiche les exemplaires d'une revue
        /// </summary>
        public void AfficheExemplairesRevue()
        {
            string idRevue = txtAbonnementNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idRevue);
            RemplirExemplairesListe(lesExemplaires);
        }

        /// <summary>
        /// Rempli et gère l'affichage du datagridview des exemplaires d'une revue
        /// </summary>
        /// <param name="exemplaires"></param>
        public void RemplirExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvAbonnementParutions.DataSource = bdgExemplairesListe;
                dgvAbonnementParutions.Columns["idEtat"].Visible = false;
                dgvAbonnementParutions.Columns["id"].Visible = false;
                dgvAbonnementParutions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvAbonnementParutions.Columns["numero"].DisplayIndex = 0;
                dgvAbonnementParutions.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource= null;
            }
        }

        /// <summary>
        /// Tri les exemplaires d'une revue au clic sur l'en-tête d'une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAbonnementParutions_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvAbonnementParutions.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirExemplairesListe(sortedList);
        }

        /// <summary>
        /// Rempli les informations liées à une revue
        /// </summary>
        /// <param name="laRevue"></param>
        public void RemplirInformationsRevue(Revue laRevue)
        {
            txtAbonnementTitre.Text = laRevue.Titre;
            txtAbonnementPeriodicite.Text= laRevue.Periodicite;
            txtAbonnementDelaiMiseADispo.Text = laRevue.DelaiMiseADispo.ToString();
            txtAbonnementGenre.Text= laRevue.Genre;
            txtAbonnementPublic.Text = laRevue.Public;
            txtAbonnementRayon.Text = laRevue.Rayon;
            txtAbonnementCheminImage.Text = laRevue.Image;
            string image = laRevue.Image;
            try
            {
                pcbAbonnementImageRevue.Image = Image.FromFile(image);
            }
            catch
            {
                pcbAbonnementImageRevue.Image = null;
            }
            AfficheExemplairesRevue();
        }

        /// <summary>
        /// Affiche les abonnements liés à une revue
        /// </summary>
        public void AfficheAbonnementsRevue()
        {
            string idRevue = txtAbonnementNumero.Text;
            lesAbonnements = controller.GetAllAbonnements(idRevue);
            RemplirAbonnementsListe(lesAbonnements);
        }

        /// <summary>
        /// Rempli et stylise le datagridview des abonnements
        /// </summary>
        /// <param name="lesAbonnements"></param>
        public void RemplirAbonnementsListe(List<Abonnement> lesAbonnements)
        {
            if (lesAbonnements != null)
            {
                bdgAbonnementsListe.DataSource = lesAbonnements;
                dgvAbonnementCommandes.DataSource = bdgAbonnementsListe;
                dgvAbonnementCommandes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvAbonnementCommandes.Columns["id"].Visible = false;
                dgvAbonnementCommandes.Columns["idRevue"].Visible = false;
                dgvAbonnementCommandes.Columns["dateCommande"].DisplayIndex = 0;
                dgvAbonnementCommandes.Columns["montant"].DisplayIndex = 1;
                dgvAbonnementCommandes.Columns["dateFinAbonnement"].DisplayIndex=2;
                dgvAbonnementCommandes.Columns["dateCommande"].HeaderText = "Date de la commande";
                dgvAbonnementCommandes.Columns["montant"].HeaderText = "Montant";
                dgvAbonnementCommandes.Columns["dateFinAbonnement"].HeaderText = "Date de fin de l'abonnement";
            }
            else
            {
                bdgAbonnementsListe.DataSource= null;
            }
        }

        /// <summary>
        /// Gère les visuels des contrôles lorsqu'un abonnement est en cours d'ajout
        /// </summary>
        /// <param name="edition"></param>
        public void AbonnementEnCoursEdition(Boolean edition)
        {
            abonnementEnCoursEdition = edition;
            dgvAbonnementCommandes.Enabled = !edition;

            if (edition)
            {
                grbAbonnementInformations.Enabled = false;
                GestionBoutonGestion();
                btnAbonnementRechercher.Enabled = false;
                txtAbonnementNumero.ReadOnly = true;
                dgvAbonnementCommandes.Enabled = false;
                grbAbonnementEdition.Enabled = true;
                tabCommandeDvd.Enabled = false;
                tabCommandeLivre.Enabled = false;
                dtpAbonnementDateCommande.Focus();
            }
            else
            {
                grbAbonnementEdition.Enabled = false;
                dtpAbonnementDateCommande.Value = DateTime.Today;
                dtpAbonnementDateFin.Value = DateTime.Today;
                txtAbonnementMontant.Text = "";
            }
        }

        /// <summary>
        /// Entrée de l'utilisateur sur l'onglet abonnement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeAbonnement_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            AbonnementEnCoursEdition(false);
            ViderInformationsRevue();
            GestionBoutonGestion();
        }

        /// <summary>
        /// Clic sur le bouton pour rechercher une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbonnementRechercher_Click(object sender, EventArgs e)
        {
            if (txtAbonnementNumero.Text != "")
            {
                Revue laRevue = lesRevues.Find(x => x.Id.Equals(txtAbonnementNumero.Text));
                if (laRevue != null)
                {
                    RemplirInformationsRevue(laRevue);
                    AfficheExemplairesRevue();
                    AfficheAbonnementsRevue();
                    GestionBoutonGestion();
                }
            }
            else
            {
                MessageBox.Show("Numéro introuvable", "Erreur");
                GestionBoutonGestion();
                txtAbonnementNumero.Focus();
            }

        }

        /// <summary>
        /// Affichage de l'image de l'exemplaire sélectionné dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAbonnementParutions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAbonnementParutions.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbAbonnementImageExemplaire.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbAbonnementImageExemplaire.Image = null;
                }
            }
            else
            {
                pcbAbonnementImageExemplaire.Image = null;
            }
        }

        /// <summary>
        /// Tri des abonnements au clic sur les en-têtes des colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAbonnementCommandes_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvAbonnementCommandes.Columns[e.ColumnIndex].HeaderText;
            List<Abonnement> sortedList = new List<Abonnement>();
            switch (titreColonne)
            {
                case "Date de la commande":
                    sortedList = lesAbonnements.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesAbonnements.OrderBy(o => o.Montant).ToList();
                    break;
                case "Date de fin de l'abonnement":
                    sortedList = lesAbonnements.OrderBy(o => o.DateFinAbonnement).ToList();
                    break;
            }
            RemplirAbonnementsListe(sortedList);
        }

        /// <summary>
        /// Changement de sélection dans le datagridview présentant les abonnements
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAbonnementCommandes_SelectionChanged(object sender, EventArgs e)
        {
            GestionBoutonGestion();
        }

        /// <summary>
        /// Clic sur le bouton pour ajouter ou renouveller un abonnement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbonnementAjouter_Click(object sender, EventArgs e)
        {
            AbonnementEnCoursEdition(true);
        }

        /// <summary>
        /// Clic sur le bouton pour valider l'édition d'un abonnement ou d'un renouvellement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbonnementValider_Click(object sender, EventArgs e)
        {
            if (!txtAbonnementNumero.Text.Equals(""))
            {
                try
                {
                    string id = controller.CreerNouvelIdCommande();
                    DateTime dateCommande = dtpAbonnementDateCommande.Value;
                    double montant = double.Parse(txtAbonnementMontant.Text);
                    DateTime dateFinAbonnement = dtpAbonnementDateFin.Value;
                    string idRevue = txtAbonnementNumero.Text;
                    if (dateCommande >= dateFinAbonnement)
                    {
                        MessageBox.Show("La date de fin de l'abonnement doit être supérieure à la date de la commande", "Erreur");
                    }
                    else
                    {
                        Abonnement nouvelAbonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);
                        if (controller.CreerNouvelAbonnement(nouvelAbonnement))
                        {
                            MessageBox.Show("Nouvel abonnement / renouvellement ajouté", "Succès");
                            AbonnementEnCoursEdition(false);
                            GestionBoutonGestion();
                            AfficheAbonnementsRevue();
                        }
                        else
                        {
                            MessageBox.Show("L'abonnement / le renouvellement n'as pas pu être ajouté", "Erreur");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Une erreur s'est produite lors de l'ajout de la commande", "Erreur", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Le numéro d'une revue doit être préalablement saisi", "Erreur");
                AbonnementEnCoursEdition(false);
                GestionBoutonGestion();
                txtAbonnementNumero.Focus();
            }
        }

        /// <summary>
        /// Clic sur le bouton pour annuler l'édition d'un abonnement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbonnementAnnuler_Click(object sender, EventArgs e)
        {
            AbonnementEnCoursEdition(false);
            GestionBoutonGestion();
        }

        private void btnAbonnementSupprimer_Click(object sender, EventArgs e)
        {
            Abonnement abonnementSelected = (Abonnement)bdgAbonnementsListe.List[bdgAbonnementsListe.Position];
            string idCommande = abonnementSelected.Id;
            if (MessageBox.Show("Etes-vous sur de vouloir supprimer l'abonnement sélectionné ?", "Validation", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (controller.SupprimerCommande(idCommande))
                {
                    MessageBox.Show("L'abonnement a été supprimé avec succès", "Succès");
                    AfficheAbonnementsRevue();
                    GestionBoutonGestion();
                }
                else
                {
                    MessageBox.Show("Une erreur est survenue", "Echec");
                }
            }
        }

        #endregion


    }
}