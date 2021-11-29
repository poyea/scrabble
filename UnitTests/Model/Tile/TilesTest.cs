using NUnit.Framework;
using Scrabble;
using Scrabble.Model;
using System;

namespace UnitTests
{
    public class TilesTest
    {
        Tile tile;
        [SetUp]
        public void Setup()
        {
            tile = new Tile('c', 10);
        }

        [Test]
        public void Tiles_TileChar_Get_Should_Return_c()
        {
            // Arrange

            // Act
            var result = tile.TileChar;

            // Reset

            // Assert
            Assert.AreEqual('c', result);
        }

        [Test]
        public void Tiles_TileChar_Set_Should_Return_d()
        {
            // Arrange
            tile.TileChar = 'd';

            // Act
            var result = tile.TileChar;

            // Reset
            tile.TileChar = 'c';

            // Assert
            Assert.AreEqual('d', result);
        }

        [Test]
        public void Tiles_TileChar_Get_Should_Return_10()
        {
            // Arrange

            // Act
            var result = tile.TileScore;

            // Reset

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void Tiles_TileChar_Set_Should_Return_5()
        {
            // Arrange
            tile.TileScore = 5;

            // Act
            var result = tile.TileScore;

            // Reset
            tile.TileScore = 10;

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void Tiles_TileChar_CompareTo_Null_Should_Return_1()
        {
            // Arrange

            // Act
            var result = tile.CompareTo(null);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Tiles_TileChar_CompareTo_Valid_Tile_Should_Return_1()
        {
            // Arrange
            Tile otherTile = new Tile('b', 5);

            // Act
            var result = tile.CompareTo(otherTile);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Tiles_TileChar_CompareTo_Valid_Tile_Should_Return_Negative_23()
        {
            // Arrange
            Tile otherTile = new Tile('z', 5);

            // Act
            var result = tile.CompareTo(otherTile);

            // Reset

            // Assert
            Assert.AreEqual(-23, result);
        }

        [Test]
        public void Tiles_TileChar_CompareTo_Invalid_Object_Should_Throw_Exception()
        {
            // Arrange
            BoardTiles otherTile = new BoardTiles();

            // Act

            // Reset

            // Assert
            Assert.Throws<ArgumentException>(() => tile.CompareTo(otherTile));
        }
    }
}