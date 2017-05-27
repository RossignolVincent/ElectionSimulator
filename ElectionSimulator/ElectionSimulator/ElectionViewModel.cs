using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ElectionLibrary.Environment;
using ElectionLibrary.Character;
using ElectionLibrary.Character.Behavior;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ElectionLibrary.Factory;
using ElectionLibrary;
using ElectionLibrary.Parties;

namespace ElectionSimulator
{
    public class ElectionViewModel : BaseViewModel
    {
        private string applicationTitle;

        public string ApplicationTitle
        {
            get
            {
                return applicationTitle;
            }
            set
            {
                applicationTitle = value;
                OnPropertyChanged("ApplicationTitle");
            }
        }

        public Boolean Running { get; set; }

        public int DimensionX;

        public int DimensionY;

        public int RefreshRate { get; set; }

        public ElectionFactory factory = new ElectionFactory(); 

        public List<List<AbstractArea>> Areas { get; set; }

        public List<ElectionCharacter> Characters { get; set; }

        public List<PoliticalParty> Parties { get; set; }

        public ElectionViewModel()
        {
            DimensionX = 20;
            DimensionY = 20;
            RefreshRate = 500;
            Areas = new List<List<AbstractArea>>();
            Characters = new List<ElectionCharacter>();
            Parties = new List<PoliticalParty>();
            //Activist activist = new Activist("activist1", new Position(1, 1), new PoliticalParty("FI"));
            //Characters.Add(activist);
        }

        internal void GenerateCharacters()
        {
            foreach(PoliticalParty party in Parties)
            {
                Position HQPosition = findHQ(party);
                for(int i = 0; i < 4; i++)
                {
                    Activist activist = (Activist)factory.CreateActivist(HQPosition, party);
                    Characters.Add(activist);
                }
            }
        }

        private Position findHQ(PoliticalParty party)
        {
            foreach (List<AbstractArea> areaList in Areas)
            {
                foreach (AbstractArea area in areaList)
                {
                    if(area is HQ)
                    {
                        HQ hq = (HQ)area;
                        if (hq.Party == party)
                            return hq.position;
                    }
                }
            }

            return null;
        }

        internal void GenerateAccessAndHQs()
        {
            GenerateHQs();
            GenerateAccess();
        }

        private void GenerateHQs()
        {
            List<Building> buildings = new List<Building>();
            foreach (List<AbstractArea> areaList in Areas)
            {
                foreach (AbstractArea area in areaList)
                {
                    if(area is Building)
                        buildings.Add((Building) area);
                }
            }

            foreach (PoliticalParty party in Parties)
            {
                Position HQPosition = buildings[MainWindow.random.Next(buildings.Count)].position;
                Areas[HQPosition.Y][HQPosition.X] = factory.CreateHQ(HQPosition, party);
            }
        }

        internal void GenerateAccess()
        {
            for (int y = 0; y < Areas.Count; y++)
            {
                for (int x = 0; x < Areas[y].Count; x++)
                {
                    AbstractArea current = Areas[y][x];
                    ElectionAccess access;

                    // Check adding access to left
                    if (x > 0)
                    {
                        access = CheckAccess(current, Areas[y][x - 1]);
                        if (access != null)
                        {
                            current.AddAccess(access);
                        }
                    }

                    // Check adding access to right
                    if (x + 1 < Areas[y].Count)
                    {
                        access = CheckAccess(current, Areas[y][x + 1]);
                        if (access != null)
                        {
                            current.AddAccess(access);
                        }
                    }

                    // Check adding access to top
                    if (y > 0)
                    {
                        access = CheckAccess(current, Areas[y - 1][x]);
                        if (access != null)
                        {
                            current.AddAccess(access);
                        }
                    }

                    // Check adding access to bottom
                    if (y + 1 < Areas.Count)
                    {
                        access = CheckAccess(current, Areas[y + 1][x]);
                        if (access != null)
                        {
                            current.AddAccess(access);
                        }
                    }
                }
            }
        }

        internal ElectionAccess CheckAccess(AbstractArea a, AbstractArea b)
        {
            if ((a is AbstractElectionArea && b is Street) ||
                (a is Street && b is AbstractElectionArea) ||
                (a is Street && b is Street) ||
                (a is HQ && b is Street) ||
                (a is Street && b is HQ))
            {
                return new ElectionAccess(a, b);
            }
            return null;
        }

        internal void NextTurn()
        {
            foreach (ElectionCharacter character in Characters)
            {
                character.NextTurn(Areas[character.position.Y][character.position.X]);
            }
        }

        internal void Play()
        {
            Running = true;
            while (Running)
            {
                Thread.Sleep(RefreshRate);
                NextTurn();
            }
        }

        internal void Stop()
        {
            Running = false;
        }

        internal void AddStreet(int i)
        {
            Areas[i].Add(factory.CreateStreet(new Position(Areas[i].Count, i)));
        }

        internal void AddBuilding(int i)
        {
            List<PoliticalParty> list = new List<PoliticalParty>();
            Areas[i].Add(factory.CreateBuilding(new Opinion(list), new Position(Areas[i].Count, i)));
        }

        internal void AddEmpty(int i)
        {
            Areas[i].Add(factory.CreateEmptyArea(new Position(Areas[i].Count, i)));
        }
    }
}
