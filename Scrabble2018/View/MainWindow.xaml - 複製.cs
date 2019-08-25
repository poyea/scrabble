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
    public partial class MainWindow : Window
    {
        Game game;
        List<Button> RackTileButtons;
        int PlayerNow;
        Button[,] BoardButtons = new Button[15,15];
        char[,] BoardCharView = new char[15,15];
        List<Button> ListSwapRackButton = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();
            TextWindow tw = new TextWindow();
            tw.Show();
            ListSwapRackButton = new List<Button>();
            RackTileButtons = new List<Button>();
            // Adding board buttons
            for (int i = 0; i < 15; ++i) 
            {
                for (int j = 0; j < 15; ++j)
                {
                    Button b = new Button();
                    b.Click += Copier;
                    b.FontSize = 33;
                    BoardGrid.Children.Add(b);
                    b.Content = '\0';
                    b.Background = BoardTiles.DetermineColor(i, j);
                    BoardButtons[i,j] = b;
                    //Binding bind = new Binding();
                    //Console.Write("\"BTD{0}{1}\",", (char)(i + 65), j.ToString());
                    //bind.Path = new PropertyPath("BTD{0}{1}", (char)(i + 65), j.ToString());
                    //bind.Mode = BindingMode.OneWay;
                    //bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    //BindingOperations.SetBinding(b, Button.ContentProperty, bind);
                    //b.SetBinding(Button.CommandProperty, new Binding("ClickBoardTile"));
                }
            }
            // Adding rack buttons
            for (int i = 0; i < 7; ++i)
            {
                Button t = new Button();
                t.Click += Poster;
                t.FontSize = 33;
                HandGrid.Children.Add(t);
                t.Background = Brushes.Chocolate;
                t.Content = '\0';
                RackTileButtons.Add(t);
                //bind bind = new Binding();
                //bind.Path = new PropertyPath("RackTileDisplay");
                //bind.Mode = BindingMode.OneWay;
                //t.SetBinding(Button.CommandProperty, new Binding("ClickRackTile"));
                //bind.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                //BindingOperations.SetBinding(t, Button.ContentProperty, bind);
            }
            game = new Game();
            LogBoardWriter(Welcome.WelcomeText);
            ValidateButton.IsEnabled = false;
            StorageLbl.Content = '\0';
        }

        Button LastButton;
        private void Poster(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) // just to be on the safe side
                return;
            if(LastButton == SwapButton)
            {
                ListSwapRackButton.Add(clickedButton);
                clickedButton.IsEnabled = false;
            }
            else
            {
                if (Convert.ToChar(StorageLbl.Content) == '\0' || LastButton==null)
                {
                    StorageLbl.Content = clickedButton.Content;
                    LastButton = clickedButton;
                    clickedButton.IsEnabled = false;
                }
                else
                {
                    LastButton.IsEnabled = true;
                    StorageLbl.Content = clickedButton.Content;
                    LastButton = clickedButton;
                    clickedButton.IsEnabled = false;
                }
            }

                
        }

        private void Copier(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) // just to be on the safe side
                return;
            if (Convert.ToChar(clickedButton.Content) != '\0')
                return;
            clickedButton.Content = StorageLbl.Content;
            StorageLbl.Content = '\0';

            for(int i = 0; i< BoardButtons.GetLength(0) && Convert.ToChar(clickedButton.Content) != '\0'; ++i)
            {
                for (int j = 0; j < BoardButtons.GetLength(1); ++j)
                {
                    if (BoardButtons[i, j] == clickedButton)
                    {
                        if ((char)BoardButtons[i, j].Content == '-')
                        {
                            BlankTileForm bf = new BlankTileForm();
                            if (bf.ShowDialog() == true)
                            {
                                BoardButtons[i, j].Content = bf.List.SelectedItem;
                            }
                        }
                        BoardCharView[i, j] = (char)BoardButtons[i, j].Content;
                        game.movementRecorder.Record(i,j);
                        Console.WriteLine("{0},{1}",i,j);
                    }
                }

            }
        }

        private void ModeStartToPlay()
        {
            StartButton.Visibility = Visibility.Hidden;
            NumLbl.Visibility = Visibility.Hidden;
            NumBox.Visibility = Visibility.Hidden;
            ValidateButton.Visibility = Visibility.Visible;
            PassButton.Visibility = Visibility.Visible;
            SwapButton.Visibility = Visibility.Visible;
            ReloadButton.Visibility = Visibility.Visible;
        }
        private void ModePlayToStart()
        {
            StartButton.Visibility = Visibility.Visible;
            StartButton.IsEnabled = true;
            NumLbl.IsEnabled = true;
            NumLbl.Visibility = Visibility.Visible;
            NumBox.Visibility = Visibility.Visible;
            ValidateButton.Visibility = Visibility.Hidden;
            PassButton.Visibility = Visibility.Hidden;
            SwapButton.Visibility = Visibility.Hidden;
            ReloadButton.Visibility = Visibility.Hidden;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            NumBox.IsEnabled=!game.CanStart(NumBox.Text);
            if (game.CanStart(NumBox.Text))
            {
                StartButton.IsEnabled = false;
                ValidateButton.IsEnabled = true;
                ModeStartToPlay();
                LogBoardWriter("Game starts...");
                LogBoardWriter("This is a " + NumBox.Text + " players game.");
                LogBoardWriter(game.Start());
                PlayerNow = game.NextPlayer();

                UpdatePlayerInfoLbl(PlayerNow);
                for(int i = 0; i < RackTileButtons.Count; ++i)
                {
                    char c = game.gs.ListOfPlayers[PlayerNow].PlayingTiles[i].TileChar;
                    RackTileButtons[i].Content = c;
                    RackTileButtons[i].IsEnabled = true;
                }
            }
            else
            {
                LogBoardWriter("Please input a valid number! You entered " + NumBox.Text + ".");
            }
        }

        private void UpdatePlayerInfoLbl(int p)
        {
            PlayerInfoLbl.Content = "Player "+(p+1)+"'s turn.";
        }

        private void LoadBoardView()
        {
            for (int i = 0; i < BoardButtons.GetLength(0); ++i)
            {
                for (int j = 0; j < BoardButtons.GetLength(1); ++j)
                {
                    BoardButtons[i, j].Content = game.gs.BoardChar[i, j];
                    BoardCharView[i,j] = game.gs.BoardChar[i, j];
                    BoardButtons[i,j].Background = game.UpdateColor(i,j);
                    Console.Write(BoardCharView[i, j]);
                }
                Console.WriteLine();
                //Console.WriteLine();
            }
        }

        private void LogBoardWriter(string s)
        {
            //Console.WriteLine(LogBoard.Text);
            LogBoard.Text = s + "\n" + LogBoard.Text;
        }

        private void GetNewTiles()
        {
            List<char> LoC = new List<char>();
            foreach(Button b in RackTileButtons)
            {
                if (b.IsEnabled == false)
                {
                    LoC.Add((char)b.Content);
                }
            }
            game.GetNewTiles(LoC, PlayerNow);
        }

        private void Retry()
        {
            foreach (Button b in RackTileButtons)
            {
                if (b.IsEnabled == false)
                {
                    b.IsEnabled = true;
                }
            }
            game.ClearMovement();
        }

        private void LoadRackView()
        {
            foreach(Button b in RackTileButtons)
            {
                b.IsEnabled = false;
            }
            for (int i = 0; i < game.gs.ListOfPlayers[PlayerNow].PlayingTiles.Count; ++i)
            {
                char c = game.gs.ListOfPlayers[PlayerNow].PlayingTiles[i].TileChar;
                RackTileButtons[i].Content = c;
                RackTileButtons[i].IsEnabled = true;
            }

        }

        private void ListingWords(Dictionary<string,int> dic)
        {
            string s = "Player " + (PlayerNow + 1) + " made the words: ";
            foreach(KeyValuePair<string,int> kvp in dic)
            {
                s += kvp.Key + "(" + kvp.Value + " scores) ";
            }
            LogBoardWriter(s);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            //UpdateBoard();
            if (MoveValidator.Validate(game.gs,BoardCharView,game.movementRecorder) != -1 && !game.GameEnd())
            {
                ListingWords(game.gs.CorrectWords);
                game.UpdateState(BoardCharView);
                GetNewTiles();
                LogBoardWriter(game.gs.ListOfPlayers[PlayerNow].ToString());
                PlayerNow = game.NextPlayer();
                UpdatePlayerInfoLbl(PlayerNow);
                Console.WriteLine("PlayerNow:"+PlayerNow);
                //gs.Update(bc, PlayerNow, sum);
                LoadBoardView();
                LoadRackView();
            }
            else
            {
                LogBoardWriter("Game Judge: \"You didn't score. Please try again!\"");
                LoadBoardView();
                Retry();
            }
            if (game.GameEnd())
            {
                foreach(Player p in game.gs.ListOfPlayers)
                {
                    LogBoardWriter(game.gs.ListOfPlayers[PlayerNow].ToString());
                }
                game.gs.ListOfPlayers.Sort();
                LogBoardWriter("Game Winner is Player "+(game.gs.ListOfPlayers[0].Id+1)+" with scores"+ (game.gs.ListOfPlayers[0].Score)+"!!!");
            }
        }

        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            LogBoardWriter("Player "+(PlayerNow+1)+" decides to pass the turn!");
            PlayerNow = game.NextPlayer();
            UpdatePlayerInfoLbl(PlayerNow);
            LoadBoardView();
            LoadRackView();
        }


        bool SwapMode = false;
        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            LastButton = SwapButton;
            if (!SwapMode)
            {
                SwapMode = true;
                LoadBoardView();
                LoadRackView();
                SwapButton.Content = "FINISH SWAPPING";
                ValidateButton.IsEnabled = false;
                PassButton.IsEnabled = false;
                if (game.CanSwap())
                {
                    LogBoardWriter("Select the tiles you don't want...Then press the SWAP button again.");
                }
                else
                {
                    LogBoardWriter("You can't swap tiles now becuase less than 7 tiles are left in the bag!");
                    SwapButton.IsEnabled = false;
                }
            }
            else
            {
                foreach(Button b in ListSwapRackButton)
                {
                    b.Content = game.SwapChar((char)b.Content);
                }
                LastButton = null;
                SwapMode = false;
                ValidateButton.IsEnabled = true;
                ListSwapRackButton.Clear();
                PassButton.IsEnabled = true;
                SwapButton.Content = "SWAP";
                LogBoardWriter("Player " + (PlayerNow + 1) + " finishing swapping tiles!");
                PlayerNow = game.NextPlayer();
                UpdatePlayerInfoLbl(PlayerNow);
                LoadRackView();
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBoardView();
            Retry();
        }



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A)&&(e.Key <= Key.Z))
            {
                foreach(Button b in RackTileButtons)
                {
                    KeyConverter kc = new KeyConverter();
                    var str = kc.ConvertToString(e.Key);
                    if (b.Content.ToString() == str)
                    {
                        Poster(b, null);
                    }
                }
                //do some stuff here
                return;
            }
        }
    }
}
