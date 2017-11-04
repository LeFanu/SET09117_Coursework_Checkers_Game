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

        private static List<GameHistory_Caretaker> savedGames;


        static void Main(string[] args)
        {

            Console.WriteLine("********************************************************");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("**              WELCOME TO CHECKERS GAME              **");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("********************************************************");


            //mainMenu();
            Game testingBoard = Game.CurrentGameInstance;
            testingBoard.setGame("3");
            testingBoard.PlayGame();
            saveLastPlayedGame(testingBoard.GameHistory);
            
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
                Console.WriteLine("\t 3.Quit");

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
                                Console.WriteLine("\t 3.Computer VS Computer");
                                Console.WriteLine("\t 4. Return to previous menu");

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
                                        saveLastPlayedGame(chosenGame.GameHistory);
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
                            } while (gameOption != 4);
                            break;
                        }
                    case 2:
                    {
                        //savedGames =  loadSavedGames();
                        foreach (GameHistory_Caretaker savedGame in savedGames)
                        {
                            
                        }
                        break;
                        }
                    default:
                        {
                            Console.WriteLine("Please choose one of the given options.");
                            break;
                        }
                }
            } while (option != 3);

        }

        private static void saveLastPlayedGame(GameHistory_Caretaker gameHistory)
        {
            Console.WriteLine("\n___________________________________________________________________________________________________________\n");
            Console.WriteLine("Do you want to save the game you just played to replay this later?");
            string choice;
            int option = - 1;

                choice = Console.ReadLine();
                try
                {
                    option = Int32.Parse(choice);
                }
                catch (Exception)
                {
                    Console.WriteLine("Please choose 1 for yes or anything else for no.");
                }

            if (option == 1)
            {
                //save current game
                try
                {
                    savedGames.Add(gameHistory);
                }
                catch (Exception)
                {
                    Console.WriteLine("There are no saved games or the file was corrupted.");
                }
            }
        }

        //private static List<GameHistory_Caretaker> loadSavedGames()
        //{
        //    //read from a file

        //    //add saved games to the list

        //}
    }
}
