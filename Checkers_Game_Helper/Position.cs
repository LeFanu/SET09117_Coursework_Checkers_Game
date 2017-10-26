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


    public class Position
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        private PositionState positionState;
        private PositionLetterCoordinates letterCoordinates;
        private int sideCoordinates;
        private String oponent;

        public PositionState State
        {
            get { return positionState; }
            set { positionState = value; }
        }
        public PositionLetterCoordinates LetterCoordinates
        {
            get { return letterCoordinates; }
            set { letterCoordinates = value; }
        }
        public int SideCoordinates
        {
            get { return sideCoordinates; }
            set { sideCoordinates = value; }
        }


//-------------- Class Constructor ------------------------------------------------------------------------
        public Position()
        {

        }


//|||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||
        public override string ToString()
        {
            return  State.ToString();
        }

        public void getOponent()
        {
            if (positionState == PositionState.White)
            {
                oponent = "Black";
            }
            else if (positionState == PositionState.Black) 
            {
                oponent = "White";
            }   
        }
    }
}