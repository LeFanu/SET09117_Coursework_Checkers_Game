using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
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
        *
        ** Last Update: 07/11/2017
        */

        private static List<GameHistory_Caretaker> savedGames;

        static void Main(string[] args)
        {

            Console.WriteLine("********************************************************");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("**              WELCOME TO CHECKERS GAME              **");
            Console.WriteLine("**                                                    **");
            Console.WriteLine("********************************************************");

            savedGames = new List<GameHistory_Caretaker>();

            mainMenu();
        }

        private static void mainMenu()
        {
            Game chosenGame;
            int option;
            do
            {
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("\nPlease choose one of the following options:");
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

                            do
                            {
                                Console.WriteLine("\n--------------------------------------------------------");
                                Console.WriteLine("\nPlease choose one of the following options:");
                                Console.WriteLine("\t 1.Player VS Computer");
                                Console.WriteLine("\t 2.Player VS Player");
                                Console.WriteLine("\t 3.Computer VS Computer");
                                Console.WriteLine("\t 4.Return to previous menu");

                                Console.Write("Please enter your choice >>> \t");
                                gameOption = Int32.Parse(Console.ReadLine());


                                switch (gameOption)
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    {
                                        Console.WriteLine("\n\n-----------------------------------------------------------------------------");
                                            Console.WriteLine("                   Starting New Game!                        ");
                                        Console.WriteLine("-----------------------------------------------------------------------------");

                                        //clearing saved games until saving to file will not be implemented
                                        savedGames.Clear();
                                        chosenGame = Game.CurrentGameInstance;
                                        chosenGame.setGame(gameOption.ToString());
                                        chosenGame.PlayGame();
                                        saveLastPlayedGame(chosenGame.GameHistory);
                                        gameOption = 4;
                                        break;
                                    }
                                    case 4:
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
                        if (savedGames.Count > 0 )
                        {
                            Board boardToDisplay = Board.CurrentBoardInstance;
                            PiecePosition[,] moveToDisplay;
                            foreach (GameHistory_Caretaker gameToReplay in savedGames)
                            {
                                Console.WriteLine();
                                Console.WriteLine("__________________________________________________________________________________________");
                                Console.WriteLine("Replay of stored game:");
                                //this variable stores the number of moves
                                int gameLength = gameToReplay.getHistoryLength();

                                //Starting from 1 omits the initial state of the board needed for undo feature
                                for (int i = 1; i < gameLength; i++)
                                {
                                    Console.WriteLine("\n\n************************************************************************************************");
                                    Console.WriteLine(gameToReplay.getPiecePositionsHistory(i).getPlayerDetails);
                                    Console.WriteLine();

                                    moveToDisplay = gameToReplay.getPiecePositionsHistory(i).getPiecesPositions;
                                    boardToDisplay.drawBoard(moveToDisplay);

                                    Console.WriteLine();
                                    Console.WriteLine("\n************************************************************************************************");

                                    if (i + 1 < gameLength)
                                    {
                                        Console.Write("Next move in ");
                                        for (int n = 3; n > 0; n--)
                                        {
                                            Console.Write(n + "... ");
                                            System.Threading.Thread.Sleep(800);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("The last move in the game!");
                                        System.Threading.Thread.Sleep(1000);
                                    }
                                }
                                Console.WriteLine();
                                Console.WriteLine("__________________________________________________________________________________________");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nThere are no saved games. Please play a game first and save it at the end to be able to replay that.\n");
                            Console.WriteLine("__________________________________________________________________________________________");
                        }
                        break;
                    }
                    case 3:
                    {
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
            Console.WriteLine("Do you want to save (enter 1) the game you just played to replay this later?");
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
                    Console.WriteLine("Your game was successfully saved!\n");
                }
                catch (Exception)
                {
                    Console.WriteLine("There are no saved games or the file was corrupted.");
                }
            }
        }
    }
}
