using System;
using System.Collections.Generic;
using System.Linq;
using Scrabble2018.Model.Game;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2018.Model.Game
{
    public static class GameStartDraw
    {
        // Game start drawing to be used once in a game
        public static Dictionary<int, Tile> Drawn;
        private static Random rnd = new Random();
        private static AllTiles TilesBag;
        private static List<Tile> ListGot;
        public static void Draw()
        {
            Drawn = new Dictionary<int, Tile>();
            TilesBag = new AllTiles();
            for (int i = 0; i < GameState.GSInstance.NumOfPlayers; i++)
            {
                Drawn.Add(i, TilesBag.ListTiles[rnd.Next(0, TilesBag.ListTiles.Count)]);
            }
            ListGot = Drawn.Values.ToList();
            ListGot.Sort();
            GameState.GSInstance.PlayerNow = Drawn.FirstOrDefault(x => x.Value == ListGot[0]).Key;
        }
    }
}