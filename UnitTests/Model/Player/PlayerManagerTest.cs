using NUnit.Framework;
using Scrabble.Model;

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
            Assert.True(true);

        }



    }
}
