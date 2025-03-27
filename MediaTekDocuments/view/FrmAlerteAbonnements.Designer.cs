namespace MediaTekDocuments.view
{
    partial class FrmAlerteAbonnements
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblAlerteAbonnements = new System.Windows.Forms.Label();
            this.dgvAlerteAbonnements = new System.Windows.Forms.DataGridView();
            this.btnAlerteAbonnementsFermer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteAbonnements)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAlerteAbonnements
            // 
            this.lblAlerteAbonnements.AutoSize = true;
            this.lblAlerteAbonnements.Location = new System.Drawing.Point(35, 49);
            this.lblAlerteAbonnements.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAlerteAbonnements.Name = "lblAlerteAbonnements";
            this.lblAlerteAbonnements.Size = new System.Drawing.Size(310, 13);
            this.lblAlerteAbonnements.TabIndex = 0;
            this.lblAlerteAbonnements.Text = "Attention, ces abonnements arriveront à expiration d\'ici 30 jours :";
            // 
            // dgvAlerteAbonnements
            // 
            this.dgvAlerteAbonnements.AllowUserToAddRows = false;
            this.dgvAlerteAbonnements.AllowUserToDeleteRows = false;
            this.dgvAlerteAbonnements.AllowUserToResizeColumns = false;
            this.dgvAlerteAbonnements.AllowUserToResizeRows = false;
            this.dgvAlerteAbonnements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlerteAbonnements.Location = new System.Drawing.Point(8, 78);
            this.dgvAlerteAbonnements.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvAlerteAbonnements.MultiSelect = false;
            this.dgvAlerteAbonnements.Name = "dgvAlerteAbonnements";
            this.dgvAlerteAbonnements.ReadOnly = true;
            this.dgvAlerteAbonnements.RowHeadersVisible = false;
            this.dgvAlerteAbonnements.RowHeadersWidth = 62;
            this.dgvAlerteAbonnements.RowTemplate.Height = 28;
            this.dgvAlerteAbonnements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlerteAbonnements.Size = new System.Drawing.Size(357, 98);
            this.dgvAlerteAbonnements.TabIndex = 1;
            // 
            // btnAlerteAbonnementsFermer
            // 
            this.btnAlerteAbonnementsFermer.Location = new System.Drawing.Point(99, 186);
            this.btnAlerteAbonnementsFermer.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnAlerteAbonnementsFermer.Name = "btnAlerteAbonnementsFermer";
            this.btnAlerteAbonnementsFermer.Size = new System.Drawing.Size(147, 27);
            this.btnAlerteAbonnementsFermer.TabIndex = 2;
            this.btnAlerteAbonnementsFermer.Text = "Continuer vers l\'application";
            this.btnAlerteAbonnementsFermer.UseVisualStyleBackColor = true;
            this.btnAlerteAbonnementsFermer.Click += new System.EventHandler(this.btnAlerteAbonnementsFermer_Click);
            // 
            // FrmAlerteAbonnements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 224);
            this.Controls.Add(this.btnAlerteAbonnementsFermer);
            this.Controls.Add(this.dgvAlerteAbonnements);
            this.Controls.Add(this.lblAlerteAbonnements);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmAlerteAbonnements";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Abonnements arrivant à expiration";
            this.Load += new System.EventHandler(this.FrmAlerteAbonnements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteAbonnements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAlerteAbonnements;
        private System.Windows.Forms.DataGridView dgvAlerteAbonnements;
        private System.Windows.Forms.Button btnAlerteAbonnementsFermer;
    }
}