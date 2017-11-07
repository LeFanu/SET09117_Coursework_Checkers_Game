using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Checkers_Game_Helper;


/** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/11/04
    *
    ** Description:
    *   This class contains is responsible for saving  and loading all played games into and from a file.
    *   It is basically a storage for all games that would allow player to replay any game previously played. Not just the last one.
    *   This class is not operational and will be developed at later time.
    *   
    ** Future updates:
    *   
    ** Design Patterns Used:
    *   
    ** Last Update: 07/11/2017
    */

namespace Checkers_Console_v1
{
    [Serializable]
    public class GamesDB
    {
        private List<GameHistory_Caretaker> games_DB;
        //-----------READING FILES ON STARTUP AND CREATING LISTS----------------------------
        //fields and references for serialization

        FileStream stream;

        string filename;

        BinaryFormatter formatter = new BinaryFormatter();


        public void saveGamesToFile()
        {
            filename = "gamesDB.data";
            stream = File.Create(filename);
            formatter.Serialize(stream, games_DB);
            stream.Close();

        }

        private  List<GameHistory_Caretaker> loadSavedGames()
        {
            //read from a file

            //add saved games to the list


            try
            {
                //we are accessing our file
                filename = "gamesDB.data";
                stream = File.OpenRead(filename);
                try
                {
                    games_DB = (List<GameHistory_Caretaker>)formatter.Deserialize(stream);
                }
                catch (Exception)
                {

                }
                stream.Close();

            }
            catch (FileNotFoundException)
            {
                
            }

            return games_DB;
        }
    }
}