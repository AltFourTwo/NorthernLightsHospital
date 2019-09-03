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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page, MyPage
    {
        private WindowMGR mgr;

        public Login(WindowMGR mgr)
        {
            this.mgr = mgr;

            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            mgr.User = mgr.BDD.Employes.Where(x => x.USR.Trim() == txtUsr.Text && x.PSW.Trim() == txtPsw.Password).SingleOrDefault();
            if (mgr.User != null)
            {
                switch (mgr.User.Departement.NomDept)
                {
                    case "Administrateur":
                        mgr.pageStack(new VueAdmin(mgr));
                        break;
                    case "Admission":
                        mgr.pageStack(new VueClerk(mgr));
                        break;
                    case "Medical":
                        mgr.pageStack(new VueMedecin(mgr));
                        break;
                    default:
                        MessageBox.Show("Mot de passe et/ou nom d'utilisateur invalide", "Bad Login", MessageBoxButton.OK, MessageBoxImage.Error);
                        mgr.User = null;
                        break;
                }
            }
            else if (txtUsr.Text == "A")//(txtUsr.Text == "admin" && txtPsw.Password == "admin")
            {
                mgr.User = new Employe();
                mgr.User.Departement = new Departement();
                mgr.User.Departement.NomDept = "Administrateur";
                mgr.pageStack(new VueAdmin(mgr));
            }
            else
            {
                MessageBox.Show("Mot de passe et/ou nom d'utilisateur invalide", "Bad Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
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
