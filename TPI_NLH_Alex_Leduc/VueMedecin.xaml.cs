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
    /// Interaction logic for VueMedecin.xaml
    /// </summary>
    public partial class VueMedecin : Page, MyPage
    {
        private WindowMGR mgr;

        public VueMedecin(WindowMGR mgr)
        {
            this.mgr = mgr;
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualiser();
        }

        private void dgSejours_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgSejours.SelectedItem != null)
            {
                SejMedView viewItem = dgSejours.SelectedItem as SejMedView;
                Patient patient = mgr.BDD.Patients.Where(x => x.ID == viewItem.PatientID).SingleOrDefault();
                MessageBoxResult result = MessageBox.Show(
                    "Voulez vous donner congé a " + patient.Prenom + " " + patient.Nom,
                    "Congé?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Sejour sejour = mgr.BDD.Sejours.Where(x => x.ID == viewItem.ID).SingleOrDefault();
                    sejour.DateFin = DateTime.Now;
                    Lit lit = mgr.BDD.Lits.Where(x => x.ID == viewItem.LitID).SingleOrDefault();
                    lit.Occupe = false;

                    mgr.BDD.SaveChanges();
                    actualiser();
                }
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        public void actualiser()
        {
            string term = txtSearch.Text;
            Medecin userMed = mgr.BDD.Medecins.Where(x => x.EmpID == mgr.User.ID).SingleOrDefault();
            if (term != String.Empty)
            {
                dgSejours.DataContext = mgr.BDD.SejMedViews.Where(x => x.MedID == userMed.MedID && x.DateFin == null && (x.Nom.Contains(term) || x.Prenom.Contains(term))).ToList();
            }
            else
            {
                dgSejours.DataContext = mgr.BDD.SejMedViews.Where(x => x.MedID == userMed.MedID && x.DateFin == null).ToList();
            }
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
