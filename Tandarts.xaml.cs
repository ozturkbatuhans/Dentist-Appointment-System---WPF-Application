using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ozturk_Batuhan_PE1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> geregisteerdePatienten = new Dictionary<string, string>();
        Queue<string> wachtendePatienten = new Queue<string>();
        List<string> behandeldePatienten = new List<string>();



        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {

            string naam = $"{txtFistName.Text} {txtLastName.Text}".Trim().ToLower();
            if (!geregisteerdePatienten.ContainsKey(naam))
            {
                geregisteerdePatienten.Add(naam, "niet behandeld");
                lstAfsrpaak.Items.Add(naam);
            }
        }

        private void btnAanmelding_Click(object sender, RoutedEventArgs e)
        {
            string naam = $"{txtFistName1.Text} {txtLastName1.Text}".Trim().ToLower();
            if (geregisteerdePatienten.ContainsKey (naam) && !wachtendePatienten.Contains(naam))
            {
                wachtendePatienten.Enqueue(naam);
                lstAanmelding.Items.Add(naam);
            }
        }

        private void btnGaNaarTandarts_Click(object sender, RoutedEventArgs e)
        {
            if (wachtendePatienten.Count > 0)
            {
                string patient = wachtendePatienten.Dequeue();
                lblPatientNaam.Content = patient;
                lstAanmelding.Items.Remove(patient);
            }
        }

        private void btnAfronden_Click(object sender, RoutedEventArgs e)
        {

            string patient = lblPatientNaam.Content.ToString();
            if (!string.IsNullOrEmpty(patient))
            {
                string resultaat = $"{(chkGaatjes.IsChecked == true ? "Gaatjes" : "")} {(chkReinigen.IsChecked == true ? "Reinigen" : "")}";
                geregisteerdePatienten[patient] = resultaat;
                behandeldePatienten.Add(patient);
                cmbPatienten.Items.Add(patient);
                lblPatientNaam.Content = "";
            }

        }

        private void cmbPatienten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedPatient = cmbPatienten.SelectedItem?.ToString();
            if (selectedPatient != null && geregisteerdePatienten.ContainsKey(selectedPatient))
            {
                lblResult.Content = geregisteerdePatienten[selectedPatient];
            }
        }

        }
    }
    
