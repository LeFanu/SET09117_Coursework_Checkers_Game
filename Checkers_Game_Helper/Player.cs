using System;
using System.Collections.Generic;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    *   This class is to provide a player for the game with all the features needed.
    *
    ** Future updates:
    *   
    ** Design Patterns Used:
    *
    ** Last Update: 27/10/2017
    */



    public class Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        protected String name;
//********************* NOT NEEDED
       // protected Boolean isMovingNow;
        protected Boolean hasWon;

        protected int numberOfPawns;
        protected PieceState pawnsColour;
        protected String oponent;

        //collections of pawns positions with the details about each field
        protected List<PiecePosition> piecesOfThePlayer = new List<PiecePosition>();

        protected List<int[]> legalMoves;
        //instance of the board for a current game
        protected Board currentBoard = Board.CurrentBoardInstance;
        protected Game currentGame = Game.CurrentGameInstance;

        //coordinates validation
        private bool validStart;
        private bool validDedstination;

        protected String orientation;

//_______________________________________________________________________________________________
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public PieceState PawnsColour
        {
            get { return pawnsColour; }
            set { pawnsColour = value; }
        }
        public int NumberOfPawns
        {
            get { return numberOfPawns; }
            set { numberOfPawns = value; }
        }
        public bool HasWon
        {
            get { return hasWon; }
            set { hasWon = value; }
        }

        public bool ValidStart
        {
            get { return validStart; }
            set { validStart = value; }
        }
        public bool ValidDedstination
        {
            get { return validDedstination; }
            set { validDedstination = value; }
        }

        public List<PiecePosition> PiecesOfThePlayer
        {
            get { return piecesOfThePlayer; }
            set { piecesOfThePlayer = value; }
        }


//-------------- Class Constructor ------------------------------------------------------------------------
        public Player()
        {
            
        }

        public Player(String name, int playerNumber)
        {
            this.name = name;
            numberOfPawns = 12;
            chooseColour(playerNumber);
            setOponent();
        }

//||||||||||||||||||||||||||||||||||||||| CLASS METHODS ||||||||||||||||||||||||||||||||||||||||||||

//*********PROBABLY NOT NEEDED 
        protected void chooseColour(int number)
        {
            if (number == 1)
            {
                pawnsColour = PieceState.White;
                orientation = "Down";
            }
            else
            {
                pawnsColour = PieceState.Red;
                orientation = "Up";
            }
        }


        //checking the board for the current player pawns
        //counting them and assigining thos positions to player's dictionary

        

        //checking if given coordinates are valid to start the move
        public void isValidCoordinateToUse()
        {
            String fieldState;
            try
            {
                //asking for coordinates and checking if this is current's player pawn to be moved
                String coordinates = Console.ReadLine();

                //fieldState = getCoordinatesToCheckState(coordinates);
                //if (!fieldState.Equals(pawnsColour))
                //{
                //    Console.WriteLine("You can only move your pawns. Please choose again");
                //    validStart = false;
                //}
                //validStart = true;
                //if (!fieldState.Equals("Valid"))
                //{
                //    Console.WriteLine("You cannot move your pawn onto this field. Please choose again");
                //    validDedstination = false;
                //}
                validDedstination = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid coordinates");
            }
        }


        private int[] getCoordinatesToCheckState(String coordinates)
        {
            int column = getColumn(char.ToUpper(coordinates[0]));
            int row = getRow(coordinates.Substring(1));
            int[] temp = {column, row};
            return temp;

        }




        ////checking if both start and destination fields are correctly chosend and if the move can be performed
        public Boolean makeValidMove()
        {
            int[] playersMove = new int[4];
            //asking for valid coordinates until they're valid

            Console.Write("\nPlease enter coordinates of the pawn you want to move: >>>\t");
            String startCoordinates = Console.ReadLine();
            try
            {
                playersMove[0] = getCoordinatesToCheckState(startCoordinates)[0];
                playersMove[1] = getCoordinatesToCheckState(startCoordinates)[1];
                
            }
            catch (Exception )
            {
                Console.WriteLine("Invalid start coordinates.");
                return false;
            }

            Console.Write("\nPlease enter destination coordinates >>>\t");
            String destinationCoordinates = Console.ReadLine();
            try
            {
                playersMove[2] = getCoordinatesToCheckState(destinationCoordinates)[0];
                playersMove[3] = getCoordinatesToCheckState(destinationCoordinates)[1];
                
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid start coordinates.");
                return false;
            }
            return true;

        }



        //checking if current player has any capture moves
        public Boolean canCapture()
        {
            int letter;
            int side;
            string startCoordinates;
            //string fieldState;
            foreach (var pawn in PiecesOfThePlayer)
            {
                letter = (int)(pawn.XCoordinates) % 8;
                side = pawn.YCoordinates;
                //startCoordinates = pawn;
                //for each pawn of current player check diagonal fields
               // Console.WriteLine("Checking field " + startCoordinates);
                //checkNeighbours(pawn.Value);
                //getStatusOfFieldsAhead(letter, side, startCoordinates);

            }




            return false;
        }

        public void checkingFieldsAround(int letter, int row, string startCoordinates)
        {
            //strings to hold coordinates numbers
            string leftTopField, rightTopField, leftBottomField, rightBottomField;
            //strings to hold the state of thise fields
            string neighbourFieldState, desitnationField = "";
            try
            {
                //getting coordinates of diagonal fields
                leftTopField = (char)(letter - 2) + (row - 2).ToString();
                neighbourFieldState = (char)(letter - 1) + (row - 1).ToString();
                //getting the states of those diagonal fields
                //neighbourFieldState = currentBoard.PlayerPositions[neighbourFieldState].State.ToString();
                //desitnationField = currentBoard.PlayerPositions[leftTopField].State.ToString();
                //if the neighbour is oponent and next field is valid, then we have a capture move
                if (neighbourFieldState.Equals(oponent) && leftTopField.Equals("Valid"))
                {
                    String[] validMove = { startCoordinates, desitnationField };

                    //captureMoves.Add(validMove);
                }
            }
            catch (Exception)
            {
                
            }
        }



        private void setOponent()
        {
            if (pawnsColour == PieceState.Red)
            {
                oponent = "Red";
            }
            else
            {
                oponent = "White";
            }
        }


        private int getColumn(char coordinates)
        {
            int column;
            try
            {
                column = Int32.Parse(coordinates.ToString());
            }
            catch (Exception)
            {
                column = -1;
            }
            return column;
        }

        private int getRow(string coordinates)
        {
            int row;
            try
            {
                row = Int32.Parse(coordinates);
            }
            catch (Exception)
            {
                row = -1;
            }
            return row;
        }

        

       
    }
}