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
    public class BoardTests
    {
        [Test]
        public void ResetBoardTest()
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            board.Reset();
        }
        [Test]
        public void IsOneColumnFull_TrueTest()
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0},
                {0, 1, 0, 0, 0, 0, 0}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(true, board.IsColumnFull(1));
        }
        [Test]
        public void AreAllColumnsFull_TrueTest()
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            for (int i = 0; i < 7; i++)
            {
                Assert.AreEqual(true, board.IsColumnFull(i));
            }
        }
        [Test]
        public void IsColumnFull_FalseTest()
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(false, board.IsColumnFull(3));
        }
        [Test]
        public void GetLowestEmptyRowTest()
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(1, board.GetLowestEmptyRow(3));
        }

        [TestCase(0, 4, 3)]
        [TestCase(1, 3, 2)]
        [TestCase(2, 2, 1)]
        [TestCase(3, 1, 0)]
        [TestCase(4, 3, 2)]
        [TestCase(5, 4, 3)]
        [TestCase(6, 5, 4)]
        public void InsertChipTest(int column, int row, int expectedLowestEmptyRow)
        {
            var fields = TestHelper.MockFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 1, 1, 0, 0, 0},
                {0, 1, 1, 1, 1, 0, 0},
                {1, 1, 1, 1, 1, 1, 0}
            });
            IBoard board = new Board(fields);
            board.ToString();
            board.InsertChip(row, column, 2);
            board.ToString();
            Assert.AreEqual(expectedLowestEmptyRow, board.GetLowestEmptyRow(column));
        }
    }
}
