using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Diagnostics
{
    public class TestSample
    {
        public int[,] Ids { get; }
        public int NextPlayerMove { get; }

        public TestSample(int[,] ids, int nextPlayerMove)
        {
            Ids = ids;
            NextPlayerMove = nextPlayerMove;
        }
    }
}