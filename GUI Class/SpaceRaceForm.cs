﻿using System;
//  Uncomment  this using statement after you have remove the large Block Comment below 
using System.Drawing;
using System.Windows.Forms;
using Game_Logic_Class;
//  Uncomment  this using statement when you declare any object from Object Classes, eg Board,Square etc.
using Object_Classes;

namespace GUI_Class
{
    public partial class SpaceRaceForm : Form
    {
        // The numbers of rows and columns on the screen.
        const int NUM_OF_ROWS = 7;
        const int NUM_OF_COLUMNS = 8;
        public static int counter = 0; // For single steps

        // When we update what's on the screen, we show the movement of a player 
        // by removing them from their old square and adding them to their new square.
        // This enum makes it clear that we need to do both.
        enum TypeOfGuiUpdate { AddPlayer, RemovePlayer };


        public SpaceRaceForm()
        {
            InitializeComponent();

            Board.SetUpBoard();
            ResizeGUIGameBoard();
            SetUpGUIGameBoard();
            SetupPlayersDataGridView();
            DetermineNumberOfPlayers();
            SpaceRaceGame.SetUpPlayers();
            PrepareToPlayGame();
        }


        /// <summary>
        /// Handle the Exit button being clicked.
        /// Pre:  the Exit button is clicked.
        /// Post: the game is terminated immediately
        /// </summary>
        private void exitButton_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        //  ******************* Uncomment - Remove Block Comment below
        //                         once you've added the TableLayoutPanel to your form.
        //
        //       You will have to replace "tableLayoutPanel" by whatever (Name) you used.
        //
        //        Likewise with "playerDataGridView" by your DataGridView (Name)
        //  ******************************************************************************************


        /// <summary>
        /// Resizes the entire form, so that the individual squares have their correct size, 
        /// as specified by SquareControl.SQUARE_SIZE.  
        /// This method allows us to set the entire form's size to approximately correct value 
        /// when using Visual Studio's Designer, rather than having to get its size correct to the last pixel.
        /// Pre:  none.
        /// Post: the board has the correct size.
        /// </summary>
        private void ResizeGUIGameBoard()
        {
            const int SQUARE_SIZE = SquareControl.SQUARE_SIZE;
            int currentHeight = tableLayoutPanel.Size.Height;
            int currentWidth = tableLayoutPanel.Size.Width;
            int desiredHeight = SQUARE_SIZE * NUM_OF_ROWS;
            int desiredWidth = SQUARE_SIZE * NUM_OF_COLUMNS;
            int increaseInHeight = desiredHeight - currentHeight;
            int increaseInWidth = desiredWidth - currentWidth;
            this.Size += new Size(increaseInWidth, increaseInHeight);
            tableLayoutPanel.Size = new Size(desiredWidth, desiredHeight);

        }// ResizeGUIGameBoard


        /// <summary>
        /// Creates a SquareControl for each square and adds it to the appropriate square of the tableLayoutPanel.
        /// Pre:  none.
        /// Post: the tableLayoutPanel contains all the SquareControl objects for displaying the board.
        /// </summary>
        private void SetUpGUIGameBoard()
        {
            for (int squareNum = Board.START_SQUARE_NUMBER; squareNum <= Board.FINISH_SQUARE_NUMBER; squareNum++)
            {
                Square square = Board.Squares[squareNum];
                SquareControl squareControl = new SquareControl(square, SpaceRaceGame.Players);
                AddControlToTableLayoutPanel(squareControl, squareNum);
            }//endfor

        }// end SetupGameBoard

        private void AddControlToTableLayoutPanel(Control control, int squareNum)
        {
            int screenRow = 0;
            int screenCol = 0;
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            tableLayoutPanel.Controls.Add(control, screenCol, screenRow);
        }// end Add Control


        /// <summary>
        /// For a given square number, tells you the corresponding row and column number
        /// on the TableLayoutPanel.
        /// Pre:  none.
        /// Post: returns the row and column numbers, via "out" parameters.
        /// </summary>
        /// <param name="squareNumber">The input square number.</param>
        /// <param name="rowNumber">The output row number.</param>
        /// <param name="columnNumber">The output column number.</param>
        private static void MapSquareNumToScreenRowAndColumn(int squareNum, out int screenRow, out int screenCol)
        {
            int[] colArray = new int[8] { 7, 6, 5, 4, 3, 2, 1, 0 };
            int[] rowArray = new int[7] { 6, 5, 4, 3, 2, 1, 0 };
            // Code needs to be added here to do the mapping
            if (squareNum / 8 == 1 || squareNum / 8 == 3 || squareNum / 8 == 5)
            {
                screenRow = rowArray[squareNum / 8];
                screenCol = colArray[squareNum % 8]; // show which columns 
            }
            else
            {
                screenRow = rowArray[squareNum / 8];
                screenCol = squareNum % 8;
            }

        }//end MapSquareNumToScreenRowAndColumn


