using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Scrabble2018
{
    public class BoardTiles
    {
        // Reference BoardTiles and utility
        // Reference is for cancelling used colors
        public static string[,] Placement = new string[15, 15]
        {
            {"tw","","","dl","","","","tw","","","","dl","","","tw"},
            {"","dw","","","","tl","","","","tl","","","","dw",""},
            {"","","dw","","","","dl","","dl","","","","dw","",""},
            {"dl","","","dw","","","","dl","","","","dw","","","dl"},
            {"","","","","dw","","","","","","dw","","","","" },
            {"","tl","","","","tl","","","","tl","","","","tl",""},
            {"","","dl","","","","dl","","dl","","","","dl","",""},
            //middle
            {"tw","","","dl","","","","st","","","","dl","","","tl"},
            //middle
            {"","","dl","","","","dl","","dl","","","","dl","",""},
            {"","tl","","","","tl","","","","tl","","","","tl",""},
            {"","","","","dw","","","","","","dw","","","","" },
            {"dl","","","dw","","","","dl","","","","dw","","","dl"},
            {"","","dw","","","","dl","","dl","","","","dw","",""},
            { "","dw","","","","tl","","","","tl","","","","dw",""},
            {"tw","","","dl","","","","tw","","","","dl","","","tw"}
        };

        public string[,] PlaceInUse;
        private bool[,] Visited;

        public BoardTiles()
        {
            PlaceInUse = Placement;
            Visited = new bool[15, 15];
        }

        public int WordMultiplier(int i,int j)
        {
            if (PlaceInUse[i, j] == "tw")
            {
                Visited[i, j] = true;
                return 3;
            }
            else if (PlaceInUse[i, j] == "dw")
            {
                Visited[i, j] = true;
                return 2;
            }
            else return 1;
        }

        public int LetterMultiplier(int i, int j)
        {
            if (PlaceInUse[i, j] == "tl")
            {
                Visited[i, j] = true;
                return 3;
            }
            else if (PlaceInUse[i, j] == "dl")
            {
                Visited[i, j] = true;
                return 2;
            }
            else return 1;
        }

        public void ApplyVisited()
        {
            for(int i = 0; i < Visited.GetLength(0); i++)
            {
                for(int j= 0; j < Visited.GetLength(1); j++)
                {
                    if (Visited[i, j])
                    {
                        PlaceInUse[i, j] = "";
                        Visited[i, j] = false;
                    }
                }
            }

        }

        public void CleanVisited()
        {
            for (int i = 0; i < Visited.GetLength(0); i++)
            {
                for (int j = 0; j < Visited.GetLength(1); j++)
                {
                        Visited[i, j] = false;
                }
            }
        }

        public static SolidColorBrush DetermineColor(int i, int j)
        {
            if (Placement[i, j] == "tw") return Brushes.OrangeRed;
            else if (Placement[i, j] == "dw") return Brushes.Coral;
            else if (Placement[i, j] == "dl") return Brushes.LightSkyBlue;
            else if (Placement[i, j] == "tl") return Brushes.MediumBlue;
            else if (Placement[i, j] == "st") return Brushes.Gold;
            else return Brushes.Bisque;
        }


    }
}
