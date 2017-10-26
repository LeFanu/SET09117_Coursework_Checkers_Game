using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    *   This is the main class for the game that will use all related classes to organize whole game and provide all required features.
    *
    ** Future updates:
    *   
    ** Design Patterns Used:
    *   This class is a Singleton. The reason for this is that we want to have only one active game at the time.
    *   Another reason is that all related classess need to access some of the game data and it has to be the same for all
    *
    ** Last Update: 16/10/2017
    */



        //Singleton
    public class Game
    {

//-------------- Instance Fields ------------------------------------------------------------------------
        private GameType gameType;
        private static Game currentGame_Instance;
        private List<Player> players = new List<Player>();
        private Board gameBoard;
        private String playerName;
        private Boolean winner;
        private String nameOfTheWinner;
        private int turn;

        private int movingPlayer = 1;

//__________________________________________________________________________________________________________
        public static Game CurrentGameInstance
        {
            get
            {
                if (currentGame_Instance == null)
                {
                    currentGame_Instance = new Game();
                    
                }
                return currentGame_Instance;
            }
        }

//-------------- Class Constructor ------------------------------------------------------------------------
        private Game()
        {
            
        }



//|||||||||||||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //main method for playing the game 
        public void PlayGame()
        {

////LINIA TESTOWA---------------------------------------------------
Console.WriteLine("\n\nYou are in game " + gameType);
Console.WriteLine();

            //making move until one of the players wins
            do
            {
                int playerMoving = 0;
                //Players are moving moves
                for (int i = 0; i < 2; i++)
                {
                    gameBoard.drawNames(players[0], players[1], turn);
                   // Console.WriteLine("Player 1 colour is " + players[0].PawnsColour + "\tPlayer 2 colour is " + players[1].PawnsColour);
                    gameBoard.drawBoard();
                    playerMove(players[playerMoving]);
                    
                    //Need to check if player won at this point
                    if (checkIfWinner(players[playerMoving]))
                    {
                        break;
                    }
                    playerMoving++;
                }

                //end of turn
                turn++;
            } while (!winner);


        }

        //reset players and their state to default
        public void setGame(string gameType)
        {
            this.gameType = (GameType)Enum.Parse(typeof(GameType), gameType);
            //players = new Player[2];
            chooseGameType();
            gameBoard = Board.CurrentBoardInstance;
            turn = 1;
            winner = false;
        }
        
        //choosing the type of the game to play based on selection from menu
        private void chooseGameType()
        {
            //choosing game type depending on the game chosen in menu
            switch (gameType)
            {
//***************CHANGE TO USE LIST**
                case GameType.Player_VS_CPU:
                {
                    Console.Write("Player 1 please enter your name: >>> \t");
                    players.Add(playerNames(1));
                    players.Add(new AI_Player());
                    break;
                }
                case GameType.Player_VS_Player:
                {
                    Console.Write("Player 1 please enter your name: >>> \t");
                    players.Add(playerNames(1));
                    Console.Write("Player 2 please enter your name: >>> \t");
                    players.Add(playerNames(2));

                    break;
                }
                case GameType.CPU_VS_Player:
                {
//***************CHANGE TO USE LIST**

                        players[0] = new AI_Player();
                    Console.Write("Player 2 please enter your name: >>> \t");
                    players[1] = playerNames(2);
                    break;
                }
                case GameType.CPU_VS_CPU:
                {
//***************CHANGE TO USE LIST*****

                        players[0] = new AI_Player();
                    players[1] = new AI_Player();
                    break;
                }
            }
//************* MAKE PROPER USE OF THIS
            //players[0].IsMovingNow = true;
        }

        //obtaining players names and returning objects
        private Player playerNames(int playerNumber)
        {
            playerName = Console.ReadLine();
            try
            {
                playerName = char.ToUpper(playerName[0]) + playerName.Substring(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Player temp = new Player(playerName, playerNumber);
            return temp;
        }

        
        //checking if any player has won
        private bool checkIfWinner(Player currentPlayer)
        {
            //checking each player for number of pawns
            foreach (var player in players)
            {
                //if player lost all pawns then the oponent won
                 if (player.NumberOfPons == 0)
                 {
                        player.HasWon = false;
                        winner = true;
                    //if currently moving player is not the one who lost all pawns then it is a winner
                    if (!currentPlayer.Equals(player) && currentPlayer.NumberOfPons > 0)
                     {
                        currentPlayer.HasWon = true;
                         nameOfTheWinner = currentPlayer.Name;
                     }
                     return true;
                 }
            }
            return false;
        }

        private void playerMove(Player currentPlayer)
        {
            //first check if current player can capture
            //canCapture();

            //temporary variables for current method
            String pawnStartCoordinates= "";
            String pawnDestinationCoordinates = "";
            String currentPlayerColour = currentPlayer.PawnsColour;


            Console.WriteLine("\n");

            //first checking who's moving 
            if (movingPlayer%2 != 0)
            {
                Console.Write("\nPlayer 1: " + players[0].Name + " is moving.");
            }
            else
            {
                Console.Write("\nPlayer 2: " + players[1].Name + " is moving.");
            }

                //checking if it's computer
                if (currentPlayer.GetType() == typeof(AI_Player))
                {
                    Console.WriteLine("\nComputer is moving");
                    //making a new reference variable to access AI Player methods
                    AI_Player aiPlayer = (AI_Player) currentPlayer;
                    //checking if computer can make any move at all
                    if (aiPlayer.canCapture())
                    {
                        
                    }
                    else
                    {
                        aiPlayer.checkIfMoveIsPossible();
                        aiPlayer.makeRandomMove();
                    }
                }
                //if it's human asking what moves to make
                else
                {
                    //asking for valid coordinates until they're valid
                    do
                    {
                        Console.Write("\nPlease enter coordinates of the pawn you want to move: >>>\t");
                        currentPlayer.isValidStartPosition(currentPlayerColour);
                    } while (!currentPlayer.ValidStart);

                    do
                    {
                        Console.Write("\nPlease enter destination coordinates >>>\t");
                        currentPlayer.isValidDestination();
                    } while (!currentPlayer.ValidDedstination);
                }
                //if both coordinates are valid, player can move
                currentPlayer.isValidMove();
            movingPlayer++;
        }

    }

    
}
