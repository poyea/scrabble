using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;

namespace UnitTests
{
    public class BoardTileTest
    {
        BoardTiles bt;
        [SetUp]
        public void Setup()
        {
            bt = new BoardTiles();
        }

        [Test]
        public void BoardTile_WordMultiplier_WordTriple_Should_Return_3()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(0, 0);

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }
    }
}