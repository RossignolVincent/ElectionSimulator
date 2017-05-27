using ElectionLibrary;
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
        }

        private void PartiesSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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
            validated = true;
            Close();
        }
    }
}
