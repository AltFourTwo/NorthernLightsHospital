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
    /// Interaction logic for VueClerk.xaml
    /// </summary>
    public partial class VueClerk : Page, MyPage
    {
        private WindowMGR mgr;

        public VueClerk(WindowMGR mgr)
        {
            this.mgr = mgr;
            InitializeComponent();
        }

        private void btnPatients_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueClerkListePatients(mgr, false));
        }

        private void btnNouvelleAdmission_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueClerkAdmission(mgr));
        }

        private void btnDossiers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLitsEtChambres_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }

        public void actualiser()
        {
            return;
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            throw new NotImplementedException();
        }
    }
}
