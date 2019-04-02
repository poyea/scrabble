using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2018.Model
{
    public class PlayerManager
    {
        // Manage players
        Random rnd = new Random();
        public void CreatePlayers(GameState gs)
        {
            for (int i = 0; i < gs.NumOfPlayers; ++i)
            {
                Player p = new Player();
                p.Id = i + 1;
                for (int j = 0; j < 7; ++j)
                {
                    int rNum = rnd.Next(0, gs.TilesBag.ListTiles.Count);
                    p.PlayingTiles.Add(gs.TilesBag.ListTiles[rNum]);
                    gs.TilesBag.ListTiles.RemoveAt(rNum);

                }
                gs.ListOfPlayers.Add(p);
            }
        }

        public void AddScoresToPlayer(Player p, int score)
        {
            p.Score += score;
        }

        public void GetNewTiles(GameState gs, List<char> LoC, int num)
        {
            List<Tile> LoT = new List<Tile>();
            foreach (Tile t in gs.ListOfPlayers[num].PlayingTiles)
            {
                if (LoC.Contains(t.TileChar))
                {
                    LoT.Add(t);
                    LoC.Remove(t.TileChar);
                }
            }
            foreach(Tile t in LoT)
            {
                gs.ListOfPlayers[num].PlayingTiles.Remove(t);
                int rNum = rnd.Next(0, gs.TilesBag.ListTiles.Count);
                gs.ListOfPlayers[num].PlayingTiles.Add(gs.TilesBag.ListTiles[rNum]);
                gs.TilesBag.ListTiles.RemoveAt(rNum);
            }
        }

        public char Swap(char c)
        {
            int rNum = rnd.Next(0, GameState.GSInstance.TilesBag.ListTiles.Count);
            Tile t = GameState.GSInstance.TilesBag.ListTiles[rNum];
            GameState.GSInstance.TilesBag.ListTiles.RemoveAt(rNum);
            foreach (Tile tmp in GameState.GSInstance.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles)
            {
                if (tmp.TileChar == c)
                {
                    GameState.GSInstance.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles.Remove(tmp);
                    GameState.GSInstance.ListOfPlayers[GameState.GSInstance.PlayerNow].PlayingTiles.Add(t);
                    break;
                }
            }
            GameState.GSInstance.TilesBag.ListTiles.Add(new Tile(c, AllTiles.ScoreOfLetter(c)));
            return t.TileChar;
        }
    }
}