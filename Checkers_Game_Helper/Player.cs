using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers_Game_Helper
{
    /** Author: Karol Pasierb - Software Engineering - 40270305
    * Created by Karol Pasierb on 2017/10/08
    *
    ** Description:
    *   This class is to provide a player for the game with all the features needed.
    *   This class holds the list of all player Pawns and array for legal move chosen during the turn
    *   
    *
    ** Last Update: 07/11/2017
    */


    public class Player
    {
//-------------- Instance Fields ------------------------------------------------------------------------
        protected String name;

        protected Boolean hasWon;

        protected int numberOfPawns;
        protected PieceState pawnsColour;
        protected PieceState opponent;

        //collections of pawns positions with the details about each field
        protected List<PiecePosition> piecesOfThePlayer = new List<PiecePosition>();
        protected List<int[]> legalMoves;

        protected Boolean captureMovePossible;

        //instance of the board for a current game
        protected Board currentBoard = Board.CurrentBoardInstance;
        protected Game currentGame = Game.CurrentGameInstance;

//_______________________________________________________________________________________________
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public PieceState PawnsColour
        {
            get { return pawnsColour; }
            set { pawnsColour = value; }
        }
        public int NumberOfPawns
        {
            get { return numberOfPawns; }
            set { numberOfPawns = value; }
        }
        public bool HasWon
        {
            get { return hasWon; }
            set { hasWon = value; }
        }
        public List<PiecePosition> PiecesOfThePlayer
        {
            get { return piecesOfThePlayer; }
            set { piecesOfThePlayer = value; }
        }
        public PieceState GetOpponent => opponent;
        public bool CaptureMovePossible => captureMovePossible;

//-------------- Class Constructor ------------------------------------------------------------------------
        public Player(String name, String playerColour)
        {
            this.name = name;
            if (playerColour.Equals("White"))
            {
                pawnsColour = PieceState.White;
            }
            else
            {
                pawnsColour = PieceState.Red;
            }

            numberOfPawns = 12;
            setOponent();
        }

//||||||||||||||||||||||||||||||||||||||| CLASS METHODS ||||||||||||||||||||||||||||||||||||||||||||

        //oponent colour is needed for checking capture moves
        protected void setOponent()
        {
            if (pawnsColour == PieceState.Red)
            {
                opponent = PieceState.White;
            }
            else
            {
                opponent = PieceState.Red;
            }
        }

        //checking if current player has any capture moves
        public void  canCapture()
        {
            if (currentGame.getPossibleCaptureMoves.Count > 0)
            {
                //if there are any capture moves player must do them, therefore they are only legal moves
                captureMovePossible = true;
                legalMoves = currentGame.getPossibleCaptureMoves;
            }
            else
            {
                captureMovePossible = false;
                legalMoves = currentGame.getLegalMoves;
            }
        }

        
        private int getColumn(char coordinates)
        {
            int x;
            try
            {
                x = coordinates % 8;
                if (x == 0)
                {
                    x = 8;
                }
            }
            catch (Exception)
            {
                x = -1;
            }
            return x - 1;
        }

        private int getRow(char coordinates)
        {
            int y;
            try
            {
                y = Int32.Parse(coordinates.ToString());
            }
            catch (Exception)
            {
                y = -1;
            }
            return y - 1;
        }

        //if player has any capture moves they must be performed
        //therefore player must choose one of existing ones
        public void showAndSelectCaptureMoves()
        {
            String startCoordinates;
            String destinationCoordinates;
            Console.WriteLine("You have folowing capture moves: ");
            int i = 1;
            foreach (int[] move in legalMoves)
            {
                startCoordinates = (char)(move[0] + 65) + (move[1] + 1).ToString();
                destinationCoordinates = (char)(move[2]+65) + (move[3] + 1).ToString();
                Console.WriteLine(i + ". From: " + startCoordinates + " To: " + destinationCoordinates);
                i++;
            }
            int selectedMove = -1;
            do
            {
                Console.Write("Please select one: >>>\t");
                try
                {
                    selectedMove = Int32.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.Write("Please enter a number for a chosen move: \t");

                }
            } while (selectedMove == -1);
            makeMove(legalMoves[selectedMove - 1]);
        }

        //checking if both start and destination fields are correctly chosend and if the move can be performed
        public Boolean makeValidMove()
        {
            int[] playerMove = new int[6];
            //asking for valid coordinates until they're valid

            Console.Write("\nPlease enter coordinates of the pawn you want to move: >>>\t");
            String startCoordinates = Console.ReadLine();
            if (startCoordinates.Length == 2)
            {
                try
                {
                    playerMove[0] = getColumn(char.ToUpper(startCoordinates[0]));
                    playerMove[1] = getRow(startCoordinates[1]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid start coordinates.");
                    return false;
                }
                if (!(playerMove[0] >= 0 && playerMove[0] < 8)
                    || !(playerMove[1] >= 0 && playerMove[1] < 8)
                    || currentBoard.GridXY[playerMove[0], playerMove[1]].State == PieceState.Valid
                    || currentBoard.GridXY[playerMove[0], playerMove[1]].State == PieceState.Invalid)
                {
                    Console.WriteLine("Invalid start coordinates.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid start coordinates.");
                return false;
            }

            Console.Write("\nPlease enter destination coordinates >>>\t");
            String destinationCoordinates = Console.ReadLine();
            if (destinationCoordinates.Length == 2)
            {
                try
                {
                    playerMove[2] = getColumn(char.ToUpper(destinationCoordinates[0]));
                    playerMove[3] = getRow(destinationCoordinates[1]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid destination coordinates.");
                    return false;
                }
                if (!(playerMove[2] >= 0 && playerMove[2] < 8)
                    || !(playerMove[3] >= 0 && playerMove[3] < 8)
                    || currentBoard.GridXY[playerMove[2], playerMove[3]].State != PieceState.Valid)
                {
                    Console.WriteLine("Invalid destination coordinates.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid destination coordinates.");
                return false;
            }
            //capture coordinates are set to 0 as if there is a capture move this would be displayed to player and it cannot be omitted
            playerMove[4] = 0;
            playerMove[5] = 0;
            return makeMove(playerMove);
        }
        
        //making actual move by comparing selected coordinates with the valid moves
        private Boolean makeMove(int[] playerMove)
        {
            foreach (int[] validMove in legalMoves)
            {
                if (playerMove.SequenceEqual(validMove))
                {
                    changeFieldsOnBoardAfterMove(validMove);
                    return true;
                }
            }
            Console.WriteLine("The move you tried to make was invalid." +
                              "\nPlease remember you can move only 1 field diagonal if it is empty or 2 fields if you plan to capture opponent's piece." +
                              "\nPlease enter both coordinates again.");
            return false;
        }

        //change the statuses of the fields involved in the move
        protected void changeFieldsOnBoardAfterMove(int[] selectedMove)
        {
            bool isKing = currentBoard.GridXY[selectedMove[0], selectedMove[1]].IsKing;

            currentBoard.GridXY[selectedMove[0], selectedMove[1]].State = PieceState.Valid;
            currentBoard.GridXY[selectedMove[0], selectedMove[1]].IsKing = false;
            //if it is a capture move, then we have to remove opponent's piece
            if (CaptureMovePossible)
            {
                currentBoard.GridXY[selectedMove[4], selectedMove[5]].State = PieceState.Valid;
                currentBoard.GridXY[selectedMove[4], selectedMove[5]].IsKing = false;
            }
            //check if destination field will make this piece a King
            if (pawnsColour == PieceState.White && selectedMove[3] == 7)
            {
                currentBoard.GridXY[selectedMove[2], selectedMove[3]].State = PieceState.White;
                currentBoard.GridXY[selectedMove[2], selectedMove[3]].IsKing = true;
            }
            else if (pawnsColour == PieceState.Red && selectedMove[3] == 0)
            {
                currentBoard.GridXY[selectedMove[2], selectedMove[3]].State = PieceState.Red;
                currentBoard.GridXY[selectedMove[2], selectedMove[3]].IsKing = true;
            }
            else
            {
                if (isKing)
                {
                    currentBoard.GridXY[selectedMove[2], selectedMove[3]].IsKing = true;

                }
                    currentBoard.GridXY[selectedMove[2], selectedMove[3]].State = PawnsColour;
            }
            
        }
    }
}