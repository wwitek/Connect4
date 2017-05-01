using Connect4.API;
using Connect4.Domain.Factories;
using Connect4.Domain.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.Enums;
using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Entities.Players;
using System.Diagnostics;

namespace Connect4.Diagnostics
{
    class Program
    {
        static void Main(string[] args)
        {
            Diagnostics diagnostics = new Diagnostics();
            List<long> times = new List<long>();
            double avg = 0;

            int count = 5;

            times = diagnostics.BotMoveTest(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 2, 0, 0, 2, 0, 0},
                {2, 2, 0, 0, 1, 2, 0},
                {1, 1, 1, 0, 1, 1, 0}
            }, count);
            avg = times.Average();
            Console.WriteLine($"Avg Time={ Math.Round(avg, 0) }ml");

            times = diagnostics.BotMoveTest(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {2, 1, 0, 0, 0, 0, 0},
                {1, 1, 1, 2, 0, 2, 0}
            }, count);
            avg = times.Average();
            Console.WriteLine($"Avg Time={ Math.Round(avg, 0) }ml");

            times = diagnostics.BotMoveTest(new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 1, 0, 2},
                {0, 0, 0, 2, 2, 0, 1},
                {0, 0, 0, 2, 1, 1, 1},
                {0, 0, 0, 1, 2, 1, 2},
                {1, 1, 2, 2, 1, 2, 2}
            }, count);
            avg = times.Average();
            Console.WriteLine($"Avg Time={ Math.Round(avg, 0) }ml");
            Console.ReadKey();
        }
    }
}
