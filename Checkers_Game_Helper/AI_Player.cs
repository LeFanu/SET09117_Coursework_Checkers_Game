using System;
using System.Threading;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 16/10/2017
    *
    ** Description:
    *   This class is to provide AI player for the game with all the features needed.
    *   At this point it consists only of one method for making random move. 
    *   It simply checks all of it's possible moves and select one at random.
    *   There is no need to worry about capture moves as they will be only valid ones if they exist.
    *   The rest of the logic happens in superclass
    *
    ** Future updates:
    *   
    ** Design Patterns Used:
    *
    ** Last Update: 04/11/2017
    */

    public class AI_Player : Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------

        
//-------------- Class Constructor ------------------------------------------------------------------------
        public AI_Player(String colour) : base("Computer", colour)
        {

        }

//|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||
     
        //making random move from the list of available ones
        public void makeRandomMove()
        {
            //get random to choose one of the moves
            Random rnd = new Random();
            int[] selectedMove = { -1};

            try
            {
                int randomMove = rnd.Next(0, (legalMoves.Count));
                //get random move
                selectedMove = legalMoves[randomMove];                
            }
            catch (Exception )
            {
                //if the array is returned empty it means there are no moves for the player
                Console.WriteLine("This player has no valid moves this turn");
            }

            //if there is any move we want to make it
            if (selectedMove[0] != -1)
            {
                //this method changes all the fields on the board and adjust them to match the chosen move
                changeFieldsOnBoardAfterMove(selectedMove);

                //the code below simply informs what move computer is taking
                String startCoordinates = (char)(selectedMove[0] + 65) + (selectedMove[1] + 1).ToString();
                String destinationCoordinates = (char)(selectedMove[2] + 65) + (selectedMove[3] + 1).ToString();
                Thread.Sleep(500);
                Console.WriteLine("Start coordinates: " + startCoordinates);
                Thread.Sleep(500);
                Console.WriteLine("Destination coordinates: " + destinationCoordinates);
                Thread.Sleep(200);
            }

            Console.Write("Player turn in ");
            for (int i = 3; i > 0; i--)
            {
                Console.Write(i + "... ");
                Thread.Sleep(800);
            }
        }

        
    }
}