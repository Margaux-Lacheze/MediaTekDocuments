using MediaTekDocuments.view;
using System;
using System.Windows.Forms;

namespace MediaTekDocuments
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FrmAuthentification frmAuth = new FrmAuthentification();

            DialogResult result = frmAuth.ShowDialog();

            if (result == DialogResult.OK)
            {
                Application.Run(new FrmMediatek());
            }
        }
    }
}
