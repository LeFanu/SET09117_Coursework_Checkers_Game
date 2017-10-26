using System;
using System.Collections.Generic;


namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 16/10/2017
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

//-------------- Class Constructor ------------------------------------------------------------------------
    public class AI_Player : Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------

        //list of valid moves
        private List<String[]> validMoves = new List<String[]>();
        
//-------------- Class Constructor ------------------------------------------------------------------------
        public AI_Player()
        {
            Name = "Computer";
            //chooseColour(number);
            pawnsColour = "Black";
            orientation = "Up";
            setPositions();
        }

//|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||

        //cheacking each pawn in the player's collection if there is available move       
        public  void checkIfMoveIsPossible()
        {
            int letter;
            int side;
            string startCoordinates;
            //string fieldState;
            foreach (var pawn in pawnsPositions)
            {
                letter = (int)pawn.Value.LetterCoordinates;
                side = pawn.Value.SideCoordinates;
                startCoordinates = pawn.Key;

                getStatusOfFieldsAhead(letter, side, startCoordinates);

            }
        }

        private void getStatusOfFieldsAhead(int letter, int row, string startCooridnates)
        {
            String fieldState;
            if (letter > 65)
            {
                if (orientation.Equals("Up"))
                {
                    leftDiagonalField = (char)(letter - 1) + (row - 1).ToString();
                }
                else
                {
                    leftDiagonalField = (char)(letter - 1) + (row + 1).ToString();
                }

                fieldState = currentBoard.PlayerPositions[leftDiagonalField].State.ToString();
                if (fieldState.Equals("Valid"))
                {
                    String[] validMove = { startCooridnates, leftDiagonalField };

                    validMoves.Add(validMove);
                }
                

            }
            if (letter < 72)
            {

                if (orientation.Equals("Up"))
                {
                    rightDiagonalField = (char)(letter + 1) + (row - 1).ToString();
                }
                else
                {
                    rightDiagonalField = (char)(letter + 1) + (row + 1).ToString();
                }

                fieldState = currentBoard.PlayerPositions[rightDiagonalField].State.ToString();
                if (fieldState.Equals("Valid"))
                {
                    String[] validMove = { startCooridnates, rightDiagonalField };
                    validMoves.Add(validMove);
                }

            }

        }

        //making random move from the list of available ones
        public void makeRandomMove()
        {
            Random rnd = new Random();
            int randomMove = rnd.Next(validMoves.Count);
            ValidStart = true;
            ValidDedstination = true;
            StartCoordinates = validMoves[randomMove][0];
            DestinationCoordinates = validMoves[randomMove][1];
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Start coordinates: " + StartCoordinates);
            System.Threading.Thread.Sleep(500);
            Console.WriteLine("Destination coordinates: " + DestinationCoordinates);
            System.Threading.Thread.Sleep(500);
            isValidMove();

            Console.Write("Player turn in ");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(i + "... ");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}