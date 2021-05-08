using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;

namespace UnitTests
{
    public class WelcomeTest
    {
        [Test]
        public void Welcome_NumOfPlayersInfo_2_Should_Pass()
        {
            //Arrange
            String result = Welcome.NumOfPlayersInfo(2);

            //Act

            //Assert
            Assert.AreEqual("This is a 2 players game...", result);
        }

        [Test]
        public void Welcome_WelcomeText_Should_Return_Default_Text()
        {
            //Arrange
            String result = Welcome.WelcomeText;

            //Act

            //Assert
            Assert.AreEqual("Welcome players! Welcome to Scrabble!", result);
        }
    }
}
