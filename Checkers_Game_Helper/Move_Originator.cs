using System;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/11/01
    *
    ** Description:
    *   This class is a part of Memento Pattern
    *
    ** Future updates:
    *   This should be also a Singleton as each game do not need more than one Originator
    *   
    ** Design Patterns used:   
    *   Part of the Memento Pattern. Originator is responsible for creating mementos and storing them in Caretaker
    *
    ** Last Update: 07/11/2017
    */


    public class Move_Originator
    {
        //state of the board with all states of the positions
        private PiecePosition[,] currentPiecesPositions;
        private String movingPlayerDetails;

//|||||||||||||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //saves the current version of the memento(state of the board) and all pieces on it at this point
        public void saveCurrentMove(PiecePosition[,] newPiecePositions)
        {
            currentPiecesPositions = new PiecePosition[8,8];
            //Console.WriteLine("From Originator: Current state of the board: ");
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    currentPiecesPositions[x, y] =  (PiecePosition) newPiecePositions[x, y].Clone();
                }
            }
        }

        //saving some details about current move for replaying feature
        public void savePlayerDetails(String playerDetails)
        {
            movingPlayerDetails = playerDetails;
        }
        //creates new state of the board and passes in the copy of the board grid to save
        public BoardState_Memento createBoardStateMemento()
        {
            //Console.WriteLine("Originator saved new state of the board");

            return new BoardState_Memento(currentPiecesPositions, movingPlayerDetails);
        }

        //asks caretaker for the copy of the state of the board previously saved to restore that
        public PiecePosition[,] restoreMoveFromHistory(BoardState_Memento savedStateOfTheBoardPositions)
        {
               // Console.WriteLine("Originator restored board state from Memento");
                currentPiecesPositions = savedStateOfTheBoardPositions.getPiecesPositions;

            return currentPiecesPositions;
        }


    }
}
