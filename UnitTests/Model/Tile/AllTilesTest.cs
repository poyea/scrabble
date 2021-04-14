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

        [Test]
        public void AllTiles_ScoreOfLetter_E_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('E');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_A_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('A');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_I_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('I');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_O_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('O');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_N_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('N');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_R_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('R');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_T_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('T');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_L_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('L');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_S_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('S');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_U_Should_Return_1()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('U');

            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_Dash_Should_Return_0()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('-');

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_IllegalCharacter_Should_Return_0()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('*');

            // Reset

            // Assert
            Assert.AreEqual(0, result);
        }
    }
}