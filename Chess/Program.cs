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
            do
            {

                    Console.WriteLine(ConsoleCheck.BoardPrint());
                //View.Presentation.RunPresentation();
                do
                {
                    Console.WriteLine("Please enter a starting space and a destination space... (Ex: 2,4 to 4,5");
                    string consoleIn = Console.ReadLine();
                    string[] playermove = consoleIn.Split(new string[] { moveSeparate }, StringSplitOptions.None);
                    string[] firstCoord = playermove[0].Split(',');
                    string[] secondCoord = playermove[1].Split(',');
                    Coordinate[] playerMove = { new Coordinate(int.Parse(firstCoord[0]), int.Parse(firstCoord[1])), new Coordinate(int.Parse(secondCoord[0]), int.Parse(secondCoord[1])) };
                    validMov = GameBoard.MovePiece(new Move(playerMove[0], playerMove[1]));
                    if (!string.IsNullOrEmpty(validMov)){
                        Console.WriteLine(validMov);
                    }
                } while (!string.IsNullOrEmpty(validMov));

            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
	}
}
