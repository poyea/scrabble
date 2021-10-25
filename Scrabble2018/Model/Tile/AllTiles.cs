using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Scrabble2018.Model
{
    public class AllTiles
    {
        public bool Empty()
        {
            if (ListTiles.Count > 0) return false;
            else return true;
        }

        public List<Tile> ListTiles;

        public AllTiles()
        {
            MakeTiles();
        }
        /*public Tiles(){
            MakeTiles();
        }*/

        public void MakeTiles()
        {
            ListTiles = new List<Tile>();
            for (char c = 'A'; c <= 'Z'; c++)
            {
                for (int x = 0; x < NumOfLetters(c); x++)
                {
                    Tile t = new Tile(c, ScoreOfLetter(c));
                    ListTiles.Add(t);
                }
            }
            Tile b = new Tile('-', 0);
            ListTiles.Add(b); // blank
            ListTiles.Add(b);
        }

        /*
         * Tiles distribution
         * 98 tiles
         * 2 blank tiles(?)
         */
        public static int ScoreOfLetter(char c)
        {
            if (c == 'E' || c == 'A' || c == 'I' || c == 'O' || c == 'N' || c == 'R' || c == 'T' || c == 'L' || c == 'S' || c == 'U') return 1;
            else if (c == 'D' || c == 'G') return 2;
            else if (c == 'B' || c == 'C' || c == 'M' || c == 'P') return 3;
            else if (c == 'F' || c == 'H' || c == 'V' || c == 'W' || c == 'Y') return 4;
            else if (c == 'K') return 5;
            else if (c == 'J' || c == 'X') return 8;
            else if (c == 'Q' || c == 'Z') return 10;
            else if (c == '-') return 0;
            else return 0;
        }

        [ExcludeFromCodeCoverage]
        private static int NumOfLetters(char c)
        {
            if (c == 'Z' || c == 'Q' || c == 'X' || c == 'J' || c == 'K') return 1;
            else if (c == 'Y' || c == 'W' || c == 'V' || c == 'F' || c == 'H' || c == 'P' || c == 'M' || c == 'C' || c == 'B') return 2;
            else if (c == 'D' || c == 'S' || c == 'U' || c == 'L') return 4;
            else if (c == 'G') return 3;
            else if (c == 'N' || c == 'R' || c == 'T') return 6;
            else if (c == 'O') return 8;
            else if (c == 'A' || c == 'I') return 9;
            else if (c == 'E') return 12;
            else return 0;
        }
    }
}
