using Connect4.Domain.AI;
using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class AlphaBetaTests
    {
        [Test]
        public void AlphaBetaTests1()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 1, 0, 0},
                {0, 2, 0, 0, 1, 0, 0},
                {0, 2, 1, 2, 1, 0, 0},
                {1, 1, 1, 2, 2, 2, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            AlphaBeta alphaBeta = new AlphaBeta();
            int madeMove = alphaBeta.GenerateMove(boardStub.Object, 2, 1);

            Assert.AreEqual(6, madeMove);
        }

        [Test]
        public void AlphaBetaTest2()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 2, 2, 2, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            AlphaBeta alphaBeta = new AlphaBeta();
            int madeMove = alphaBeta.GenerateMove(boardStub.Object, 2, 1);

            Assert.AreEqual(6, madeMove);
        }
    }
}
