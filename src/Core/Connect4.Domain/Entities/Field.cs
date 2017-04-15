using Connect4.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Entities
{
    public class Field : IField
    {
        public int PlayerId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Field(int row, int column)
        {
            Row = row;
            Column = column;
            PlayerId = 0;
        }

        public void Reset()
        {
            PlayerId = 0;
        }
    }
}
