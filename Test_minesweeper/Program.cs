using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Test_minesweeper
{
    
    class Program
    {
        static int BOMB;
        public static int x_choice, y_choice, SIZE, level;
        
        
        public static void FindEmpty(ref char[,] char_board, int x, int y,ref bool[,] bool_board)
        {
            if(bool_board[x,y] == false)
            {
                if (char_board[x, y] != 'B')
                {
                    bool_board[x, y] = true;
                }
                if (x > 0)
                {
                    if (char_board[x - 1, y] != 'B')
                    {
                        bool_board[x - 1, y] = true;

                    }
                }

                if (x > 0 && y < SIZE - 1)
                {
                    if (char_board[x - 1, y + 1] != 'B')
                    {
                        bool_board[x - 1, y + 1] = true;

                    }
                }

                if (x > 0 && y > 0)
                {
                    if (char_board[x - 1, y - 1] != 'B')
                    {
                        bool_board[x - 1, y - 1] = true;

                    }
                }

                if (y > 0)
                {
                    if (char_board[x, y - 1] != 'B')
                    {
                        bool_board[x, y - 1] = true;

                    }
                }

                if (y < SIZE - 1)
                {
                    if (char_board[x, y + 1] != 'B')
                    {
                        bool_board[x, y + 1] = true;

                    }
                }

                if (x < SIZE - 1 && y < SIZE - 1)
                {
                    if (char_board[x + 1, y + 1] != 'B')
                    {
                        bool_board[x + 1, y + 1] = true;

                    }
                }

                if (x < SIZE - 1 && y > 0)
                {
                    if (char_board[x + 1, y - 1] != 'B')
                    {
                        bool_board[x + 1, y - 1] = true;

                    }
                }


            }
        }
            
        public static void CheckGame(ref char[,] char_board,int x,int y,ref bool lose_statuse,ref bool win_statuse,ref bool[,] bool_board,ref bool continue_statuse)
        {
            if(char_board[x,y] == 'B')
            {
                continue_statuse = false;

                lose_statuse = true;
                win_statuse = false;
                Console.Clear();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                
                Console.WriteLine("=================\\ !!! You Lose !!! /=================");
                for (int i = 0; i < SIZE; i++)
                {
                    
                    for (int j = 0; j < SIZE; j++)
                    {
                        Console.Write($"|{char_board[i, j]}|");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.Write("Reset Game(Y or N):");
                string reset = Console.ReadLine();
                if(reset == "Y" || reset == "y")
                {
                    continue_statuse = true;
                    Console.ResetColor();
                    ResetGame(ref lose_statuse,ref win_statuse,ref char_board,ref bool_board);
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\t\t Game Over");
                    Thread.Sleep(3999);
                    Console.ResetColor();
                    Environment.Exit(0);

                }

            }
            else
            {
                FindEmpty(ref char_board, x, y, ref bool_board);
            }
            CheckWin(ref win_statuse, bool_board);
            if(win_statuse)
            {
                continue_statuse = false;

                lose_statuse = true;
                win_statuse = false;
                Console.Clear();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                
                Console.WriteLine("=================\\ !!! You Win !!! /=================");
                for (int i = 0; i < SIZE; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    for (int j = 0; j < SIZE; j++)
                    {
                        Console.Write($"|{char_board[i, j]}|");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.Write("Reset Game(Y or N):");
                string reset = Console.ReadLine();
                if (reset == "Y" || reset == "y")
                {
                    continue_statuse = true;
                    Console.ResetColor();
                    ResetGame(ref lose_statuse, ref win_statuse, ref char_board, ref bool_board);
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n\n\t\t Game Over");
                    Thread.Sleep(3999);
                    Console.ResetColor();
                    Environment.Exit(0);

                }
            }
        }
        public static void PrintBoard(char[,] char_board,bool statuse, bool[,] bool_board)
        {
            for (int k = 0; k < SIZE; k++)
            {
                if(k == 0)
                    Console.Write($"    {k + 1}");
                else if (k !=9)
                    Console.Write($"  {k + 1}");
                else if (k == 8)
                    Console.Write($" {k + 1} ");
                else
                    Console.Write($"  {k + 1}");
            }
            Console.WriteLine();
            
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (j == 0 && i != 9)
                        Console.Write($" {i + 1} ");
                    else if (i == 9 && j == 0)
                        Console.Write($"{i + 1} ");
                    
                    //print bomb if lose
                    if (char_board[i, j] == 'B' && statuse)
                    {
                        Console.Write($"|B|");
                    }
                    // print "?" 
                    else if((char_board[i, j] == 'B' && !statuse) || (char_board[i,j] == ' ' && bool_board[i,j] == false) ||(char_board[i, j] != 'B' && char_board[i, j] != ' ' && bool_board[i, j] == false))
                    {
                        Console.Write($"|?|");
                    }
                    // print 1,2,3 if selected
                    else if (char_board[i, j] != 'B' && bool_board[i, j] == true)
                    {
                        if (char_board[i, j] != ' ')
                            if (char_board[i, j] == '1')
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                            else if (char_board[i, j] == '2')
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                            else if (char_board[i, j] == '3')
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                            else if (char_board[i, j] == '4')
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            else if (char_board[i, j] == '5')
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            else if (char_board[i, j] == '6')
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                            else
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"|{char_board[i,j]}|");
                        Console.ResetColor();

                    }

                }
                Console.WriteLine();
            }
        }
        public static void SetBombs(ref char[,] board_char,int number,ref bool[,] board_bool)
        {

            Random rand = new Random();
            int x, y;
            while(number > 0)
            {
                do
                {
                    x = rand.Next(0, SIZE);
                    y = rand.Next(0, SIZE);
                    //Console.WriteLine($"{x},{y}");
                }while(board_char[x,y] == 'B');
                board_char[x, y] = 'B';
                number--;
            }
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(board_char[i, j] != 'B')
                    {
                        board_char[i, j] = ' ';
                    }
                    board_bool[i, j] = false; 
                }
            }
            int num_bomb = 0;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(board_char[i,j] != 'B')
                    {
                        if (i > 0 && board_char[i - 1, j] == 'B') // North neighbor
                        {
                            num_bomb++; 
                        }
                        if (i > 0 && j < SIZE - 1 && board_char[i - 1, j + 1] == 'B') // East North neighbor
                        {
                            num_bomb++;
                        }

                        if (i > 0 && j > 0 && board_char[i - 1, j - 1] == 'B') // West North neighbor
                        {
                            num_bomb++;
                        }

                        if (j > 0 && board_char[i, j - 1] == 'B') // West neighbor
                        {
                            num_bomb++;
                        }
                        
                        if (j < SIZE - 1 && board_char[i, j + 1] == 'B') // East neighbor
                        {
                            num_bomb++;
                        }
                        
                        if(i < SIZE -1  && j < SIZE - 1 && board_char[i + 1, j + 1] == 'B') // South neighbor
                        {
                            num_bomb++;
                        }
                        
                        if(i < SIZE - 1 && j > 0 && board_char[i + 1, j - 1] == 'B') // West South neighbor
                        {
                            num_bomb++;
                        }
                        if(i < SIZE -1 && board_char[i + 1, j] == 'B') // South
                        {
                            num_bomb++;
                        }
                        
                    }
                    if(num_bomb > 0)
                    {
                        string str = num_bomb.ToString();
                        char[] temp = str.ToCharArray();
                        board_char[i, j] = temp[0];
                        
                    }
                    num_bomb = 0;
                    

                }
            }
        }
        static void CheckWin(ref bool win, bool[,] board_bool)
        {
            int cnt = 0;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if(board_bool[i,j] == true)
                    {
                        cnt++;
                    }
                }
            }
            if(cnt + BOMB == (SIZE * SIZE))
            {
                win = true;
            }
        }
        static void StartGame(ref int BOMB,ref int SIZE)
        {
            Console.Clear();
            bool true_info = false;
            while (!true_info)
            {
                for (int i = 0; i < 11; i++)
                    Console.Write("* ");
                
                for (int j = 0; j < 2; j++)
                {
                    Console.WriteLine("*");
                    if(j == 0)
                    {
                        Console.WriteLine("*  Farzad Foroozanfar *");

                    }
                    if(j == 1)
                    {
                        Console.WriteLine("*  Copyright(c) 2022  *");
                    }
                    
                }
                for (int i = 0; i < 12; i++)
                    Console.Write("* ");
                
                Console.Write("\n\n 1-Beginner(5*5)\n 2-Normal(8*8)\n 3-Hard(10*10)\n Select the Game level :");
                try
                {
                    level = int.Parse(Console.ReadLine());
                    true_info = true;
                    switch(level)
                    {
                        case 1:
                            BOMB = 5;
                            SIZE = 5;
                            break;

                        case 2:
                            BOMB = 15;
                            SIZE = 8;
                            break;
                        case 3:
                            BOMB = 25;
                            SIZE = 10;
                            break;

                        default:
                            BOMB = 15;
                            SIZE = 10;
                            break;
                    }
                }
                catch
                {
                    BOMB = 15;
                    SIZE = 10;
                }
                
                
            }

            
        }
        static void ResetGame(ref bool lose, ref bool win , ref char[,] char_board, ref bool[,] bool_board)
        {
            lose = false;
            win = false;
            StartGame(ref BOMB, ref SIZE);
            bool[,] choice_board = new bool[SIZE, SIZE];
            char[,] game_board = new char[SIZE, SIZE];
            char_board = game_board;
            bool_board = choice_board;
            SetBombs(ref char_board, BOMB, ref bool_board);
        }
        static void Main(string[] args)
        {
            bool lose = false;
            bool win = false;
            bool continue_game = true;
            StartGame(ref BOMB,ref SIZE);
            Thread.Sleep(100);

            bool[,] choice_board = new bool[SIZE, SIZE];
            char[,] game_board = new char[SIZE,SIZE];
            bool true_info;
            SetBombs(ref game_board, BOMB,ref choice_board);
            while(continue_game)
            {
                
                true_info = true;
                Console.Clear();
                PrintBoard(game_board,lose, choice_board);
                Console.WriteLine();
                
                try
                {
                    Console.Write("insert x:");
                    x_choice = int.Parse(Console.ReadLine());
                    Console.Write("insert y:");
                    y_choice = int.Parse(Console.ReadLine());
                }
                catch { true_info = false; }
                if (x_choice < 1 || x_choice > SIZE || y_choice < 1 || y_choice > SIZE)
                    true_info = false;
                CheckWin(ref win, choice_board);
                if (true_info)
                    CheckGame(ref game_board,x_choice - 1,y_choice - 1,ref lose,ref win,ref choice_board,ref continue_game);
                CheckWin(ref win, choice_board);

                
            }
            Console.ReadKey();
        }
    }
}
