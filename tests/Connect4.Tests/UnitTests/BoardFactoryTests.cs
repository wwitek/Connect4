using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using NUnit.Framework;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class BoardFactoryTests
    {
        [Test]
        public void CreateBoardTest()
        {
            IFieldFactory fieldFactory = new FieldFactory();
            IBoardFactory boardFactory = new BoardFactory(fieldFactory);

            IBoard board = boardFactory.Create(6,7); 
        }
    }
}