using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EightQueens
{
    class Chessboard
    {
        private const int SIZE = 8;
        private const int QUOTA = 8;
        private const int DIG_SHIFT = 10;

        /// <summary>
        /// 棋盤空格
        /// </summary>
        public List<int> SlotList { get; set; }

        /// <summary>
        /// 皇后擺放位置
        /// </summary>
        public List<int> QueenPositionList { get; set; }

        /// <summary>
        /// 空格數小於未放皇后數量，無解
        /// </summary>
        //TODO 可以使用 => 簡寫
        public bool NoSolution { get { return SlotList.Count < QUOTA - QueenPositionList.Count; } }

        /// <summary>
        /// 皇后數量達標
        /// </summary>
        //TODO 可以使用 => 簡寫
        public bool Finish { get { return QUOTA == QueenPositionList.Count; } }

        /// <summary>
        /// 空白棋盤
        /// </summary>
        public Chessboard()
        {
            //TODO 變數要小寫
            var Grid = Enumerable.Range(1, SIZE); //產生1~8
            var GridExpan = Grid.SelectMany(x => Enumerable.Range(1, SIZE).Select(y => x * DIG_SHIFT + y)); //產生11, 12, 13..., 21, 22..., 88

            SlotList = new List<int>(GridExpan);
            QueenPositionList = new List<int>();
        }

        /// <summary>
        /// 現有棋盤在特定位置放上皇后生出新棋盤
        /// </summary>
        /// <param name="board">現有棋盤</param>
        /// <param name="pos">皇后位置</param>
        public Chessboard(Chessboard board, int pos)
        {
            SlotList = new List<int>(board.SlotList.Except(new int[] { pos }));
            QueenPositionList = new List<int>(board.QueenPositionList.Concat(new int[] { pos }));
            int qX = pos / DIG_SHIFT;
            int qY = pos % DIG_SHIFT;

            for (var x = 1; x <= SIZE; x++)
            {
                for (var y = 1; y <= SIZE; y++)
                {
                    removeSlot(x, qY); // 移除水平空格
                    removeSlot(qX, y); // 移除垂直空格
                }
                var tx = x + (qX - qY);
                removeSlot(tx, x); // 移除左上右下斜線空格
                tx = qX + qY - x;
                removeSlot(tx, x); // 移除右上左下斜線空格
            }
        }

        /// <summary>
        /// 移除空格
        /// </summary>
        /// <param name="px"></param>
        /// <param name="py"></param>
        //TODO method 必須大寫開頭
        private void removeSlot(int px, int py)
        {
            if (px < 1 || px > SIZE || py < 1 || py > SIZE) return; //超出棋盤範圍
            var pos = px * DIG_SHIFT + py;
            if (SlotList.Contains(pos)) SlotList.Remove(pos);
        }

        /// <summary>
        /// 印出棋盤
        /// </summary>
        public void Print()
        {
            for (var y = 1; y <= SIZE; y++)
            {
                for (var x = 1; x <= SIZE; x++)
                {
                    var pos = x * DIG_SHIFT + y;
                    if (QueenPositionList.Contains(pos))
                    {
                        Console.Write("Q");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
