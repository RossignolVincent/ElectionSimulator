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
using ElectionLibrary.Environment;

namespace ElectionSimulator
{
    class ElectionInitializer
    {
        public List<List<string>> Map {get; set;}

        public ElectionInitializer()
        {
            string MapFile = GetMapFile();
            if (MapFile != null)
            {
                LoadMap(MapFile);
                App.ElectionVM.GenerateAccess();
            }
        }

        private void LoadMap(string MapFile)
        {
            using (TextFieldParser parser = new TextFieldParser(MapFile))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                int i = 0;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    App.ElectionVM.Areas.Add(new List<AbstractArea>());
                    foreach (string field in fields)
                    {
                        AddElement(field, i);
                    }
                    i++;
                }
            }
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
