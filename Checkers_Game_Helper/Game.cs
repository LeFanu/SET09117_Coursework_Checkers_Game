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
    *   Checking the state of the field method should be moved to the Board class
    *   
    ** Design Patterns Used:
    *   This class is a Singleton. The reason for this is that we want to have only one active game at the time.
    *   Another reason is that all related classess need to access some of the game data and it has to be the same for all
    *   
    *   This class also uses Memento pattern to store the game history and utilize Undo/Redo feature. 
    *   GameHistory class is the caretaker in memento pattern which stores the history of all moves and allows access to them
    *   Move is the originator that creates the mementos and stores them in the History
    *
    ** Last Update: 04/11/2017
    */

    //Singleton Class
    public class Game
    {

//-------------- Instance Fields ------------------------------------------------------------------------
        //variables for the status of the game
        private GameType gameType;
        private static Game currentGame_Instance;
        private Board gameBoard;
        //memento pattern variables
        private GameHistory_Caretaker gameHistory;
        private Move_Originator move;
        private int currentMoveNumber = -1;
        private int savedFiles = -1;
        private Boolean undoEnabled;
        private Boolean redoEnabled;

        //details of the players
        private Dictionary<String, Player> players = new Dictionary<String, Player>();
        private Boolean winner;
        private String nameOfTheWinner;
        private String turnColour;
        private int turnNum;


        //lists of available moves for each turnColour
        private List<int[]> possibleCaptureMoves = new List<int[]>();
        private List<int[]> legalMoves = new List<int[]>();

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
        public List<int[]> getLegalMoves => legalMoves;
        public List<int[]> getPossibleCaptureMoves => possibleCaptureMoves;
        public GameHistory_Caretaker GameHistory => gameHistory;


//-------------- Class Constructor ------------------------------------------------------------------------
        private Game()
        {
            gameHistory = GameHistory_Caretaker.GameHistory;
            move = new Move_Originator();
            undoEnabled = false;
            redoEnabled = false;
        }


//|||||||||||||||||||||||||||||||||| CLASS METHODS |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        //reset players and their state to default
        //used at the begining of the game
        public void setGame(string gameType)
        {
            gameBoard = Board.CurrentBoardInstance;
            this.gameType = (GameType)Enum.Parse(typeof(GameType), gameType);
            chooseGameType();

            //on the start of the game this will change to White
            turnColour = "Red";
            turnNum = 1;
            winner = false;

            //saving the state of the game is for replay option
            saveMoveToGameHistory();


            //ask if user wants to use Undo feature
            Console.WriteLine("_____________________________________________________________________________________________");
            Console.WriteLine("Do you want to turn on Undo feature that would allow you to roll back the whole turn?");
            Console.Write("1 for yes, any other key for playing normal. >>> \t");
            int choice = -1;
            try
            {
                choice = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
            }
            if (choice == 1)
            {
                undoEnabled = true;
                Console.WriteLine("Undo enabled!");
                Console.WriteLine("____________________________________________________________________________________________");
            }
            System.Threading.Thread.Sleep(1000);
        }
        
        //choosing the type of the game to play based on selection from menu
        private void chooseGameType()
        {
            //choosing game type depending on the game chosen in menu
            switch (gameType)
            {
                case GameType.Player_VS_CPU:
                {
                    Player p1 = setPlayerDetails();
                    String colour = p1.PawnsColour.ToString();
                    players[colour] = p1;
                    if (colour == "White")
                    {
                        players["Red"] = (new AI_Player("Red"));
                    }
                    else
                    {
                        players["White"] = (new AI_Player("White"));
                    }
                    
                    break;
                }
                case GameType.Player_VS_Player:
                {
                    Console.Write("Player 1 please enter your name: >>> \t");
                    //players.Add(setPlayerDetails());
                    Console.Write("Player 2 please enter your name: >>> \t");
                    //players.Add(setPlayerDetails());

                    break;
                }
                case GameType.CPU_VS_CPU:
                {

                    players["White"] = new AI_Player("White");
                    players["Red"] = new AI_Player("Red");
                    break;
                }
            }
        }

        //getting details for creating new player
        private Player setPlayerDetails()
        {
            Console.Write("Please enter your name: >>> \t");
            String playerName = Console.ReadLine();

            Console.WriteLine("Please choose your colour. 1 for White, 2 for Red");
            int colour = -1;
            try
            {
               colour = Int32.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Wrong number. Your colour is white");
            }

            String playerColour;
            if (colour == 1)
            {
                playerColour = "White";
            }
            else
            {
                playerColour = "Red";
            }
            try
            {
                playerName = char.ToUpper(playerName[0]) + playerName.Substring(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new Player(playerName, playerColour);
        }


        //getting all player's pieces into one list for every turnColour
        //this will be used to check the valid moves as we only do that for currently moving player
        public void setPlayerPositions(Player currentlyMovingPlayer)
        {
            //first clear the current list
            currentlyMovingPlayer.PiecesOfThePlayer.Clear();
            foreach (var position in gameBoard.GridXY)
            {
                if (position.State  == currentlyMovingPlayer.PawnsColour || (position.IsKing && currentlyMovingPlayer.PawnsColour == position.State))
                {
                    currentlyMovingPlayer.PiecesOfThePlayer.Add(position);
                }
            }
            //count the total of the pawns
            currentlyMovingPlayer.NumberOfPawns = currentlyMovingPlayer.PiecesOfThePlayer.Count;
        }

        //checking if any player has won
        private bool checkIfWinner(Player currentlyMovingPlayer)
        {
            //checking each player for number of pawns
            if (currentlyMovingPlayer.NumberOfPawns <= 0)
            {
                currentlyMovingPlayer.HasWon = false;
                winner = true;
                players[currentlyMovingPlayer.GetOpponent.ToString()].HasWon = true;
                nameOfTheWinner = players[currentlyMovingPlayer.GetOpponent.ToString()].Name;
                return true;
            }
            return false;
        }

        //main method for playing the game 
        public void PlayGame()
        {
            Console.WriteLine("\n\n");
            //making move until one of the players wins
            do
            {
                if (turnColour.Equals("Red"))
                {
                    turnColour = "White";
                }
                else
                {
                    turnColour = "Red";
                }

                //better reference variable for cleaner code
                Player currentlyMovingPlayer = players[turnColour];
                
                //get all pieces of current player and check them all for legal moves
                setPlayerPositions(currentlyMovingPlayer);

                //Need to check if player won at this point
                //If the last turn resulted in 0 pawns for any of the players than the oponent won
                if (checkIfWinner(currentlyMovingPlayer))
                {
                    System.Threading.Thread.Sleep(1000);

                    Console.WriteLine("\n\n");
                    Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||");
                    Console.WriteLine("||||||||||||||| CONGRATULATIONS " + nameOfTheWinner + " HAS WON!!!!!!!!! ||||||||||||||||||||||");
                    Console.WriteLine("|||||||||||||||                                                          ||||||||||||||||||||||");
                    Console.WriteLine("|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n");
                    System.Threading.Thread.Sleep(1000);

                    break;
                }

                //checking if there are any legal moves
                findLegalMoves(currentlyMovingPlayer);
                    
                //drawing the board
                gameBoard.drawNames(players["White"], players["Red"], turnColour, turnNum);
                gameBoard.drawBoard();
                    
                //ask player to move
                playerMove(currentlyMovingPlayer);
                    
                //saving current move to originator and adding to the list of mementos in Caretaker
                saveMoveToGameHistory();

                //chosing undo is available only to human players
                if (undoEnabled && currentlyMovingPlayer.GetType() == typeof(Player))
                {
                    bool moveCompleted;
                    do
                    {
                        moveCompleted = undoRedoTurn();
                    } while (!moveCompleted);
                }

                turnNum++;
                System.Threading.Thread.Sleep(1000);

            } while (!winner);
        }

        //procedure for a move of a player
        private void playerMove(Player currentlyMovingPlayer)
        {
            //first check if current player can capture
            currentlyMovingPlayer.canCapture();


            //checking if it's computer
            if (currentlyMovingPlayer.GetType() == typeof(AI_Player))
            {
                Console.WriteLine("\nComputer is moving");
                //making a new reference variable to access AI Player methods
                AI_Player aiPlayer = (AI_Player) currentlyMovingPlayer;
               
                //making a move for a computer means selecting a random available move
                //if this is a capture move the list of legal moves will contain only those
                aiPlayer.makeRandomMove();
            }
            //if it's human asking what moves to make
            else
            {
                if (currentlyMovingPlayer.CaptureMovePossible)
                {
                    //if human player can capture we want to ask which capture move to choose
                      currentlyMovingPlayer.showAndSelectCaptureMoves();
                }
                else
                {
                    //if no capture moves we ask for coordinates
                    bool validMove;
                        //if both coordinates are valid, player can move
                        do
                        {
                            validMove = currentlyMovingPlayer.makeValidMove();
                        } while (!validMove);
                }
            }
            possibleCaptureMoves.Clear();
            legalMoves.Clear();
        }

        //checking all current player's pawns for any possible moves
        private void findLegalMoves(Player currentlyMovingPlayer)
        {
            int x;
            int y;
            //coordinates for valid move
            int[] moveCoordinates = new int[6];

            
            //checking each piece of currently moving player
            foreach (PiecePosition piece in currentlyMovingPlayer.PiecesOfThePlayer)
            {
                //states of fields needed for deciding if this is a valid move
                PieceState startField;
                PieceState destinationField;
                PieceState landingField;
                x = piece.XCoordinates;
                y = piece.YCoordinates;
                startField = checkFieldState(x, y);
//Console.WriteLine("\nChecking coordinates " + (char)(x + 65)+(y + 1) + ", colour is " + startField);
                
                //check if starting field is players field
                if (startField == currentlyMovingPlayer.PawnsColour || piece.IsKing)
                {
                    //If current piece is a King it should check all the fields around it
                    //otherwise just one side depending on the player's orientation
                    //checking the orientation of the player
                    //if goes down
                    if (currentlyMovingPlayer.PawnsColour == PieceState.White || piece.IsKing)
                    {
                        //checking LEFT DIAGONAL
                        destinationField = checkFieldState(x - 1, y + 1);
//Console.WriteLine("Left Diagonal down: Destination " + (char)(x - 1 + 65) + (y + 2) + ", field state: " + destinationField);
                        //if it's empty it's a valid move
                        if (destinationField == PieceState.Valid)
                        {
                            moveCoordinates = new [] { x, y, x - 1 , y + 1, 0, 0};
                            legalMoves.Add(moveCoordinates);
                        }
                        //if it's opponent's colour we check if capturing is possible
                        else if (destinationField == currentlyMovingPlayer.GetOpponent)
                        {
                            landingField = checkFieldState( x - 2, y + 2);
                            //if landing field is free this is a valid move and valid capture move
                            if (landingField == PieceState.Valid)
                            {
//Console.WriteLine("Oponent is aside. Destination " + (char)(x - 2 + 65) + (y + 3) + ", field state: " + destinationField);
                                //last 2 coordinates added are fot
                                moveCoordinates = new [] { x, y, x - 2, y + 2, x - 1, y + 1};
                                possibleCaptureMoves.Add(moveCoordinates);
//Console.WriteLine("Added to capture moves");
                            }
                        }


                        //then we check RIGHT DIAGONAL
                        destinationField = checkFieldState(x + 1, y + 1);
//Console.WriteLine("Right diagonal down: Destination " + (char)(x + 1 + 65) + (y + 2) + ", field state: " + destinationField);
                        if (destinationField == PieceState.Valid)
                        {
                            moveCoordinates = new[] { x, y, x + 1, y + 1, 0, 0 };
                            legalMoves.Add(moveCoordinates);
                        }
                        //if it's opponent field we check if there is a capture move
                        else if (destinationField == currentlyMovingPlayer.GetOpponent)
                        {
                            landingField = checkFieldState(x + 2, y + 2);
                            //if field is empty then it's a valid move and valid capture move
                            if (landingField == PieceState.Valid)
                            {
//Console.WriteLine("Oponent is aside. Destination " + (char)(x + 2 + 65) + (y + 3) + ", field state: " + destinationField);
                                moveCoordinates = new[] { x, y, x + 2, y + 2, x + 1, y + 1 };
                                possibleCaptureMoves.Add(moveCoordinates);
//Console.WriteLine("Added to capture moves");
                            }
                        }
                    }
                    //if goes up
                    if (currentlyMovingPlayer.PawnsColour == PieceState.Red || piece.IsKing)
                    {
                        //LEFT DIAGONAL
                        destinationField = checkFieldState(x - 1, y - 1);
//Console.WriteLine("Left Diagonal up: Destination " + (char)(x - 1 + 65) + (y) + ", field state: " + destinationField);
                        if (destinationField == PieceState.Valid)
                        {
                            moveCoordinates = new[] { x, y , x - 1, y - 1, 0, 0 };
                            legalMoves.Add(moveCoordinates);
                        }
                        else if (destinationField == currentlyMovingPlayer.GetOpponent)
                        {
                            landingField = checkFieldState(x - 2, y - 2);
//Console.WriteLine("Oponent is aside. Destination " + (char)(x - 2 + 65) + (y - 1) + ", field state: " + destinationField);
                            if (landingField == PieceState.Valid)
                            {
                                moveCoordinates = new[] { x, y, x - 2, y - 2, x - 1, y - 1 };
                                possibleCaptureMoves.Add(moveCoordinates);
//Console.WriteLine("Added to capture moves");
                            }
                        }
                        //RIGHT DIAGONAL
                        destinationField = checkFieldState(x + 1, y - 1);
//Console.WriteLine("Right Diagonal up: Destination " + (char)(x + 1 + 65) + (y) + ", field state: " + destinationField);
                        if (destinationField == PieceState.Valid)
                        {
                            moveCoordinates = new[] { x, y, x + 1, y - 1, 0, 0};
                            legalMoves.Add(moveCoordinates);
                        }
                        else if (destinationField == currentlyMovingPlayer.GetOpponent)
                        {
                            landingField = checkFieldState(x + 2, y - 2);
//Console.WriteLine("Oponent is aside. Destination " + (char)(x + 2 + 65) + (y - 1) + ", field state: " + destinationField);
                            if (landingField == PieceState.Valid)
                            {
                                moveCoordinates = new[] { x, y, x + 2, y - 2 , x + 1, y - 1};
                                possibleCaptureMoves.Add(moveCoordinates);
//Console.WriteLine("Added to capture moves");
                            }
                        }
                    }
//Console.ReadKey();
                }
            }
        }

        //checking the state of the requested field on the board
        public PieceState checkFieldState(int x, int y)
        {
            //arrays are numbered from 0, therefore we need one less of each coordinate
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                return gameBoard.GridXY[x,y].State;
            }
            //if the coordinates given don't lie within the board we return invalid as that's actually correct
            return PieceState.Invalid;
        }


        private void saveMoveToGameHistory()
        {
            move.saveCurrentMove(gameBoard.GridXY);
            GameHistory.addPiecesPositionsToHistory(move.createBoardStateMemento());
            savedFiles++;
            currentMoveNumber++;
        }

        private Boolean undoRedoTurn()
        {
            //allowing to undo only if there are any moves previously made
            
                
                int endOfMoveChoice = -1;
                do
                {
                    //asking player to complete the move or to undo
                    Console.WriteLine("Do you want to complete the move(enter 1), undo (enter 2) or redo (enter 3) ?");
                    try
                    {
                        string choice = Console.ReadLine();
                        endOfMoveChoice = Int32.Parse(choice);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please enter 1 or 2");

                    }
                } while (endOfMoveChoice < 1 || endOfMoveChoice > 3);

                //if player chooses to undo the move then we get the previous version of the board and ask player to move again
                if (endOfMoveChoice == 1)
                {
                    //if player choses to complete move we disable redo
                    redoEnabled = false;
                    //this method removes any newer moves existing in the history as they aren't needed anymore
                    gameHistory.deleteNewerHistoryAfterUndoRedo(currentMoveNumber + 1);
                    return true;
                }
                if (endOfMoveChoice == 2)
                {
                    undoMove();
                }
                if (endOfMoveChoice == 3)
                {
                    if (redoEnabled)
                    {
                        redoMove();
                    }
                    else
                    {
                        Console.WriteLine("There are no more moves to redo.");
                    }
                }
            return false;
        }

        private Boolean undoMove()
        {
            //undo is possible only if there is at least one move in the history
            if (currentMoveNumber >= 1)
            {
                //TEST
                Console.WriteLine("You have undone last move.");
                //Console.WriteLine("Board after your last move");
                //gameBoard.drawBoard();
               // Console.ReadKey();

                //turn goes down as the move is undone
                turnNum--;
                //as undo was chosen the current move is a previous one now
                currentMoveNumber--;

                //restoring the state of the board at given point
                gameBoard.GridXY =
                    move.restoreMoveFromHistory(GameHistory.getPiecePositionsHistory(currentMoveNumber));

                //TEST
                //Drawing test board
                //Console.WriteLine("Drawing test board after undo");
                //Console.WriteLine(
                //    "_________________________________________________________________________________________\n");

                //redo becomes enabled as the move was undone
                redoEnabled = true;

                //TEST
                //gameBoard.drawBoard();
                //Console.ReadKey();
            }
            else
            {
                Console.WriteLine("There are no more moves to undo");
                return false;
            }
            return true;
        }

        private Boolean redoMove()
        {
            //redo can be chosen only if the selected move is one less than saved ones
            //this means there is at least one new version
            if (savedFiles - 1 > currentMoveNumber)
            {
                //TEST
                Console.WriteLine("You have redone last move.");
                //Console.WriteLine("Board after your last move");
                //gameBoard.drawBoard();
                //Console.ReadKey();
                
                
                //Increment the current move as we redo the previously undone move
                currentMoveNumber++;
                turnNum++;
                //restoring the state of the board with the next move in the history
                gameBoard.GridXY = move.restoreMoveFromHistory(gameHistory.getPiecePositionsHistory(currentMoveNumber));

                //TEST
                //Drawing test board
                //Console.WriteLine("Drawing test board after undo");
                //Console.WriteLine("_________________________________________________________________________________________\n");
                //gameBoard.drawBoard();
                //Console.ReadKey();
            }
            //if there are no more newer moves in the history it means redo is not possible
            else
            {
                Console.WriteLine("There are no more moves to redo.");
                redoEnabled = false;
                return false;
            }
            return true;
        }
    }

    
}
