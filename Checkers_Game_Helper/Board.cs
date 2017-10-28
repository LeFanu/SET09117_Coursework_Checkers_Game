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
    *   
    ** Design Patterns Used:
    *   Board is a Singleton class as there is only one board needed to play the game. And it has to be the same for both players
    *
    ** Last Update: 27/10/2017
    */

        //Singleton
    public class Board
    {
        private static Board currentBoard_Instance;

//-------------- Instance Fields ------------------------------------------------------------------------
        private static int MAX_SIZE = 8;
        
        //grid of position objects to store the details about each field on board
        private PiecePosition[,] gridYX = new PiecePosition[8, 8];
        private PiecePosition pawnPiecePosition;

        //fields for setting the board with the positions
        private int TOP_LETTERS = 65;
        private const int SIDE_NUMBERS = 1;
        private int topDesignation = 1;
        private int sideDesignation = SIDE_NUMBERS;

//_______________________________________________________________________________________________________________
        public PiecePosition[,] GridYX
        {
            get { return gridYX; }
            set { gridYX = value; }
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
            for (int i = 0; i < MAX_SIZE; i++)
            {
                Console.WriteLine("\n\t   ________________________________");
                //position on the left side of the board
                Console.Write("\t "+ sideDesignation + " ");


                //writing each column
                for (int j = 0; j < MAX_SIZE; j++)
                {
                    Console.Write("|" + fieldState(gridYX[i,j].State) + "|");
                    topDesignation++;

                }
                topDesignation = TOP_LETTERS;
                Console.Write("  " + sideDesignation + " ");
                sideDesignation++;

            }

            //Writing bottom line of positions
            Console.Write("\t  ");           
            drawLetterCoordinates();
            sideDesignation = SIDE_NUMBERS;
        }

        //method for reseting the board at start or when quiting the game
        private void resetBoardPositions()
        {
            //writing each row with positions
            for (int y = 1; y <= MAX_SIZE; y++)
            {
                //writing each column
                for (int x = 1; x <= MAX_SIZE; x++)
                {
                    //creating new position object to save in the dictionary
                    pawnPiecePosition = new PiecePosition();

                    //checking field validation
                    if ((sideDesignation%2 != 0 && topDesignation%2 != 0) || (sideDesignation % 2 == 0 && topDesignation % 2 == 0))
                    {
                        //setting white pawns
                        if (sideDesignation <= 3)
                        {
                            pawnPiecePosition.State = PieceState.White;
                            pawnPiecePosition.PieceColour = PlayerColour.White;
                        }
                        //setting red pawns
                        else if (sideDesignation >= 6)
                        {
                            pawnPiecePosition.State = PieceState.Red;
                            pawnPiecePosition.PieceColour = PlayerColour.Red;
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

                    GridYX[y - 1,x - 1] = pawnPiecePosition;
                    //moving to the next field
                    topDesignation++;

                }

                //setting letters back to A and moving row down
                sideDesignation++;
                topDesignation = 1;
            }
            sideDesignation = SIDE_NUMBERS;
            topDesignation = 1;
        }


        private void drawLetterCoordinates()
        {
            //Writing line of letter coordinates
            Console.WriteLine("\n");
            Console.Write("\t  ");
            for (int i = 0; i < MAX_SIZE; i++)
            {
                //casting integer to letter charachter
                Console.Write("  " + (char)topDesignation + " ");
                topDesignation++;
            }
            topDesignation = TOP_LETTERS;
        }


        //method for printing board fields depending on the state
        private String fieldState(Enum fieldState)
        {
            String fieldDesignation = " ";
            switch (fieldState)
            {
                case PieceState.Invalid:
                {
                    fieldDesignation = "__";
                    break;
                }
                case PieceState.Valid:
                {
                    fieldDesignation = "  ";
                        break;
                }
                case PieceState.White:
                {
                    fieldDesignation = " w";
                        break;
                }
                case PieceState.Red:
                {
                    fieldDesignation = " r";
                        break;
                }
                case PieceState.White_King:
                {
                    fieldDesignation = " W";
                        break;
                }
                case PieceState.Red_King:
                {
                    fieldDesignation = " R";
                        break;
                }

            }
            return fieldDesignation;
        }

        public void drawNames(Player player1, Player player2, int turn)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
            Console.WriteLine("Turn " + turn + " starts: ");
            Console.WriteLine("Player 1: " + player1.Name + " " + player1.PawnsColour + "\t\t\tPlayer 2: " + player2.Name + " " + player2.PawnsColour);
            Console.Write("________________________________________________________________________________________");
        }
    }
}