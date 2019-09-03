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
    /// Interaction logic for WindowMGR.xaml
    /// </summary>
    public partial class WindowMGR : Window
    {

        private Stack<Page> nav = new Stack<Page>();
        private NLH_ALEX_LEDUCEntities myBDD;

        public NLH_ALEX_LEDUCEntities BDD
        {
            get { return myBDD; }
        }


        private Employe user;
        public Employe User
        {
            get { return user; }
            set { user = value; }
        }

        public WindowMGR()
        {
            InitializeComponent();
            myBDD = new NLH_ALEX_LEDUCEntities();
            nav.Push(new Login(this));
            actualiser();
        }

        public void pageStack(Page page)
        {
            nav.Push(page);
            actualiser();
        }

        public void pageBack()
        {
            nav.Pop();
            if (nav.Count() == 0)
            {
                Close();
            }
            else
            {
                actualiser();
            }
        }

        public void pageBack(Object transfer, string objectType)
        {
            nav.Pop();
            if (nav.Count() == 0)
            {
                Close();
            }
            else
            {
                actualiser();
                ((MyPage)nav.Peek()).receiveTransfer(transfer, objectType);
            }
        }

        public void actualiser()
        {
            ((MyPage)nav.Peek()).actualiser();
            
            mainFrame.Content = nav.Peek();
        }

        public void refresh()
        {
            myBDD.Dispose();
            myBDD = new NLH_ALEX_LEDUCEntities();
        }

        public int SaveChanges()
        {
            return myBDD.SaveChanges();
        }

        private void adminPrompt()
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Key == Key.A)
                {
                    adminPrompt();
                }
                else if (e.Key == Key.R) {
                    pageBack();
                }
                
            }
        }

        private void miAdmin_Click(object sender, RoutedEventArgs e)
        {
            adminPrompt();
        }

        private void miRetour_Click(object sender, RoutedEventArgs e)
        {
            pageBack();
        }

        private void miExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
