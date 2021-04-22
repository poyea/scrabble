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

        [Test]
        public void BoardTile_WordMultiplier_WordDouble_Should_Return_2()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(1, 1);

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void BoardTile_WordMultiplier_Default_Square_Should_Return_1()
        {
            // Arrange

            // Act
            var result = bt.WordMultiplier(1, 0);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_TripleLetter_Should_Return_3()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(1, 5);

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_DoubleLetter_Should_Return_2()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(0, 3);

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void BoardTile_LetterMultiplier_Default_Square_Should_Return_1()
        {
            // Arrange

            // Act
            var result = bt.LetterMultiplier(0, 1);

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}