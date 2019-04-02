using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrabble2018.Model
{
    public class Welcome
    {
        public static string NumOfPlayersInfo(int num)
        {
            return "This is a "+num+" players game...";
        }
        public static string WelcomeText
        {
            get { return "Welcome players! Welcome to Scrabble!"; }
        }

    }
}
