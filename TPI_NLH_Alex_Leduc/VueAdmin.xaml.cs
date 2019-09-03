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
    /// Interaction logic for VueAdmin.xaml
    /// </summary>
    public partial class VueAdmin : Page, MyPage
    {
        private WindowMGR mgr;

        public VueAdmin(WindowMGR mgr)
        {
            this.mgr = mgr;
            InitializeComponent();
        }

        // Retour à la page de connexion
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        private void btnEmloye_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueAdminPersonnel(mgr));
        }

        private void btnDep_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueAdminDepartements(mgr));
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueClerkListePatients(mgr, false));
        }

        private void btnDossiers_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueDossiers(mgr));
        }

        private void btnChambre_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueChambresEtLits(mgr, null));
        }

        // Aucune actualisation nécessaire
        public void actualiser()
        {
            ShowsNavigationUI = false;
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
