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

            List<int[,]> data = new List<int[,]>
            {
                new int[,]{
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 2, 0, 0, 2, 0, 0},
                    {2, 2, 0, 0, 1, 2, 0},
                    {1, 1, 1, 0, 1, 1, 0}
                },
                new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 1, 0, 0, 0, 0, 0},
                    {1, 1, 1, 2, 0, 2, 0}
                },
                new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 2},
                    {0, 0, 0, 2, 2, 0, 1},
                    {0, 0, 0, 2, 1, 1, 1},
                    {0, 0, 0, 1, 2, 1, 2},
                    {1, 1, 2, 2, 1, 2, 2}
                }
            };


            List<long> times = new List<long>();
            int count = 5;
            foreach (var ids in data)
            {
                times = diagnostics.AlphaBetaTest(ids, count);
                Console.WriteLine($"[AlphaBeta] t={ Math.Round(times.Average(), 0) }ml");

                times.Clear();

                times = diagnostics.IterativeDeepeningSearchTest(ids, count);
                Console.WriteLine($"[IterativeDeepening] t={ Math.Round(times.Average(), 0) }ml");

                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}