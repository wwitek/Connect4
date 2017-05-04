using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.AI;
using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class BoardGetEvaluationIdsTests
    {
        static object[] SampleBoards4s =
        {
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                }, new List<int> { 0, 0, 0, 0 }
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 2, 0, 0, 0},
                    {0, 0, 1, 0, 2, 0, 0},
                    {1, 1, 0, 0, 0, 2, 2},
                    {1, 1, 0, 0, 0, 2, 2},
                    {0, 0, 1, 0, 2, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0}
                }, new List<int> { 1111, 1112, 1222, 2222 }
            },
        };

        [Test, TestCaseSource(nameof(SampleBoards4s))]
        public void Evaluate4Test(int[,] ids, List<int> score)
        {
            var fieldStubs = TestHelper.FakeFieldArray(ids);
            IBoard board = new Board(fieldStubs);
            List<int> evalIds = board.GetEvaluationIds(4).OrderBy(i => i).ToList();
            score = score.OrderBy(i => i).ToList();

            Assert.AreEqual(score[0], evalIds[0]);
            Assert.AreEqual(score[1], evalIds[1]);
            Assert.AreEqual(score[2], evalIds[2]);
            Assert.AreEqual(score[3], evalIds[3]);
        }

        static object[] SampleBoards5s =
        {
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                }, new List<int> { 0, 0, 0, 0 }
            },
            new object[] {
                new[,]
                {
                    {0, 0, 1, 0, 0, 0, 0},
                    {1, 0, 0, 2, 0, 0, 2},
                    {0, 1, 1, 0, 0, 2, 0},
                    {0, 0, 1, 0, 2, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 0}
                }, new List<int> { 120, 11111, 1222, 12000 }
            },
        };

        [Test, TestCaseSource(nameof(SampleBoards5s))]
        public void Evaluate5Test(int[,] ids, List<int> score)
        {
            var fieldStubs = TestHelper.FakeFieldArray(ids);
            IBoard board = new Board(fieldStubs);
            List<int> evalIds = board.GetEvaluationIds(5).OrderBy(i => i).ToList();
            score = score.OrderBy(i => i).ToList();

            Assert.AreEqual(score[0], evalIds[0]);
            Assert.AreEqual(score[1], evalIds[1]);
            Assert.AreEqual(score[2], evalIds[2]);
            Assert.AreEqual(score[3], evalIds[3]);
        }

        static object[] SampleBoards6s =
        {
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                }, new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 2, 0, 0, 0},
                    {0, 0, 0, 1, 0, 0, 0},
                    {0, 0, 0, 2, 0, 0, 0}
                }, new List<int> { 100, 200, 1000, 1212, 2000, 0, 0, 0, 0, 0, 0 }
            },
        };

        [Test, TestCaseSource(nameof(SampleBoards6s))]
        public void Evaluate6Test(int[,] ids, List<int> score)
        {
            var fieldStubs = TestHelper.FakeFieldArray(ids);
            IBoard board = new Board(fieldStubs);
            List<int> evalIds = board.GetEvaluationIds(6).OrderBy(i => i).ToList();
            score = score.OrderBy(i => i).ToList();

            Assert.AreEqual(score[0], evalIds[0]);
            Assert.AreEqual(score[1], evalIds[1]);
            Assert.AreEqual(score[2], evalIds[2]);
            Assert.AreEqual(score[3], evalIds[3]);
            Assert.AreEqual(score[4], evalIds[4]);
            Assert.AreEqual(score[5], evalIds[5]);
            Assert.AreEqual(score[6], evalIds[6]);
            Assert.AreEqual(score[7], evalIds[7]);
            Assert.AreEqual(score[8], evalIds[8]);
            Assert.AreEqual(score[9], evalIds[9]);
            Assert.AreEqual(score[10], evalIds[10]);
        }

        static object[] SampleBoards7s =
        {
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0}
                }, new List<int> { 0, 0, 0, 0, 0, 0 }
            },
            new object[] {
                new[,]
                {
                    {0, 2, 0, 0, 0, 0, 0},
                    {0, 1, 0, 2, 0, 0, 0},
                    {0, 2, 0, 1, 0, 0, 0},
                    {2, 1, 0, 2, 1, 0, 0},
                    {2, 1, 2, 1, 2, 1, 0},
                    {1, 2, 1, 2, 1, 2, 1}
                }, new List<int> { 200000, 102000, 201000, 2102100, 2121210, 1212121 }
            },
        };

        [Test, TestCaseSource(nameof(SampleBoards7s))]
        public void Evaluate7Test(int[,] ids, List<int> score)
        {
            var fieldStubs = TestHelper.FakeFieldArray(ids);
            IBoard board = new Board(fieldStubs);
            List<int> evalIds = board.GetEvaluationIds(7).OrderBy(i => i).ToList();
            score = score.OrderBy(i => i).ToList();

            Assert.AreEqual(score[0], evalIds[0]);
            Assert.AreEqual(score[1], evalIds[1]);
            Assert.AreEqual(score[2], evalIds[2]);
            Assert.AreEqual(score[3], evalIds[3]);
            Assert.AreEqual(score[4], evalIds[4]);
            Assert.AreEqual(score[5], evalIds[5]);
        }
    }
}
