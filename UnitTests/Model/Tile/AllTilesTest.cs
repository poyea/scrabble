using NUnit.Framework;
using Scrabble2018.Model;

namespace UnitTests
{
    public class AllTilesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AllTiles_Empty_Should_Return_False()
        {
            // Arrange
            AllTiles tiles = new AllTiles();

            // Act
            var result = tiles.Empty();

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}