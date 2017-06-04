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
    class TextureLoader
    {
        struct Accesses
        {
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
            new Uri("resource/streets/turns/turn-up-right.png", UriKind.Relative)
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

        List<Uri> PublicPlaces = new List<Uri>(new Uri[] {
            new Uri("resource/public-places/public-place1.png", UriKind.Relative),
            new Uri("resource/public-places/public-place2.png", UriKind.Relative)
        });

        public ElectionViewModel electionViewModel;

        public TextureLoader(ElectionViewModel electionViewModel)
        {
            this.electionViewModel = electionViewModel;
        }

        public void LoadFirstTextures(Grid board)
        {
            for (int i = 0; i < electionViewModel.Areas[0].Count; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < electionViewModel.Areas.Count; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < electionViewModel.Areas[0].Count; y++)
            {
                map.Add(new List<Image>());
                for (int x = 0; x < electionViewModel.Areas.Count; x++)
                {
                    Image image = LoadOneTexture(electionViewModel.Areas[y][x]);
                    board.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                    map[y].Add(image);
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
            else if (a is PublicPlace)
                image.Source = getPublicPlaceTexture();
            else if (a is HQ)
            {
                HQ hq = (HQ)a;
                image.Source = getHQTexture(hq.Party);
            }

            return image;
        }

        public void LoadAllTextures(Grid board)
        {
            for (int i = 0; i < electionViewModel.Areas[0].Count; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < electionViewModel.Areas.Count; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
            }

            for (int y = 0; y < electionViewModel.Areas[0].Count; y++)
            {
                for (int x = 0; x < electionViewModel.Areas.Count; x++)
                {
                    Image image = map[y][x];
                    board.Children.Add(image);
                    Grid.SetColumn(image, x);
                    Grid.SetRow(image, y);
                }
            }
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

        public BitmapImage getActivistTexture(ElectionCharacter character)
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


        private ImageSource getPublicPlaceTexture()
        {
            return new BitmapImage(PublicPlaces[random.Next(PublicPlaces.Count)]);
        }

        private ImageSource getStreetTexture(AbstractArea a)
        {
            Accesses accesses = generateAccesses(a);
            switch (accesses.getCount())
            {
                case 4:
                    return new BitmapImage(Streets[0]);
                case 3:
                    return getTricrosses(accesses);
                case 2:
                    return getTurn(accesses);
                case 1:
                    if (accesses.left == true || accesses.right == true)
                        return new BitmapImage(Streets[1]);
                    return new BitmapImage(Streets[2]);
            }
            return null;
        }

        private ImageSource getTurn(Accesses accesses)
        {
            if (accesses.bottom == true && accesses.left == true)
                return new BitmapImage(Turns[0]);
            if (accesses.bottom == true && accesses.right == true)
                return new BitmapImage(Turns[1]);
            if (accesses.top == true && accesses.left == true)
                return new BitmapImage(Turns[2]);
            if (accesses.top == true && accesses.right == true)
                return new BitmapImage(Turns[3]);
            if (accesses.top == true && accesses.bottom == true)
                return new BitmapImage(Streets[2]);
            if (accesses.left == true && accesses.right == true)
                return new BitmapImage(Streets[1]);
            return null;
        }

        private ImageSource getTricrosses(Accesses accesses)
        {
            if (accesses.top == false)
                return new BitmapImage(Tricrosses[0]);
            if (accesses.right == false)
                return new BitmapImage(Tricrosses[1]);
            if (accesses.bottom == false)
                return new BitmapImage(Tricrosses[2]);
            if (accesses.left == false)
                return new BitmapImage(Tricrosses[3]);
            return null;
        }

        private Accesses generateAccesses(AbstractArea a)
        {
            Accesses accesses = new Accesses();
            foreach (ElectionAccess access in a.Accesses)
            {
                AbstractArea first = (AbstractArea)access.FirstArea;
                AbstractArea end = (AbstractArea)access.EndArea;
                if (first is Street && end is Street)
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
