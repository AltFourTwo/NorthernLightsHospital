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
    /// Interaction logic for VueClerkListeMedecins.xaml
    /// </summary>
    public partial class VueClerkListeMedecins : Page, MyPage
    {
        WindowMGR mgr;
        DepartementMedical depMed;

        public VueClerkListeMedecins(WindowMGR mgr, DepartementMedical depMed)
        {
            this.mgr = mgr;
            this.depMed = depMed;
            InitializeComponent();
        }

        private void dgMedecins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgMedecins.SelectedItem != null)
            {
                mgr.pageBack(dgMedecins.SelectedItem as MedView, "Medecin");
            }
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualiser();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        public void actualiser()
        {
            string term = txtSearch.Text;
            if (term != String.Empty)
            {
                dgMedecins.DataContext = mgr.BDD.MedViews
                    .Where(x => (x.Nom.Contains(term) || x.Prenom.Contains(term)) 
                                && x.NomDeptMed == depMed.NomDeptMed).ToList();
            }
            else
            {
                dgMedecins.DataContext = mgr.BDD.MedViews.Where(x => x.NomDeptMed == depMed.NomDeptMed).ToList();
            }
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
