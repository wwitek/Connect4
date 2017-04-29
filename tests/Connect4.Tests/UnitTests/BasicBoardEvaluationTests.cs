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
        [Test]
        public void EvaluateZeroTest()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void EvaluateOneTest()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(3, value);
        }

        [Test]
        public void Evaluate_11_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(11, value);
        }

        [Test]
        public void Evaluate_119_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(119, value);
        }

        [Test]
        public void Evaluate_Win_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0},
                        {2, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(10120, value);
        }

        [Test]
        public void EvaluateOpponentOneTest()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(-6, value);
        }

        [Test]
        public void Evaluate_Opponent_18_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(-18, value);
        }

        [Test]
        public void Evaluate_Opponent_230_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(-230, value);
        }

        [Test]
        public void Evaluate_OpponentWin_Test()
        {
            var fieldStubs = TestHelper.FakeFieldArray(new int[,]
            {
                        {0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0}
            });
            var boardStub = new Mock<Board>(fieldStubs) { CallBase = true };

            BasicBoardEvaluation evaluation = new BasicBoardEvaluation();
            int value = evaluation.Evaluate(boardStub.Object, 2, 1);
            Assert.AreEqual(-15232, value);
        }
    }
}
