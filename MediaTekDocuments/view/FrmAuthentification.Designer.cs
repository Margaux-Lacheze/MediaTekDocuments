namespace MediaTekDocuments.view
{
    partial class FrmAuthentification
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAuthLogin = new System.Windows.Forms.TextBox();
            this.txtAuthPwd = new System.Windows.Forms.TextBox();
            this.btnAuthConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Mot de passe :";
            // 
            // txtAuthLogin
            // 
            this.txtAuthLogin.Location = new System.Drawing.Point(163, 74);
            this.txtAuthLogin.Name = "txtAuthLogin";
            this.txtAuthLogin.Size = new System.Drawing.Size(293, 26);
            this.txtAuthLogin.TabIndex = 1;
            // 
            // txtAuthPwd
            // 
            this.txtAuthPwd.Location = new System.Drawing.Point(163, 135);
            this.txtAuthPwd.Name = "txtAuthPwd";
            this.txtAuthPwd.Size = new System.Drawing.Size(293, 26);
            this.txtAuthPwd.TabIndex = 2;
            this.txtAuthPwd.UseSystemPasswordChar = true;
            // 
            // btnAuthConnect
            // 
            this.btnAuthConnect.Location = new System.Drawing.Point(145, 221);
            this.btnAuthConnect.Name = "btnAuthConnect";
            this.btnAuthConnect.Size = new System.Drawing.Size(187, 37);
            this.btnAuthConnect.TabIndex = 3;
            this.btnAuthConnect.Text = "Se connecter";
            this.btnAuthConnect.UseVisualStyleBackColor = true;
            this.btnAuthConnect.Click += new System.EventHandler(this.btnAuthConnect_Click);
            // 
            // FrmAuthentification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 302);
            this.Controls.Add(this.btnAuthConnect);
            this.Controls.Add(this.txtAuthPwd);
            this.Controls.Add(this.txtAuthLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmAuthentification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Authentification";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAuthLogin;
        private System.Windows.Forms.TextBox txtAuthPwd;
        private System.Windows.Forms.Button btnAuthConnect;
    }
}