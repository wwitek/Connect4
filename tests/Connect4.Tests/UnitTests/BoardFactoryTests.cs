using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Moq;
using NUnit.Framework;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class BoardFactoryTests
    {
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(6, 7)]
        [TestCase(10, 5)]
        public void BoardFactory_CreateBoardTest(int rows, int columns)
        {
            var fieldStub = new Mock<IField>();

            var mockedFieldFactory = new Mock<IFieldFactory>();
            mockedFieldFactory.SetReturnsDefault<IField>(fieldStub.Object);

            IBoardFactory boardFactory = new BoardFactory(mockedFieldFactory.Object);
            boardFactory.Create(rows, columns);
            mockedFieldFactory.Verify(m => m.Create(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(rows * columns));
        }
    }
}