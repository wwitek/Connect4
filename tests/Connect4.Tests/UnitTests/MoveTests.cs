using Connect4.Domain.Entities;
using Connect4.Domain.Exceptions;
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
    public class MoveTests
    {
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void CreateMoveTest(bool isWinner, bool isDraw)
        {
            IMove move = new Move(1, 2, 3, isWinner, isDraw);

            Assert.AreEqual(1, move.Row);
            Assert.AreEqual(2, move.Column);
            Assert.AreEqual(3, move.PlayerId);
            Assert.AreEqual(isWinner, move.IsWinner);
            Assert.AreEqual(isDraw, move.IsDraw);
        }

        [TestCase(true, true)]
        public void CreateMove_ExceptionTest(bool isWinner, bool isDraw)
        {
            Assert.Throws<MoveException>(() => new Move(1, 2, 3, isWinner, isDraw));
        }
    }
}