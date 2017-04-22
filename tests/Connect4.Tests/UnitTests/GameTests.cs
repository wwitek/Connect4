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

        [Test]
        public void GameOnMoveMadeTest()
        {
            bool OnMoveMadeWasRasied = false;

            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.FakeLocalPlayer(1));
            players.Add(TestHelper.FakeLocalPlayer(2));

            IField[,] fieldStubs = TestHelper.FakeEmptyFieldArray(6, 7);
            var boardStub = new Mock<Board>(fieldStubs);

            IGame game = new Game(boardStub.Object, players);
            game.OnMoveMade += (s, e) => OnMoveMadeWasRasied = true;

            game.TryMove(0);

            Assert.AreEqual(true, OnMoveMadeWasRasied);
        }
    }
}
