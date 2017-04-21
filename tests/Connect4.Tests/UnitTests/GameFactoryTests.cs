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
        public void CreateGameTest()
        {
            List<IPlayer> players = new List<IPlayer>();
            players.Add(TestHelper.MockPlayer(1));
            players.Add(TestHelper.MockPlayer(2));

            IBoardFactory boardFactory = TestHelper.MockBoardFactory();


            IGameFactory gameFactory = new GameFactory(boardFactory);

            IGame game = gameFactory.Create(players);
            bool result = game.TryMove(0);
            Assert.AreEqual(true, result);
        }
    }




}
