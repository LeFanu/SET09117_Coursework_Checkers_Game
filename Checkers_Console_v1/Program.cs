using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_Console_v1;
using Checkers_Game_Helper;


namespace Checkers_Console_v1
{
    class Program
    {

        /** Author: Karol Pasierb - Software Engineering - 40270305
        * Created by Karol Pasierb on 2017/10/08
        *
        ** Description:
        *   This is the main class for the whole game. This is the base and the starting point of the game. 
        *   It will contain all the menus and will provide the initial interaction. 
        *
        ** Future updates:
        *   
        ** Design Patterns Used:
        *
        ** Last Update: 16/10/2017
        */

        static void Main(string[] args)
        {

            Console.WriteLine("********************************************************");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("**              WELCOME TO CHECKERS GAME              **");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("********************************************************");


            //mainMenu();
            Game testingBoard = Game.CurrentGameInstance;
            testingBoard.setGame("1");
            testingBoard.PlayGame();

            Console.ReadKey();
        }

        private static void mainMenu()
        {
            int option;
            do
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Please choose one of the following options:");
                Console.WriteLine("\t 1.Start New Game");
                Console.WriteLine("\t 2.Replay Game");
                Console.WriteLine("\t 3.Save Last Game");
                Console.WriteLine("\t 4.Quit");

                Console.Write("Please enter your choice >>> \t");
                option = Int32.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        {
                            Console.WriteLine("\nStarting New Game...");
                            int gameOption;

                            Game chosenGame;

                            do
                            {
                                Console.WriteLine("\n--------------------------------------------------------");
                                Console.WriteLine("Please choose one of the following options:");
                                Console.WriteLine("\t 1.Player VS Computer");
                                Console.WriteLine("\t 2.Player VS Player");
                                Console.WriteLine("\t 3.Computer VS Player");
                                Console.WriteLine("\t 4.Computer VS Computer");
                                Console.WriteLine("\t 5. Return to previous menu");

                                Console.Write("Please enter your choice >>> \t");
                                gameOption = Int32.Parse(Console.ReadLine());


                                switch (gameOption)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    {
                                        chosenGame = Game.CurrentGameInstance;
                                            chosenGame.setGame(gameOption.ToString());
                                            chosenGame.PlayGame();
                                            break;
                                        }

                                    case 5:
                                        {

                                            break;
                                        }
                                    default:
                                        {
                                            Console.WriteLine("\nPlease choose one of the given options.");
                                            break;
                                        }
                                }
                            } while (gameOption != 5);
                            break;
                        }
                    case 2:
                        {

                            break;
                        }
                    case 3:
                        {

                            break;
                        }
                    case 4:
                        {

                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Please choose one of the given options.");
                            break;
                        }
                }

            } while (option != 4);

            
        }
    }
}
