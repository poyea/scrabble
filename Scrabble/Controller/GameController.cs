using System;
using System.Collections.Generic;
using Scrabble.Model;
using Scrabble.Model.Word;
using Scrabble.View;
using System.Windows.Media;

namespace Scrabble.Controller
{
    public class Game
    {
        private Random rnd = new Random();
        public int PlayerFirst, PlayerNow;
        public AllTiles TilesBag;
        public bool BoardChanged;
        public GameState gs;
        public MoveRecorder moveRecorder;
        public List<char> ListSwap;

        public Game()
        {

            moveRecorder = new MoveRecorder();
            this.gs = GameState.GSInstance;
            this.ListSwap = new List<char>();
            this.BoardChanged = false;
        }


        public bool Validate(char[,] bc)
        {
            if( MoveValidator.Validate(gs, bc, moveRecorder) != -1 && !GameEnd() )
            {
                return true;
            }
            else return false;
        }

        public void Subs(IView view)
        {
            if( view != null ) gs.ListOfViews.Add(view);
        }

        public void Unsubs(IView view)
        {
            if( view != null ) gs.ListOfViews.Remove(view);
        }

        public void GetNewTiles(List<char> Loc, int num)
        {
            gs.playerManager.GetNewTiles(gs, Loc, num);
        }

        public void UpdateState(char[,] b)
        {
            gs.UpdateState(b);
        }

        public bool CanSwap()
        {
            return gs.TilesBag.ListTiles.Count >= 7;
        }

        public char SwapChar(char c)
        {
            return GameState.GSInstance.playerManager.Swap(c);
        }

        public bool GameEnd()
        {
            return GameEndVerify.GameEndScoring(gs);
        }
        public void ClearMovement()
        {
            moveRecorder.Moves.Clear();
        }

        public SolidColorBrush UpdateColor(int i, int j)
        {
            switch( gs.boardTiles.PlaceInUse[i, j] )
            {
                case TileType.WordTriple:
                    return Brushes.OrangeRed;
                case TileType.WordDouble:
                    return Brushes.Coral;
                case TileType.LetterDouble:
                    return Brushes.LightSkyBlue;
                case TileType.LetterTriple:
                    return Brushes.MediumBlue;
                case TileType.Start:
                    return Brushes.Gold;
                default:
                    return Brushes.Bisque;
            }
        }

    }

}
