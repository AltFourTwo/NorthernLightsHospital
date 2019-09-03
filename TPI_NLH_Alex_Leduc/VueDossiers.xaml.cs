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
    /// Interaction logic for VueDossiers.xaml
    /// </summary>
    public partial class VueDossiers : Page, MyPage
    {
        WindowMGR mgr;

        public VueDossiers(WindowMGR mgr)
        {
            this.mgr = mgr;
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualiser();
        }

        public void actualiser()
        {
            string term = txtSearch.Text;
            if (term != String.Empty)
            {
                dgSejours.DataContext = mgr.BDD.SejourViews.Where(x => 
                    x.Pprenom.Contains(term) || x.Pnom.Contains(term) || 
                    x.Eprenom.Contains(term) || x.Enom.Contains(term) || 
                    x.NomDeptMed.Contains(term) || x.Type.Contains(term)).ToList();
            }
            else
            {
                dgSejours.DataContext = mgr.BDD.SejourViews.ToList();
            }
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
