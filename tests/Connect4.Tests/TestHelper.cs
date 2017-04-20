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
        public static IField MockField(int row, int column, int playerId)
        {
            var mock = new Mock<IField>();
            mock.SetupAllProperties();
            mock.Object.Column = column;
            mock.Object.Row = row;
            mock.Object.PlayerId = playerId;
            return mock.Object;
        }

        public static IField[,] MockEmptyFieldArray(int rows, int columns)
        {
            IField[,] fields = new IField[rows, columns];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = MockField(i, j, 0);
                }
            }
            return fields;
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

        public static IBoard MockEmptyBoard()
        {
            var mock = new Mock<IBoard>();
            return mock.Object;
        }

        public static IPlayer MockPlayer(int id)
        {
            var mock = new Mock<IPlayer>();
            return mock.Object;
        }
    }
}
