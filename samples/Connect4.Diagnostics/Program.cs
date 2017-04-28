﻿using Connect4.API;
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
            List<TestSample> samples = new List<TestSample>()
            {
                new TestSample(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 2, 0, 0, 2, 0, 0},
                    {2, 2, 0, 0, 1, 2, 0},
                    {1, 1, 0, 0, 1, 1, 0}
                }, 2),
                new TestSample(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 0, 0, 0},
                    {2, 0, 0, 0, 0, 0, 0},
                    {1, 1, 1, 2, 0, 2, 0}
                }, 1),
                new TestSample(new int[,]
                {
                    {0, 0, 0, 0, 0, 0, 0},
                    {0, 0, 0, 0, 1, 0, 2},
                    {0, 0, 0, 2, 2, 0, 1},
                    {0, 0, 0, 2, 1, 1, 1},
                    {0, 0, 0, 1, 2, 1, 2},
                    {0, 1, 2, 2, 1, 2, 2}
                }, 0),
            };

            int testCounter = 0;
            foreach(var sample in samples)
            {
                long totalTime = 0;
                for(int i=0;i<10;i++)
                {
                    long oneTestTime = diagnostics.Test(sample.Ids, sample.NextPlayerMove);
                    totalTime += oneTestTime;
                }
                Console.WriteLine($"Test { ++testCounter }) Time={ Convert.ToDouble((totalTime / 10)) }ms");
            }
            Console.ReadKey();
        }
    }
}
