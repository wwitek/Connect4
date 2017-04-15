using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests.UnitTests
{
    [TestFixture]
    public class FieldTests
    {
        [Test]
        public void ResetFieldTest()
        {
            IField field = new Field(0, 0);
            field.PlayerId = 1;
            field.Reset();
            Assert.AreEqual(0, field.PlayerId);
        }

        [Test]
        public void IsEmpty_TrueTest()
        {
            IField field = new Field(0, 0);
            Assert.AreEqual(true, field.PlayerId == 0);
        }

        [Test]
        public void IsEmpty_FalseTest()
        {
            IField field = new Field(0, 0);
            field.PlayerId = 1;
            Assert.AreEqual(false, field.PlayerId == 0);
        }

        [Test]
        public void IsOwnedBy_TrueTest()
        {
            IField field = new Field(0, 0);
            field.PlayerId = 1;
            Assert.AreEqual(true, field.PlayerId == 1);
        }

        [Test]
        public void IsOwnedBy_FalseTest()
        {
            IField field = new Field(0, 0);
            field.PlayerId = 1;
            Assert.AreEqual(false, field.PlayerId == 2);
        }
    }
}