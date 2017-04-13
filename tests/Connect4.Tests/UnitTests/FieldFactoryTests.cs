using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class FieldFactoryTests
    {
        [Test]
        public void CreateFieldRowTest()
        {
            IFieldFactory fieldFactory = new FieldFactory();
            IField field = fieldFactory.Create(2, 3);
            Assert.AreEqual(2, field.Row);
        }

        [Test]
        public void CreateFieldColumnTest()
        {
            IFieldFactory fieldFactory = new FieldFactory();
            IField field = fieldFactory.Create(2, 3);
            Assert.AreEqual(3, field.Column);
        }

        [Test]
        public void CreateFieldWithPlayerTest()
        {
            IFieldFactory fieldFactory = new FieldFactory();
            IField field = fieldFactory.Create(0, 0);
            Assert.AreEqual(0, field.PlayerId);
        }
    }
}
