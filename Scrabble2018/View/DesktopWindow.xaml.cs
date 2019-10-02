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
using Scrabble2018.Model.Game;


namespace Scrabble2018
{

    public partial class DesktopWindow : Window,IView
    {
        Game game;
        List<Button> RackTileButtons;
        private int ThisPlayer;
        int PlayerNow;
        Button[,] BoardButtons = new Button[15,15];
        char[,] BoardCharView = new char[15,15];
        List<Button> ListSwapRackButton = new List<Button>();
        public DesktopWindow(int P, Game g)
        {
            InitializeComponent();
            ThisPlayer = P;
            game = g;
            game.Subs(this);
            GameState.GSInstance.OnStateChanged += OnStateChanged;
            this.Title = "Player " + (P + 1)+" - ScrabbleDesktop";
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
                    b.PreviewMouseLeftButtonUp += EndDragLetter;
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
                t.PreviewMouseLeftButtonDown += StartDragLetter;
                RackTileButtons.Add(t);
            }
            LogBoardWriter(Welcome.WelcomeText);
            LogBoardWriter("Game starts...");
            LogBoardWriter("This is a " + GameState.GSInstance.NumOfPlayers + " players game.");

            foreach (KeyValuePair<int, Tile> kvp in GameStartDraw.Drawn)
            {
                LogBoardWriter("Player "+(kvp.Key+1)+" gets "+kvp.Value.TileChar+"!");
            }
            LogBoardWriter("Player "+(GameState.GSInstance.PlayerNow+1)+" first!");

            for (int i = 0; i < RackTileButtons.Count; ++i)
            {
                char c = GameState.GSInstance.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar;
                RackTileButtons[i].Content = c;
                if (GameState.GSInstance.PlayerNow == ThisPlayer) { RackTileButtons[i].IsEnabled = true; EnableAll(); }

                else { RackTileButtons[i].IsEnabled = false; DisableAll(); }
            }
            StorageLbl.Content = '\0';
        }

        private void StartDragLetter(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button 
                && button.Content is char c 
                && c != '\0')
            {
                Poster(sender, null);
            }
        }

        private void EndDragLetter(object sender, MouseButtonEventArgs e)
        {
            if (sender is Button button && LastButton != null
                && button.Content is char c
                && c == '\0')
            {
                Copier(sender, null);
            }
        }

        Button LastButton;
        private void Poster(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) // safety reason
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
            if (clickedButton == null) // safety reason
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
                        game.moveRecorder.Record(i,j);
                    }
                }

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
                }

            }
        }

        private void LogBoardWriter(string s)
        {
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
            game.GetNewTiles(LoC, ThisPlayer);
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
            DisableAll();
            this.Topmost = false;
            for (int i = 0; i < game.gs.ListOfPlayers[ThisPlayer].PlayingTiles.Count; ++i)
            {
                char c = game.gs.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar;
                RackTileButtons[i].Content = c;
            }
            if (ThisPlayer == GameState.GSInstance.PlayerNow) { EnableAll(); LogBoardWriter("Your turn!");
            }
        }


        private void ListingPrevWords()
        {
            string s = "Player " + (GameState.GSInstance.PrevPlayer+1) + " made the words: ";
            foreach(KeyValuePair<string,int> kvp in GameState.GSInstance.CorrectWords)
            {
                s += kvp.Key + "(" + kvp.Value + " scores) ";
            }
            LogBoardWriter(s);
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            //UpdateBoard();
            if (game.Validate(BoardCharView))
            {
                GetNewTiles();
                game.UpdateState(BoardCharView);
                PlayerNow = GameState.GSInstance.PlayerNow;
                UpdatePlayerInfoLbl(PlayerNow);

            }
            else
            {
                LogBoardWriter("Game Judge: \"You didn't score. Please try again!\"");
                LoadBoardView();
                Retry();
            }
        }

        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            LogBoardWriter("Player "+(PlayerNow+1)+" decides to pass the turn!");
            GameState.GSInstance.GamePass();
            UpdatePlayerInfoLbl(GameState.GSInstance.PlayerNow);
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
                SwapButton.Content = "FINISH";
                ValidateButton.IsEnabled = false;
                PassButton.IsEnabled = false;
                ReloadButton.IsEnabled = false;
                if (game.CanSwap())
                {
                    LogBoardWriter("Select the tiles you don't want...Then press the FINISH button.");
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
                game.UpdateState(null);
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            LoadBoardView();
            Retry();
        }

        private void EnableAll()
        {
            foreach (Button b in RackTileButtons)
            {
                b.IsEnabled = true;
            }
            ValidateButton.IsEnabled = true;
            PassButton.IsEnabled = true;
            SwapButton.IsEnabled = true;
            ReloadButton.IsEnabled = true;
            HelpButton.IsEnabled = true;
        }

        private void DisableAll()
        {
            foreach (Button b in RackTileButtons)
            {
                b.IsEnabled = false;
            }
            ValidateButton.IsEnabled = false;
            PassButton.IsEnabled = false;
            SwapButton.IsEnabled = false;
            ReloadButton.IsEnabled = false;
            HelpButton.IsEnabled = false;
        }

        private void DisableEverthing()
        {
            DisableAll();
            foreach(Button b in BoardGrid.Children)
            {
                b.IsEnabled = false;
            }
        }

        public void OnStateChanged()
        {
            LogBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1) + " finished his turn!");
            if (game.GameEnd())
            {
                foreach (Player p in game.gs.ListOfPlayers)
                {
                    LogBoardWriter(game.gs.ListOfPlayers[PlayerNow].ToString());
                }
                game.gs.ListOfPlayers.Sort();
                LogBoardWriter("Game Winner is Player " + (game.gs.ListOfPlayers[0].Id + 1) + " with scores" + (game.gs.ListOfPlayers[0].Score) + "!!!");
                LogBoardWriter("Close this window to restart Scrabble!");
                DisableEverthing();
                return;
            }
            //Enable all buttons
            if (GameState.GSInstance.LastAction == "play")
            {
                ListingPrevWords();
                LogBoardWriter(game.gs.ListOfPlayers[GameState.GSInstance.PrevPlayer].ToString());
            }else if (GameState.GSInstance.LastAction == "pass")
            {
                LogBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1)+ " passed!");
            }
            else if(GameState.GSInstance.LastAction == "swap")
            {
                LogBoardWriter("Player " + (GameState.GSInstance.PrevPlayer + 1)+" swapped his tiles!");
            }
            PlayerNow = GameState.GSInstance.PlayerNow;
            UpdatePlayerInfoLbl(GameState.GSInstance.PlayerNow);
            LoadBoardView();
            LoadRackView();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.A)&&(e.Key <= Key.Z) && ThisPlayer==GameState.GSInstance.PlayerNow)
            {
                foreach(Button b in RackTileButtons)
                {
                    KeyConverter kc = new KeyConverter();
                    var str = kc.ConvertToString(e.Key);
                    if (b.Content.ToString() == str && b.IsEnabled==true)
                    {
                        Poster(b, null);
                    }
                }
                //do some stuff here
                return;
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow hw = new HelpWindow();
            hw.ShowDialog();
        }
    }
}
