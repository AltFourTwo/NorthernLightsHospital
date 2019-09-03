using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for VueAdminPersonnel.xaml
    /// </summary>
    public partial class VueAdminPersonnel : Page, MyPage
    {
        private WindowMGR mgr;
        private Employe empEdit;
        private Medecin medEdit;
        private string message;
        private bool editing;
        private bool editingMed;
        private bool isMedecin;

        // Messages d'erreurs possible
        // -----------------------------
        private string[] msgs =
        {
            "Le champ 'Nom' est vide.\n",
            "Le champ 'Prenom' est vide.\n",
            "Le champ 'Nom d'utilisateur' est vide.\n",
            "Le champ 'Mot de passe' est vide.\n",
            "Aucun département n'a été choisi.\n",
            "Aucun département médical n'a été choisi.\n",
            "Le nom d'utilisateur ne peux pas dépasser 16 caractères.\n",
            "Le mot de passe ne peux pas dépasser 16 caractères.\n"
        };
        // -----------------------------

        public VueAdminPersonnel(WindowMGR mgr)
        {
            this.mgr = mgr;
            message = String.Empty;
            editing = false;
            isMedecin = false;
            InitializeComponent();
        }

        // Button Ajouter/Enregistrer
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                if (!editing)
                {
                    // Ajout
                    Employe emp = new Employe();
                    emp.Nom = txtNom.Text;
                    emp.Prenom = txtPrenom.Text;
                    emp.USR = txtUsr.Text;
                    emp.PSW = txtPsw.Password;
                    emp.DepID = (cbDepts.SelectedItem as Departement).ID;

                    mgr.BDD.Employes.Add(emp);
                    mgr.SaveChanges();

                    if ((cbDepts.SelectedItem as Departement).NomDept == "Medical")
                    {
                        // Ajout Medecin
                        mgr.BDD.Entry(emp).GetDatabaseValues();
                        Medecin med = new Medecin();
                        med.EmpID = emp.ID;
                        med.DepMedID = (cbDeptsMed.SelectedItem as DepartementMedical).ID;

                        mgr.BDD.Medecins.Add(med);
                        mgr.SaveChanges();
                    }
                    actualiser();
                    MessageBox.Show("Employé Ajouté!", "Succès!", MessageBoxButton.OK);
                }
                else
                {
                    // Edition Employé
                    empEdit.Nom = txtNom.Text;
                    empEdit.Prenom = txtPrenom.Text;
                    empEdit.USR = txtUsr.Text;
                    empEdit.PSW = txtPsw.Password;
                    empEdit.DepID = (cbDepts.SelectedItem as Departement).ID;
                    
                    if (editingMed)
                    {
                        // Edition Medecin
                        medEdit.DepMedID = (cbDeptsMed.SelectedItem as DepartementMedical).ID;
                    }

                    mgr.SaveChanges();
                    actualiser();
                    addMode();
                    MessageBox.Show("Modification(s) effectuée(s)!", "Succès!", MessageBoxButton.OK);
                }
            }
            else
            {
                // En cas d'erreur
                MessageBox.Show(message, "Erreur d'entrée", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                message = String.Empty;
            }
        }

        // Button Vider/Annuler
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            if (editing)
            {
                addMode();
                clearFields();
            }
            else
            {
                clearFields();
            }
        }

        // Datagrid Employés Menu Click-droit/Edit
        private void menuEditEmp_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployes.SelectedItem == null)
            {
                return;
            }
            editMode();

            EmpView empViewSelection = dgEmployes.SelectedItem as EmpView;
            empEdit = mgr.BDD.Employes.Where(x => x.ID == empViewSelection.ID).FirstOrDefault();

            txtNom.Text = empEdit.Nom;
            txtPrenom.Text = empEdit.Prenom;
            txtUsr.Text = empEdit.USR;
            txtPsw.Password = empEdit.PSW;
            cbDepts.SelectedItem = empEdit.Departement;
        }

        // Datagrid Employés Menu Click-droit/Supprimer
        private void menuSuppEmp_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployes.SelectedItem == null)
            {
                return;
            }
            if (empEdit != null && (dgEmployes.SelectedItem as EmpView).ID == empEdit.ID)
            {
                addMode();
            }
            EmpView empViewSelection = dgEmployes.SelectedItem as EmpView;
            Employe delEmp = mgr.BDD.Employes.Where(x => x.ID == empViewSelection.ID).FirstOrDefault();
            MessageBoxResult confirmer = MessageBox.Show(
                "Êtes-vous sûr de vouloir supprimer l'employé " + delEmp.Prenom + " " + delEmp.Nom + "?",
                "Confirmez",
                MessageBoxButton.YesNo,
                MessageBoxImage.Hand);

            if (confirmer == MessageBoxResult.Yes)
            {
                mgr.BDD.Employes.Remove(delEmp);
                mgr.SaveChanges();
                actualiser();
            }
        }

        // Datagrid Medecins Menu Click-droit/Edit
        private void menuEditMed_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedecins.SelectedItem == null)
            {
                return;
            }
            editMode();
            editingMed = true;
            cbDepts.IsEnabled = false;

            MedView medViewSelection = dgMedecins.SelectedItem as MedView;
            empEdit = mgr.BDD.Employes.Where(x => x.ID == medViewSelection.ID).FirstOrDefault();
            medEdit = mgr.BDD.Medecins.Where(x => x.MedID == medViewSelection.MedID).FirstOrDefault();
                
            txtNom.Text = empEdit.Nom;
            txtPrenom.Text = empEdit.Prenom;
            txtUsr.Text = empEdit.USR;
            txtPsw.Password = empEdit.PSW;
            cbDepts.SelectedItem = empEdit.Departement;
            cbDeptsMed.SelectedItem = medEdit.DepartementMedical;
        }

        // Datagrid Medecins Menu Click-droit/Supprimer
        private void menuSuppMed_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedecins.SelectedItem == null)
            {
                return;
            }
            if (empEdit != null && (dgMedecins.SelectedItem as MedView).ID == empEdit.ID)
            {
                addMode();
            }
            MedView medViewSelection = dgMedecins.SelectedItem as MedView;
            Employe delEmp = mgr.BDD.Employes.Where(x => x.ID == medViewSelection.ID).FirstOrDefault();
            Medecin delMed = mgr.BDD.Medecins.Where(x => x.MedID == medViewSelection.MedID).FirstOrDefault();
            MessageBoxResult confirmer = MessageBox.Show(
                "Êtes-vous sûr de vouloir supprimer le médecin " + delEmp.Prenom + " " + delEmp.Nom + "?",
                "Confirmez",
                MessageBoxButton.YesNo,
                MessageBoxImage.Hand);

            if (confirmer == MessageBoxResult.Yes)
            {
                mgr.BDD.Medecins.Remove(delMed);
                mgr.BDD.Employes.Remove(delEmp);
                mgr.SaveChanges();
                actualiser();
            }
        }

        // Verification pour l'accessibilité des départements médicaux
        private void cbDepts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            refreshMedDepVisibility();
        }

        // Verification pour l'accessibilité des départements médicaux
        private void refreshMedDepVisibility()
        {
            if (cbDepts.SelectedIndex > -1 && (cbDepts.SelectedItem as Departement).NomDept == "Medical")
            {
                isMedecin = true;
                lblArrow.Visibility = Visibility.Visible;
                lblDepMed.Visibility = Visibility.Visible;
                cbDeptsMed.Visibility = Visibility.Visible;
            }
            else
            {
                isMedecin = false;
                lblArrow.Visibility = Visibility.Hidden;
                lblDepMed.Visibility = Visibility.Hidden;
                cbDeptsMed.Visibility = Visibility.Hidden;
            }
        }

        // Verification des champs
        private bool checkFields()
        {
            if (txtNom.Text == String.Empty) message += msgs[0];
            if (txtPrenom.Text == String.Empty) message += msgs[1];
            if (txtUsr.Text == String.Empty) message += msgs[2];
            if (txtPsw.Password == String.Empty) message += msgs[3];
            if (cbDepts.SelectedIndex == -1) message += msgs[4];
            if (isMedecin && cbDeptsMed.SelectedIndex == -1) message += msgs[5];
            if (txtUsr.Text.Length > 16) message += msgs[6];
            if (txtPsw.Password.Length > 16) message += msgs[7];

            return (message == String.Empty);
        }

        // Vider les champs
        private void clearFields()
        {
            txtNom.Text = String.Empty;
            txtPrenom.Text = String.Empty;
            txtUsr.Text = String.Empty;
            txtPsw.Password = String.Empty;
            cbDepts.SelectedIndex = -1;
            cbDeptsMed.SelectedIndex = -1;
            refreshMedDepVisibility();
        }

        // Actualiser les composants liés a la base de données
        public void actualiser()
        {
            mgr.refresh();
            dgEmployes.DataContext = mgr.BDD.EmpViews.ToList();
            dgMedecins.DataContext = mgr.BDD.MedViews.ToList();
            cbDepts.ItemsSource = mgr.BDD.Departements.ToList();
            cbDeptsMed.ItemsSource = mgr.BDD.DepartementMedicals.ToList();
            clearFields();
        }

        // Lorsqu'une edition se termine
        private void addMode()
        {
            editing = false;
            editingMed = false;
            btnOk.Content = "Ajouter";
            btnNo.Content = "Vider";
            cbDepts.IsEnabled = true;
        }

        // Mode edition
        private void editMode()
        {
            editing = true;
            btnOk.Content = "Enregistrer";
            btnNo.Content = "Annuler";
        }

        // Afficher les départements
        private void btnDepts_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueAdminDepartements(mgr));
        }

        // Retour à la page admin
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
