using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2018.Model.Word
{
    public static class MoveValidator
    {
        // Utility class to validate moves
        public static bool CorrectMove(MoveRecorder movement, GameState gs)
        {
            bool VNotChange = true, HNotChange = true;
            bool isConsecutive = true;
            List<int> tempIndex = new List<int>();
            if( gs.FirstMove && !movement.Moves.Contains(new Tuple<int, int>(7, 7)) ) return false;
            if( movement.Moves.Count > 1 )
            {
                Tuple<int, int> reff = movement.Moves[0];
                foreach( Tuple<int, int> t in movement.Moves )
                {
                    if( reff.Item1 != t.Item1 ) HNotChange = false;
                    if( reff.Item2 != t.Item2 ) VNotChange = false;
                }
                if( HNotChange )
                {
                    movement.Fixed = movement.Moves[0].Item1;
                    movement.Direction = "H";
                    foreach( Tuple<int, int> t in movement.Moves )
                    {
                        movement.Index.Add(t.Item2);
                        tempIndex.Add(t.Item2);
                    }
                    movement.Index.Sort();
                    for( int i = movement.Index[0] ; i < gs.BoardChar.GetLength(0) ; i++ )
                    {
                        if( gs.BoardChar[movement.Fixed, i] != '\0' )
                        {
                            tempIndex.Add(i);
                        }
                    }
                }
                else if( VNotChange )
                {
                    movement.Fixed = movement.Moves[0].Item2;
                    movement.Direction = "V";
                    foreach( Tuple<int, int> t in movement.Moves )
                    {
                        movement.Index.Add(t.Item1);
                        tempIndex.Add(t.Item1);
                    }
                    movement.Index.Sort();
                    for( int i = movement.Index[0] ; i < gs.BoardChar.GetLength(1) ; i++ )
                    {
                        if( gs.BoardChar[i, movement.Fixed] != '\0' )
                        {
                            tempIndex.Add(i);
                        }
                    }
                }
            }
            else if( movement.Moves.Count == 1 )
            {
                movement.Direction = "N";
            }
            if( movement.Index.Count > 0 )
            {
                tempIndex.Sort();

                isConsecutive = !tempIndex.Select((i, j) => i - j).Distinct().Skip(1).Any();
            }

            return ( VNotChange || HNotChange ) && isConsecutive;
        }

        public static int Validate(GameState gs, char[,] bc, MoveRecorder movement)
        {
            gs.CorrectWords.Clear();
            if( !CorrectMove(movement, gs) )
            {
                gs.boardTiles.CleanVisited();
                movement.Reset();
                return -1;
            }
            /* 0. Check wrong placement (above)
              * 1. We check if any element has been changed
              * 2. If yes we go through the validate process
              */
            char[,] Tempbc = gs.BoardChar;
            int sum = 0;
            bool BoardChanged = false;
            bool HasInvalidWord = false;


            for( int i = 0 ; i < Tempbc.GetLength(0) ; ++i )
            {
                for( int j = 0 ; j < Tempbc.GetLength(1) ; ++j )
                {
                    if( bc[i, j] != gs.BoardChar[i, j] )
                        BoardChanged = true;
                }
            }
            if( !BoardChanged )
            {
                gs.boardTiles.CleanVisited();
                movement.Reset();
                return -1;
            }
            else
            {
                if( movement.Direction == "V" )
                {
                    int col = movement.Fixed;
                    int rowTop = 0;
                    /* move to the top and collect */
                    for( rowTop = movement.Index[0] ; rowTop > 0 ; rowTop-- )
                    {
                        if( gs.BoardChar[rowTop - 1, col] != '\0' ) continue;
                        else break;
                    }
                    int t = WordCollector.VCollect(rowTop, col, bc, gs);
                    if( t == -1 )
                    {
                        HasInvalidWord = true;
                    }
                    else if( t > 0 )
                    {
                        sum += t;
                    }
                    /* foreach current move, expand to the left and collect*/
                    foreach( int row in movement.Index )
                    {
                        int colLeft = 0;
                        for( colLeft = movement.Fixed ; colLeft > 0 ; colLeft-- )
                        {
                            if( gs.BoardChar[row, colLeft - 1] != '\0' ) continue;
                            else break;
                        }
                        t = WordCollector.HCollect(row, colLeft, bc, gs);
                        if( t == -1 )
                        {
                            HasInvalidWord = true;
                        }
                        else if( t > 0 )
                        {
                            sum += t;
                        }
                    }
                }
                else if( movement.Direction == "H" )
                {
                    int row = movement.Fixed;
                    int colLeft = 0;
                    /* move to the left and collect */
                    for( colLeft = movement.Index[0] ; colLeft > 0 ; colLeft-- )
                    {
                        if( gs.BoardChar[row, colLeft - 1] != '\0' ) continue;
                        else break;
                    }
                    int t = WordCollector.HCollect(row, colLeft, bc, gs);
                    if( t == -1 )
                    {
                        HasInvalidWord = true;
                    }
                    else if( t > 0 )
                    {
                        sum += t;
                    }
                    /* foreach current move, expand to the top and collect*/
                    foreach( int col in movement.Index )
                    {
                        int rowTop = 0;
                        for( rowTop = movement.Fixed ; rowTop > 0 ; colLeft-- )
                        {
                            if( gs.BoardChar[rowTop - 1, col] != '\0' ) continue;
                            else break;
                        }
                        t = WordCollector.VCollect(rowTop, col, bc, gs);
                        if( t == -1 )
                        {
                            HasInvalidWord = true;
                        }
                        else if( t > 0 )
                        {
                            sum += t;
                        }
                    }
                }
                else if( movement.Direction == "N" )
                {
                    int colLeft = 0;
                    int rowTop = 0;
                    int SoleRow = movement.Moves[0].Item1;
                    int SoleCol = movement.Moves[0].Item2;
                    /* move to the left and collect */
                    for( colLeft = SoleCol ; colLeft > 0 ; colLeft-- )
                    {
                        if( gs.BoardChar[SoleRow, colLeft - 1] != '\0' ) continue;
                        else break;
                    }
                    int t = WordCollector.HCollect(SoleRow, colLeft, bc, gs);
                    if( t == -1 )
                    {
                        HasInvalidWord = true;
                    }
                    else if( t > 0 )
                    {
                        sum += t;
                    }
                    /* move to the top and collect */
                    for( rowTop = SoleRow ; rowTop > 0 ; rowTop-- )
                    {
                        if( gs.BoardChar[rowTop - 1, SoleCol] != '\0' ) continue;
                        else break;
                    }
                    t = WordCollector.VCollect(rowTop, SoleCol, bc, gs);
                    if( t == -1 )
                    {
                        HasInvalidWord = true;
                    }
                    else if( t > 0 )
                    {
                        sum += t;
                    }
                }
            }
            movement.Reset();
            if( ( sum == 0 && BoardChanged ) || HasInvalidWord ) { gs.boardTiles.CleanVisited(); movement.Reset(); return -1; }
            gs.PlayerCountingScore = sum;
            gs.boardTiles.ApplyVisited();
            return sum;
        }
    }
}
