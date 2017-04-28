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
        public int Height { get { return (Fields != null ? Fields.GetLength(0) : 0); } }
        public int Width { get { return (Fields != null ? Fields.GetLength(1) : 0); } }

        public Board(IField[,] fields)
        {
            Requires.IsNotNull(fields, "fields");
            Requires.IsArrayContentNotNull(fields, "fields");
            Fields = fields;
        }

        public bool IsColumnFull(int column)
        {
            return Fields.Cast<IField>().Take(7).ToList()[column].PlayerId != 0;
        }

        public bool IsBoardFull()
        {
            for (int i = 0; i < Width; i++)
            {
                if (!IsColumnFull(i)) return false;
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
            if (Fields[row, column].PlayerId != 0) throw new BoardException($"The field [row={row},column={column}] is already occupied by player={playerId}");

            Fields[row, column].PlayerId = playerId;
        }

        public void RemoveChip(int row, int column)
        {
            if (Fields[row, column].PlayerId == 0) throw new BoardException($"The field [row={row},column={column}] is already empty");

            Fields[row, column].PlayerId = 0;
        }

        public bool IsChipConnected(int row, int column)
        {
            int playerId = Fields[row, column].PlayerId;
            int inARow = 1;

            // Vertically
            // --------------------------------------------
            for (int i = row - 1; i >= 0; i--)
            {
                if (Fields[i, column].PlayerId != playerId) break;
                inARow++;
            }
            for (int i = row + 1; i < Height; i++)
            {
                if (Fields[i, column].PlayerId != playerId) break;
                inARow++;
            }

            if (inARow >= 4) return true;
            inARow = 1;

            // Horizontally
            // --------------------------------------------
            for (int i = column - 1; i >= 0; i--)
            {
                if (Fields[row, i].PlayerId != playerId) break;
                inARow++;
            }
            for (int i = column + 1; i < Width; i++)
            {
                if (Fields[row, i].PlayerId != playerId) break;
                inARow++;
            }

            if (inARow >= 4) return true;
            inARow = 1;

            // Diagonally - Northwest to Southeast
            // --------------------------------------------

            // Northwest
            for (int i = 1; i <= Math.Min(row, column); i++)
            {
                if (Fields[row - i, column - i].PlayerId != playerId) break;
                inARow++;
            }
            // Southeast
            for (int i = 1; i < Math.Min(Height - row, Width - column); i++)
            {
                if (Fields[row + i, column + i].PlayerId != playerId) break;
                inARow++;
            }

            if (inARow >= 4) return true;
            inARow = 1;

            // Diagonally - Southwest to Northeast  
            // --------------------------------------------

            // Southwest
            for (int i = 1; i <= Math.Min(Height - row - 1, column); i++)
            {
                if (Fields[row + i, column - i].PlayerId != playerId) break;
                inARow++;
            }
            // Northeast
            for (int i = 1; i <= Math.Min(row, Width - column - 1); i++)
            {
                if (Fields[row - i, column + i].PlayerId != playerId) break;
                inARow++;
            }

            if (inARow >= 4) return true;
            return false;
        }

        public List<IField> GetConnectedChips(int row, int column)
        {
            int playerId = Fields[row, column].PlayerId;

            List<IField> tempConnectedFields = new List<IField>();
            List<IField> allConnectedFields = new List<IField>();
            allConnectedFields.Add(Fields[row, column]);

            // Vertically
            // --------------------------------------------
            for (int i = row - 1; i >= 0; i--)
            {
                if (Fields[i, column].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[i, column]);
            }
            for (int i = row + 1; i < Height; i++)
            {
                if (Fields[i, column].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[i, column]);
            }

            if (tempConnectedFields.Count >= 3) allConnectedFields.AddRange(tempConnectedFields);
            tempConnectedFields.Clear();

            // Horizontally
            // --------------------------------------------
            for (int i = column - 1; i >= 0; i--)
            {
                if (Fields[row, i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row, i]);
            }
            for (int i = column + 1; i < Width; i++)
            {
                if (Fields[row, i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row, i]);
            }

            if (tempConnectedFields.Count >= 3) allConnectedFields.AddRange(tempConnectedFields);
            tempConnectedFields.Clear();

            // Diagonally - Northwest to Southeast
            // --------------------------------------------

            // Northwest
            for (int i = 1; i <= Math.Min(row, column); i++)
            {
                if (Fields[row - i, column - i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row - i, column - i]);
            }
            // Southeast
            for (int i = 1; i < Math.Min(Height - row, Width - column); i++)
            {
                if (Fields[row + i, column + i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row + i, column + i]);
            }

            if (tempConnectedFields.Count >= 3) allConnectedFields.AddRange(tempConnectedFields);
            tempConnectedFields.Clear();

            // Diagonally - Southwest to Northeast  
            // --------------------------------------------

            // Southwest
            for (int i = 1; i <= Math.Min(Height - row - 1, column); i++)
            {
                if (Fields[row + i, column - i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row + i, column - i]);
            }
            // Northeast
            for (int i = 1; i <= Math.Min(row, Width - column - 1); i++)
            {
                if (Fields[row - i, column + i].PlayerId != playerId) break;
                tempConnectedFields.Add(Fields[row - i, column + i]);
            }

            if (tempConnectedFields.Count >= 3) allConnectedFields.AddRange(tempConnectedFields);
            tempConnectedFields.Clear();

            return allConnectedFields;
        }

        public List<IField[]> PossibleFours
        {
            get
            {
                List<IField[]> list = new List<IField[]>()
                {
                    // Vertical Fours
                    new IField[4] { Fields[0, 0], Fields[1, 0], Fields[2, 0], Fields[3, 0] },
                    new IField[4] { Fields[1, 0], Fields[2, 0], Fields[3, 0], Fields[4, 0] },
                    new IField[4] { Fields[2, 0], Fields[3, 0], Fields[4, 0], Fields[5, 0] },
                    new IField[4] { Fields[0, 1], Fields[1, 1], Fields[2, 1], Fields[3, 1] },
                    new IField[4] { Fields[1, 1], Fields[2, 1], Fields[3, 1], Fields[4, 1] },
                    new IField[4] { Fields[2, 1], Fields[3, 1], Fields[4, 1], Fields[5, 1] },
                    new IField[4] { Fields[0, 2], Fields[1, 2], Fields[2, 2], Fields[3, 2] },
                    new IField[4] { Fields[1, 2], Fields[2, 2], Fields[3, 2], Fields[4, 2] },
                    new IField[4] { Fields[2, 2], Fields[3, 2], Fields[4, 2], Fields[5, 2] },
                    new IField[4] { Fields[0, 3], Fields[1, 3], Fields[2, 3], Fields[3, 3] },
                    new IField[4] { Fields[1, 3], Fields[2, 3], Fields[3, 3], Fields[4, 3] },
                    new IField[4] { Fields[2, 3], Fields[3, 3], Fields[4, 3], Fields[5, 3] },
                    new IField[4] { Fields[0, 4], Fields[1, 4], Fields[2, 4], Fields[3, 4] },
                    new IField[4] { Fields[1, 4], Fields[2, 4], Fields[3, 4], Fields[4, 4] },
                    new IField[4] { Fields[2, 4], Fields[3, 4], Fields[4, 4], Fields[5, 4] },
                    new IField[4] { Fields[0, 5], Fields[1, 5], Fields[2, 5], Fields[3, 5] },
                    new IField[4] { Fields[1, 5], Fields[2, 5], Fields[3, 5], Fields[4, 5] },
                    new IField[4] { Fields[2, 5], Fields[3, 5], Fields[4, 5], Fields[5, 5] },
                    new IField[4] { Fields[0, 6], Fields[1, 6], Fields[2, 6], Fields[3, 6] },
                    new IField[4] { Fields[1, 6], Fields[2, 6], Fields[3, 6], Fields[4, 6] },
                    new IField[4] { Fields[2, 6], Fields[3, 6], Fields[4, 6], Fields[5, 6] },

                    // Horizontal Fours
                    new IField[4] { Fields[0, 0], Fields[0, 1], Fields[0, 2], Fields[0, 3] },
                    new IField[4] { Fields[0, 1], Fields[0, 2], Fields[0, 3], Fields[0, 4] },
                    new IField[4] { Fields[0, 2], Fields[0, 3], Fields[0, 4], Fields[0, 5] },
                    new IField[4] { Fields[0, 3], Fields[0, 4], Fields[0, 5], Fields[0, 6] },
                    new IField[4] { Fields[1, 0], Fields[1, 1], Fields[1, 2], Fields[1, 3] },
                    new IField[4] { Fields[1, 1], Fields[1, 2], Fields[1, 3], Fields[1, 4] },
                    new IField[4] { Fields[1, 2], Fields[1, 3], Fields[1, 4], Fields[1, 5] },
                    new IField[4] { Fields[1, 3], Fields[1, 4], Fields[1, 5], Fields[1, 6] },
                    new IField[4] { Fields[2, 0], Fields[2, 1], Fields[2, 2], Fields[2, 3] },
                    new IField[4] { Fields[2, 1], Fields[2, 2], Fields[2, 3], Fields[2, 4] },
                    new IField[4] { Fields[2, 2], Fields[2, 3], Fields[2, 4], Fields[2, 5] },
                    new IField[4] { Fields[2, 3], Fields[2, 4], Fields[2, 5], Fields[2, 6] },
                    new IField[4] { Fields[3, 0], Fields[3, 1], Fields[3, 2], Fields[3, 3] },
                    new IField[4] { Fields[3, 1], Fields[3, 2], Fields[3, 3], Fields[3, 4] },
                    new IField[4] { Fields[3, 2], Fields[3, 3], Fields[3, 4], Fields[3, 5] },
                    new IField[4] { Fields[3, 3], Fields[3, 4], Fields[3, 5], Fields[3, 6] },
                    new IField[4] { Fields[4, 0], Fields[4, 1], Fields[4, 2], Fields[4, 3] },
                    new IField[4] { Fields[4, 1], Fields[4, 2], Fields[4, 3], Fields[4, 4] },
                    new IField[4] { Fields[4, 2], Fields[4, 3], Fields[4, 4], Fields[4, 5] },
                    new IField[4] { Fields[4, 3], Fields[4, 4], Fields[4, 5], Fields[4, 6] },
                    new IField[4] { Fields[5, 0], Fields[5, 1], Fields[5, 2], Fields[5, 3] },
                    new IField[4] { Fields[5, 1], Fields[5, 2], Fields[5, 3], Fields[5, 4] },
                    new IField[4] { Fields[5, 2], Fields[5, 3], Fields[5, 4], Fields[5, 5] },
                    new IField[4] { Fields[5, 3], Fields[5, 4], Fields[5, 5], Fields[5, 6] },

                    // Diagonal /
                    new IField[4] { Fields[3, 0], Fields[2, 1], Fields[1, 2], Fields[0, 3] }, //4
                    new IField[4] { Fields[4, 0], Fields[3, 1], Fields[2, 2], Fields[1, 3] }, //5
                    new IField[4] { Fields[3, 1], Fields[2, 2], Fields[1, 3], Fields[0, 4] }, //5
                    new IField[4] { Fields[5, 0], Fields[4, 1], Fields[3, 2], Fields[2, 3] }, //6
                    new IField[4] { Fields[4, 1], Fields[3, 2], Fields[2, 3], Fields[1, 4] }, //6
                    new IField[4] { Fields[3, 2], Fields[2, 3], Fields[1, 4], Fields[0, 5] }, //6
                    new IField[4] { Fields[5, 1], Fields[4, 2], Fields[3, 3], Fields[2, 4] }, //6
                    new IField[4] { Fields[4, 2], Fields[3, 3], Fields[2, 4], Fields[1, 5] }, //6
                    new IField[4] { Fields[3, 3], Fields[2, 4], Fields[1, 5], Fields[0, 6] }, //6
                    new IField[4] { Fields[5, 2], Fields[4, 3], Fields[3, 4], Fields[2, 5] }, //5
                    new IField[4] { Fields[4, 3], Fields[3, 4], Fields[2, 5], Fields[1, 6] }, //5
                    new IField[4] { Fields[5, 3], Fields[4, 4], Fields[3, 5], Fields[2, 6] }, //4

                    // Diagonal \
                    new IField[4] { Fields[2, 0], Fields[3, 1], Fields[4, 2], Fields[5, 3] }, //4
                    new IField[4] { Fields[1, 0], Fields[2, 1], Fields[3, 2], Fields[4, 3] }, //5
                    new IField[4] { Fields[2, 1], Fields[3, 2], Fields[4, 3], Fields[5, 4] }, //5
                    new IField[4] { Fields[0, 0], Fields[1, 1], Fields[2, 2], Fields[3, 3] }, //6
                    new IField[4] { Fields[1, 1], Fields[2, 2], Fields[3, 3], Fields[4, 4] }, //6
                    new IField[4] { Fields[2, 2], Fields[3, 3], Fields[4, 4], Fields[5, 5] }, //6
                    new IField[4] { Fields[0, 1], Fields[1, 2], Fields[2, 3], Fields[3, 4] }, //6
                    new IField[4] { Fields[1, 2], Fields[2, 3], Fields[3, 4], Fields[4, 5] }, //6
                    new IField[4] { Fields[2, 3], Fields[3, 4], Fields[4, 5], Fields[5, 6] }, //6
                    new IField[4] { Fields[0, 2], Fields[1, 3], Fields[2, 4], Fields[3, 5] }, //5
                    new IField[4] { Fields[1, 3], Fields[2, 4], Fields[3, 5], Fields[4, 5] }, //5
                    new IField[4] { Fields[0, 3], Fields[1, 4], Fields[2, 5], Fields[3, 5] }, //4
                };
                return list;
            }
        }
    }
}