        private void SetupPlayersDataGridView()
        {
            // Stop the playersDataGridView from using all Player columns.
            playersDataGridView.AutoGenerateColumns = false;
            // Tell the playersDataGridView what its real source of data is.
            playersDataGridView.DataSource = SpaceRaceGame.Players;

        }// end SetUpPlayersDataGridView



        /// <summary>
        /// Obtains the current "selected item" from the ComboBox
        ///  and
        ///  sets the NumberOfPlayers in the SpaceRaceGame class.
        ///  Pre: none
        ///  Post: NumberOfPlayers in SpaceRaceGame class has been updated
        /// </summary>
        private void DetermineNumberOfPlayers()
        {
            // Store the SelectedItem property of the ComboBox in a string
            string input = numOfPlayersComboBox.GetItemText(numOfPlayersComboBox.SelectedItem);
            // Parse string to a number
            int input_int = int.Parse(input);
            // Set the NumberOfPlayers in the SpaceRaceGame class to that number
            SpaceRaceGame.NumberOfPlayers = input_int;
        }//end DetermineNumberOfPlayers

        /// <summary>
        /// The players' tokens are placed on the Start square
        /// </summary>
        private void PrepareToPlayGame()
        {
            // More code will be needed here to deal with restarting 
            // a game after the Reset button has been clicked. 
            //
            // Leave this method with the single statement 
            // until you can play a game through to the finish square
            // and you want to implement the Reset button event handler.
            //

            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);

        }//end PrepareToPlay()


        /// <summary>
        /// Tells you which SquareControl object is associated with a given square number.
        /// Pre:  a valid squareNumber is specified; and
        ///       the tableLayoutPanel is properly constructed.
        /// Post: the SquareControl object associated with the square number is returned.
        /// </summary>
        /// <param name="squareNumber">The square number.</param>
        /// <returns>Returns the SquareControl object associated with the square number.</returns>
        private SquareControl SquareControlAt(int squareNum)
        {
            int screenRow;
            int screenCol;

            // Uncomment the following lines once you've added the tableLayoutPanel to your form. 
            //     and delete the "return null;" 
            //
            MapSquareNumToScreenRowAndColumn(squareNum, out screenRow, out screenCol);
            return (SquareControl)tableLayoutPanel.GetControlFromPosition(screenCol, screenRow);

            // return null; //added so code compiles
        }


        /// <summary>
        /// Tells you the current square number of a given player.
        /// Pre:  a valid playerNumber is specified.
        /// Post: the square number of the player is returned.
        /// </summary>
        /// <param name="playerNumber">The player number.</param>
        /// <returns>Returns the square number of the player.</returns>
        private int GetSquareNumberOfPlayer(int playerNumber)
        {
            // Code needs to be added here.

            //     delete the "return -1;" once body of method has been written 
            return SpaceRaceGame.Players[playerNumber].Position;
        }//end GetSquareNumberOfPlayer


        /// <summary>
        /// When the SquareControl objects are updated (when players move to a new square),
        /// the board's TableLayoutPanel is not updated immediately.  
        /// Each time that players move, this method must be called so that the board's TableLayoutPanel 
        /// is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the board's TableLayoutPanel shows the latest information 
        ///       from the collection of SquareControl objects in the TableLayoutPanel.
        /// </summary>
        private void RefreshBoardTablePanelLayout()
        {
            // Uncomment the following line once you've added the tableLayoutPanel to your form.
            tableLayoutPanel.Invalidate(true);
        }

        /// <summary>
        /// When the Player objects are updated (location, etc),
        /// the players DataGridView is not updated immediately.  
        /// Each time that those player objects are updated, this method must be called 
        /// so that the players DataGridView is told to refresh what it is displaying.
        /// Pre:  none.
        /// Post: the players DataGridView shows the latest information 
        ///       from the collection of Player objects in the SpaceRaceGame.
        /// </summary>
        private void UpdatesPlayersDataGridView()
        {
            SpaceRaceGame.Players.ResetBindings();
        }

