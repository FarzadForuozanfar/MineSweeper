using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_minesweeper
{
    
    class Program
    {
        static int BOMB = 15;
        public static void FindEmpty(ref char[,] char_board, int x, int y,ref bool[,] bool_board)
        {
            if(char_board[x,y] != 'B')
            {
                bool_board[x, y] = true;
            }
            if(x > 0)
            {
                if (char_board[x - 1, y] == ' ' && char_board[x - 1, y] != 'B')
                {
                    bool_board[x - 1, y] = true;
                    
                }
            }
            
            if(x > 0 && y < 9)
            {
                if (char_board[x - 1, y + 1] == ' ' && char_board[x - 1, y + 1] != 'B')
                {
                    bool_board[x - 1, y + 1] = true;
                    
                }
            }
            
            if(x > 0 && y > 0)
            {
                if (char_board[x - 1, y - 1] == ' ' && char_board[x - 1, y - 1] != 'B' && x > 0 && y > 0)
                {
                    bool_board[x - 1, y - 1] = true;
                    
                }
            }
            
            if(y > 0)
            {
                if (char_board[x, y - 1] == ' ' && char_board[x, y - 1] != 'B' )
                {
                    bool_board[x, y - 1] = true;
                    
                }
            }
            
            if(y < 9)
            {
                if (char_board[x, y + 1] == ' ' && char_board[x, y+1] != 'B')
                {
                    bool_board[x, y + 1] = true;
                    
                }
            }
            
            if(x < 9 && y < 9)
            {
                if (char_board[x + 1, y + 1] == ' ' && char_board[x + 1, y + 1] != 'B')
                {
                    bool_board[x + 1, y + 1] = true;
                    
                }
            }

            if(x < 9 && y > 0)
            {
                if (char_board[x + 1, y - 1] == ' ' && char_board[x + 1, y-1] != 'B')
                {
                    bool_board[x + 1, y - 1] = true;
                    
                }
            }
            
            
        }
        public static void CheckGame(ref char[,] char_board,int x,int y,ref bool lose_statuse,ref bool win_statuse,ref bool[,] bool_board)
        {
            if(char_board[x,y] == 'B')
            {
                lose_statuse = true;
                win_statuse = false;
                Console.Clear();
                Console.WriteLine("=================\\ |You Lose| /=================");
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Console.Write($"|{char_board[i, j]}|");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                FindEmpty(ref char_board, x, y, ref bool_board);
            }
        }
        public static void PrintBoard(char[,] char_board,bool statuse, bool[,] bool_board)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
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
                        Console.Write($"|{char_board[i,j]}|");
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
                    x = rand.Next(0, 10);
                    y = rand.Next(0, 10);
                    //Console.WriteLine($"{x},{y}");
                }while(board_char[x,y] == 'B');
                board_char[x, y] = 'B';
                number--;
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(board_char[i, j] != 'B')
                    {
                        board_char[i, j] = ' ';
                    }
                    board_bool[i, j] = false; 
                }
            }
            int num_bomb = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(board_char[i,j] != 'B')
                    {
                        if (i > 0)
                        {
                            if (board_char[i - 1, j] == 'B' && i > 0)
                                num_bomb++;
                        }
                        if (i > 0 && j < 9)
                        {
                            if (board_char[i - 1, j + 1] == 'B')
                                num_bomb++;
                        }
                        
                        if (i > 0 && j > 0)
                        {
                            if (board_char[i - 1, j - 1] == 'B')
                                num_bomb++;
                        }
                        
                        if(j > 0)
                        {
                            if (board_char[i, j - 1] == 'B')
                                num_bomb++;
                        }
                        
                        if (j < 9)
                        {
                            if (board_char[i, j + 1] == 'B')
                                num_bomb++;
                        }
                        
                        if(i < 9 && j < 9)
                        {
                            if (board_char[i + 1, j + 1] == 'B')
                                num_bomb++;
                        }
                        
                        if(i < 9 && j > 0)
                        {
                            if (board_char[i + 1, j - 1] == 'B')
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
                num_bomb = 0;

            }
        }
        static void CheckWin(ref bool win, bool[,] board_bool)
        {
            int cnt = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(board_bool[i,j] == true)
                    {
                        cnt++;
                    }
                }
            }
            if(cnt + BOMB == (10 * 10))
            {
                win = true;
            }
        }
        static void Main(string[] args)
        {
            bool lose = false;
            bool win = false;
            int x_choice, y_choice;
            bool[,] choice_board = new bool[10, 10];
            char[,] game_board = new char[10,10];
            
            SetBombs(ref game_board, BOMB,ref choice_board);
            while(! lose && ! win)
            {
                Console.Clear();
                PrintBoard(game_board,lose, choice_board);
                Console.WriteLine();
                Console.Write("insert x:");
                x_choice = int.Parse(Console.ReadLine());
                Console.Write("insert y:");
                y_choice = int.Parse(Console.ReadLine());
                CheckGame(ref game_board,x_choice,y_choice,ref lose,ref win,ref choice_board);
                CheckWin(ref win, choice_board);
                
            }
             
        }
    }
}
