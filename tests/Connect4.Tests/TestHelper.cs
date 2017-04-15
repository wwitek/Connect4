using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Tests
{
    public static class TestHelper
    {
        public static IField MockField(int x, int y, int playerId)
        {
            var mock = new Mock<IField>();
            mock.Setup(m => m.Column).Returns(x);
            mock.Setup(m => m.Row).Returns(y);
            mock.Setup(m => m.PlayerId).Returns(playerId);
            return mock.Object;
        }

        public static IField[,] MockFieldArray(int[,] ids)
        {
            IField[,] fields = new IField[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = MockField(i, j, ids[i, j]);
                }
            }
            return fields;
        }
    }
}
