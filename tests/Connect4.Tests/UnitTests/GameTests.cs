using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            players.Add(TestHelper.MockPlayer(1));
            players.Add(TestHelper.MockPlayer(2));
            IBoard board = TestHelper.MockEmptyBoard();

            IGame game = new Game(board, players);
            bool result = game.TryMove(0);
            Assert.AreEqual(true, result);
        }
    }
}
