using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;

namespace UnitTests
{
    public class PlayerTest
    {
        Player player;
        [SetUp]
        public void Setup()
        {
            player = new Player();
            player.Score = 0;
            player.Id = 1;
        }

        [Test]
        public void Player_Score_Get_Should_Return_0()
        {
            //Act
            var result = player.Score;

            //Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Player_Score_Set_Should_Return_50()
        {
            //Arrange 
            player.Score = '2';

            //Act
            var result = player.Score;

            //Reset
            player.Score = '0';

            //Assert
            Assert.AreEqual(50, result);
        }

        [Test]
        public void Player_CompareTo_Null_Should_Return_1()
        {
            //Arrange

            //Act
            var result = player.CompareTo(null);

            //Assert
            Assert.AreEqual(1, result);

        }

        [Test]
        public void Player_CompareTo_Valid_Player_Should_Return_1()
        {
            //Arrange
            Player otherPlayer = new Player();
            player.Score = 2;
            otherPlayer.Id = 2;
            otherPlayer.Score = 1;

            //Act
            var result = player.CompareTo(otherPlayer);

            //Assert
            Assert.AreEqual(1, result);

        }

        [Test]
        public void Player_CompareTo_Valid_Player_Should_Return_2()
        {
            //Arrange
            Player otherPlayer = new Player();

            //Act
            var result = player.CompareTo(null);

            //Assert
            Assert.AreEqual(1, result);

        }

        [Test]
        public void Player_CompareTo_Invalid_Object_Should_Throw_Exception()
        {
            //Arrange
            PlayerManager pm = new PlayerManager();

            //Act
            

            //Assert
            Assert.Throws<ArgumentException>(() =>player.CompareTo(pm));

        }
    }
}
