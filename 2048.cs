using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048
{
    class Program
    {
        static void Main()
        {

            Board game = new Board();

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

        public Board()
        {
            score = 0;
            random = new Random();
            gameBoard = new int[4, 4];
            Enumerable.Repeat(0, 16).ToArray();
            Spawn();
            Spawn();
            Draw();
        }

        private void Spawn()
        {
            List<int[]> t = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (gameBoard[i, j] == 0)
                    {
                        t.Add(new int[2] { i, j });
                    }
                }
            }

            if (t.Count() == 0) { return; }

            int[] item = t[random.Next(0, t.Count())];
            int outNum;

            if (random.Next(0, 2) == 0) { outNum = 2; } else { outNum = 4; }
            gameBoard[item[0], item[1]] = outNum;
        }

        public void Move(string dir)
        {
            switch (dir)
            {
                case "RightArrow":
                    for (int i = 0; i < 4; i++) { moveTile(-1, 0, i, 0, 3); }
                    Spawn();
                    Draw();
                    break;
                case "UpArrow":
                    for (int i = 0; i < 4; i++) { moveTile(1, 1, i, 3, 0); }
                    Spawn();
                    Draw();
                    break;
                case "LeftArrow":
                    for (int i = 0; i < 4; i++) { moveTile(1, 0, i, 3, 0); }
                    Spawn();
                    Draw();
                    break;
                case "DownArrow":
                    for (int i = 0; i < 4; i++) { moveTile(-1, 1, i, 0, 3); }
                    Spawn();
                    Draw();
                    break;
            }
        }

        private void moveTile(int dir, int xy, int i, int m, int s)
        {
            switch (xy)
            {
                case 0:
                    int x = s;
                    while (x != m)
                    {
                        RemoveZero(dir, xy, i, m, s, 3);
                        if (gameBoard[i, x] == gameBoard[i, x + dir])
                        {
                            gameBoard[i, x] *= 2;
                            gameBoard[i, x + dir] = 0;
                            score += gameBoard[i, x];
                        }
                        x += dir;
                    }

                    break;

                case 1:
                    int y = s;
                    while (y != m)
                    {
                        RemoveZero(dir, xy, i, m, s, 2);
                        if (gameBoard[y, i] == gameBoard[y + dir, i])
                        {
                            gameBoard[y, i] *= 2;
                            gameBoard[y + dir, i] = 0;
                            score += gameBoard[y, i];
                        }
                        y += dir;
                    }

                    break;
            }


        }
        private void RemoveZero(int dir, int xy, int i, int m, int s, int n)
        {

            switch (xy)
            {
                case 0:
                    int x = s;
                    while (x != m)
                    {
                        if ((gameBoard[i, x + dir] != 0) && (gameBoard[i, x] == 0))
                        {
                            gameBoard[i, x] = gameBoard[i, x + dir];
                            gameBoard[i, x + dir] = 0;
                        }
                        x += dir;
                    }

                    break;

                case 1:
                    int y = s;
                    while (y != m)
                    {
                        if ((gameBoard[y + dir, i] != 0) && (gameBoard[y, i] == 0))
                        {
                            gameBoard[y, i] = gameBoard[y + dir, i];
                            gameBoard[y + dir, i] = 0;
                        }
                        y += dir;
                    }

                    break;
            }
            if (n > 0) { RemoveZero(dir, xy, i, m, s, (n - 1)); }
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
        private void Color(int i, int j)
        {
            int val = gameBoard[i, j];
            switch (val)
            {
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 8:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case 16:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 32:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 64:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case 128:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 256:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case 512:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case 1024:
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case 2048:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }


}
