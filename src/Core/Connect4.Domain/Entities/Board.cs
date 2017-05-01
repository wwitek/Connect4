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
        private IField[,] _linesOfFour;
        private IField[,] _linesOfFive;
        private IField[,] _linesOfSix;
        private IField[,] _linesOfSeven;
        private IField[,] LinesOfFour
        {
            get
            {
                if (_linesOfFour != null) return _linesOfFour;
                IField[,] list = {
                    { Fields[3, 0], Fields[2, 1], Fields[1, 2], Fields[0, 3] },
                    { Fields[5, 3], Fields[4, 4], Fields[3, 5], Fields[2, 6] },
                    { Fields[2, 0], Fields[3, 1], Fields[4, 2], Fields[5, 3] },
                    { Fields[0, 3], Fields[1, 4], Fields[2, 5], Fields[3, 5] }
                };
                _linesOfFour = list;
                return list;
            }
        }
        private IField[,] LinesOfFive
        {
            get
            {
                if (_linesOfFive != null) return _linesOfFive;
                IField[,] list = {
                    // Diagonal /
                    { Fields[4, 0], Fields[3, 1], Fields[2, 2], Fields[1, 3], Fields[0, 4] },
                    { Fields[5, 2], Fields[4, 3], Fields[3, 4], Fields[2, 5], Fields[1, 6] },
                    // Diagonal \
                    { Fields[1, 0], Fields[2, 1], Fields[3, 2], Fields[4, 3], Fields[5, 4] },
                    { Fields[0, 2], Fields[1, 3], Fields[2, 4], Fields[3, 5], Fields[4, 5] }
                };
                _linesOfFive = list;
                return list;
            }
        }
        private IField[,] LinesOfSix
        {
            get
            {
                if (_linesOfSix != null) return _linesOfSix;
                IField[,] list = {
                    // Vertical Fours
                    { Fields[0, 0], Fields[1, 0], Fields[2, 0], Fields[3, 0], Fields[4, 0], Fields[5, 0] },
                    { Fields[0, 1], Fields[1, 1], Fields[2, 1], Fields[3, 1], Fields[4, 1], Fields[5, 1] },
                    { Fields[0, 2], Fields[1, 2], Fields[2, 2], Fields[3, 2], Fields[4, 2], Fields[5, 2] },
                    { Fields[0, 3], Fields[1, 3], Fields[2, 3], Fields[3, 3], Fields[4, 3], Fields[5, 3] },
                    { Fields[0, 4], Fields[1, 4], Fields[2, 4], Fields[3, 4], Fields[4, 4], Fields[5, 4] },
                    { Fields[0, 5], Fields[1, 5], Fields[2, 5], Fields[3, 5], Fields[4, 5], Fields[5, 5] },
                    { Fields[0, 6], Fields[1, 6], Fields[2, 6], Fields[3, 6], Fields[4, 6], Fields[5, 6] },
                    // Diagonal /
                    { Fields[5, 0], Fields[4, 1], Fields[3, 2], Fields[2, 3], Fields[1, 4], Fields[0, 5] },
                    { Fields[5, 1], Fields[4, 2], Fields[3, 3], Fields[2, 4], Fields[1, 5], Fields[0, 6] },
                    // Diagonal \
                    { Fields[0, 0], Fields[1, 1], Fields[2, 2], Fields[3, 3], Fields[4, 4], Fields[5, 5] },
                    { Fields[0, 1], Fields[1, 2], Fields[2, 3], Fields[3, 4], Fields[4, 5], Fields[5, 6] }
                };
                _linesOfSix = list;
                return list;
            }
        }
        private IField[,] LinesOfSeven
        {
            get
            {
                if (_linesOfSeven != null) return _linesOfSeven;
                IField[,] list = {
                    // Horizontal Fours
                    { Fields[0, 0], Fields[0, 1], Fields[0, 2], Fields[0, 3], Fields[0, 4], Fields[0, 5], Fields[0, 6] },
                    { Fields[1, 0], Fields[1, 1], Fields[1, 2], Fields[1, 3], Fields[1, 4], Fields[1, 5], Fields[1, 6] },
                    { Fields[2, 0], Fields[2, 1], Fields[2, 2], Fields[2, 3], Fields[2, 4], Fields[2, 5], Fields[2, 6] },
                    { Fields[3, 0], Fields[3, 1], Fields[3, 2], Fields[3, 3], Fields[3, 4], Fields[3, 5], Fields[3, 6] },
                    { Fields[4, 0], Fields[4, 1], Fields[4, 2], Fields[4, 3], Fields[4, 4], Fields[4, 5], Fields[4, 6] },
                    { Fields[5, 0], Fields[5, 1], Fields[5, 2], Fields[5, 3], Fields[5, 4], Fields[5, 5], Fields[5, 6] },
                };
                _linesOfSeven = list;
                return list;
            }
        }

        public IField[,] Fields { get; }
        public int Height => Fields?.GetLength(0) ?? 0;
        public int Width => Fields?.GetLength(1) ?? 0;

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

        public List<int> GetEvaluationIds(int rowLength)
        {
            List<int> resultList = new List<int>();

            switch (rowLength)
            {
                case 4:
                    for (int i = 0; i < 4; i++)
                    {
                        int id = LinesOfFour[i, 0].PlayerId * 1000 +
                                 LinesOfFour[i, 1].PlayerId * 100 +
                                 LinesOfFour[i, 2].PlayerId * 10 +
                                 LinesOfFour[i, 3].PlayerId;
                        resultList.Add(id);
                    }
                    return resultList;
                case 5:
                    for (int i = 0; i < 4; i++)
                    {
                        int id = LinesOfFive[i, 0].PlayerId * 10000 +
                                 LinesOfFive[i, 1].PlayerId * 1000 +
                                 LinesOfFive[i, 2].PlayerId * 100 +
                                 LinesOfFive[i, 3].PlayerId * 10 +
                                 LinesOfFive[i, 4].PlayerId;
                        resultList.Add(id);
                    }
                    return resultList;
                case 6:
                    for (int i = 0; i < 11; i++)
                    {
                        int id = LinesOfSix[i, 0].PlayerId * 100000 +
                                 LinesOfSix[i, 1].PlayerId * 10000 +
                                 LinesOfSix[i, 2].PlayerId * 1000 +
                                 LinesOfSix[i, 3].PlayerId * 100 +
                                 LinesOfSix[i, 4].PlayerId * 10 +
                                 LinesOfSix[i, 5].PlayerId;
                        resultList.Add(id);
                    }
                    return resultList;
                case 7:
                    for (int i = 0; i < 6; i++)
                    {
                        int id = LinesOfSeven[i, 0].PlayerId * 1000000 +
                                 LinesOfSeven[i, 1].PlayerId * 100000 +
                                 LinesOfSeven[i, 2].PlayerId * 10000 +
                                 LinesOfSeven[i, 3].PlayerId * 1000 +
                                 LinesOfSeven[i, 4].PlayerId * 100 +
                                 LinesOfSeven[i, 5].PlayerId * 10 +
                                 LinesOfSeven[i, 6].PlayerId;
                        resultList.Add(id);
                    }
                    return resultList;
            }
            return null;
        }
    }
}