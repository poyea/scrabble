namespace Scrabble.Model
{
    public class Welcome
    {
        public static string NumOfPlayersInfo(int num)
        {
            return "This is a " + num + " players game...";
        }
        public static string WelcomeText
        {
            get { return "Welcome players! Welcome to Scrabble!"; }
        }

    }
}
