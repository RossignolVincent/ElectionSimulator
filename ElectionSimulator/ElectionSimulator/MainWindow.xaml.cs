using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        /*List<BitmapImage> buildings = new List<BitmapImage>(new BitmapImage[] {
            BitmapImage Building1 = new BitmapImage(new Uri("resource/building1.png", UriKind.Relative)),
            BitmapImage Building2 = new BitmapImage(new Uri("resource/building2.png", UriKind.Relative)),
            BitmapImage Building3 = new BitmapImage(new Uri("resource/building3.png", UriKind.Relative))
        });*/

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ElectionVM;
        }
        []
        private void InitBoard()
        {
            Board.ColumnDefinitions.Clear();
            Board.RowDefinitions.Clear();
            Board.Children.Clear();

            for (int i = 0; i < App.ElectionVM.DimensionX; i++)
            {
                Board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < App.ElectionVM.DimensionY; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
            }
        }

        private void StartNewSimulation(object sender, RoutedEventArgs e)
        {
            string MapFile = GetMapFile();
            if (MapFile != null)
            {
                List<List<string>> Map = LoadMap(MapFile);
                App.ElectionVM.DimensionX = Map[0].Count;
                App.ElectionVM.DimensionY = Map.Count;
                InitBoard();
                LoadTextures(Map);
            }
        }

        private void LoadTextures(List<List<string>> map)
        {
            
        }

        private List<List<string>> LoadMap(string MapFile)
        {
            List<List<string>> Map = new List<List<string>>();
            using (TextFieldParser parser = new TextFieldParser(MapFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                int i = 0;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    Map.Add(new List<string>());
                    foreach (string field in fields)
                    {
                        Map[i].Add(field);
                        Console.Write(field);
                    }
                    Console.WriteLine();
                    i++;
                }
            }
            return Map;
        }

        private string GetMapFile()
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
    }
}
