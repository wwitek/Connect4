using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Interfaces
{
    public interface IBoard
    {
        bool IsColumnFull(int column);
        bool IsBoardFull();
        int GetLowestEmptyRow(int column);
        void InsertChip(int row, int column, int player);
        bool IsChipConnected(int row, int column);
        List<IField> GetConnectedChips(int row, int column);
    }
}