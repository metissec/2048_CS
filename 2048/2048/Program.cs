using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    class Program
    {
        static void Main()
        {

            var game = new Board();

            ConsoleKeyInfo cki;
            do
            {
                cki = Console.ReadKey();
                game.Move(cki.Key.ToString());
            } while (cki.Key != ConsoleKey.Escape);
            

        }
    }

    public class Board
    {

        public int[,] gameBoard;
        private Random random;
        public int score;
        //Constants
        public const int FEILD_MAX = 3;
        public const int FEILD_MIN = 0;

        public Board()
        {
            score = 0;
            random = new Random();
            gameBoard = new int[FEILD_MAX + 1, FEILD_MAX + 1];
            //Spawn is called twice to add two starting tiles
            Spawn();
            Spawn();
            Draw();
        }

        private void Spawn()
        {
            var zeroLoc = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        zeroLoc.Add(new int[2] { i, j });
                    }
                }
            }

            if (zeroLoc.Count() == 0) { return; }

            int[] item = zeroLoc[random.Next(0, zeroLoc.Count())];
            int outNum;

            outNum = random.Next(0, 2) == 0 ? 2 : 4;
            
            gameBoard[item[0], item[1]] = outNum;
        }

        public void Move(string dir)
        {
            switch (dir)
            {
                case "RightArrow":
                    for (int i = 0; i < 4; i++) { moveTile(-1, 0, i, FEILD_MIN, FEILD_MAX); }
                    Spawn();
                    Draw();
                    break;
                case "UpArrow":
                    for (int i = 0; i < 4; i++) { moveTile(1, 1, i, FEILD_MAX, FEILD_MIN); }
                    Spawn();
                    Draw();
                    break;
                case "LeftArrow":
                    for (int i = 0; i < 4; i++) { moveTile(1, 0, i, FEILD_MAX, FEILD_MIN); }
                    Spawn();
                    Draw();
                    break;
                case "DownArrow":
                    for (int i = 0; i < 4; i++) { moveTile(-1, 1, i, FEILD_MIN, FEILD_MAX); }
                    Spawn();
                    Draw();
                    break;
            }
        }

        private void moveTile(int dir, int xy, int index, int end, int start)
        {
            switch (xy)
            {
                case 0:
                    int x = start;
                    while (x != end)
                    {
                        RemoveZero(dir, xy, index, end, start, 3);
                        if (gameBoard[index, x] == gameBoard[index, x + dir])
                        {
                            gameBoard[index, x] *= 2;
                            gameBoard[index, x + dir] = 0;
                            score += gameBoard[index, x];
                        }
                        x += dir;
                    }

                    break;

                case 1:
                    int y = start;
                    while (y != end)
                    {
                        RemoveZero(dir, xy, index, end, start, 2);
                        if (gameBoard[y, index] == gameBoard[y + dir, index])
                        {
                            gameBoard[y, index] *= 2;
                            gameBoard[y + dir, index] = 0;
                            score += gameBoard[y, index];
                        }
                        y += dir;
                    }

                    break;
            }


        }
        private void RemoveZero(int dir, int xy, int index, int end, int start, int n)
        {

            switch (xy)
            {
                case 0:
                    int x = start;
                    while (x != end)
                    {
                        if ((gameBoard[index, x + dir] != 0) && (gameBoard[index, x] == 0))
                        {
                            gameBoard[index, x] = gameBoard[index, x + dir];
                            gameBoard[index, x + dir] = 0;
                        }
                        x += dir;
                    }

                    break;

                case 1:
                    int y = start;
                    while (y != end)
                    {
                        if ((gameBoard[y + dir, index] != 0) && (gameBoard[y, index] == 0))
                        {
                            gameBoard[y, index] = gameBoard[y + dir, index];
                            gameBoard[y + dir, index] = 0;
                        }
                        y += dir;
                    }

                    break;
            }
            if (n > 0) { RemoveZero(dir, xy, index, end, start, (n - 1)); }
            else { return; }
        }

        private void Draw()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Score: " + score + "\n");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Color(i, j);
                    Console.Write(string.Format("{0,4} ", gameBoard[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + "Use Arrow keys to Move");
        }
        enum Colors
        {
            DarkGray = 0,
            Blue = 2,
            Red = 4,
            Magenta = 8,
            Yellow = 16,
            Green = 32,
            Cyan = 64,
            Grey = 128,
            DarkBlue = 256,
            DarkRed = 512,
            DarkMagenta = 1024,
            White = 2047
        }
        private void Color(int i, int j)
        {
            int val = gameBoard[i, j];
            string mycolorvalue = ((Colors)val).ToString();
            ConsoleColor myCC;
            if (Enum.TryParse(mycolorvalue, out myCC))
                Console.ForegroundColor = myCC;
        }
    }


}
