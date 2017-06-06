using ElectionLibrary.Event;
using ElectionLibrary.Parties;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ElectionSimulator
{
    /// <summary>
    /// Logique d'interaction pour ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(Poll poll)
        {
            InitializeComponent();

            for (int i = 0; i < poll.Result.opinionList.Count; i++)
            {
                Result.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 2; i++)
            {
                Result.ColumnDefinitions.Add(new ColumnDefinition());
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
                Result.Children.Add(partyName);
                Result.Children.Add(percent);
                Grid.SetRow(partyName, j);
                Grid.SetRow(percent, j);
                Grid.SetColumn(partyName, 0);
                Grid.SetColumn(percent, 1);
                j++;
            }
        }

        private void ValidateResult(object sender, RoutedEventArgs e)
        {
            Close();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
