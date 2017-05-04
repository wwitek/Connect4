using Connect4.Domain.Entities;
using Connect4.Domain.Exceptions;
using Connect4.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class BoardTests
    {
        [Test]
        public void CreateBoardTest()
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
        }
        [Test]
        public void CreateBoardNullExceptionTest()
        {
            Assert.That(() => new Board(null), Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void CreateBoardContentNullExceptionTest()
        {
            IField[,] fields = new IField[6, 7];
            Assert.That(() => new Board(fields), Throws.TypeOf<ArgumentNullException>());
        }
        [Test]
        public void IsBoardFull_TrueTest()
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(true, board.IsBoardFull());
        }
        [Test]
        public void IsBoardFull_FalseTest()
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 0},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(false, board.IsBoardFull());
        }
        [Test]
        public void IsBoardFull_Empty_FalseTest()
        {
            var fields = TestHelper.FakeEmptyFieldArray(6, 7);
            IBoard board = new Board(fields);
            Assert.AreEqual(false, board.IsBoardFull());
        }
        [Test]
        public void IsOneColumnFull_TrueTest()
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
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
        public void AreAllColumnsFull_TrueTest([Values(0,1,2,3,4,5,6)] int column)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(true, board.IsColumnFull(column));
        }
        [Test]
        public void IsColumnFull_FalseTest([Values(0, 1, 2, 3, 4, 5, 6)] int column)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 1, 1, 0},
                {0, 0, 0, 1, 1, 1, 0},
                {0, 0, 1, 1, 1, 1, 0},
                {0, 1, 1, 1, 1, 1, 0}
            });
            IBoard board = new Board(fields);
            Assert.AreEqual(false, board.IsColumnFull(column));
        }
        [Test]
        public void GetLowestEmptyRowTest()
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
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
        [Test]
        public void GetLowestEmptyRowExceptionTest([Values(0, 1, 2, 3, 4, 5, 6)] int column)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            Assert.That(() => board.GetLowestEmptyRow(column), Throws.TypeOf<BoardException>());
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
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 1, 1, 0, 0, 0},
                {0, 1, 1, 1, 1, 0, 0},
                {1, 1, 1, 1, 1, 1, 0}
            });
            IBoard board = new Board(fields);
            board.InsertChip(row, column, 2);
            Assert.AreEqual(expectedLowestEmptyRow, board.GetLowestEmptyRow(column));
        }
        [Test]
        public void InsertChipIntoTopRowTest([Values(0, 1, 2, 3, 4, 5, 6)] int column)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            board.InsertChip(0, column, 2);
            Assert.AreEqual(true, board.IsColumnFull(column));
        }
        [Test]
        public void InsertChip_FullBoardExceptionTest([Values(0, 1, 2, 3, 4, 5, 6)] int column)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1, 1, 1}
            });
            IBoard board = new Board(fields);
            Assert.That(() => board.InsertChip(0, column, 1), Throws.TypeOf<BoardException>());
        }

        [TestCase(0, 5)]
        [TestCase(1, 4)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        [TestCase(4, 1)]
        [TestCase(5, 0)]
        [TestCase(6, 5)]
        public void InsertChip_FieldOccupiedExceptionTest(int column, int row)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 2, 2, 0},
                {0, 0, 0, 2, 2, 1, 0},
                {0, 0, 1, 1, 1, 2, 0},
                {0, 1, 2, 1, 2, 1, 0},
                {1, 2, 2, 1, 2, 2, 2}
            });
            IBoard board = new Board(fields);
            Assert.That(() => board.InsertChip(row, column, 1), Throws.TypeOf<BoardException>());
        }

        [TestCase(0, 5)]
        [TestCase(1, 4)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        [TestCase(4, 1)]
        [TestCase(5, 0)]
        [TestCase(6, 5)]
        public void RemoveChipTest(int column, int row)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 1, 0},
                {0, 0, 0, 0, 2, 2, 0},
                {0, 0, 0, 2, 2, 1, 0},
                {0, 0, 1, 1, 1, 2, 0},
                {0, 1, 2, 1, 2, 1, 0},
                {1, 2, 2, 1, 2, 2, 2}
            });
            IBoard board = new Board(fields);
            board.RemoveChip(row, column);
            board.InsertChip(row, column, 1);
        }

        [TestCase(0, 5)]
        [TestCase(1, 4)]
        [TestCase(2, 3)]
        [TestCase(3, 2)]
        [TestCase(4, 1)]
        [TestCase(5, 0)]
        [TestCase(6, 5)]
        public void RemoveChip_AlreadyEmptyExceptionTest(int column, int row)
        {
            var fields = TestHelper.FakeFieldArray(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 2, 0},
                {0, 0, 0, 0, 2, 1, 0},
                {0, 0, 0, 1, 1, 2, 0},
                {0, 0, 2, 1, 2, 1, 0},
                {0, 2, 2, 1, 2, 2, 0}
            });
            IBoard board = new Board(fields);
            Assert.That(() => board.RemoveChip(row, column), Throws.TypeOf<BoardException>());
        }

        public static IEnumerable<IField[,]> ConnectedFieldArrays
        {
            get
            {
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 1, 1, 1, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 1, 1, 1, 1, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 1, 1, 1},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {1, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 0, 0, 1, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 1},
                    {0, 0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 1, 0, 0, 0, 0, 0}
                });

                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 1},
                    {0, 0, 0, 0, 0, 1, 0},
                    {0, 0, 0, 1, 1, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 1, 1, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 1, 0, 0, 1},
                    {0, 0, 0, 1, 0, 1, 0},
                    {0, 0, 0, 1, 1, 0, 0},
                    {0, 0, 0, 1, 1, 1, 1},
                    {0, 0, 1, 1, 0, 0, 0},
                    {0, 1, 0, 1, 0, 0, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {1, 0, 0, 0, 0, 0, 1},
                    {0, 1, 0, 0, 0, 1, 0},
                    {0, 0, 1, 1, 1, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 1, 1, 1, 0, 0},
                    {0, 1, 0, 1, 0, 1, 0}
                });
            }
        }
        [TestCaseSource(nameof(ConnectedFieldArrays))]
        public void IsConnected_TrueTest(IField[,] fields)
        {
            IBoard board = new Board(fields);
            Assert.AreEqual(true, board.IsChipConnected(3,3));
        }

        public static IEnumerable<IField[,]> NotConnectedFieldArrays
        {
            get
            {
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 2, 1, 2, 0, 0},
                    {0, 2, 2, 1, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 1, 1, 0},
                    {0, 0, 2, 2, 2, 2, 0},
                    {0, 2, 2, 2, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 1, 1, 1, 0, 0, 0},
                    {0, 2, 2, 2, 2, 0, 0},
                    {0, 2, 2, 2, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 2, 2, 2, 0, 0},
                    {0, 2, 2, 1, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 1, 0, 0, 0, 0},
                    {0, 0, 2, 1, 0, 0, 0},
                    {0, 0, 2, 0, 1, 0, 0},
                    {0, 2, 2, 0, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 0},
                    {0, 0, 0, 1, 2, 0, 0},
                    {0, 0, 1, 0, 2, 0, 0},
                    {0, 2, 2, 0, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 1, 1, 1, 0, 0},
                    {0, 0, 2, 1, 2, 0, 0},
                    {0, 2, 2, 1, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 2, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 2, 2, 2, 0, 0},
                    {0, 2, 2, 2, 2, 2, 0}
                });
                yield return TestHelper.FakeFieldArray(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 1, 2, 2, 0, 0},
                    {0, 1, 2, 2, 2, 2, 0}
                });
            }
        }
        [TestCaseSource(nameof(NotConnectedFieldArrays))]
        public void IsConnected_FalseTest(IField[,] fields)
        {
            IBoard board = new Board(fields);
            Assert.AreEqual(false, board.IsChipConnected(3, 3));
        }

        public static IEnumerable<Tuple<IField[,],List<IField>>> ConnectedFields
        {
            get
            {
                List<IField> rootNotConnectedField = new List<IField>();
                rootNotConnectedField.Add(TestHelper.FakeField(3, 3, 1));
                yield return new Tuple<IField[,], List<IField>>(
                    TestHelper.FakeFieldArray(new int[,]
                    {
                                        {1, 1, 1, 1, 1, 1, 1},
                                        {1, 2, 2, 2, 2, 2, 1},
                                        {1, 2, 1, 1, 1, 2, 1},
                                        {1, 2, 1, 1, 1, 2, 1},
                                        {1, 2, 1, 1, 1, 2, 1},
                                        {1, 2, 2, 2, 2, 2, 1}
                    }), rootNotConnectedField);

                List<IField> expectedConnectedFields = new List<IField>();
                expectedConnectedFields.Add(TestHelper.FakeField(3, 3, 1));
                // Vertically
                expectedConnectedFields.Add(TestHelper.FakeField(2, 3, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(1, 3, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(0, 3, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(4, 3, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(5, 3, 1));
                // Horizontally
                expectedConnectedFields.Add(TestHelper.FakeField(3, 2, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(3, 1, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(3, 0, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(3, 4, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(3, 5, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(3, 6, 1));
                // Diagonally - Northwest to Southeast
                expectedConnectedFields.Add(TestHelper.FakeField(2, 2, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(1, 1, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(0, 0, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(4, 4, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(5, 5, 1));
                // Diagonally - Southwest to Northeast
                expectedConnectedFields.Add(TestHelper.FakeField(4, 2, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(5, 1, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(2, 4, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(1, 5, 1));
                expectedConnectedFields.Add(TestHelper.FakeField(0, 6, 1));
                yield return new Tuple<IField[,], List<IField>>(
                    TestHelper.FakeFieldArray(new int[,]
                    {
                        {1, 0, 0, 1, 0, 0, 1},
                        {0, 1, 0, 1, 0, 1, 0},
                        {0, 0, 1, 1, 1, 0, 0},
                        {1, 1, 1, 1, 1, 1, 1},
                        {0, 0, 1, 1, 1, 0, 0},
                        {0, 1, 2, 1, 2, 1, 0}
                    }), expectedConnectedFields);
            }
        }
        [TestCaseSource(nameof(ConnectedFields))]
        public void ConnectedFieldsTest(Tuple<IField[,], List<IField>> fields)
        {
            IBoard board = new Board(fields.Item1);
            List<IField> connectedFields = board.GetConnectedChips(3, 3);
            Assert.AreEqual(fields.Item2.Select(i => new { i.Row, i.Column }), 
                            connectedFields.Select(i => new { i.Row, i.Column }));
        }
    }
}