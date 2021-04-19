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
        public void AllTiles_Empty_Should_Return_True()
        {
            // Arrange
            AllTiles tiles = new AllTiles();

            // Act
            tiles.ListTiles.Clear();
            var result = tiles.Empty();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
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
        public void AllTiles_ScoreOfLetter_D_Should_Return_2()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('D');

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_G_Should_Return_2()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('G');

            // Reset

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_B_Should_Return_3()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('B');

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_C_Should_Return_3()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('C');

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_M_Should_Return_3()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('M');

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_P_Should_Return_3()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('P');

            // Reset

            // Assert
            Assert.AreEqual(3, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_F_Should_Return_4()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('F');

            // Reset

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_H_Should_Return_4()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('H');

            // Reset

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_V_Should_Return_4()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('V');

            // Reset

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_W_Should_Return_4()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('W');

            // Reset

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_Y_Should_Return_4()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('Y');

            // Reset

            // Assert
            Assert.AreEqual(4, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_K_Should_Return_5()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('K');

            // Reset

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_J_Should_Return_8()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('J');

            // Reset

            // Assert
            Assert.AreEqual(8, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_X_Should_Return_8()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('X');

            // Reset

            // Assert
            Assert.AreEqual(8, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_Q_Should_Return_10()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('Q');

            // Reset

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void AllTiles_ScoreOfLetter_Z_Should_Return_10()
        {
            // Arrange

            // Act
            var result = AllTiles.ScoreOfLetter('Z');

            // Reset

            // Assert
            Assert.AreEqual(10, result);
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