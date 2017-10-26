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
    ** Last Update: 16/10/2017
    */

        //Singleton
    public class Board
    {
        private static Board currentBoard_Instance;

//-------------- Instance Fields ------------------------------------------------------------------------
        private static int MAX_SIZE = 8;

        //dictionary for storing the coordinates of fields and their state
        private SortedDictionary<String, Position> playerPositions = new SortedDictionary<String, Position>();

        //position and coordinates for the dictionary to use during the game
        private String positionCoordinates;
        private String fieldDesignation;
        private Position pawnPosition;

        //fields for setting the board with the positions
        private const int TOP_LETTERS = 65;
        private const int SIDE_NUMBERS = 1;
        private int topDesignation = TOP_LETTERS;
        private int sideDesignation = SIDE_NUMBERS;


        public SortedDictionary<string, Position> PlayerPositions
        {
            get { return playerPositions; }
            set { playerPositions = value; }
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
                    positionCoordinates = (char)topDesignation + sideDesignation.ToString();
                    Console.Write("|" + fieldState(PlayerPositions[positionCoordinates].State) + "|");
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
            for (int i = 1; i <= MAX_SIZE; i++)
            {
                //writing each column
                for (int j = 1; j <= MAX_SIZE; j++)
                {
                    //creating new position object to save in the dictionary
                    pawnPosition = new Position();

                    //settings coordinates for the dictionary
                    positionCoordinates = (char) topDesignation + sideDesignation.ToString();

                    //checking field validation
                    if ((sideDesignation%2 != 0 && topDesignation%2 != 0) || (sideDesignation % 2 == 0 && topDesignation % 2 == 0))
                    {
                        //setting black pons
                        if (sideDesignation <= 3)
                        {
                            pawnPosition.State = PositionState.White;
                            
                        }
                        //setting white pons
                        else if (sideDesignation >= 6)
                        {
                            pawnPosition.State = PositionState.Black;
                            
                        }
                        //remaining fields are empty
                        else
                        {
                            pawnPosition.State = PositionState.Valid;
                        }
                    }
                    //all other fields are invalid and their state will remain the same
                    else
                    {
                            pawnPosition.State = PositionState.Invalid;
                    }
                    //adding current coordinates state to the dictionary as a part of the board
                    pawnPosition.LetterCoordinates = (PositionLetterCoordinates)topDesignation;
                    pawnPosition.SideCoordinates = sideDesignation;
                    pawnPosition.getOponent();
                    PlayerPositions.Add(positionCoordinates, pawnPosition);
                    //moving to the next field
                    topDesignation++;
                    
                }

                //setting letters back to A and moving row down
                sideDesignation++;
                topDesignation = TOP_LETTERS;
            }
            sideDesignation = SIDE_NUMBERS;
            topDesignation = TOP_LETTERS;
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
                case PositionState.Invalid:
                {
                    fieldDesignation = "__";
                    break;
                }
                case PositionState.Valid:
                {
                    fieldDesignation = "  ";
                        break;
                }
                case PositionState.White:
                {
                    fieldDesignation = " w";
                        break;
                }
                case PositionState.Black:
                {
                    fieldDesignation = " b";
                        break;
                }
                case PositionState.White_King:
                {
                    fieldDesignation = " W";
                        break;
                }
                case PositionState.Black_King:
                {
                    fieldDesignation = " B";
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