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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ElectionVM;
        }

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
            ElectionInitializer electionInitializer = new ElectionInitializer();
            electionInitializer.LoadAllTextures(Board);
            App.ElectionVM.DimensionX = Board.ColumnDefinitions.Count;
            App.ElectionVM.DimensionY = Board.RowDefinitions.Count;
        }
    }
}
