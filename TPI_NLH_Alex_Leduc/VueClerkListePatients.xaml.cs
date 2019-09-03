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
    /// Interaction logic for VueClerkListePatients.xaml
    /// </summary>
    public partial class VueClerkListePatients : Page, MyPage
    {
        private WindowMGR mgr;
        private bool onlySelect;

        public VueClerkListePatients(WindowMGR mgr, bool onlySelect)
        {
            this.mgr = mgr;
            this.onlySelect = onlySelect;
            InitializeComponent();
            if (mgr.User.Departement.NomDept == "Administrateur")
            {
                btnNouveauPatient.Visibility = Visibility.Hidden;
            }
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        private void btnNouveauPatient_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueClerkDetailsPatient(mgr, null));
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualiser();
        }

        private void dgPatients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgPatients.SelectedItem != null && mgr.User.Departement.NomDept != "Administrateur")
            {
                if (onlySelect)
                {
                    mgr.pageBack(dgPatients.SelectedItem as Patient, "Patient");
                }
                else
                {
                    mgr.pageStack(new VueClerkDetailsPatient(mgr, dgPatients.SelectedItem as Patient));
                }
            }
        }

        public void actualiser()
        {
            string term = txtSearch.Text;
            if (term != String.Empty)
            {
                dgPatients.DataContext = mgr.BDD.Patients.Where(x => x.Nom.Contains(term) || x.Prenom.Contains(term)).ToList();
            }
            else
            {
                dgPatients.DataContext = mgr.BDD.Patients.ToList();
            }
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
