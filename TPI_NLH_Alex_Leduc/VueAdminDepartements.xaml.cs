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
    /// Logique d'interaction pour VueAdminDepartements.xaml
    /// </summary>
    public partial class VueAdminDepartements : Page, MyPage
    {
        private WindowMGR mgr;
        private Departement depEdit;
        private DepartementMedical depMedEdit;
        
        private bool editing;
        private bool editingMedical;


        public VueAdminDepartements(WindowMGR mgr)
        {
            this.mgr = mgr;
            InitializeComponent();
        }

        // Bouton Ajouter/Enregistrer
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomDep.Text != String.Empty)
            {
                if (!editing)
                {
                    if (!(bool)xbIsMedical.IsChecked)
                    {
                        // Ajout Département
                        Departement dep = new Departement();
                        dep.NomDept = txtNomDep.Text;

                        mgr.BDD.Departements.Add(dep);
                        mgr.SaveChanges();

                        actualiser();
                    }
                    else
                    {
                        // Ajout Département Médical
                        DepartementMedical depMed = new DepartementMedical();
                        depMed.NomDeptMed = txtNomDep.Text;

                        mgr.BDD.DepartementMedicals.Add(depMed);
                        mgr.SaveChanges();

                        actualiser();
                    }

                    MessageBox.Show("Département Ajouté!", "Succès!", MessageBoxButton.OK);
                }
                else
                {
                    if (!editingMedical)
                    {
                        // Edition Département
                        MessageBoxResult confirmer = deptCheck(depEdit.NomDept, true, false);
                        if (confirmer == MessageBoxResult.Yes)
                        {
                            depEdit.NomDept = txtNomDep.Text;
                            mgr.SaveChanges();
                            actualiser();
                            addMode();
                        }
                    }
                    else
                    {
                        // Edition Département Médical
                        MessageBoxResult confirmer = deptCheck(depMedEdit.NomDeptMed, true, false);
                        if (confirmer == MessageBoxResult.Yes)
                        {
                            depMedEdit.NomDeptMed = txtNomDep.Text;
                            mgr.SaveChanges();
                            actualiser();
                            addMode();
                        }
                    }
                }
            }
            else
            {
                // En cas d'erreur
                MessageBox.Show("Entrez un nom pour le département", "Erreur d'entrée", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        // Annuler l'édition
        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            addMode();
        }

        // Activer l'édition Département
        private void menuEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepts.SelectedItem == null)
            {
                return;
            }
            editing = true;
            editingMedical = false;
            btnOk.Content = "Enregistrer";
            depEdit = (dgDepts.SelectedItem as Departement);
            txtID.Text = depEdit.ID.ToString();
            txtNomDep.Text = depEdit.NomDept;
            btnNo.Visibility = Visibility.Visible;
            xbIsMedical.IsChecked = false;
            xbIsMedical.IsEnabled = false;
        }

        // Suppression Département
        private void menuSupp_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepts.SelectedItem == null)
            {
                return;
            }

            Departement delDept = dgDepts.SelectedItem as Departement;
            MessageBoxResult confirmer = deptCheck(delDept.NomDept, false, false);

            if (confirmer == MessageBoxResult.Yes)
            {
                if (delDept == depEdit)
                {
                    addMode();
                }
                mgr.BDD.Departements.Remove(delDept);
                mgr.SaveChanges();
                actualiser();
            }
        }

        // Activer l'édition Département Mécical
        private void menuEditMed_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeptMed.SelectedItem == null)
            {
                return;
            }
            editing = true;
            editingMedical = true;
            btnOk.Content = "Enregistrer";
            depMedEdit = (dgDeptMed.SelectedItem as DepartementMedical);
            txtID.Text = depMedEdit.ID.ToString();
            txtNomDep.Text = depMedEdit.NomDeptMed;
            btnNo.Visibility = Visibility.Visible;
            xbIsMedical.IsChecked = true;
            xbIsMedical.IsEnabled = false;
        }

        // Suppression Département Mécical
        private void menuSuppMed_Click(object sender, RoutedEventArgs e)
        {
            if (dgDeptMed.SelectedItem == null)
            {
                return;
            }

            DepartementMedical delDeptMed = dgDeptMed.SelectedItem as DepartementMedical;
            MessageBoxResult confirmer = deptCheck(delDeptMed.NomDeptMed, false, true);

            if (confirmer == MessageBoxResult.Yes)
            {
                if (delDeptMed == depMedEdit)
                {
                    addMode();
                }
                mgr.BDD.DepartementMedicals.Remove(delDeptMed);
                mgr.SaveChanges();
                actualiser();
            }
        }

        // Veuillez ne pas modifier/supprimer ces départements pour le bon fonctionnement de l'application! :)
        private MessageBoxResult deptCheck(string deptName, bool edit, bool med)
        {
            if (deptName == "Administrateur" || deptName == "Admission" || deptName == "Medical" || 
                deptName == "Général"        || deptName == "Pédiatrie" || deptName == "Chirurgie")
            {
                return MessageBox.Show(
                    "Vous ne pouvez pas " + (edit ? "éditer" : "supprimer") + " ce département",
                    "Erreur",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            else
            {
                return MessageBox.Show(
                "Êtes-vous sûr de vouloir " + (edit ? "modifier" : "supprimer") + " le département" + (med ? "médical " : " ") + deptName + "?",
                "Confirmez",
                MessageBoxButton.YesNo,
                MessageBoxImage.Hand);
            }
        }

        // Lorsqu'une edition se termine
        private void addMode()
        {
            depEdit = null;
            depMedEdit = null;
            editing = false;
            editingMedical = false;
            btnOk.Content = "Ajouter";
            clearFields();
            btnNo.Visibility = Visibility.Hidden;
            xbIsMedical.IsEnabled = true;
        }

        // Vider les champs
        private void clearFields()
        {
            txtNomDep.Text = String.Empty;
            txtID.Text = String.Empty;
            xbIsMedical.IsChecked = false;
        }

        // Actualiser les composants liés a la base de données
        public void actualiser()
        {
            dgDepts.ItemsSource = mgr.BDD.Departements.ToList();
            dgDeptMed.ItemsSource = mgr.BDD.DepartementMedicals.ToList();
        }

        // Retour
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
