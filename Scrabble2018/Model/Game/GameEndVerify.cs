namespace Scrabble2018.Model
{
    public static class GameEndVerify
    {
        // Utility class to check whether the game has ended or not
        public static Player p0;
        public static bool TilebagLessThanSeven(GameState gs)
        {
            if (gs.TilesBag.ListTiles.Count < 7) return true;
            return false;
        }

        public static bool ExistsPlayerNoTiles(GameState gs)
        {
            foreach (Player p in gs.ListOfPlayers)
            {
                if (p.PlayingTiles.Count == 0) { p0 = p; return true; }
            }
            return false;
        }

        public static bool GameEndScoring(GameState gs)
        {
            if (TilebagLessThanSeven(gs))
            {
                if (ExistsPlayerNoTiles(gs))
                {
                    foreach (Player p in gs.ListOfPlayers)
                    {
                        if (p != p0)
                        {
                            foreach (Tile t in p.PlayingTiles)
                            {
                                p0.Score += AllTiles.ScoreOfLetter(t.TileChar);
                                p.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                            }
                        }
                    }
                }
                else
                {
                    foreach (Player p in gs.ListOfPlayers)
                    {
                        foreach (Tile t in p.PlayingTiles)
                        {
                            p.Score -= AllTiles.ScoreOfLetter(t.TileChar);
                        }
                    }
                }
                return true;
            }
            return false;
        }
    }
}
