using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace ElectionSimulator
{
    class ElectionInitializer
    {
        static Random random = new Random();
        public List<List<string>> Map {get; set;}

        public ElectionInitializer()
        {
            string MapFile = GetMapFile();
            if (MapFile != null)
            {
                Map = LoadMap(MapFile);
            }
        }

        List<Uri> Buildings = new List<Uri>(new Uri[] {
            new Uri("resource/buildings/building1.png", UriKind.Relative),
            new Uri("resource/buildings/building2.png", UriKind.Relative),
            new Uri("resource/buildings/building3.png", UriKind.Relative)
        });

        List<Uri> Streets = new List<Uri>(new Uri[] {
            new Uri("resource/streets/street-h.png", UriKind.Relative)
        });

        List<Uri> Empties = new List<Uri>(new Uri[] {
            new Uri("resource/empties/empty1.png", UriKind.Relative),
            new Uri("resource/empties/empty2.png", UriKind.Relative)
        });


        public void LoadAllTextures(Grid board)
        {
            for (int i = 0; i < Map[0].Count; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < Map.Count; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < Map[0].Count; y++)
            {
                for (int x = 0; x < Map.Count; x++)
                {
                    Image image = LoadOneTexture(Map[y][x]);
                    board.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                }
            }
        }

        private Image LoadOneTexture(string v)
        {
            Image image = new Image();
            switch (v)
            {
                case "S":
                    image.Source = getStreetTexture();
                    break;
                case "B":
                    image.Source = getBuildingTexture();
                    break;
                case "E":
                    image.Source = getEmptyTexture();
                    break;
                default:
                    break;
            }
            return image;
        }

        private ImageSource getEmptyTexture()
        {
            return new BitmapImage(Empties[random.Next(Empties.Count)]);
        }

        private ImageSource getStreetTexture()
        {
            return new BitmapImage(Streets[random.Next(Streets.Count)]);
        }

        private ImageSource getBuildingTexture()
        {
            return new BitmapImage(Buildings[random.Next(Buildings.Count)]);
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
                        AddElement(field, i);
                    }
                    i++;
                }
            }
            return Map;
        }

        private void AddElement(string field, int i)
        {
            switch (field)
            {
                case "S":
                    App.ElectionVM.AddStreet(i);
                    break;
                case "B":
                    App.ElectionVM.AddBuilding(i);
                    break;
                case "E":
                    App.ElectionVM.AddEmpty(i);
                    break;
                default:
                    break;
            }
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
    }
}
