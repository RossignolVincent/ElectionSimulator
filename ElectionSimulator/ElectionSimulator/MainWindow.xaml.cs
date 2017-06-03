using ElectionLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Event;
using ElectionLibrary.Parties;
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
        public static Random random = new Random();
        List<List<Image>> map = new List<List<Image>>();

        List<Uri> Buildings = new List<Uri>(new Uri[] {
            new Uri("resource/buildings/building1.png", UriKind.Relative),
            new Uri("resource/buildings/building2.png", UriKind.Relative),
            new Uri("resource/buildings/building3.png", UriKind.Relative)
        });

        List<Uri> Streets = new List<Uri>(new Uri[] {
            new Uri("resource/streets/street-m.png", UriKind.Relative),
            new Uri("resource/streets/street-h.png", UriKind.Relative),
            new Uri("resource/streets/street-v.png", UriKind.Relative)
        });

        List<Uri> Turns = new List<Uri>(new Uri[] {
            new Uri("resource/streets/turns/turn-down-left.png", UriKind.Relative),
            new Uri("resource/streets/turns/turn-down-right.png", UriKind.Relative),
            new Uri("resource/streets/turns/turn-up-left.png", UriKind.Relative),
            new Uri("resource/streets/turns/turn-down-right.png", UriKind.Relative)
        });

        List<Uri> Tricrosses = new List<Uri>(new Uri[] {
            new Uri("resource/streets/tricrosses/tri-left-right-down.png", UriKind.Relative),
            new Uri("resource/streets/tricrosses/tri-left-up-down.png", UriKind.Relative),
            new Uri("resource/streets/tricrosses/tri-left-up-right.png", UriKind.Relative),
            new Uri("resource/streets/tricrosses/tri-up-right-down.png", UriKind.Relative)
        });

        List<Uri> Empties = new List<Uri>(new Uri[] {
            new Uri("resource/empties/empty1.png", UriKind.Relative),
            new Uri("resource/empties/empty2.png", UriKind.Relative)
        });

        List<Uri> HQs = new List<Uri>(new Uri[] {
            new Uri("resource/hqs/hq-em.png", UriKind.Relative),
            new Uri("resource/hqs/hq-fn.png", UriKind.Relative),
            new Uri("resource/hqs/hq-fi.png", UriKind.Relative),
            new Uri("resource/hqs/hq-lr.png", UriKind.Relative)
        });

        List<Uri> Activists = new List<Uri>(new Uri[] {
            new Uri("resource/characters/activists/activist-em.png", UriKind.Relative),
            new Uri("resource/characters/activists/activist-fn.png", UriKind.Relative),
            new Uri("resource/characters/activists/activist-fi.png", UriKind.Relative),
            new Uri("resource/characters/activists/activist-lr.png", UriKind.Relative),
        });

        struct Accesses {
            public bool left;
            public bool right;
            public bool top;
            public bool bottom;

            public static implicit operator Accesses(bool boolean)
            {
                return new Accesses() { left = boolean, right = boolean, top = boolean, bottom = boolean };
            }

            public int getCount()
            {
                int count = 0;
                if (left)
                    count++;

                if (right)
                    count++;

                if (top)
                    count++;

                if (bottom)
                    count++;

                return count;
            }
        }

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
            NewSimulationWindow newSimulationWindow = new NewSimulationWindow();
            newSimulationWindow.ShowDialog();
            if (newSimulationWindow.validated)
            {
                App.ElectionVM.Parties = newSimulationWindow.Parties;
                ElectionInitializer electionInitializer = new ElectionInitializer(newSimulationWindow.MapFile, newSimulationWindow.Parties);
                App.ElectionVM.GenerateAccessAndHQs();
                LoadFirstTextures(Board);
                App.ElectionVM.DimensionX = Board.ColumnDefinitions.Count;
                App.ElectionVM.DimensionY = Board.RowDefinitions.Count;
                App.ElectionVM.GenerateCharacters();
                RefreshBoard();
            }
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
                // Add test for type of character
                Image characterImage = new Image();
                BitmapImage characterSource = getActivistTexture(character);
                characterImage.Source = characterSource;
                Board.Children.Add(characterImage);
                Grid.SetColumn(characterImage, character.position.X);
                Grid.SetRow(characterImage, character.position.Y);
            }

            if(App.ElectionVM.Event != null)
            {
                if(App.ElectionVM.Event is Poll)
                {
                    ShowEvent();
                }
                App.ElectionVM.Stop();
                if (sw.IsRunning)
                    sw.Stop();
                App.ElectionVM.Event = null;
            }
        }

        private void ShowEvent()
        {
            Event.ColumnDefinitions.Clear();
            Event.RowDefinitions.Clear();
            Event.Children.Clear();

            Poll poll = (Poll)App.ElectionVM.Event;

            for (int i = 0; i < poll.Result.opinionList.Count; i++)
            {
                Event.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 2; i++)
            {
                Event.ColumnDefinitions.Add(new ColumnDefinition());
            }

            int j = 0;
            foreach (PoliticalParty party in poll.Result.opinionList.Keys)
            {
                Label partyName = new Label();
                partyName.Content = party.Name;
                Label percent = new Label();
                percent.Content = string.Format("{0:0.00}", poll.Result.opinionList[party]) + " %";
                Event.Children.Add(partyName);
                Event.Children.Add(percent);
                Grid.SetRow(partyName, j);
                Grid.SetRow(percent, j);
                Grid.SetColumn(partyName, 0);
                Grid.SetColumn(percent, 1);
                j++;
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
                image.Source = getStreetTexture(a);
            else if (a is Building)
                image.Source = getBuildingTexture();
            else if (a is EmptyArea)
                image.Source = getEmptyTexture();
            else if (a is HQ)
            {
                HQ hq = (HQ)a;
                image.Source = getHQTexture(hq.Party);
            }
                
            return image;
        }

        private ImageSource getHQTexture(PoliticalParty party)
        {
            switch (party.Name)
            {
                case "En Marche":
                    return new BitmapImage(HQs[0]);
                case "Front National":
                    return new BitmapImage(HQs[1]);
                case "France Insoumise":
                    return new BitmapImage(HQs[2]);
                case "Les Républicains":
                    return new BitmapImage(HQs[3]);
            }
            return null;
        }

        private BitmapImage getActivistTexture(ElectionCharacter character)
        {
            Activist activist = (Activist)character;
            switch (activist.PoliticalParty.Name)
            {
                case "En Marche":
                    return new BitmapImage(Activists[0]);
                case "Front National":
                    return new BitmapImage(Activists[1]);
                case "France Insoumise":
                    return new BitmapImage(Activists[2]);
                case "Les Républicains":
                    return new BitmapImage(Activists[3]);
            }
            return null;
        }

        private ImageSource getEmptyTexture()
        {
            return new BitmapImage(Empties[random.Next(Empties.Count)]);
        }

        private ImageSource getStreetTexture(AbstractArea a)
        {
            Accesses acceses = generateAccesses(a);
            switch (acceses.getCount())
            {
                case 4:
                    return new BitmapImage(Streets[0]);
                case 3:
                    return getTricrosses(acceses);
                case 2:
                    return getTurn(acceses);
            }
            return null;
        }

        private ImageSource getTurn(Accesses acceses)
        {
            if (acceses.bottom == true && acceses.left == true)
                return new BitmapImage(Turns[0]);
            if (acceses.bottom == true && acceses.right == true)
                return new BitmapImage(Turns[1]);
            if (acceses.top == true && acceses.left == true)
                return new BitmapImage(Turns[2]);
            if (acceses.top == true && acceses.right == true)
                return new BitmapImage(Turns[3]);
            if (acceses.top == true && acceses.bottom == true)
                return new BitmapImage(Streets[2]);
            if (acceses.left == true && acceses.right == true)
                return new BitmapImage(Streets[1]);
            return null;
        }

        private ImageSource getTricrosses(Accesses acceses)
        {
            if (acceses.top == false)
                return new BitmapImage(Tricrosses[0]);
            if (acceses.right == false)
                return new BitmapImage(Tricrosses[1]);
            if (acceses.bottom == false)
                return new BitmapImage(Tricrosses[2]);
            if (acceses.left == false)
                return new BitmapImage(Tricrosses[3]);
            return null;
        }

        private Accesses generateAccesses(AbstractArea a)
        {
            Accesses accesses = new Accesses();
            foreach (ElectionAccess access in a.Accesses)
            {
                AbstractArea first = (AbstractArea) access.FirstArea;
                AbstractArea end = (AbstractArea)access.EndArea;
                if(first is Street && end is Street)
                {
                    if (first.Position.X < end.Position.X)
                        accesses.right = true;
                    if (first.Position.X > end.Position.X)
                        accesses.left = true;
                    if (first.Position.Y > end.Position.Y)
                        accesses.top = true;
                    if (first.Position.Y < end.Position.Y)
                        accesses.bottom = true;
                }
                
            }
            return accesses;
        }

        private ImageSource getBuildingTexture()
        {
            return new BitmapImage(Buildings[random.Next(Buildings.Count)]);
        }

    }
}
