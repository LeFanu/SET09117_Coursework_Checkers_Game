using System;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/11/01
    *
    ** Description:
    *   This class is a part of Memento Pattern. It creates an object of the details we want to store and encapsulates that.
    *
    ** Future updates:
    *   
    ** Design Patterns used:   
    *   Memento is the main part of Memento pattern. It is the state of the object we want to save and store in a list.
    *   Memento allows to create an object of various variables and/or object we want to store
    *
    ** Last Update: 07/11/2017
    */


    public class BoardState_Memento
    {
        //state of the board with all states of the positions
        private PiecePosition[,] piecesPositions;
        private String playerDetails;

        //constructor
        public BoardState_Memento(PiecePosition[,] piecesPositions, String playerDetails)
        {
            this.piecesPositions = piecesPositions;
            this.playerDetails = playerDetails;
        }

        public PiecePosition[,] getPiecesPositions => piecesPositions;

        public string getPlayerDetails => playerDetails;
    }
}