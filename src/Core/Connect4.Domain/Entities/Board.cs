using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Connect4.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Board : IBoard
    {
        private IField[,] Fields { get; }
        private int Width { get; }
        private int Height { get; }

        public Board(IField[,] fields)
        {
            Requires.IsNotNull(fields, "fields");
            Requires.IsArrayContentNotNull(fields, "fields");

            Fields = fields;
            Width = fields.GetLength(0);
            Height = fields.GetLength(1);
        }

        public void Reset()
        {
            for (int i = 0; i < Fields.GetLength(0); i++)
            {
                for (int j = 0; j < Fields.GetLength(1); j++)
                {
                    Fields[i, j].PlayerId = 0;
                }
            }
        }

        public bool IsColumnFull(int column)
        {
            return false;
        }

        public int DropChip(int column, int player)
        {
            return 0;
        }

        public bool IsChipConnected(int row, int column)
        {
            return false;
        }

        public List<IField> GetConnectedChips(int row, int column)
        {
            return null;
        }
    }
}