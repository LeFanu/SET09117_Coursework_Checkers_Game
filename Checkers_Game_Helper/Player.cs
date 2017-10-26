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
    ** Last Update: 24/10/2017
    */



    public class Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        protected String name;
//********************* NOT NEEDED
       // protected Boolean isMovingNow;
        protected Boolean hasWon;

        protected int numberOfPons;
        protected String pawnsColour;

        //collections of pawns positions with the details about each field
        protected SortedDictionary<String, Position> pawnsPositions = new SortedDictionary<String, Position>();
        
        //instance of the board for a current game
        protected Board currentBoard = Board.CurrentBoardInstance;
        
        //the coordinates for each move
        private String startCoordinates;
        private String destinationCoordinates;
        
        //coordinates validation
        private bool validStart;
        private bool validDedstination;

        protected String orientation;

        protected String leftDiagonalField;
        protected String rightDiagonalField;
        //_______________________________________________________________________________________________
        //public bool IsMovingNow
        //{
        //    get { return isMovingNow; }
        //    set { isMovingNow = value; }
        //}
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string PawnsColour
        {
            get { return pawnsColour; }
            set { pawnsColour = value; }
        }
        public int NumberOfPons
        {
            get { return numberOfPons; }
            set { numberOfPons = value; }
        }
        public bool HasWon
        {
            get { return hasWon; }
            set { hasWon = value; }
        }
        //this dictionary contains all positions of the player pawns
        public SortedDictionary<string, Position> PawnsPositions
        {
            get { return pawnsPositions; }
            set { pawnsPositions = value; }
        }
        public string StartCoordinates
        {
            get { return startCoordinates; }
            set { startCoordinates = value; }
        }
        public string DestinationCoordinates
        {
            get { return destinationCoordinates; }
            set { destinationCoordinates = value; }
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


//-------------- Class Constructor ------------------------------------------------------------------------
        public Player()
        {
            
        }

        public Player(String name, int playerNumber)
        {
            this.name = name;
            chooseColour(playerNumber);
           setPositions();
        }

//||||||||||||||||||||||||||||||||||||||| CLASS METHODS ||||||||||||||||||||||||||||||||||||||||||||

//*********PROBABLY NOT NEEDED 
        protected void chooseColour(int number)
        {
            if (number == 1)
            {
                pawnsColour = "White";
                orientation = "Down";
            }
            else
            {
                pawnsColour = "Black";
                orientation = "Up";
            }
        }


        //checking the board for the current player pawns
        //counting them and assigining thos positions to player's dictionary
        public void setPositions()
        {
            pawnsPositions.Clear();
            foreach (var position in currentBoard.PlayerPositions)
            {
                if (position.Value.State.ToString() == pawnsColour)
                {
                    pawnsPositions.Add(position.Key, position.Value);
                }
            }
            numberOfPons = pawnsPositions.Count;
        }

        //checking if given coordinates are valid to start the move
        public void isValidStartPosition(String colour)
        {
            String fieldState;
            try
            {
                //asking for coordinates and checking if this is current's player pawn to be moved
                startCoordinates = Console.ReadLine();
                startCoordinates = char.ToUpper(startCoordinates[0])+startCoordinates.Substring(1);
                fieldState = currentBoard.PlayerPositions[startCoordinates].State.ToString();
                if (!fieldState.Equals(colour))
                {
                    Console.WriteLine("You can only move your pawns. Please choose again");
                    ValidStart =  false;
                }
                //Console.WriteLine("Start: " + startCoordinates);
                ValidStart =  true;
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid coordinates");
            }
        }

        //checking if given coordinates are valid for the destination of current move
        public void isValidDestination()
        {
            String fieldState;
            try
            {
                //asking for the destination coordinates and checking if this is a free field
                destinationCoordinates = Console.ReadLine();
                destinationCoordinates = char.ToUpper(destinationCoordinates[0]) + destinationCoordinates.Substring(1);

                fieldState = currentBoard.PlayerPositions[destinationCoordinates].State.ToString();
                if (!fieldState.Equals("Valid"))
                {
                    Console.WriteLine("You cannot move your pawn onto this field. Please choose again");
                    ValidDedstination = false;
                }
                //Console.WriteLine("Dest: " + destinationCoordinates);
                ValidDedstination = true;
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid coordinates");
            }
        }

        //checking if both start and destination fields are correctly chosend and if the move can be performed
        public Boolean isValidMove()
        {
            if (ValidStart && ValidDedstination)
            {
                currentBoard.PlayerPositions[StartCoordinates].State = PositionState.Valid;

                if (PawnsColour == "Black")
                {
                    currentBoard.PlayerPositions[DestinationCoordinates].State = PositionState.Black;
                    
                }
                else
                {
                    currentBoard.PlayerPositions[DestinationCoordinates].State = PositionState.White;
                    
                }
                //setting new player positions after the move
                setPositions();
                return true;
            }
            return false;
        }

        
        //checking if current player has any capture moves
        public Boolean canCapture()
        {
            foreach (var position in pawnsPositions)
            {

            }
            //for each pawn of current player check diagonal fields


            return false;
        }



    }
}