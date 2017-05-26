using ElectionLibrary.Character;
using ElectionLibrary.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        static Random random = new Random();
        List<List<Image>> map = new List<List<Image>>();

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

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ElectionVM;
            dt.Tick += Draw_tick;
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.ElectionVM.RefreshRate);
        }

        private void InitBoard()
        {
            Board.ColumnDefinitions.Clear();
            Board.RowDefinitions.Clear();
            Board.Children.Clear();
        }

        private void StartNewSimulation(object sender, RoutedEventArgs e)
        {
            ElectionInitializer electionInitializer = new ElectionInitializer();
            LoadFirstTextures(Board);
            App.ElectionVM.DimensionX = Board.ColumnDefinitions.Count;
            App.ElectionVM.DimensionY = Board.RowDefinitions.Count;
            RefreshBoard();
        }

        private void PlaySimulation(object sender, RoutedEventArgs e)
        {
            sw.Start();
            dt.Start();
            Thread t = new Thread(App.ElectionVM.Play);
            t.Start();
        }

        private void Draw_tick(object sender, EventArgs e)
        {
            if (sw.IsRunning)
                RefreshBoard();
        }

        private void PauseSimulation(object sender, RoutedEventArgs e)
        {
            App.ElectionVM.Stop();
            if (sw.IsRunning)
                sw.Stop();
        }

        private void NextTurn(object sender, RoutedEventArgs e)
        {
            App.ElectionVM.NextTurn();
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            InitBoard();

            LoadAllTextures(Board);

            foreach (ElectionCharacter character in App.ElectionVM.Characters)
            {
                Image activist = new Image();
                BitmapImage activistSource = new BitmapImage(new Uri("resource/characters/activists/activistfi.png", UriKind.Relative));
                activist.Source = activistSource;
                Board.Children.Add(activist);
                Grid.SetColumn(activist, character.position.X);
                Grid.SetRow(activist, character.position.Y);
            }
        }

        public void LoadFirstTextures(Grid board)
        {
            for (int i = 0; i < App.ElectionVM.Areas[0].Count; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < App.ElectionVM.Areas.Count; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < App.ElectionVM.Areas[0].Count; y++)
            {
                map.Add(new List<Image>());
                for (int x = 0; x < App.ElectionVM.Areas.Count; x++)
                {
                    Image image = LoadOneTexture(App.ElectionVM.Areas[y][x]);
                    board.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                    map[y].Add(image);
                }
            }
        }

        public void LoadAllTextures(Grid board)
        {
            for (int i = 0; i < App.ElectionVM.Areas[0].Count; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < App.ElectionVM.Areas.Count; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < App.ElectionVM.Areas[0].Count; y++)
            {
                for (int x = 0; x < App.ElectionVM.Areas.Count; x++)
                {
                    Image image = map[y][x];
                    board.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                }
            }
        }

        private Image LoadOneTexture(AbstractArea a)
        {
            Image image = new Image();
            if (a is Street)
                image.Source = getStreetTexture();
            else if (a is Building)
                image.Source = getBuildingTexture();
            else if(a is EmptyArea)
                    image.Source = getEmptyTexture();
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

    }
}