        /// <summary>
        /// At several places in the program's code, it is necessary to update the GUI board,
        /// so that player's tokens are removed from their old squares
        /// or added to their new squares. E.g. at the end of a round of play or 
        /// when the Reset button has been clicked.
        /// 
        /// Moving all players from their old to their new squares requires this method to be called twice: 
        /// once with the parameter typeOfGuiUpdate set to RemovePlayer, and once with it set to AddPlayer.
        /// In between those two calls, the players locations must be changed. 
        /// Otherwise, you won't see any change on the screen.
        /// 
        /// Pre:  the Players objects in the SpaceRaceGame have each players' current locations
        /// Post: the GUI board is updated to match 
        /// </summary>
        private void UpdatePlayersGuiLocations(TypeOfGuiUpdate typeOfGuiUpdate)
        {
            // Code needs to be added here which does the following:
            //
            //   for each player
            //       determine the square number of the player
            //       retrieve the SquareControl object with that square number
            //       using the typeOfGuiUpdate, update the appropriate element of 
            //          the ContainsPlayers array of the SquareControl object.
            //           
            for (int i = 0; i < SpaceRaceGame.NumberOfPlayers; i++)
            {
                int position = GetSquareNumberOfPlayer(i);
                SquareControl squareControl = SquareControlAt(position);
                if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
                {
                    //Square square = new Square(position.ToString(), position);
                    //SquareControlAt(position);
                    squareControl.ContainsPlayers[i] = true;
                }
                else
                {
                    squareControl.ContainsPlayers[i] = false;
                }
            }
            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void UpdatePlayersGuiLocationsSingleSteps(TypeOfGuiUpdate typeOfGuiUpdate, int counter)
        {
            int position = GetSquareNumberOfPlayer(counter);
            SquareControl squareControl = SquareControlAt(position);
            if (typeOfGuiUpdate == TypeOfGuiUpdate.AddPlayer)
            {
                //Square square = new Square(position.ToString(), position);
                //SquareControlAt(position);
                squareControl.ContainsPlayers[counter] = true;
            }
            else
            {
                squareControl.ContainsPlayers[counter] = false;
            }
            RefreshBoardTablePanelLayout();//must be the last line in this method. Do not put inside above loop.
        } //end UpdatePlayersGuiLocations

        private void rolldiceButton_Click(object sender, EventArgs e)
        {
            // c)
            if (noRadioButton.Checked == true)
            {
                rolldiceButton.Enabled = false;
                playersDataGridView.Enabled = false;
                exitButton.Enabled = false;
                numOfPlayersComboBox.Enabled = false;
                UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
                SpaceRaceGame.PlayOneRound();
                UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
                UpdatesPlayersDataGridView();

                // After round
                numOfPlayersComboBox.Enabled = false;
                gameresetButton.Enabled = true; //Enable resetButton end of every round including game end
                exitButton.Enabled = true; // Enable Exit button after the game is end
                string winner = "";
                if (SpaceRaceGame.GameFinish() == false)
                {
                    rolldiceButton.Enabled = true; //enable rollDice button after a round.
                }
                else
                {
                    foreach (Player player in SpaceRaceGame.Players)
                    {
                        if (player.AtFinish)
                        {
                            winner += player.Name + ", ";
                        }
                    }
                    MessageBox.Show("The following player(s) finished the game\n\t" + winner, "", MessageBoxButtons.OK);
                }
            }
            else if (yesRadioButton.Checked == true)
            {
                rolldiceButton.Enabled = false;
                playersDataGridView.Enabled = false;
                exitButton.Enabled = false;
                numOfPlayersComboBox.Enabled = false;
                UpdatePlayersGuiLocationsSingleSteps(TypeOfGuiUpdate.RemovePlayer, counter);
                SpaceRaceGame.PlayOneRoundSingleSteps(counter);
                UpdatePlayersGuiLocationsSingleSteps(TypeOfGuiUpdate.AddPlayer, counter);
                UpdatesPlayersDataGridView();
                counter++;
                if (counter == SpaceRaceGame.NumberOfPlayers) counter = 0;

                // after round
                numOfPlayersComboBox.Enabled = false;
                gameresetButton.Enabled = true; //Enable resetButton end of every round including game end
                exitButton.Enabled = true; // Enable Exit button after the game is end

                if (SpaceRaceGame.GameFinish() == false) rolldiceButton.Enabled = true; //enable rollDice button after a round.
                else
                {
                    foreach (Player player in SpaceRaceGame.Players)
                    {
                        if (player.AtFinish) MessageBox.Show("The following player(s) finished the game\n\t" + player.Name, "", MessageBoxButtons.OK);
                    }

                }
            }
        }

        private void gameresetButton_Click(object sender, EventArgs e)
        {
            gameresetButton.Enabled = false;
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            counter = 0;
            foreach (Player players in SpaceRaceGame.Players)
            {

                players.Position = 0;
                players.Location = Board.StartSquare;
                players.RocketFuel = 60;
                players.HasPower = true;
                players.AtFinish = false;
            }
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
            UpdatesPlayersDataGridView();
            playersDataGridView.Enabled = true;
            numOfPlayersComboBox.Enabled = true;
            rolldiceButton.Enabled = true;
            groupBox1.Enabled = true;
        }

        private void numOfPlayersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.RemovePlayer);
            //SpaceRaceGame.NumberOfPlayers = int.Parse(numOfPlayersComboBox.SelectedItem.ToString());
            DetermineNumberOfPlayers();
            UpdatePlayersGuiLocations(TypeOfGuiUpdate.AddPlayer);
        }

        private void yesRadioButton_Click(object sender, EventArgs e)
        {
            rolldiceButton.Enabled = true;

            groupBox1.Enabled = false;
        }

        private void noRadioButton_Click(object sender, EventArgs e)
        {
            rolldiceButton.Enabled = true;

            groupBox1.Enabled = false;
        }
    }// end class
}
