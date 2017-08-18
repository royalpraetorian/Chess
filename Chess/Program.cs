using Chess.Control;
using Chess.Model;
using Chess.Model.Ranks;
using Chess.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            string moveSeparate = " to ";
            string validMov;
            Coordinate[] playerMove = new Coordinate[] { };
            do
            {
                do
                {
                    string[] playerIn;
                    bool acceptMove = false;
                    do
                    {

                        Console.WriteLine(ConsoleCheck.BoardPrint());
                        string consoleIn;
                        do
                        {
                            Console.WriteLine("Please enter a starting space and a destination space... (Ex: 2,4 to 4,5)");
                            consoleIn = Console.ReadLine();
                            if (string.IsNullOrEmpty(consoleIn))
                            {
                                Console.WriteLine("Invalid input!");
                            }
                        } while (string.IsNullOrEmpty(consoleIn));
                        playerIn = consoleIn.Split(new string[] { moveSeparate }, StringSplitOptions.None);
                        string[] firstCoord = playerIn[0].Split(',');
                        string[] secondCoord = playerIn[1].Split(',');
                        acceptMove = true;
                        try
                        {
                            playerMove = new Coordinate[] { new Coordinate(int.Parse(firstCoord[0]), int.Parse(firstCoord[1])), new Coordinate(int.Parse(secondCoord[0]), int.Parse(secondCoord[1])) };
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("\n-- Invalid coordinate! --\n");
                            acceptMove = false;
                        }
                    } while (!acceptMove);
                    validMov = GameBoard.MovePiece(new Move(playerMove[0], playerMove[1]));
                    if (!string.IsNullOrEmpty(validMov))
                    {
                        Console.WriteLine("\n-- " + validMov + " --\n");
                    }
                } while (!string.IsNullOrEmpty(validMov));

            } while (true);
        }
    }
}
