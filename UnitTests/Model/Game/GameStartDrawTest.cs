using NUnit.Framework;
using Scrabble2018.Model.Game;

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