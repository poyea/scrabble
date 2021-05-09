using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using Scrabble2018.Model.Game;
using System;

namespace UnitTests
{
    public class GameStartDrawTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GameStartDraw_Draw_Should_Pass()
        {
            // Arrange

            // Act
            GameStartDraw.Draw();

            // Assert
            Assert.IsTrue(true);

            // Reset
        }
    }
}