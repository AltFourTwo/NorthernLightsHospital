using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TPI_NLH_Alex_Leduc
{
    /// <summary>
    /// Interaction logic for VueClerkNouveauPatient.xaml
    /// </summary>
    public partial class VueClerkDetailsPatient : Page, MyPage
    {
        private WindowMGR mgr;
        private string message;
        private bool editing;
        private Patient editPat;

        // Messages d'erreurs possible
        // -----------------------------
        private string[] msgs =
        {
            "• Le champ 'Nom' est vide.\n",
            "• Le champ 'Prenom' est vide.\n",
            "• Le champ 'Parent' est vide.\n",
            "• Le champ 'Date de naissance' est vide.\n",
            "• Le champ 'Assurance' est vide.\n",
            "• Le nom  ne peux pas dépasser 50 caractères.\n",
            "• Le prenom  ne peux pas dépasser 50 caractères.\n",
            "• Le parent  ne peux pas dépasser 50 caractères.\n",
            "• L'assurance  ne peux pas dépasser 50 caractères.\n"
        };
        // -----------------------------

        public VueClerkDetailsPatient(WindowMGR mgr, Patient patient)
        {
            this.mgr = mgr;
            if(patient != null)
            {
               editing = true;
               this.editPat = patient;
            }
            else
            {
                editing = false;
            }
            InitializeComponent();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                if (!editing)
                {
                    // Nouveau Patient
                    Patient patient = new Patient();
                    patient.Nom = txtNom.Text;
                    patient.Prenom = txtPrenom.Text;
                    patient.Parent = txtParent.Text;
                    patient.BirthDate = dpDateNaiss.SelectedDate;
                    patient.Assurance = txtAssu.Text;

                    mgr.BDD.Patients.Add(patient);
                    mgr.SaveChanges();
                    MessageBox.Show("Nouveau Patient Ajouter aux Dossiers!", "Succès!", MessageBoxButton.OK);
                    mgr.pageBack();
                }
                else
                {
                    // Edition Patient
                    editPat.Nom = txtNom.Text;
                    editPat.Prenom = txtPrenom.Text;
                    editPat.Parent = txtParent.Text;
                    editPat.BirthDate = dpDateNaiss.SelectedDate;
                    editPat.Assurance = txtAssu.Text;

                    mgr.SaveChanges();
                    MessageBox.Show("Modification(s) effectuée(s)!", "Succès!", MessageBoxButton.OK);
                    mgr.pageBack();
                }
            }
            else
            {
                // En cas d'erreur
                txtBoxErreur.Text = message;
                message = String.Empty;
            }
        }

        private void btnVider_Click(object sender, RoutedEventArgs e)
        {

        }

        // Verification des champs
        private bool checkFields()
        {
            if (txtNom.Text == String.Empty) message += msgs[0];
            if (txtPrenom.Text == String.Empty) message += msgs[1];
            if (txtParent.Text == String.Empty) message += msgs[2];
            if (dpDateNaiss.SelectedDate == null) message += msgs[3];
            if (txtAssu.Text == String.Empty) message += msgs[4];
            if (txtNom.Text.Length > 16) message += msgs[5];
            if (txtPrenom.Text.Length > 50) message += msgs[6];
            if (txtParent.Text.Length > 50) message += msgs[7];
            if (txtAssu.Text.Length > 50) message += msgs[8];

            return (message == String.Empty);
        }

        // Vider les champs
        private void clearFields()
        {
            txtNom.Text = String.Empty;
            txtPrenom.Text = String.Empty;
            txtParent.Text = String.Empty;
            dpDateNaiss.SelectedDate = null;
            dpDateNaiss.DisplayDate = DateTime.Today;
            txtAssu.Text = String.Empty;
        }

        public void actualiser()
        {
            if (editing)
            {
                txtNom.Text = editPat.Nom;
                txtPrenom.Text = editPat.Prenom;
                txtParent.Text = editPat.Parent;
                dpDateNaiss.SelectedDate = editPat.BirthDate;
                txtAssu.Text = editPat.Assurance;
                lblHeader.Content = "Modifier Patient";
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
