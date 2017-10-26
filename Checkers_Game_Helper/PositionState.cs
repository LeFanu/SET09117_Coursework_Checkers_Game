namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    *  Class of Enumaration type for storing the state of each field on the board.
    *
    ** Future updates:
    *   
    *
    ** Last Update: 16/10/2017
    */


    public enum PositionState
    {
        Invalid = -1,
        Valid = 0,
        White = 1,
        Black = 2,
        White_King = 3,
        Black_King = 4
    }
}