using Connect4.Domain.Entities;
using Connect4.Domain.Entities.Players;
using Connect4.Domain.Factories;
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
        public static IField FakeField(int row, int column, int playerId)
        {
            var mock = new Mock<IField>();
            mock.SetupAllProperties();
            mock.Object.Column = column;
            mock.Object.Row = row;
            mock.Object.PlayerId = playerId;
            return mock.Object;
        }

        public static IField[,] FakeEmptyFieldArray(int rows, int columns)
        {
            IField[,] fields = new IField[rows, columns];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = FakeField(i, j, 0);
                }
            }
            return fields;
        }

        public static IField[,] FakeFieldArray(int[,] ids)
        {
            IField[,] fields = new IField[ids.GetLength(0), ids.GetLength(1)];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = FakeField(i, j, ids[i, j]);
                }
            }
            return fields;
        }

        public static IBoard FakeEmptyBoard()
        {
            IField[,] fieldStubs = FakeEmptyFieldArray(6, 7);
            var fakeBoard = new Mock<Board>(fieldStubs);
            return fakeBoard.Object;
        }

        public static IPlayer FakeLocalPlayer(int id)
        {
            var mock = new Mock<LocalPlayer>(id) { CallBase = true };
            return mock.Object;
        }
    }
}
