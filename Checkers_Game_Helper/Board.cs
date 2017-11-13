using System;
using System.Collections.Generic;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    *   This class contains information about the board. 
    *   It is responsible to print and reset the board as required and will provide all the logic for the interactions with the board.
    *
    ** Future updates:
    *   This class should provide all the interactions with the board. At this point Player class is changing the status of the fields after the move.
    *   It would be more reasonable and better if this class would do so.
    *   This class presents the board and reset the board to original state at the begining of the game
    *   
    ** Design Patterns Used:
    *   Board is a Singleton class as there is only one board needed to play the game. And it has to be the same for both players
    *   This was temporarily changed at the final stage of development. It is no longer Singleton, but the final decision was not made yet.
    *
    ** Last Update: 13/11/2017
    */

     //Singleton Class
    public class Board
    {
        private static Board currentBoard_Instance;

//-------------- Instance Fields ------------------------------------------------------------------------
        private static int MAX_SIZE = 8;
        
        //grid of position objects to store the details about each field on board
        private PiecePosition[,] gridXY = new PiecePosition[8, 8];
        private PiecePosition pawnPiecePosition;

        //fields for setting the board with the positions
        private int topLetters = 65;
        private int topDesignation = 0;
        private int sideDesignation = 0;

//_______________________________________________________________________________________________________________
        public PiecePosition[,] GridXY
        {
            get { return gridXY; }
            set { gridXY = value; }
        }
        public static Board CurrentBoardInstance
        {
            get
            {
                if (currentBoard_Instance == null)
                {
                    currentBoard_Instance = new Board();
                    
                }
                return currentBoard_Instance;
            }
            
        }

//-------------- Class Constructor ------------------------------------------------------------------------
        private Board()
        {
            resetBoardPositions();
        }


//|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||

        //drawing board for each move displaying the current state of the fields in game
        public void drawBoard()
        {
            //draw the letter designations
            drawLetterCoordinates();

            //writing each row with positions
            for (int y = 0; y < MAX_SIZE; y++)
            {
                Console.WriteLine("\n\t   ________________________________");
                //position on the left side of the board
                Console.Write("\t "+ (sideDesignation + 1) + " ");

                //writing each column
                for (int x = 0; x < MAX_SIZE; x++)
                {
                    Console.Write("|" + fieldState(x,y) + "|");
                    topDesignation++;
                }

                topDesignation = topLetters;
                Console.Write("  " + ( sideDesignation + 1) + " ");
                sideDesignation++;
            }

            //Writing bottom line of positions
            Console.Write("\t  ");           
            drawLetterCoordinates();
            Console.WriteLine();
            sideDesignation = 0;
        }

        public void drawBoard(PiecePosition[,] gridForReplay)
        {
            gridXY = gridForReplay;
            drawBoard();
        }

        //method for reseting the board at start or when quiting the game
        public void resetBoardPositions()
        {
            //writing each row with positions
            for (int y = 0; y < MAX_SIZE; y++)
            {
                //writing each column
                for (int x = 0; x < MAX_SIZE; x++)
                {
                    //creating new position object to save in the dictionary
                    pawnPiecePosition = new PiecePosition();
                    //checking field validation
                    if ((sideDesignation%2 != 0 && topDesignation%2 != 0) || ( topDesignation % 2 == 0 && sideDesignation % 2 == 0 ) )
                    {
                        //setting white pawns
                        if (sideDesignation < 3)
                        {
                                pawnPiecePosition.State = PieceState.White;
                        }
                        //setting red pawns
                        else if (sideDesignation >= 5)
                        {
                            pawnPiecePosition.State = PieceState.Red;
                        }
                        //remaining fields are empty
                        else
                        {
                            pawnPiecePosition.State = PieceState.Valid;
                        }
                    }
                    //all other fields are invalid and their state will remain the same
                    else
                    {
                            pawnPiecePosition.State = PieceState.Invalid;
                    }
                    //adding current coordinates state to the dictionary as a part of the board
                    pawnPiecePosition.XCoordinates = topDesignation;
                    pawnPiecePosition.YCoordinates = sideDesignation; 

                    GridXY[x,y] = pawnPiecePosition;
                    //moving to the next field
                    topDesignation++;
                }

                //setting letters back to A and moving row down
                sideDesignation++;
                topDesignation = 0;
            }
            sideDesignation = 0;
            topDesignation = 0;
        }

        private void drawLetterCoordinates()
        {
            //Writing line of letter coordinates
            Console.WriteLine("\n");
            Console.Write("\t  ");
            for (int i = 0; i < MAX_SIZE; i++)
            {
                //casting integer to letter charachter
                Console.Write("  " + (char)topLetters + " ");
                topLetters++;
            }
            topLetters = 65;
        }


        //method for printing board fields depending on the state
        private String fieldState(int x, int y)
        {
            //changes below made to match Visual Studio in JKCC
            int fieldState = (int)gridXY[x, y].State;

            String fieldDesignation = " ";
            switch (fieldState)
            {
                //case PieceState.Invalid:
                case -1:
                {
                    fieldDesignation = "__";
                    break;
                }
                //case PieceState.Valid:
                case 0:
                {
                    fieldDesignation = "  ";
                        break;
                }
                // case PieceState.White:
                case 1:
                {
                    if (gridXY[x,y].IsKing)
                    {
                        fieldDesignation = " W";
                    }
                    else
                    {
                        fieldDesignation = " w";
                    }
                    break;
                }
                //case PieceState.Red:
                case 2:
                {
                    if (gridXY[x, y].IsKing)
                    {
                        fieldDesignation = " R";
                    }
                    else
                    {
                        fieldDesignation = " r";
                    }
                        break;
                }

            }
            return fieldDesignation;
        }

        public void drawNames(Player player1, Player player2, String turnColour, int turn)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n");
            Console.WriteLine("Turn " + turn + ": " + turnColour + " player moves now: ");
            Console.WriteLine("Player 1: " + player1.Name + " " + player1.PawnsColour + " " + player1.NumberOfPawns + "\t\t\tPlayer 2: " + player2.Name + " " + player2.PawnsColour + " " + player2.NumberOfPawns);
            Console.Write("________________________________________________________________________________________");
        }
    }
}