using System.Windows;
using System.Windows.Controls;
using Scrabble2018.View;
using Scrabble2018.Model;
using Scrabble2018.Controller;

namespace Scrabble2018
{
    /*
     * Author: https://github.com/poyea
     * Repo: https://github.com/poyea/scrabble
     * April 2019
     * 
     */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int cnt = 0;
            foreach (ComboBox c in Interfaces.Children)
            {
                ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
                if (ci != null && ci.ToString() != "") cnt++;
            }
            if (cnt >= 2)
            {
                GameState.GSInstance.Initialise(cnt);
                int P = 0;
                Game g = new Game(); // Controller
                foreach (ComboBox c in Interfaces.Children)
                {
                    ComboBoxItem ci = c.SelectedItem as ComboBoxItem;
                    if (ci == null) continue;
                    if (ci.Content.ToString() == "Desktop")
                    {
                        DesktopWindow dw = new DesktopWindow(P, g);
                        dw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Text")
                    {
                        TextWindow tw = new TextWindow(P, g);
                        tw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Mobile")
                    {
                        MobileWindow mw = new MobileWindow(P, g);
                        mw.Show();
                        P++;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("You need more friends to start Scrabble!!!", "Find friends!");
            }
        }


        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow ab = new AboutWindow();
            ab.ShowDialog();
        }


    }
}
