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
    /// Interaction logic for VueClerkAdmission.xaml
    /// </summary>
    public partial class VueClerkAdmission : Page, MyPage
    {
        WindowMGR mgr;
        private Patient patient;
        private DepartementMedical depMed;
        private MedView medecin;
        private Lit lit;
        private byte predispo;
        private TimeSpan agePatient;
        private bool initRaison;
        private string message;

        // Messages d'erreurs possible
        // -----------------------------
        private string[] msgs =
        {
            "• Aucun Patient sélectionné.\n",
            "• Aucun Raison sélectionné.\n",
            "• Aucun Medecin sélectionné.\n",
            "• Aucun Lit sélectionné.\n"
        };
        // -----------------------------


        public VueClerkAdmission(WindowMGR mgr)
        {
            this.mgr = mgr;
            predispo = 0;
            agePatient = new TimeSpan();
            initRaison = true;
            InitializeComponent();
            cbRaison.ItemsSource = mgr.BDD.DepartementMedicals.ToList();
        }

        private void btnSelectPatient_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueClerkListePatients(mgr, true));
        }

        private void cbRaison_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbRaison.SelectedItem != null)
            {
                depMed = cbRaison.SelectedItem as DepartementMedical;
                autoSelectBed();
            }
        }

        private void btnSelectMedecin_Click(object sender, RoutedEventArgs e)
        {
            if (depMed != null)
            {
                mgr.pageStack(new VueClerkListeMedecins(mgr, depMed));
            }
            else
            {
                MessageBox.Show(
                       "Choisissez la raison en premier lieu.",
                       "Erreur",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
            }
        }

        private void btnLit_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageStack(new VueChambresEtLits(mgr, depMed));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkFields())
            {
                // Nouveau sejour de patient
                Sejour sejour = new Sejour();
                sejour.PatientID = patient.ID;
                sejour.MedID = medecin.MedID;
                sejour.LitID = lit.ID;
                sejour.Telephone = xbPhone.IsChecked;
                sejour.Television = xbTV.IsChecked;
                sejour.PreDispo = predispo;
                decimal totalfacture = 0;
                if (xbPhone.IsChecked.Value) totalfacture += 7.50M;
                if (xbTV.IsChecked.Value) totalfacture += 42.50M;
                Chambre chambre = mgr.BDD.Chambres.Where(x => x.ID == lit.ChambreID).FirstOrDefault();
                if (chambre.Type.Trim() == "SemiPrivé" && predispo == 0) totalfacture += 267;
                if (chambre.Type.Trim() == "Privé" && predispo < 2) totalfacture += 571;
                sejour.TotalFacture = totalfacture;
                sejour.DateDebut = DateTime.Now;

                mgr.BDD.Sejours.Add(sejour);
                mgr.SaveChanges();
                lit.Occupe = true;
                mgr.SaveChanges();
                clearFields();
                actualiser();

                txtBoxPredispo.Text = "• Sejour enregistré!";
            }
            else
            {
                // En cas d'erreur
                txtBoxErreur.Text = message;
                message = String.Empty;
            }
        }

        private void btnVider_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void autoSelectBed()
        {
            predispo = 0;

            List<Chambre> initChambres = mgr.BDD.Chambres.Where(x => x.DepMedID == depMed.ID && x.Type == "Standard").ToList();
            Lit initLit = null;
            foreach (Chambre chambre in initChambres)
            {
                initLit = mgr.BDD.Lits.Where(x => x.Occupe == false && x.ChambreID == chambre.ID).FirstOrDefault();
                if (initLit != null) break;
            }
            if (initLit == null)
            {
                predispo++;
            }
            if (predispo == 1)
            {
                initChambres = mgr.BDD.Chambres.Where(x => x.DepMedID == depMed.ID && x.Type == "SemiPrivé").ToList();
                foreach (Chambre chambre in initChambres)
                {
                    initLit = mgr.BDD.Lits.Where(x => x.Occupe == false && x.ChambreID == chambre.ID).FirstOrDefault();
                    if (initLit != null) break;
                }
                if (initLit == null)
                {
                    predispo++;
                }
            }
            if (predispo == 2)
            {
                initChambres = mgr.BDD.Chambres.Where(x => x.DepMedID == depMed.ID && x.Type == "Privé").ToList();
                foreach (Chambre chambre in initChambres)
                {
                    initLit = mgr.BDD.Lits.Where(x => x.Occupe == false && x.ChambreID == chambre.ID).FirstOrDefault();
                    if (initLit != null) break;
                }
            }

            if (initLit == null)
            {
                txtBoxPredispo.Text = "Aucun lit est disponible :(";
            }
            else
            {
                switch (predispo)
                {
                    case 0:
                        txtBoxPredispo.Text = "• Choix Normal de chambre et lit";
                        break;
                    case 1:
                        txtBoxPredispo.Text = "• Chambre/Lit Standard non disponible\n Choix Gratuit de chambre et lit semi-privé";
                        break;
                    case 2:
                        txtBoxPredispo.Text = "• Chambre/Lit Standard/Semi-Privé non disponible\nChoix Gratuit de chambre et lit privé";
                        break;
                    default:
                        txtBoxErreur.Text += "Erreur lors de la selection automatique d'un lit.\n";
                        break;
                }
                lit = initLit;
            }
        }

        public void actualiser()
        {
            if (patient != null)
            {
                agePatient = (TimeSpan)(DateTime.Today - patient.BirthDate);
                txtPatient.Text = patient.Prenom + " " + patient.Nom + " " + (int)(agePatient.Days/365.25) + " ans";
                if (initRaison)
                {
                    if (agePatient.Days < 16 * 365.25)
                    {
                        cbRaison.SelectedItem = mgr.BDD.DepartementMedicals.Where(x => x.NomDeptMed == "Pédiatrie").FirstOrDefault();
                    }
                    else
                    {
                        cbRaison.SelectedItem = mgr.BDD.DepartementMedicals.Where(x => x.NomDeptMed == "Général").FirstOrDefault();
                    }
                    initRaison = false;
                }
            }
            else { txtPatient.Text = string.Empty; }

            cbRaison.DataContext = mgr.BDD.DepartementMedicals.ToList();

            if (medecin != null)
            {
                txtMedecin.Text = medecin.Prenom + " " + medecin.Nom;
            }
            else { txtMedecin.Text = string.Empty; }
            
            if (lit != null)
            {
                int id = lit.ID;
                string displayID = id.ToString();
                if (id < 100)
                {
                    displayID = ((id < 10) ? "00" : "0") + displayID;
                }
                displayID = lit.Chambre.DepartementMedical.NomDeptMed.Substring(0, 1) + displayID;
                txtLit.Text = lit.Chambre.DepartementMedical.NomDeptMed + " " + displayID;

            } else { txtLit.Text = string.Empty; }
        }

        // Verification des champs
        private bool checkFields()
        {
            if (patient == null) message += msgs[0];
            if (cbRaison.SelectedIndex == -1) message += msgs[1];
            if (medecin == null) message += msgs[2];
            if (lit == null) message += msgs[3];

            return (message == String.Empty);
        }


        // Vider les champs
        private void clearFields()
        {
            patient = null;
            cbRaison.SelectedIndex = -1;
            medecin = null;
            lit = null;
            xbPhone.IsChecked = false;
            xbTV.IsChecked = false;
            actualiser();
        }

        public void receiveTransfer(object transfer, string objectType)
        {
            switch (objectType)
            {
                case "Patient":
                    if (transfer != patient)
                    {
                        initRaison = true;
                    }
                    patient = transfer as Patient;
                    break;
                case "Medecin":
                    medecin = transfer as MedView;
                    break;
                case "Lit":
                    lit = transfer as Lit;
                    break;
                default:
                    MessageBox.Show(
                       "Objet Reçu invalide\nObjet de type " + objectType + " reçu.",
                       "Erreur",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    break;
            }
            actualiser();
        }

        private void btnRetour_Click(object sender, RoutedEventArgs e)
        {
            mgr.pageBack();
        }
    }
}
