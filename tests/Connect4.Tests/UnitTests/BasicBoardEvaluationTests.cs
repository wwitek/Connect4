using Connect4.Domain.AI;
using Connect4.Domain.Entities;
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
    public class BasicBoardEvaluationTests
    {
        static object[] SampleBoards =
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
                }, 0
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0}
                }, 3
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0}
                }, 11
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0}
                }, 119
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0}
                }, 10120
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0}
                }, -6
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0}
                }, -18
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0}
                }, -230
            },
            new object[] {
                new[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0},
                    {1, 0, 0, 0, 0, 0, 0}
                }, -15232
            },
        };

        [Test, TestCaseSource(nameof(SampleBoards))]
        public void EvaluateTest(int[,] ids, int score)
        {
            var fieldStubs = TestHelper.FakeFieldArray(ids);
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(score, value);
        }
    }
}