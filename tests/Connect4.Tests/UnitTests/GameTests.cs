using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void GameTryMoveTest()
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.FakeLocalPlayer(1));
            players.Add(TestHelper.FakeLocalPlayer(2));

            IField[,] fieldStubs = TestHelper.FakeEmptyFieldArray(6, 7);
            var boardStub = new Mock<Board>(fieldStubs);

            IGame game = new Game(boardStub.Object, players);
            bool result = game.TryMove(0);
            Assert.AreEqual(true, result);
        }

        [Test, Timeout(1000)]
        public void GameOnMoveMadeTest()
        {
            AutoResetEvent eventSignal = new AutoResetEvent(false);

            int OnMoveMadeWasRasiedCounter = 0;
            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.FakeLocalPlayer(1));
            players.Add(TestHelper.FakeLocalPlayer(2));

            IField[,] fieldStubs = TestHelper.FakeEmptyFieldArray(6, 7);
            var boardStub = new Mock<Board>(fieldStubs);

            IGame game = new Game(boardStub.Object, players);
            game.OnMoveMade += (s, e) =>
            {
                eventSignal.Set();
                OnMoveMadeWasRasiedCounter++;
            };
            game.TryMove(0);

            eventSignal.WaitOne();
            Assert.AreEqual(1, OnMoveMadeWasRasiedCounter);
        }

        [Test, Timeout(5000)]
        public void GameMultipleOnMoveMadeTest()
        {
            AutoResetEvent eventSignal = new AutoResetEvent(false);

            int onMoveMadeWasRasiedCounter = 0;
            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.FakeLocalPlayer(1));
            players.Add(TestHelper.FakeLocalPlayer(2));

            IField[,] fieldStubs = TestHelper.FakeEmptyFieldArray(6, 7);
            var boardStub = new Mock<Board>(fieldStubs);

            IGame game = new Game(boardStub.Object, players);
            game.OnMoveMade += (s, e) =>
            {
                eventSignal.Set();
                onMoveMadeWasRasiedCounter++;
            };

            game.TryMove(0);
            eventSignal.WaitOne();
            Assert.AreEqual(1, onMoveMadeWasRasiedCounter);
            
            game.TryMove(0);
            eventSignal.WaitOne();
            Assert.AreEqual(2, onMoveMadeWasRasiedCounter);

            game.TryMove(0);
            eventSignal.WaitOne();
            Assert.AreEqual(3, onMoveMadeWasRasiedCounter);

            game.TryMove(0);
            eventSignal.WaitOne();
            Assert.AreEqual(4, onMoveMadeWasRasiedCounter);
        }
    }
}
