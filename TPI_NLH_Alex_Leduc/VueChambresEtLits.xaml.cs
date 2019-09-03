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
    /// Interaction logic for VueChambresEtLits.xaml
    /// </summary>
    public partial class VueChambresEtLits : Page, MyPage
    {
        WindowMGR mgr;
        DepartementMedical depMed;
        bool initDepMed;

        public VueChambresEtLits(WindowMGR mgr, DepartementMedical depMed)
        {
            this.mgr = mgr;
            this.depMed = depMed;
            if (depMed != null) initDepMed = true;
            else initDepMed = false;
            InitializeComponent();
            if (mgr.User.Departement.NomDept == "Administrateur")
            {
                btnAddChambre.Visibility = Visibility.Visible;
                btnDelChambre.Visibility = Visibility.Visible;
                btnAddLit.Visibility = Visibility.Visible;
                btnDelLit.Visibility = Visibility.Visible;
                lblType.Visibility = Visibility.Visible;
                cbChambreType.Visibility = Visibility.Visible;
            }
        }

        private void cbDepMed_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualiserChambres();
        }

        private void lbChambres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualiserLits();
        }

        private void dgLits_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgLits.SelectedItem != null)
            {
                mgr.pageBack(dgLits.SelectedItem as Lit, "Lit");
            }
        }

        private void btnAddChambre_Click(object sender, RoutedEventArgs e)
        {
            if (cbChambreType.SelectedIndex != -1 && cbDepMed.SelectedItem != null)
            {
                Chambre chambre = new Chambre();
                chambre.Type = cbChambreType.Text;
                chambre.DepMedID = (cbDepMed.SelectedItem as DepartementMedical).ID;
                if (cbChambreType.Text == "Standard")
                {
                    chambre.Tarif = 0;
                }
                else if (cbChambreType.Text == "SemiPrivé")
                {
                    chambre.Tarif = 267;
                }
                else if (cbChambreType.Text == "Privé")
                {
                    chambre.Tarif = 571;
                }
                mgr.BDD.Chambres.Add(chambre);
                mgr.SaveChanges();
                actualiserChambres();
            }
            else
            {
                MessageBox.Show(
                       "Choisissez le type de chambre à ajouter.",
                       "Erreur",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
            }
        }

        private void btnDelChambre_Click(object sender, RoutedEventArgs e)
        {
            if (dgChambres.SelectedItem != null)
            {
                MessageBoxResult confirmer = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer cette chambre?",
                    "Confirmez",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Hand);

                if (confirmer == MessageBoxResult.Yes)
                {
                    mgr.BDD.Chambres.Remove(dgChambres.SelectedItem as Chambre);
                    mgr.SaveChanges();
                    actualiser();
                }
            }
            else return;
        }

        private void btnAddLit_Click(object sender, RoutedEventArgs e)
        {
            if (dgChambres.SelectedItem != null)
            {
                Lit lit = new Lit();
                lit.Occupe = false;
                lit.ChambreID = (dgChambres.SelectedItem as Chambre).ID;

                mgr.BDD.Lits.Add(lit);
                mgr.SaveChanges();
                actualiserLits();
            }
        }

        private void btnDelLit_Click(object sender, RoutedEventArgs e)
        {
            if (dgLits.SelectedItem != null)
            {
                MessageBoxResult confirmer = MessageBox.Show(
                    "Êtes-vous sûr de vouloir supprimer ce lit?",
                    "Confirmez",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Hand);

                if (confirmer == MessageBoxResult.Yes)
                {
                    mgr.BDD.Lits.Remove(dgLits.SelectedItem as Lit);
                    mgr.SaveChanges();
                    actualiser();
                }
            }
            else return;
        }

        public void actualiserChambres()
        {
            if (cbDepMed.SelectedItem != null)
            {
                int depMedID = (cbDepMed.SelectedItem as DepartementMedical).ID;
                dgChambres.DataContext = mgr.BDD.Chambres.Where(x => x.DepMedID == depMedID).ToList();
            }
            else dgChambres.DataContext = null;
        }

        public void actualiserLits()
        {
            if (dgChambres.SelectedItem != null)
            {
                int chambreID = (dgChambres.SelectedItem as Chambre).ID;
                dgLits.DataContext = mgr.BDD.Lits.Where(x => x.Occupe == false && x.ChambreID == chambreID).ToList();
            }
            else dgLits.DataContext = null;
        }

        public void actualiser()
        {
            cbDepMed.DataContext = mgr.BDD.DepartementMedicals.ToList();
            if (initDepMed) cbDepMed.SelectedItem = depMed;
            actualiserChambres();
            actualiserLits();
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();   
        }
    }
}
