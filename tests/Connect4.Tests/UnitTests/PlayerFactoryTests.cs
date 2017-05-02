using Connect4.Domain.Enums;
using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.AI;
using Moq;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class PlayerFactoryTests
    {
        [Test]
        public void CreateLocalPlayer_Id_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Local, 1);
            Assert.AreEqual(1, player.Id);
        }

        [Test]
        public void CreateBotPlayer_Id_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Bot, 2);
            Assert.AreEqual(2, player.Id);
        }

        [Test]
        public void CreateOnlinePlayer_Id_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Online, 33);
            Assert.AreEqual(33, player.Id);
        }

        [Test]
        public void CreateLocalPlayer_AllowUserInteraction_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Local, 1);
            Assert.AreEqual(true, player.AllowUserInteraction);
        }

        [Test]
        public void CreateBotPlayer_AllowUserInteraction_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Bot, 1);
            Assert.AreEqual(false, player.AllowUserInteraction);
        }

        [Test]
        public void CreateOnlinePlayer_AllowUserInteraction_Test()
        {
            IterativeDeepeningSearch iterativeDeepeningSearchStub = TestHelper.FakeIterativeDeepeningSearch();

            IPlayerFactory playerFactory = new PlayerFactory(iterativeDeepeningSearchStub);
            IPlayer player = playerFactory.Create(PlayerType.Online, 1);
            Assert.AreEqual(false, player.AllowUserInteraction);
        }
    }
}
