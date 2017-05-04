using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;

namespace Connect4.Tests
{
    public static class BoardExtensions
    {
        public static List<IField[]> GetPossibleFours(this IBoard board)
        {
            List<IField[]> list = new List<IField[]>()
            {
                // Vertical Fours
                new IField[4] {board.Fields[0, 0], board.Fields[1, 0], board.Fields[2, 0], board.Fields[3, 0]},
                new IField[4] {board.Fields[1, 0], board.Fields[2, 0], board.Fields[3, 0], board.Fields[4, 0]},
                new IField[4] {board.Fields[2, 0], board.Fields[3, 0], board.Fields[4, 0], board.Fields[5, 0]},
                new IField[4] {board.Fields[0, 1], board.Fields[1, 1], board.Fields[2, 1], board.Fields[3, 1]},
                new IField[4] {board.Fields[1, 1], board.Fields[2, 1], board.Fields[3, 1], board.Fields[4, 1]},
                new IField[4] {board.Fields[2, 1], board.Fields[3, 1], board.Fields[4, 1], board.Fields[5, 1]},
                new IField[4] {board.Fields[0, 2], board.Fields[1, 2], board.Fields[2, 2], board.Fields[3, 2]},
                new IField[4] {board.Fields[1, 2], board.Fields[2, 2], board.Fields[3, 2], board.Fields[4, 2]},
                new IField[4] {board.Fields[2, 2], board.Fields[3, 2], board.Fields[4, 2], board.Fields[5, 2]},
                new IField[4] {board.Fields[0, 3], board.Fields[1, 3], board.Fields[2, 3], board.Fields[3, 3]},
                new IField[4] {board.Fields[1, 3], board.Fields[2, 3], board.Fields[3, 3], board.Fields[4, 3]},
                new IField[4] {board.Fields[2, 3], board.Fields[3, 3], board.Fields[4, 3], board.Fields[5, 3]},
                new IField[4] {board.Fields[0, 4], board.Fields[1, 4], board.Fields[2, 4], board.Fields[3, 4]},
                new IField[4] {board.Fields[1, 4], board.Fields[2, 4], board.Fields[3, 4], board.Fields[4, 4]},
                new IField[4] {board.Fields[2, 4], board.Fields[3, 4], board.Fields[4, 4], board.Fields[5, 4]},
                new IField[4] {board.Fields[0, 5], board.Fields[1, 5], board.Fields[2, 5], board.Fields[3, 5]},
                new IField[4] {board.Fields[1, 5], board.Fields[2, 5], board.Fields[3, 5], board.Fields[4, 5]},
                new IField[4] {board.Fields[2, 5], board.Fields[3, 5], board.Fields[4, 5], board.Fields[5, 5]},
                new IField[4] {board.Fields[0, 6], board.Fields[1, 6], board.Fields[2, 6], board.Fields[3, 6]},
                new IField[4] {board.Fields[1, 6], board.Fields[2, 6], board.Fields[3, 6], board.Fields[4, 6]},
                new IField[4] {board.Fields[2, 6], board.Fields[3, 6], board.Fields[4, 6], board.Fields[5, 6]},

                // Horizontal Fours
                new IField[4] {board.Fields[0, 0], board.Fields[0, 1], board.Fields[0, 2], board.Fields[0, 3]},
                new IField[4] {board.Fields[0, 1], board.Fields[0, 2], board.Fields[0, 3], board.Fields[0, 4]},
                new IField[4] {board.Fields[0, 2], board.Fields[0, 3], board.Fields[0, 4], board.Fields[0, 5]},
                new IField[4] {board.Fields[0, 3], board.Fields[0, 4], board.Fields[0, 5], board.Fields[0, 6]},
                new IField[4] {board.Fields[1, 0], board.Fields[1, 1], board.Fields[1, 2], board.Fields[1, 3]},
                new IField[4] {board.Fields[1, 1], board.Fields[1, 2], board.Fields[1, 3], board.Fields[1, 4]},
                new IField[4] {board.Fields[1, 2], board.Fields[1, 3], board.Fields[1, 4], board.Fields[1, 5]},
                new IField[4] {board.Fields[1, 3], board.Fields[1, 4], board.Fields[1, 5], board.Fields[1, 6]},
                new IField[4] {board.Fields[2, 0], board.Fields[2, 1], board.Fields[2, 2], board.Fields[2, 3]},
                new IField[4] {board.Fields[2, 1], board.Fields[2, 2], board.Fields[2, 3], board.Fields[2, 4]},
                new IField[4] {board.Fields[2, 2], board.Fields[2, 3], board.Fields[2, 4], board.Fields[2, 5]},
                new IField[4] {board.Fields[2, 3], board.Fields[2, 4], board.Fields[2, 5], board.Fields[2, 6]},
                new IField[4] {board.Fields[3, 0], board.Fields[3, 1], board.Fields[3, 2], board.Fields[3, 3]},
                new IField[4] {board.Fields[3, 1], board.Fields[3, 2], board.Fields[3, 3], board.Fields[3, 4]},
                new IField[4] {board.Fields[3, 2], board.Fields[3, 3], board.Fields[3, 4], board.Fields[3, 5]},
                new IField[4] {board.Fields[3, 3], board.Fields[3, 4], board.Fields[3, 5], board.Fields[3, 6]},
                new IField[4] {board.Fields[4, 0], board.Fields[4, 1], board.Fields[4, 2], board.Fields[4, 3]},
                new IField[4] {board.Fields[4, 1], board.Fields[4, 2], board.Fields[4, 3], board.Fields[4, 4]},
                new IField[4] {board.Fields[4, 2], board.Fields[4, 3], board.Fields[4, 4], board.Fields[4, 5]},
                new IField[4] {board.Fields[4, 3], board.Fields[4, 4], board.Fields[4, 5], board.Fields[4, 6]},
                new IField[4] {board.Fields[5, 0], board.Fields[5, 1], board.Fields[5, 2], board.Fields[5, 3]},
                new IField[4] {board.Fields[5, 1], board.Fields[5, 2], board.Fields[5, 3], board.Fields[5, 4]},
                new IField[4] {board.Fields[5, 2], board.Fields[5, 3], board.Fields[5, 4], board.Fields[5, 5]},
                new IField[4] {board.Fields[5, 3], board.Fields[5, 4], board.Fields[5, 5], board.Fields[5, 6]},

                // Diagonal /
                new IField[4] {board.Fields[3, 0], board.Fields[2, 1], board.Fields[1, 2], board.Fields[0, 3]}, //4
                new IField[4] {board.Fields[4, 0], board.Fields[3, 1], board.Fields[2, 2], board.Fields[1, 3]}, //5
                new IField[4] {board.Fields[3, 1], board.Fields[2, 2], board.Fields[1, 3], board.Fields[0, 4]}, //5
                new IField[4] {board.Fields[5, 0], board.Fields[4, 1], board.Fields[3, 2], board.Fields[2, 3]}, //6
                new IField[4] {board.Fields[4, 1], board.Fields[3, 2], board.Fields[2, 3], board.Fields[1, 4]}, //6
                new IField[4] {board.Fields[3, 2], board.Fields[2, 3], board.Fields[1, 4], board.Fields[0, 5]}, //6
                new IField[4] {board.Fields[5, 1], board.Fields[4, 2], board.Fields[3, 3], board.Fields[2, 4]}, //6
                new IField[4] {board.Fields[4, 2], board.Fields[3, 3], board.Fields[2, 4], board.Fields[1, 5]}, //6
                new IField[4] {board.Fields[3, 3], board.Fields[2, 4], board.Fields[1, 5], board.Fields[0, 6]}, //6
                new IField[4] {board.Fields[5, 2], board.Fields[4, 3], board.Fields[3, 4], board.Fields[2, 5]}, //5
                new IField[4] {board.Fields[4, 3], board.Fields[3, 4], board.Fields[2, 5], board.Fields[1, 6]}, //5
                new IField[4] {board.Fields[5, 3], board.Fields[4, 4], board.Fields[3, 5], board.Fields[2, 6]}, //4

                // Diagonal \
                new IField[4] {board.Fields[2, 0], board.Fields[3, 1], board.Fields[4, 2], board.Fields[5, 3]}, //4
                new IField[4] {board.Fields[1, 0], board.Fields[2, 1], board.Fields[3, 2], board.Fields[4, 3]}, //5
                new IField[4] {board.Fields[2, 1], board.Fields[3, 2], board.Fields[4, 3], board.Fields[5, 4]}, //5
                new IField[4] {board.Fields[0, 0], board.Fields[1, 1], board.Fields[2, 2], board.Fields[3, 3]}, //6
                new IField[4] {board.Fields[1, 1], board.Fields[2, 2], board.Fields[3, 3], board.Fields[4, 4]}, //6
                new IField[4] {board.Fields[2, 2], board.Fields[3, 3], board.Fields[4, 4], board.Fields[5, 5]}, //6
                new IField[4] {board.Fields[0, 1], board.Fields[1, 2], board.Fields[2, 3], board.Fields[3, 4]}, //6
                new IField[4] {board.Fields[1, 2], board.Fields[2, 3], board.Fields[3, 4], board.Fields[4, 5]}, //6
                new IField[4] {board.Fields[2, 3], board.Fields[3, 4], board.Fields[4, 5], board.Fields[5, 6]}, //6
                new IField[4] {board.Fields[0, 2], board.Fields[1, 3], board.Fields[2, 4], board.Fields[3, 5]}, //5
                new IField[4] {board.Fields[1, 3], board.Fields[2, 4], board.Fields[3, 5], board.Fields[4, 5]}, //5
                new IField[4] {board.Fields[0, 3], board.Fields[1, 4], board.Fields[2, 5], board.Fields[3, 5]}, //4
            };
            return list;
        }

        public static int Evaluate(this IBoard board, int myId, int opponentId)
        {
            List<IField[]> my4s = board.GetPossibleFours()
                .Where(fours => (fours.FirstOrDefault(field => field.PlayerId == myId) != null &&
                                 fours.FirstOrDefault(field => field.PlayerId == opponentId) == null)).ToList();

            List<IField[]> opponent4s = board.GetPossibleFours()
                .Where(fours => (fours.FirstOrDefault(field => field.PlayerId == opponentId) != null &&
                                 fours.FirstOrDefault(field => field.PlayerId == myId) == null)).ToList();

            int my1s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 1);
            int my2s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 2);
            int my3s = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 3);
            int myWinning = my4s.Count(fours => fours.Count(f => f.PlayerId == myId) == 4);

            int opponent1s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 1);
            int opponent2s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 2);
            int opponent3s = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 3);
            int opponentWinning = opponent4s.Count(fours => fours.Count(f => f.PlayerId == opponentId) == 4);

            int myScore = my1s + my2s * 6 + my3s * 106 + myWinning * 10000;
            int opponentScore = opponent1s * 2 + opponent2s * 8 + opponent3s * 208 + opponentWinning * 15000;
            return myScore - opponentScore;
        }
    }
}