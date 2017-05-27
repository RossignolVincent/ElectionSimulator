using ElectionLibrary;
using ElectionLibrary.Parties;
using ElectionLibrary.Parties.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour NewSimulationWindow.xaml
    /// </summary>
    public partial class NewSimulationWindow : Window
    {
        public string MapFile { get; set; }
        public List<PoliticalParty> Parties { get; set; }
        public Boolean validated = false;

        public NewSimulationWindow()
        {
            InitializeComponent();

            Parties = new List<PoliticalParty>();

            var PartiesList = typeof(PoliticalParty).Assembly.GetTypes()
                .Where(t => t.Namespace == "ElectionLibrary.Parties.Concrete").ToList();

            foreach (var obj in PartiesList)
            {
                ListBoxItem lbi = new ListBoxItem();
                lbi.Name = obj.Name;
                lbi.Content = obj.Name;
                PoliticalParties.Items.Add(lbi);
            }
        }

        private void GetFile(object sender, RoutedEventArgs e)
        {
            MapFile = GetMapFile();
        }

        public string GetMapFile()
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                return dlg.FileName;
            }

            return null;
        }

        private void Validate(object sender, RoutedEventArgs e)
        {
            foreach (ListBoxItem item in PoliticalParties.SelectedItems)
            {
                /*Assembly assembly = typeof(PoliticalParty).Assembly;
                PoliticalParty party = (PoliticalParty) Activator.CreateInstance(assembly.GetType(item.Name));
                Parties.Add(party);*/
                switch (item.Name)
                {
                    case "EM":
                        EM EnMarche = new EM();
                        Parties.Add(EnMarche);
                        break;
                    case "FI":
                        FI FranceInsoumise = new FI();
                        Parties.Add(FranceInsoumise);
                        break;
                    case "FN":
                        FN FrontNational = new FN();
                        Parties.Add(FrontNational);
                        break;
                    case "LR":
                        LR LesRepublicains = new LR();
                        Parties.Add(LesRepublicains);
                        break;
                }
            }
            validated = true;
            Close();
        }
    }
}
