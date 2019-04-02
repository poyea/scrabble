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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scrabble2018.View;
using Scrabble2018.Model;
using Scrabble2018.Controller;
using Scrabble2018.Model.Word;

namespace Scrabble2018
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    /// 

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
            if (cnt >=2)
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
                        DesktopWindow dw = new DesktopWindow(P,g);
                        dw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Text")
                    {
                        TextWindow tw = new TextWindow(P,g);
                        tw.Show();
                        P++;
                    }
                    else if (ci.Content.ToString() == "Mobile")
                    {
                        MobileWindow mw = new MobileWindow(P,g);
                        mw.Show();
                        P++;
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("You need more friends to start Scrabble!!!","Find friends!");
            }
        }


        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow ab = new AboutWindow();
            ab.ShowDialog();
        }
        

    }
}
