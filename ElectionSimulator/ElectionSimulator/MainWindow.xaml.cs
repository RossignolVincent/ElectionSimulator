using AbstractLibrary.Repository;
using AbstractLibrary.Repository.Appender;
using AbstractLibrary.Repository.Reader;
using AbstractLibrary.Serializer;
using ElectionLibrary.Character;
using ElectionLibrary.Environment;
using ElectionLibrary.Event;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AbstractLibrary.Object;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const String SAVE_PATH = "save.bin";
        DispatcherTimer dt = new DispatcherTimer();
        Stopwatch sw = new Stopwatch();

        TextureLoader tl = new TextureLoader(App.ElectionVM);

        List<Label> DetailsLabels = new List<Label>();


        public MainWindow()
        {
            InitializeComponent();
            ICommand detailsDelegate = new DelegateCommand(m => PrintDetails());
            DataContext = App.ElectionVM;
            App.ElectionVM.DetailsDelegate = detailsDelegate;
            dt.Tick += Draw_tick;
            dt.Interval = new TimeSpan(0, 0, 0, 0, App.ElectionVM.RefreshRate);
            Closing += App.ElectionVM.OnWindowClosing;
            InitDetailsLabels();
            
        }

        private void InitDetailsLabels()
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
            NewSimulationButton.IsEnabled = false;
            LoadSimulationButton.IsEnabled = false;
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

        private void SaveSimulation(object sender, RoutedEventArgs e)
        {
            FileAppender appender = new FileAppender(SAVE_PATH);
            FileReader reader = new FileReader(SAVE_PATH);
            BinaryFileRepository repository = new BinaryFileRepository(appender, reader);
            repository.Write(App.ElectionVM);
            Console.WriteLine("Saved");
        }

        private void LoadSimulation(object sender, RoutedEventArgs e)
        {
            FileAppender appender = new FileAppender(SAVE_PATH);
            FileReader reader = new FileReader(SAVE_PATH);
            BinaryFileRepository repository = new BinaryFileRepository(appender, reader);
            Console.WriteLine("Loaded");
            BinarySerializer serializer = new BinarySerializer();
            App.ElectionVM = (ElectionViewModel)serializer.Deserialize((byte[])repository.Read());
            tl.LoadFirstTextures(Board);
            RefreshBoard();
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
            if (App.ElectionVM.AreaSelected != null)
            {
                App.ElectionVM.AreaSelected = App.ElectionVM.Areas[App.ElectionVM.AreaSelected.Position.Y][App.ElectionVM.AreaSelected.Position.X];
            }
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            InitBoard();

            tl.LoadAllTextures(Board);

            foreach (AbstractElectionCharacter character in App.ElectionVM.Characters)
            {
                Image characterImage = new Image();
                BitmapImage characterSource;
                characterSource = tl.GetCharacterTexture(character); 
                characterImage.Source = characterSource;
                Board.Children.Add(characterImage);
                Grid.SetColumn(characterImage, character.Position.X);
                Grid.SetRow(characterImage, character.Position.Y);

            }

            foreach (List<ElectionLibrary.Environment.AbstractArea> areaList in App.ElectionVM.Areas)
            {
                foreach (ElectionLibrary.Environment.AbstractArea area in areaList)
                {
                    foreach (AbstractObject obj in area.Objects)
                    {
                        Image objectImage = new Image();
                        BitmapImage objectSource;
                        objectSource = tl.GetObjectTexture(obj);
                        objectImage.Source = objectSource;
                        Board.Children.Add(objectImage);
                        Grid.SetColumn(objectImage, area.Position.X);
                        Grid.SetRow(objectImage, area.Position.Y);
                    }        
                }
            }



            if (App.ElectionVM.Event != null)
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

            if (App.ElectionVM.Event is Poll poll)
            {
                if (poll.Type == Poll.PollType.End)
                {
                    EventLabel.Content = "Résultat du scrutin :";
                    PlaySimulationButton.IsEnabled = false;
                    PauseSimulationButton.IsEnabled = false;
                    NextTurnButton.IsEnabled = false;
                    ResultWindow resultWindow = new ResultWindow(poll);
                    resultWindow.ShowDialog();
                }
                else if (poll.Type == Poll.PollType.Poll)
                {
                    EventLabel.Content = "Résultat du sondage :";
                }

                EventLabel.Visibility = Visibility.Visible;

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
                    Label partyName = new Label()
                    {
                        Content = party.Name
                    };
                    Label percent = new Label()
                    {
                        Content = string.Format("{0:0.00}", poll.Result.opinionList[party]) + " %"
                    };
                    Event.Children.Add(partyName);
                    Event.Children.Add(percent);
                    Grid.SetRow(partyName, j);
                    Grid.SetRow(percent, j);
                    Grid.SetColumn(partyName, 0);
                    Grid.SetColumn(percent, 1);
                    j++;
                }

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

                App.ElectionVM.AreaSelected = App.ElectionVM.Areas[row][col];
                PrintDetails();
                SetDetailsLabelsVisible();
            }
        }


        private void SetDetailsLabelsVisible()
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

        private void PrintDetails()
        {
            ElectionLibrary.Environment.AbstractArea area = App.ElectionVM.Areas[App.ElectionVM.AreaSelected.Position.Y][App.ElectionVM.AreaSelected.Position.X];

            ClearLists();

            Position.Content = area.Position.Y + ", " + area.Position.X;

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
            if (area is AbstractElectionArea electionArea)
            {
                foreach (KeyValuePair<PoliticalParty, double> opinion in electionArea.opinion.opinionList)
                {
                    simpleOpinions.Add(new SimpleOpinion(opinion.Key.Name, opinion.Value));
                }
                Opinions.ItemsSource = simpleOpinions;
                Opinions.Visibility = Visibility.Visible;
            }
        }

        private void ClearLists()
        {
            Characters.ItemsSource = null;
            Characters.Items.Clear();
            Opinions.ItemsSource = null;
            Opinions.Items.Clear();
        }
    }
}
