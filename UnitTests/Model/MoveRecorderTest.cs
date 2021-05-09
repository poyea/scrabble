using NUnit.Framework;
using Scrabble2018;
using Scrabble2018.Model;
using System;

namespace UnitTests
{
    public class MoveRecorderTest
    {
        MoveRecorder mr;

        [SetUp]
        public void Setup()
        {
            mr = new MoveRecorder();
        }

        [Test]
        public void MoveRecorder_Constructor_Should_Not_Be_Null()
        {
            //Arrange

            //Act

            //Assert
            Assert.IsNotNull(mr);
            Assert.AreEqual(0, mr.Index.Count);
            Assert.AreEqual(0, mr.Moves.Count);
        }

        [Test]
        public void MoveRecorder_Record_Moves_Should_Not_Be_Empty()
        {
            //Arrange

            //Act
            mr.Record(1, 1);

            //Assert
            Assert.AreEqual(1, mr.Moves.Count);

            //Reset
            mr.Reset();
        }

        [Test]
        public void MoveRecorder_Reset_Check_Variables_Should_Pass()
        {
            //Arrange

            //Act
            mr.Record(1, 1);
            mr.Record(2, 2);
            mr.Reset();

            //Assert
            Assert.AreEqual(0, mr.Moves.Count);
            Assert.AreEqual(0, mr.Index.Count);
            Assert.AreEqual("", mr.Direction);
            Assert.AreEqual(-1, mr.Fixed);

            //Reset
        }
    }
}
