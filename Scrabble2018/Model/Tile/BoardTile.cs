using System.Windows.Media;

namespace Scrabble2018
{
    public enum TileType
    {
        Default,
        LetterDouble,
        LetterTriple,
        WordDouble,
        WordTriple,
        Start
    }

    public class BoardTiles
    {
        private const TileType __ = TileType.Default;
        private const TileType DL = TileType.LetterDouble;
        private const TileType TL = TileType.LetterTriple;
        private const TileType DW = TileType.WordDouble;
        private const TileType TW = TileType.WordTriple;
        private const TileType ST = TileType.Start;

        // Reference BoardTiles and utility
        // Reference is for cancelling used colors
        public static TileType[,] Placement =
        {
            {TW,__,__,DL,__,__,__,TW,__,__,__,DL,__,__,TW},
            {__,DW,__,__,__,TL,__,__,__,TL,__,__,__,DW,__},
            {__,__,DW,__,__,__,DL,__,DL,__,__,__,DW,__,__},
            {DL,__,__,DW,__,__,__,DL,__,__,__,DW,__,__,DL},
            {__,__,__,__,DW,__,__,__,__,__,DW,__,__,__,__ },
            {__,TL,__,__,__,TL,__,__,__,TL,__,__,__,TL,__},
            {__,__,DL,__,__,__,DL,__,DL,__,__,__,DL,__,__},
            //midDLe
            {TW,__,__,DL,__,__,__,ST,__,__,__,DL,__,__,TL},
            //midDLe
            {__,__,DL,__,__,__,DL,__,DL,__,__,__,DL,__,__},
            {__,TL,__,__,__,TL,__,__,__,TL,__,__,__,TL,__},
            {__,__,__,__,DW,__,__,__,__,__,DW,__,__,__,__ },
            {DL,__,__,DW,__,__,__,DL,__,__,__,DW,__,__,DL},
            {__,__,DW,__,__,__,DL,__,DL,__,__,__,DW,__,__},
            { __,DW,__,__,__,TL,__,__,__,TL,__,__,__,DW,__},
            {TW,__,__,DL,__,__,__,TW,__,__,__,DL,__,__,TW}
        };

        public TileType[,] PlaceInUse;
        private bool[,] Visited;

        public BoardTiles()
        {
            PlaceInUse = Placement;
            Visited = new bool[15, 15];
        }

        public int WordMultiplier(int i, int j)
        {
            switch( PlaceInUse[i, j] )
            {
                case TileType.WordTriple:
                    Visited[i, j] = true;
                    return 3;
                case TileType.WordDouble:
                    Visited[i, j] = true;
                    return 2;
                default:
                    return 1;
            }
        }

        public int LetterMultiplier(int i, int j)
        {
            switch( PlaceInUse[i, j] )
            {
                case TileType.LetterTriple:
                    Visited[i, j] = true;
                    return 3;
                case TileType.LetterDouble:
                    Visited[i, j] = true;
                    return 2;
                default:
                    return 1;
            }
        }

        public void ApplyVisited()
        {
            for( int i = 0 ; i < Visited.GetLength(0) ; i++ )
            {
                for( int j = 0 ; j < Visited.GetLength(1) ; j++ )
                {
                    if( Visited[i, j] )
                    {
                        PlaceInUse[i, j] = TileType.Default;
                        Visited[i, j] = false;
                    }
                }
            }

        }

        public void CleanVisited()
        {
            for( int i = 0 ; i < Visited.GetLength(0) ; i++ )
            {
                for( int j = 0 ; j < Visited.GetLength(1) ; j++ )
                {
                    Visited[i, j] = false;
                }
            }
        }

        public static SolidColorBrush DetermineColor(int i, int j)
        {
            switch( Placement[i, j] )
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
