using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;

namespace UnitTests
{
    public class PlayerManagerTest
    {
        PlayerManager pm;
        [SetUp]
        public void Setup()
        {
            pm = new PlayerManager();
        }

        [Test]
        public void PlayerManager_CreatePlayers_Should_Pass()
        {
            //Assert
            Player p = new Player();
            GameState gs = new GameState();
            gs.ListOfPlayers.Add(p);

            //Act

            //Assert
            Assert.IsNotNull(gs);

        }

        [Test]
        public void PlayerManager_AddScoreToPlayer_Should_Pass()
        {
            //Arrange
            Player p = new Player();
            p.Score = 1;

            //Act
            pm.AddScoresToPlayer(p, p.Score);

            //Assert
           //Not sure how what to assert here :(

        }

        [Test]
        public void PlayerManager_Swap_Should_Return_Pass()
        {
            //Assert
            //Boolean swapped = false;
           // char c = '1';

            //Act
           // var result = pm.Swap(c); ;

            //Assert
            //Assert.AreNotEqual(result, 'a');
        }


    }
}
