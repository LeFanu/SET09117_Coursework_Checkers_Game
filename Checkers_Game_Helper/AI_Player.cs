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
    ** Last Update: 26/10/2017
    */

    public class AI_Player : Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------

        
//-------------- Class Constructor ------------------------------------------------------------------------
        public AI_Player()
        {
            Name = "Computer";
            //chooseColour(number);
            numberOfPawns = 12;
            pawnsColour = PieceState.Red;
            orientation = "Up";
        }

//|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||

        //cheacking each pawn in the player's collection if there is available move       

        //making random move from the list of available ones
        public void makeRandomMove()
        {
            //get available legal moves for this player
            legalMoves = currentGame.getLegalMoves;
            //get random to choose one of the moves
            Random rnd = new Random();
            try
            {
                int randomMove = rnd.Next(0, (legalMoves.Count - 1));

                //get random move
                int[] selectedMove = legalMoves[randomMove];
                currentBoard.GridYX[selectedMove[0] - 1, selectedMove[1] - 1].State = PieceState.Valid;
                currentBoard.GridYX[selectedMove[2] - 1, selectedMove[3] - 1].State = PawnsColour;

                String startCoordinates = (char)(selectedMove[1] + 64) + selectedMove[0].ToString();
                String destinationCoordinates = (char)(selectedMove[3] + 64) + selectedMove[2].ToString();
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("Start coordinates: " + startCoordinates);
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("Destination coordinates: " + destinationCoordinates);
                System.Threading.Thread.Sleep(500);
            }
            catch (Exception )
            {
                Console.WriteLine("This player has no valid moves this turn");
            }


            Console.Write("Player turn in ");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(i + "... ");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}