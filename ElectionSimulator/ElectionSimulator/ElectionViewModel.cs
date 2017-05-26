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

        public List<List<AbstractArea>> Areas { get; set; }

        public List<ElectionCharacter> Characters { get; set; } 

        public ElectionViewModel()
        {
            DimensionX = 20;
            DimensionY = 20;
            Areas = new List<List<AbstractArea>>();
            Characters = new List<ElectionCharacter>();
            Activist activist = new Activist("activist1", new ActivistBehavior(), new Position(1, 1), new ElectionLibrary.PoliticalParty("FI"));
            Characters.Add(activist);
        }

        internal void NextTurn()
        {
            foreach (ElectionCharacter character in Characters)
            {
                character.NextTurn(Areas[character.position.X][character.position.Y]);
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
            
        }

        internal void AddBuilding(int i)
        {
         
        }

        internal void AddEmpty(int i)
        {
            
        }
    }
}
