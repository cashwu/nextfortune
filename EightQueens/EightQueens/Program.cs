using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueens
{
    class Program
    {
        static int SolutionSeries = 1;

        static void Main(string[] args)
        {
            Chessboard board = new Chessboard();
            Explore(board);
            Console.ReadLine();
        }

        static void Explore(Chessboard cb)
        {
            int maxPos = cb.QueenPositionList.Count > 0 ? cb.QueenPositionList.Max() : 0;
            foreach (var slot in cb.SlotList.Where(x => x > maxPos))
            {
                Chessboard newCb = new Chessboard(cb, slot);
                if (newCb.Finish)
                {
                    Console.WriteLine("// Solution " + SolutionSeries++);
                    newCb.Print();
                    Console.WriteLine();
                }
                else if (!newCb.NoSolution)
                {
                    Explore(newCb);
                }
            }
        }
    }
}
