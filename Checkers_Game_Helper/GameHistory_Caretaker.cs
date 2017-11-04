using System;
using System.Collections.Generic;

namespace Checkers_Game_Helper
{

    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/11/01
    *
    ** Description:
    *   This class is a part of Memento Pattern. It stores all the mementos, which are basically states of the game board after every move.
    *   Therefore it is actually the record of the game history
    *
    ** Future updates:
    *   
    ** Design Patterns used:   
    *   Caretaker is a part of Memento pattern responsible for storing saved mementos in the list and returning requested mementos from history
    *   It uses Singleton as well as only one game history is required for the game. At the end of the game the whole game history can be saved.
    *   But for the new game new one will be created and again only one.
    ** Last Update: 04/11/2017
    */

        //Singleton class
    public class GameHistory_Caretaker
    {
        private static GameHistory_Caretaker gameHistory;
        private List<BoardState_Memento> piecePositionsHistory;

        public static GameHistory_Caretaker GameHistory
        {
            get
            {
                if (gameHistory == null)
                {
                    gameHistory = new GameHistory_Caretaker();
                }
                return gameHistory;
            }
           
        }

//-------------- Class Constructor ------------------------------------------------------------------------
        private GameHistory_Caretaker()
        {
            piecePositionsHistory = new List<BoardState_Memento>();
        }

//|||||||||||||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        //simply adds saved memento to the list 
        public void addPiecesPositionsToHistory(BoardState_Memento piecesPositions)
        {
            
            piecePositionsHistory.Add(piecesPositions);
        }

        //gets the last item on the list
        public BoardState_Memento getPiecePositionsHistory(int index)
        {
            //get the length of the array to know what is the index of the last item
            //int lastItem = piecePositionsHistory.Count - 1;
            BoardState_Memento stateToReturn;
            //make it to return last
            try
            {
                stateToReturn =  piecePositionsHistory[index];
            }
            catch (Exception)
            {
                return null;
            }
            //probably don't want to remove last item
            //piecePositionsHistory.RemoveAt(lastItem);
            return stateToReturn;
        }

        //completing move after redo or undo deletes any newer versions of the board if they are present
        //if the undo/redo is chosen and player goes to specific point it seems logical any newer ones are not needed
        //this is also needed for replaying the game as there is no point to show undone moves
        public void deleteNewerHistoryAfterUndoRedo(int chosenPoint)
        {
            int lastIndex = piecePositionsHistory.Count;
            if (chosenPoint < lastIndex)
            {
                piecePositionsHistory.RemoveRange(chosenPoint , lastIndex - chosenPoint);
            }
        }
    }
}