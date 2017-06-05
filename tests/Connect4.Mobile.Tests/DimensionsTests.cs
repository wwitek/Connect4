using Connect4.Mobile.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.Tests
{
    [TestFixture]
    public class DimensionsTests
    {
        // Sony Xperia Z5 Premium
        [TestCase(2160, 3840)]

        // Samsung Galaxy Note 5, Samsung Galaxy S6, Huawei Nexus 6P, LG G5
        [TestCase(1440, 2560)]

        // ??
        [TestCase(1200, 1920)]

        // iPhone 6S Plus, iPhone 6 Plus, iPhone 7 Plus, Huawei P9, Sony Xperia Z5, Samsung Galaxy A5, Samsung Galaxy A7, Samsung Galaxy S5, Samsung Galaxy A9, HTC One M9, Sony Xperia M5
        [TestCase(540, 960)]
        [TestCase(1080, 1920)]

        // iPhone 6, iPhone 6S, iPhone 7
        [TestCase(375, 667)]
        [TestCase(750, 1334)]

        // Samsung Galaxy J5, Samsung Galaxy J3, Moto G4 Play, Xiaomi Redmi 3, Moto G 3rd Gen, Sony Xperia M4 Aqua
        [TestCase(720, 1280)]

        // iPhone 5, iPhone 5S, iPhone 5C, iPhone SE
        [TestCase(320, 568)]
        [TestCase(640, 1136)]

        // iPhone 4, iPhone 4S
        [TestCase(320, 480)]
        [TestCase(640, 960)]

        // Samsung Galaxy J2, Moto E 2nd Gen, Sony Xperia E4, HTC Desire 526
        [TestCase(540, 960)]

        // Huawei Y635, Nokia Lumia 635, Sony Xperia E3
        [TestCase(480, 854)]

        // Samsung Galaxy J1 (2016), Samsung Z1, Samsung Z2, Lumia 435, Alcatel Pixi 4, LG Joy, ZTE Blade G
        [TestCase(480, 800)]

        // Alcatel pixi 3, LG Wine Smart
        [TestCase(320, 480)]

        // Nokia 230, Nokia 215, Samsung Xcover 550, LG G350
        [TestCase(240, 320)]
        public void CreateDimensionsTest(int width, int height)
        {
            List<Tuple<int, int>> boardDimensions = new List<Tuple<int, int>>()
            {
                new Tuple<int, int>(5,5),
                new Tuple<int, int>(6,5),
                new Tuple<int, int>(7,6),
                new Tuple<int, int>(10,10),
                new Tuple<int, int>(15,15),
                new Tuple<int, int>(20,20),
            };

            foreach(var board in boardDimensions)
            {
                Dimensions dimensions = new Dimensions(width, height, board.Item1, board.Item2);
            }
        }
    }
}
