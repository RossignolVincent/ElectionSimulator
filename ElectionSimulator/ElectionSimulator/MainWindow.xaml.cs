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
using System.Globalization;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();
        TextureLoader tl = new TextureLoader(App.ElectionVM);
        Dictionary<Journalist, BitmapImage> journalistImages;

        List<Label> DetailsLabels = new List<Label>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.ElectionVM;
            dt.Tick += Draw_tick;
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.ElectionVM.RefreshRate);
            journalistImages = new Dictionary<Journalist, BitmapImage>();
            initDetailsLabels();
            
        }

        private void initDetailsLabels()
        {
            DetailsLabels.Add(PositionLabel);
            DetailsLabels.Add(TypeAreaLabel);
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
                tl.LoadFirstTextures(Board);
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

            tl.LoadAllTextures(Board);

            foreach (ElectionCharacter character in App.ElectionVM.Characters)
            {
                Image characterImage = new Image();
                BitmapImage characterSource;
                if (character is Journalist)
                {
                    if (journalistImages.TryGetValue((Journalist) character, out characterSource))
                    {
                        characterImage.Source = characterSource;
                    } else
                    {
                        characterSource = tl.getCharacterTexture(character);
                        journalistImages.Add((Journalist)character, characterSource);
                    }
                } else
                {
                    characterSource = tl.getCharacterTexture(character); 
                }
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

            EventLabel.Visibility = Visibility.Visible;

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

        private void ShowDetails(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // for double-click, remove this condition if only want single click
            {
                var point = Mouse.GetPosition(Board);

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in Board.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in Board.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

                printDetails(row, col);
                setDetailsLabelsVisible();
            }
        }

        private void setDetailsLabelsVisible()
        {
            foreach(Label label in DetailsLabels)
            {
                label.Visibility = Visibility.Visible;
            }
        }

        struct SimpleOpinion
        {
            public String Name { get; set; }
            public double Value { get; set; }

            public SimpleOpinion (String name, double value)
            {
                Name = name;
                Value = value;
            }
        }

        private void printDetails(int row, int col)
        {
            AbstractArea area = App.ElectionVM.Areas[row][col];

            clearLists();

            Position.Content = row + ", " + col;

            if(area is HQ)
            {
                TypeArea.Content = area.GetType().Name+" "+ ((HQ)area).Party.Name;
            } else
            {
                TypeArea.Content = area.GetType().Name;
            }
            
            Characters.ItemsSource = area.Characters;
            Characters.Visibility = Visibility.Visible;

            Opinions.Visibility = Visibility.Hidden;
            List<SimpleOpinion> simpleOpinions = new List<SimpleOpinion>();
            if(area is AbstractElectionArea)
            {
                AbstractElectionArea electionArea = (AbstractElectionArea) area;
                foreach (KeyValuePair<PoliticalParty, double> opinion in electionArea.opinion.opinionList)
                {
                    simpleOpinions.Add(new SimpleOpinion(opinion.Key.Name, opinion.Value));
                }
                Opinions.ItemsSource = simpleOpinions;
                Opinions.Visibility = Visibility.Visible;
            }
        }

        private void clearLists()
        {
            Characters.ItemsSource = null;
            Characters.Items.Clear();
            Opinions.ItemsSource = null;
            Opinions.Items.Clear();
        }
    }
}
