using System;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    * This class contains the details of each field of the board and providing information of it's current state
    *
    ** Future updates:
    *   
    ** Design Patterns Used:
    *
    ** Last Update: 16/10/2017
    */


    public class PiecePosition
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        private PieceState pieceState;
        
        private Boolean isKing;
        private PlayerColour pieceColour;
        private int yCoordinates;
        private int xCoordinates;

        
        //POSSIBLY NOT NEEDED
        private int[] neighbourFields;

        public PieceState State
        {
            get { return pieceState; }
            set { pieceState = value; }
        }
        public int XCoordinates
        {
            get { return xCoordinates; }
            set { xCoordinates = value; }
        }
        public int YCoordinates
        {
            get { return yCoordinates; }
            set { yCoordinates = value; }
        }
        public int[] NeighbourFields
        {
            get { return neighbourFields; }
            set { neighbourFields = value; }
        }
        public bool IsKing
        {
            get { return isKing; }
            set { isKing = value; }
        }
        public PlayerColour PieceColour
        {
            get { return pieceColour; }
            set { pieceColour = value; }
        }


//-------------- Class Constructor ------------------------------------------------------------------------
        public PiecePosition()
        {

        }


        //|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||

        //checking the given field
        


        //********* TO UPDATE OR CHANGE
        public void setDiagonalFields(int currentField)
        {
            NeighbourFields = new int[4];
            NeighbourFields[0] = currentField - 9;
            NeighbourFields[1] = currentField - 7;
            NeighbourFields[2] = currentField + 7;
            NeighbourFields[3] = currentField + 9;
            int i = 0;
            foreach (int fieldDistance in NeighbourFields)
            {
                
                if (fieldDistance <= 1)
                {
                    NeighbourFields[i] = -1;
                }
                i++;
            }
                
            
        }
        
    }
}