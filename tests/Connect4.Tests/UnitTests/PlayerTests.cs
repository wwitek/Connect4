using Connect4.Domain.Entities.Players;
using Connect4.Domain.Interfaces;
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
    public class PlayerTests
    {
        [Test]
        public void LocalPlayer_InjectMove_TrueTest()
        {
            IPlayer player = new LocalPlayer(1);
            bool result = player.InjectMove(1);
            Assert.AreEqual(true, result);
        }

        [Test]
        public void LocalPlayer_InjectMove_FalseTest()
        {
            IPlayer player = new LocalPlayer(1);
            player.InjectMove(1);

            bool result = player.InjectMove(1);
            Assert.AreEqual(false, result);
        }

        [Test, Timeout(1000)]
        public void LocalPlayer_WaitForMoveTest()
        {
            IBoard board = TestHelper.MockEmptyBoard();
            IPlayer player = new LocalPlayer(1);
            player.InjectMove(1);

            IMove move = player.WaitForMove(board);
            Assert.AreEqual(1, move.Column);
        }

        [Test, Timeout(1000)]
        public void LocalPlayer_WaitForMoveMultithreadedTest()
        {
            IPlayer player = new LocalPlayer(1);

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                player.InjectMove(1);
            });

            IBoard board = TestHelper.MockEmptyBoard();
            IMove move = player.WaitForMove(board);
            Assert.AreEqual(1, move.Column);
        }
    }
}