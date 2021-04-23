using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model.Word;
using System.Collections.Generic;

namespace UnitTests
{
    public class ScoreUtilityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public static void ScoreUtilityTests_ScoreCalc_Return_corretScoreSum()
        {
            //private List<int> WordMultiply;

            //Arrange
            ScoreUtility score = new ScoreUtility();

            List<int> WordMultiply = new List<int>();
            BoardTiles bt = new BoardTiles();
            int fix, j, jM;
            string direction;
            char[,] b;

            //Act
            var result = score.ScoreCalc(fix, j, jM, direction, b, bt);

            //Reset

            //Assert
            Assert.AreEqual(sum, result);
        }
    }
}