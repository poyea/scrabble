using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2018.Model
{
    public class Tile : IComparable
    {
        // Tile data
        private char tileChar;

        public char TileChar
        {
            get { return tileChar; }
            set { tileChar = value; }
        }
        private int tileScore;

        public int TileScore
        {
            get { return tileScore; }
            set { tileScore = value; }
        }

        public Tile(char c, int s)
        {
            tileChar = c;
            tileScore = s;
        }

        public int CompareTo(object obj)
        {
            if( obj == null ) return 1;

            Tile OtherTile = obj as Tile;
            if( OtherTile != null )
                return this.TileChar.CompareTo(OtherTile.TileChar);
            else
                throw new ArgumentException("Tiles Comparison Exception");
        }
    }
}
