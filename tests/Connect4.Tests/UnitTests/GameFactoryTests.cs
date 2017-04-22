using Connect4.Domain.Enums;
using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
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
    public class GameFactoryTests
    {
        [Test]
        public void CreateGameFactoryTest()
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.FakeLocalPlayer(1));
            players.Add(TestHelper.FakeLocalPlayer(2));

            var mockedBoardFactory = new Mock<IBoardFactory>();
            
            IGameFactory gameFactory = new GameFactory(mockedBoardFactory.Object);
            gameFactory.Create(players);

            mockedBoardFactory.Verify(m => m.Create(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}