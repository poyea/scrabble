using System;
using System.Collections.Generic;

namespace Scrabble2018.Model
{
    public class MoveRecorder
    {
        public List<Tuple<int, int>> Moves;
        public List<int> Index;
        public string Direction;
        public int Fixed;
        // Record movement for validation
        public MoveRecorder()
        {
            Moves = new List<Tuple<int, int>>();
            Index = new List<int>();
        }

        public void Record(int i, int j)
        {
            Moves.Add(Tuple.Create(i, j));
        }

        public void Reset()
        {
            Moves.Clear();
            Index.Clear();
            Direction = "";
            Fixed = -1;
        }
    }
}
