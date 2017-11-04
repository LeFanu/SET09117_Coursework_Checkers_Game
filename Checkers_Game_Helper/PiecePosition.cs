using System;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    * This class contains the details of each field of the board and providing information of it's current state.
    * piecestate variable is used for information about each field's state as they can change. This utilizes enums. 
    * Other variables are self explanatory. The reason why all this data exists together as a class 
    * is that this would be always required together and this was the most reasonable decision to put it together
    *
    ** Future updates:
    *
    ** Last Update: 04/11/2017
    */


    public class PiecePosition : ICloneable
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        private PieceState pieceState;
        private Boolean isKing;
        private int yCoordinates;
        private int xCoordinates;


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
        public bool IsKing
        {
            get { return isKing; }
            set { isKing = value; }
        }


        //|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}