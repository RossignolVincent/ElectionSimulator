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

        public ElectionViewModel()
        {
            DimensionX = 20;
            DimensionY = 20;
            Areas = new List<List<AbstractArea>>();
            Characters = new List<ElectionCharacter>();
            Activist activist = new Activist("activist1", new Position(1, 1), new ElectionLibrary.PoliticalParty("FI"));
            Characters.Add(activist);
        }

        internal void GenerateAccess()
        {
            for(int y = 0; y < Areas.Count; y++)
            {
                for(int x = 0; x < Areas[y].Count; x++)
                {
                    AbstractArea current = Areas[y][x];
                    ElectionAccess access;

                    // Check adding access to left
                    if(x > 0)
                    {
                        access = CheckAccess(current, Areas[y][x - 1]);
                        if(access != null)
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
            Console.Write(i);
            Areas[i].Add(factory.CreateBuilding(new Opinion(list), new Position(Areas[i].Count, i)));
        }

        internal void AddEmpty(int i)
        {
            Areas[i].Add(factory.CreateEmptyArea(new Position(Areas[i].Count, i)));
        }
    }
}
