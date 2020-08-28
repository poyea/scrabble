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
    public partial class MobileWindow : Window, IView
    {
        Game game;
        List<Button> RackTileButtons;
        private int ThisPlayer;
        int PlayerNow;
        Button[,] BoardButtons = new Button[15, 15];
        char[,] BoardCharView = new char[15, 15];
        List<Button> ListSwapRackButton = new List<Button>();
        public MobileWindow(int P, Game g)
        {
            InitializeComponent();
            ThisPlayer = P;
            game = g;
            game.Subs(this);
            GameState.GSInstance.OnStateChanged += OnStateChanged;
            this.Title = "Player " + ( P + 1 ) + " - MobileScrabble";
            ListSwapRackButton = new List<Button>();
            RackTileButtons = new List<Button>();
            // Adding board buttons
            for( int i = 0 ; i < 15 ; ++i )
            {
                for( int j = 0 ; j < 15 ; ++j )
                {
                    Button b = new Button();
                    b.Click += Copier;
                    b.FontSize = 9;
                    BoardGrid.Children.Add(b);
                    b.FontWeight = FontWeights.Bold;
                    b.FontFamily = new FontFamily("Century Gothic");
                    b.Content = '\0';
                    b.Background = BoardTiles.DetermineColor(i, j);
                    BoardButtons[i, j] = b;
                }
            }
            // Adding rack buttons
            for( int i = 0 ; i < 7 ; ++i )
            {
                Button t = new Button();
                t.Click += Poster;
                t.FontSize = 18;
                HandGrid.Children.Add(t);
                t.Background = Brushes.Chocolate;
                t.Content = '\0';
                RackTileButtons.Add(t);
            }

            for( int i = 0 ; i < RackTileButtons.Count ; ++i )
            {
                char c = GameState.GSInstance.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar;
                RackTileButtons[i].Content = c;
                if( GameState.GSInstance.PlayerNow == ThisPlayer ) { RackTileButtons[i].IsEnabled = true; EnableAll(); }

                else { RackTileButtons[i].IsEnabled = false; DisableAll(); }
            }
            StorageLbl.Content = '\0';
        }

        Button LastButton;
        private void Poster(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if( clickedButton == null ) // safety reason
                return;
            if( LastButton == SwapButton )
            {
                ListSwapRackButton.Add(clickedButton);
                clickedButton.IsEnabled = false;
            }
            else
            {
                if( Convert.ToChar(StorageLbl.Content) == '\0' || LastButton == null )
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
            if( clickedButton == null ) // safety reason
                return;
            if( Convert.ToChar(clickedButton.Content) != '\0' )
                return;
            clickedButton.Content = StorageLbl.Content;
            StorageLbl.Content = '\0';

            for( int i = 0 ; i < BoardButtons.GetLength(0) && Convert.ToChar(clickedButton.Content) != '\0' ; ++i )
            {
                for( int j = 0 ; j < BoardButtons.GetLength(1) ; ++j )
                {
                    if( BoardButtons[i, j] == clickedButton )
                    {
                        if( (char) BoardButtons[i, j].Content == '-' )
                        {
                            BlankTileForm bf = new BlankTileForm();
                            if( bf.ShowDialog() == true )
                            {
                                BoardButtons[i, j].Content = bf.List.SelectedItem;
                            }
                        }
                        BoardCharView[i, j] = (char) BoardButtons[i, j].Content;
                        game.moveRecorder.Record(i, j);
                    }
                }

            }
        }



        private void UpdatePlayerInfoLbl(int turn)
        {
            string s = "";
            if( ThisPlayer == GameState.GSInstance.PlayerNow )
            {
                PlayerInfoLbl.Content = "Your turn!";
            }
            else
            {

            }
            PlayerInfoLbl.Content = "Player now: " + ( turn + 1 );
        }


        private void WriteToLabel(string str)
        {
            if( ThisPlayer == GameState.GSInstance.PlayerNow )
            {
                PlayerInfoLbl.Content = "Your turn!";
            }
            else
            {
                string s = "";
                s += "Now:P" + ( GameState.GSInstance.PlayerNow + 1 ) + "|Score:";
                foreach( Player p in GameState.GSInstance.ListOfPlayers )
                {
                    s += "P" + p.Id + "-" + p.Score + ";";
                }
                PlayerInfoLbl.Content = s.Remove(s.Length - 1);
            }
        }


        private void LoadBoardView()
        {
            for( int i = 0 ; i < BoardButtons.GetLength(0) ; ++i )
            {
                for( int j = 0 ; j < BoardButtons.GetLength(1) ; ++j )
                {
                    BoardButtons[i, j].Content = game.gs.BoardChar[i, j];
                    BoardCharView[i, j] = game.gs.BoardChar[i, j];
                    BoardButtons[i, j].Background = game.UpdateColor(i, j);
                }
            }
        }


        private void GetNewTiles()
        {
            List<char> LoC = new List<char>();
            foreach( Button b in RackTileButtons )
            {
                if( b.IsEnabled == false )
                {
                    LoC.Add((char) b.Content);
                }
            }
            game.GetNewTiles(LoC, ThisPlayer);
        }

        private void Retry()
        {
            foreach( Button b in RackTileButtons )
            {
                if( b.IsEnabled == false )
                {
                    b.IsEnabled = true;
                }
            }
            game.ClearMovement();
        }

        private void LoadRackView()
        {
            DisableAll();
            for( int i = 0 ; i < game.gs.ListOfPlayers[ThisPlayer].PlayingTiles.Count ; ++i )
            {
                char c = game.gs.ListOfPlayers[ThisPlayer].PlayingTiles[i].TileChar;
                RackTileButtons[i].Content = c;
            }
            if( ThisPlayer == GameState.GSInstance.PlayerNow ) EnableAll();

        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
            // UpdateBoard();
            if( game.moveRecorder.Moves.Count == 0 )
            {
                PassButton_Click(sender, e);
                return;
            }
            if( game.Validate(BoardCharView) )
            {
                GetNewTiles();
                game.UpdateState(BoardCharView);
                PlayerNow = GameState.GSInstance.PlayerNow;
            }
            else
            {
                LoadBoardView();
                Retry();
            }

        }



        private void PassButton_Click(object sender, RoutedEventArgs e)
        {
            GameState.GSInstance.GamePass();
        }


        bool SwapMode = false;
        private void SwapButton_Click(object sender, RoutedEventArgs e)
        {
            LastButton = SwapButton;
            if( !SwapMode )
            {
                SwapMode = true;
                LoadBoardView();
                LoadRackView();
                SwapButton.Content = "FINISH";
                ValidateButton.IsEnabled = false;
                ReloadButton.IsEnabled = false;
                if( !game.CanSwap() )
                {
                    SwapMode = false;
                    SwapButton.IsEnabled = false;
                }
            }
            else
            {
                foreach( Button b in ListSwapRackButton )
                {
                    b.Content = game.SwapChar((char) b.Content);
                }
                LastButton = null;
                SwapMode = false;
                ValidateButton.IsEnabled = true;
                ListSwapRackButton.Clear();
                SwapButton.Content = "SWAP";
                PlayerNow = GameState.GSInstance.PlayerNow;
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
            this.Topmost = true;
            foreach( Button b in RackTileButtons )
            {
                b.IsEnabled = true;
            }
            ValidateButton.IsEnabled = true;
            SwapButton.IsEnabled = true;
            ReloadButton.IsEnabled = true;
        }

        private void DisableAll()
        {
            this.Topmost = false;
            foreach( Button b in RackTileButtons )
            {
                b.IsEnabled = false;
            }
            ValidateButton.IsEnabled = false;
            SwapButton.IsEnabled = false;
            ReloadButton.IsEnabled = false;
        }

        private void DisableEverthing()
        {
            DisableAll();
            foreach( Button b in BoardGrid.Children )
            {
                b.IsEnabled = false;
            }
        }

        public void OnStateChanged()
        {
            // Enable all buttons
            if( game.GameEnd() )
            {

                game.gs.ListOfPlayers.Sort();
                WriteToLabel("Winner is P" + ( game.gs.ListOfPlayers[0].Id + 1 ) + " with scores" + ( game.gs.ListOfPlayers[0].Score ) + "!");
                DisableEverthing();
                return;
            }
            PlayerNow = GameState.GSInstance.PlayerNow;
            UpdatePlayerInfoLbl(GameState.GSInstance.PlayerNow);
            LoadBoardView();
            LoadRackView();
            WriteToLabel(null);
        }

    }
}
