using Connect4.Domain.Exceptions;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;
using Connect4.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Board : IBoard
    {
        private IField[,] Fields { get; }
        private int Height { get { return (Fields != null ? Fields.GetLength(0) : 0); } }
        private int Width { get { return (Fields != null ? Fields.GetLength(1) : 0); } }

        public Board(IField[,] fields)
        {
            Requires.IsNotNull(fields, "fields");
            Requires.IsArrayContentNotNull(fields, "fields");
            Fields = fields;
        }

        public void Reset()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Fields[i, j].Reset();
                }
            }
        }

        public bool IsColumnFull(int column)
        {
            for (int i = 0; i < Height; i++)
            {
                if (Fields[i, column].PlayerId == 0) return false;
            }
            return true;
        }

        public int GetLowestEmptyRow(int column)
        {
            for(int i = Height - 1; i >= 0; i--)
            {
                if (Fields[i,column].PlayerId == 0)
                {
                    return i;
                }
            }
            throw new BoardException("Cannot get the lowest empty row for this column. The column is full already");
        }

        public void InsertChip(int row, int column, int playerId)
        {
            if (IsColumnFull(column)) throw new BoardException("Cannot insert chip into this column. It's full already");
            Fields[row, column].PlayerId = playerId;
        }

        public bool IsChipConnected(int row, int column)
        {
            return false;
        }

        public List<IField> GetConnectedChips(int row, int column)
        {
            return null;
        }

        public override string ToString()
        {
            string toReturn = "";
            string line = "";
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    line += (Fields[i, j].PlayerId).ToString() + " ";
                }
                toReturn += line + Environment.NewLine;
                Debug.WriteLine(line);
                line = "";
            }
            return toReturn;
        }
    }
}