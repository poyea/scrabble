using NUnit.Framework;
using Scrabble2018.Model;

namespace UnitTests
{
    public class TilesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Tiles_TileChar_Get_Should_Return_c()
        {
            // Arrange
            Tile tile = new Tile('c', 10);

            // Act
            var result = tile.TileChar;

            // Reset

            // Assert
            Assert.AreEqual('c', result);
        }
    }
}