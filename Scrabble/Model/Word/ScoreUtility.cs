using System.Collections.Generic;

namespace Scrabble.Model.Word
{
    public static class ScoreUtility
    {
        // Utility class for scoring
        public static int ScoreCalc(int fix, int j, int jM, string direction, char[,] b, BoardTiles bt)
        {
            int sum = 0;
            List<int> WordMultiply = new List<int>();
            if (direction == "h")
            {
                for (int q = j; q <= jM; ++q)
                {
                    if (b[fix, q] != '\0')
                    {
                        sum += AllTiles.ScoreOfLetter(b[fix, q]) * bt.LetterMultiplier(fix, q);
                        if (bt.WordMultiplier(fix, q) != 1)
                        {
                            WordMultiply.Add(bt.WordMultiplier(fix, q));
                        }
                    }

                }
                foreach (int n in WordMultiply)
                    sum *= n;
            }
            else if (direction == "v")
            {
                for (int q = j; q <= jM; ++q)
                {
                    if (b[q, fix] != '\0')
                    {
                        sum += AllTiles.ScoreOfLetter(b[q, fix]) * bt.LetterMultiplier(q, fix);
                        if (bt.WordMultiplier(q, fix) != 1)
                        {
                            WordMultiply.Add(bt.WordMultiplier(q, fix));
                        }
                    }
                }
                foreach (int n in WordMultiply)
                    sum *= n;
            }
            return sum;
        }
    }
}
