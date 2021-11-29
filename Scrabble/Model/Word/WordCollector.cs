using System;
using System.Collections.Generic;
using System.IO;

namespace Scrabble.Model.Word
{
    public static class WordCollector
    {
        private static List<String> TxtItems;
        private static GameState gamestate;
        private static List<char> BlackToChar = new List<char>();

        public static int Locate(string s)
        {
            var Lines = File.ReadAllLines(@"Model\Word\wordlist.txt");
            TxtItems = new List<string>(Lines);
            gamestate.WordsAppearedInValidation.Clear();
            foreach (string str in gamestate.WordsAppeared)
            {
                gamestate.WordsAppearedInValidation.Add(str);
            }

            if (TxtItems.Contains(s))
            {
                if (!gamestate.WordsAppearedInValidation.Contains(s))
                {
                    gamestate.WordsAppearedInValidation.Add(s);
                    return 1; // It's legit
                }
                else
                {
                    return 0; // It's legit but it's old
                }
            }
            else
            {
                return -1; // Not correct
            }
        }


        public static int VCollect(int i, int j, char[,] b, GameState gs)
        {
            gamestate = gs;
            string s = "";
            int Vsum = 0;
            bool VFound = true;
            // vertical
            for (int current = i; current < b.GetLength(0); current++)
            {
                if (b[current, j] != '\0')
                {

                    s += b[current, j];
                    if (current + 1 == b.GetLength(0))
                    {
                        if (s.Length > 1)
                        {
                            if (Locate(s) == 1)
                            {
                                Vsum += ScoreUtility.ScoreCalc(j, i, current, "v", b, gs.boardTiles);
                                gs.CorrectWords[s] = Vsum;
                            }
                            else if (Locate(s) == 0) break;
                            else VFound = false;
                        }
                    }
                }
                else
                {
                    if (s.Length > 1)
                    {
                        if (Locate(s) == 1)
                        {
                            Vsum += ScoreUtility.ScoreCalc(j, i, current, "v", b, gs.boardTiles);
                            gs.CorrectWords[s] = Vsum;
                        }
                        else if (Locate(s) == 0) break;
                        else VFound = false;
                    }
                    break;

                }
            }
            if (VFound && Vsum != 0)
            {
                return Vsum;
            }
            else if (!VFound)
            {
                return -1;
            }
            else return 0;
        }

        public static int HCollect(int i, int j, char[,] b, GameState gs)
        {
            gamestate = gs;
            string s = "";
            int Hsum = 0;
            bool HFound = true;
            // horizontal 
            for (int current = j; current < b.GetLength(1); current++)
            {
                if (b[i, current] != '\0')
                {
                    s += b[i, current];
                    if (current + 1 == b.GetLength(1))
                    {
                        if (s.Length > 1)
                        {
                            if (Locate(s) == 1)
                            {
                                Hsum += ScoreUtility.ScoreCalc(i, j, current, "h", b, gs.boardTiles);
                                gs.CorrectWords[s] = Hsum;
                            }
                            else if (Locate(s) == 0) break;
                            else HFound = false;
                        }
                    }
                }
                else
                {
                    if (s.Length > 1)
                    {
                        if (Locate(s) == 1)
                        {
                            Hsum += ScoreUtility.ScoreCalc(i, j, current, "h", b, gs.boardTiles);
                            gs.CorrectWords[s] = Hsum;
                        }
                        else if (Locate(s) == 0) break;
                        else HFound = false;
                    }
                    break;
                }
            }

            if (HFound && Hsum != 0)
            {
                return Hsum;
            }
            else if (!HFound)
            {
                return -1;
            }
            else return 0;
        }

        public static int Collect(int i, int j, char[,] b, GameState gs)
        {
            gamestate = gs;
            string s = "";
            int Hsum = 0;
            bool HFound = true;
            // horizontal 
            for (int current = j; current < b.GetLength(1); current++)
            {
                if (b[i, current] != '\0')
                {
                    s += b[i, current];
                }
                else
                {
                    if (s.Length > 1)
                    {
                        if (Locate(s) == 1) Hsum += ScoreUtility.ScoreCalc(i, j, current, "h", b, gs.boardTiles);
                        else if (Locate(s) == 0) break;
                        else HFound = false;
                    }
                    break;
                }
            }
            s = "";
            int Vsum = 0;
            bool VFound = true;

            if ((VFound || HFound) && (Vsum != 0 || Hsum != 0))
            {
                return Vsum + Hsum;
            }
            else if (!VFound && !HFound)
            {
                return -1;
            }
            else return 0;
        }
    }
}
