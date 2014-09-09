using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void disableAllButtons()
        {
            // this method disables all the buttons so the user can't click them
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
        }

        private void enableAllButtons()
        {
            // this method enables all the buttons so they can be clicked by the user
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
        }

        private void clearContents()
        {
            // this method clears the text in each cell
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";
            button5.Text = "";
            button6.Text = "";
            button7.Text = "";
            button8.Text = "";
            button9.Text = "";
        }

        private void changePlayers()
        {
            // this method changes the turnLabel text from one player to the other
            // if it was player one's turn change to player two
            if (turnLabel.Text == "Turn: Player One")
            {
                turnLabel.Text = "Turn: Player Two";
                // if in one player mode, and no one has won yet, the computer moves automatically during player two's turn
                if (this.Text == "TicTacToe: One Player" && button1.Enabled == true)
                {
                    ComputerMove(); // call the method for the computer to make its move
                }
            }
            else if (turnLabel.Text == "Turn: Player Two")
            {
                turnLabel.Text = "Turn: Player One";
            }
            else // umm... this shouldn't happen.
            {
                MessageBox.Show("Something has gone incredibly wrong. I'm going to shut down now", "ERROR 001");
                Close();
            }
        }

        public int CurrentPlayer()
        {
            // this method returns the integer identifier for the current player (1 or 2)
            int PlayerNumberInt = 0; // creating the return integer
            string PlayerNumberText = turnLabel.Text.Substring(turnLabel.Text.Length - 3); // grabbing the last three characters of the turnLabel, which should always be "One" or "Two"
            // assign 1 if it's player one's turn
            if (PlayerNumberText == "One")
            {
                PlayerNumberInt = 1;
            }
            // assign 2 if it's player two's turn
            else if (PlayerNumberText == "Two")
            {
                PlayerNumberInt = 2;
            }
            else // how did this happen? if we get here the label has been corrupted so let's close down
            {
                MessageBox.Show("Something has gone tragically wrong. I'm going to shut down now", "ERROR 002");
                Close();
            }
            return PlayerNumberInt; // this is how the value is passed back to the method that calls it
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            // after clicking the restart button the cells are cleared and enabled
            clearContents();
            enableAllButtons();
            // find out which player's turn it is currently
            int PlayerNumberInt = CurrentPlayer();
            // if it's player two's turn, and it's a one player game, let the computer move
               if (this.Text == "TicTacToe: One Player" && PlayerNumberInt == 2)
            {
                ComputerMove(); // call the method for the computer to make its move
            }
        }

        private void WinnerCheck()
        {
            // this method checks all possible ways to win and displays a message congratulating the winner
            string WinnerName = "";
            // first checking if player 1 just won
            if ((button1.Text == "X" && button2.Text == "X" && button3.Text == "X") | // top row
                (button4.Text == "X" && button5.Text == "X" && button6.Text == "X") | // middle row
                (button7.Text == "X" && button8.Text == "X" && button9.Text == "X") | // bottom row
                (button1.Text == "X" && button4.Text == "X" && button7.Text == "X") | // left column
                (button2.Text == "X" && button5.Text == "X" && button8.Text == "X") | // middle column
                (button3.Text == "X" && button6.Text == "X" && button9.Text == "X") | // right column
                (button1.Text == "X" && button5.Text == "X" && button9.Text == "X") | // up-left to down-right
                (button7.Text == "X" && button5.Text == "X" && button3.Text == "X"))  // down-left to up-right
            {
                WinnerName = "Player One";
            }
            // now check if player 2 just won
            if ((button1.Text == "O" && button2.Text == "O" && button3.Text == "O") |
                (button4.Text == "O" && button5.Text == "O" && button6.Text == "O") |
                (button7.Text == "O" && button8.Text == "O" && button9.Text == "O") |
                (button1.Text == "O" && button4.Text == "O" && button7.Text == "O") |
                (button2.Text == "O" && button5.Text == "O" && button8.Text == "O") |
                (button3.Text == "O" && button6.Text == "O" && button9.Text == "O") |
                (button1.Text == "O" && button5.Text == "O" && button9.Text == "O") |
                (button7.Text == "O" && button5.Text == "O" && button3.Text == "O"))
            {
                WinnerName = "Player Two";
            }

            // display a message if someone has won
            if (WinnerName != "")
            {
                MessageBox.Show(WinnerName + " Wins! Restart to play again.", "We have a winner!");
                disableAllButtons(); // don't let the user continue playing until reseting
            }
            // or, display a message if there are no remaining moves
            else if ((button1.Text != "" && button2.Text != "" && button3.Text != "") &&
                (button4.Text != "" && button5.Text != "" && button6.Text != "") &&
                (button7.Text != "" && button8.Text != "" && button9.Text != ""))
            {
                MessageBox.Show("Nobody Wins! Restart to play again.", "It's a tie!");
                disableAllButtons(); // don't let the user continue playing until reseting
            }
        }

        private void GenericButtonClick(Button thisButton)
        {
            // this method will run any time the user selects a button
            // first, check if the box has already been selected and if so, make them pick again
            if (thisButton.Text != "")
            {
                MessageBox.Show("That box has already been taken. Pick again.");
            }
            // fill in the box appropriately and change turns
            else
            {
                // find out which player's turn it is currently
                int PlayerNumberInt = CurrentPlayer();
                // place an X for player 1 and O for player 2
                if (PlayerNumberInt == 1)
                {
                    thisButton.Text = "X";
                }
                else if (PlayerNumberInt == 2)
                {
                    thisButton.Text = "O";
                }
                else // what? this shouldn't happen. shut down and try again
                {
                    MessageBox.Show("Something has gone spectacularly wrong. I'm going to shut down now", "ERROR 003");
                    Close();
                }
                // call method to check if someone has just won
                WinnerCheck();
                // call method to change to the other player's turn
                changePlayers();
            }
        }

        private void ComputerMove()
        {
            // this logic determines how the computer will select its move

            // because the "perfect" strategy will always result in a win or tie for the computer there
            //  is a percent chance the computer deviates from that strategy and picks a cell at random

            int percentImperfect = 25; // percent chance the computer does not use the perfect strategy
            Random random = new Random();
            int randomNumber = random.Next(0, 100); // pick a random number between 0 and 100

            // check if the random number is less than the percent chance of a non-perfect strategy
            if (randomNumber <= percentImperfect) // pick a random box
            {
                bool moveFlag = false; // initially setting the flag that a move has been selected to false
                while (moveFlag == false) // until a move has been selected...
                {
                    int random1to9 = random.Next(1, 9); // pick a random number between 1 and 9, corresponding to a box
                    if (random1to9 == 1 && button1.Text == "") // check if that box is available
                        {
                            GenericButtonClick(button1); // select the box
                            moveFlag = true; // change the move flag to true to exit the loop
                        }
                    else if (random1to9 == 2 && button2.Text == "")
                    {
                        GenericButtonClick(button2);
                        moveFlag = true;
                    }
                    else if (random1to9 == 3 && button3.Text == "")
                    {
                        GenericButtonClick(button3);
                        moveFlag = true;
                    }
                    else if (random1to9 == 4 && button4.Text == "")
                    {
                        GenericButtonClick(button4);
                        moveFlag = true;
                    }
                    else if (random1to9 == 5 && button5.Text == "")
                    {
                        GenericButtonClick(button5);
                        moveFlag = true;
                    }
                    else if (random1to9 == 6 && button6.Text == "")
                    {
                        GenericButtonClick(button6);
                        moveFlag = true;
                    }
                    else if (random1to9 == 7 && button7.Text == "")
                    {
                        GenericButtonClick(button7);
                        moveFlag = true;
                    }
                    else if (random1to9 == 8 && button8.Text == "")
                    {
                        GenericButtonClick(button8);
                        moveFlag = true;
                    }
                    else if (random1to9 == 9 && button9.Text == "")
                    {
                        GenericButtonClick(button9);
                        moveFlag = true;
                    }
                }
            }
            else // use the perfect strategy
            {
                // option 1: win if you have two in a row and an open third
                // 1.a - top row checks
                if (button1.Text == "" && button2.Text == "O" && button3.Text == "O")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "O" && button2.Text == "" && button3.Text == "O")
                {
                    GenericButtonClick(button2);
                }
                else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                // 1.b - middle row checks
                else if (button4.Text == "" && button5.Text == "O" && button6.Text == "O")
                {
                    GenericButtonClick(button4);
                }
                else if (button4.Text == "O" && button5.Text == "" && button6.Text == "O")
                {
                    GenericButtonClick(button5);
                }
                else if (button4.Text == "O" && button5.Text == "O" && button6.Text == "")
                {
                    GenericButtonClick(button6);
                }
                // 1.c - bottom row checks
                else if (button7.Text == "" && button8.Text == "O" && button9.Text == "O")
                {
                    GenericButtonClick(button7);
                }
                else if (button7.Text == "O" && button8.Text == "" && button9.Text == "O")
                {
                    GenericButtonClick(button8);
                }
                else if (button7.Text == "O" && button8.Text == "O" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                // 1.d - left column checks
                else if (button1.Text == "" && button4.Text == "O" && button7.Text == "O")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "O" && button4.Text == "" && button7.Text == "O")
                {
                    GenericButtonClick(button4);
                }
                else if (button1.Text == "O" && button4.Text == "O" && button7.Text == "")
                {
                    GenericButtonClick(button7);
                }
                // 1.e - middle column checks
                else if (button2.Text == "" && button5.Text == "O" && button8.Text == "O")
                {
                    GenericButtonClick(button2);
                }
                else if (button2.Text == "O" && button5.Text == "" && button8.Text == "O")
                {
                    GenericButtonClick(button5);
                }
                else if (button2.Text == "O" && button5.Text == "O" && button8.Text == "")
                {
                    GenericButtonClick(button8);
                }
                // 1.f - left column checks
                else if (button3.Text == "" && button6.Text == "O" && button9.Text == "O")
                {
                    GenericButtonClick(button3);
                }
                else if (button3.Text == "O" && button6.Text == "" && button9.Text == "O")
                {
                    GenericButtonClick(button6);
                }
                else if (button3.Text == "O" && button6.Text == "O" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                // 1.g - diagonal checks
                else if (button1.Text == "" && button5.Text == "O" && button9.Text == "O")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "O" && button5.Text == "" && button9.Text == "O")
                {
                    GenericButtonClick(button5);
                }
                else if (button1.Text == "O" && button5.Text == "O" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                else if (button7.Text == "" && button5.Text == "O" && button3.Text == "O")
                {
                    GenericButtonClick(button7);
                }
                else if (button7.Text == "O" && button5.Text == "" && button3.Text == "O")
                {
                    GenericButtonClick(button5);
                }
                else if (button7.Text == "O" && button5.Text == "O" && button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                // option 2: block opponent if they have two in a row and an open third
                // 2.a - top row checks
                else if (button1.Text == "" && button2.Text == "X" && button3.Text == "X")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "X" && button2.Text == "" && button3.Text == "X")
                {
                    GenericButtonClick(button2);
                }
                else if (button1.Text == "X" && button2.Text == "X" && button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                // 2.b - middle row checks
                else if (button4.Text == "" && button5.Text == "X" && button6.Text == "X")
                {
                    GenericButtonClick(button4);
                }
                else if (button4.Text == "X" && button5.Text == "" && button6.Text == "X")
                {
                    GenericButtonClick(button5);
                }
                else if (button4.Text == "X" && button5.Text == "X" && button6.Text == "")
                {
                    GenericButtonClick(button6);
                }
                // 2.c - bottom row checks
                else if (button7.Text == "" && button8.Text == "X" && button9.Text == "X")
                {
                    GenericButtonClick(button7);
                }
                else if (button7.Text == "X" && button8.Text == "" && button9.Text == "X")
                {
                    GenericButtonClick(button8);
                }
                else if (button7.Text == "X" && button8.Text == "X" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                // 2.d - left column checks
                else if (button1.Text == "" && button4.Text == "X" && button7.Text == "X")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "X" && button4.Text == "" && button7.Text == "X")
                {
                    GenericButtonClick(button4);
                }
                else if (button1.Text == "X" && button4.Text == "X" && button7.Text == "")
                {
                    GenericButtonClick(button7);
                }
                // 2.e - middle column checks
                else if (button2.Text == "" && button5.Text == "X" && button8.Text == "X")
                {
                    GenericButtonClick(button2);
                }
                else if (button2.Text == "X" && button5.Text == "" && button8.Text == "X")
                {
                    GenericButtonClick(button5);
                }
                else if (button2.Text == "X" && button5.Text == "X" && button8.Text == "")
                {
                    GenericButtonClick(button8);
                }
                // 2.f - left column checks
                else if (button3.Text == "" && button6.Text == "X" && button9.Text == "X")
                {
                    GenericButtonClick(button3);
                }
                else if (button3.Text == "X" && button6.Text == "" && button9.Text == "X")
                {
                    GenericButtonClick(button6);
                }
                else if (button3.Text == "X" && button6.Text == "X" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                // 2.g - diagonal checks
                else if (button1.Text == "" && button5.Text == "X" && button9.Text == "X")
                {
                    GenericButtonClick(button1);
                }
                else if (button1.Text == "X" && button5.Text == "" && button9.Text == "X")
                {
                    GenericButtonClick(button5);
                }
                else if (button1.Text == "X" && button5.Text == "X" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                else if (button7.Text == "" && button5.Text == "X" && button3.Text == "X")
                {
                    GenericButtonClick(button7);
                }
                else if (button7.Text == "X" && button5.Text == "" && button3.Text == "X")
                {
                    GenericButtonClick(button5);
                }
                else if (button7.Text == "X" && button5.Text == "X" && button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                // note - coding options 3 and 4 would require a lot more logic and they're not really necessary
                //  so while they're listed below they contain no code
                // option 3: fork - create opportunity where you can win two ways
                // option 4: block opponents fork (create two in a row to force their move, anticipate their fork move)
                // option 5: play the center
                else if (button5.Text == "")
                {
                    GenericButtonClick(button5);
                }
                // option 6: play opposite corner from opponent if open
                else if (button1.Text == "X" && button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                else if (button9.Text == "X" && button1.Text == "")
                {
                    GenericButtonClick(button1);
                }
                else if (button7.Text == "X" && button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                else if (button3.Text == "X" && button7.Text == "")
                {
                    GenericButtonClick(button7);
                }
                // option 7: play any empty corner
                else if (button9.Text == "")
                {
                    GenericButtonClick(button9);
                }
                else if (button1.Text == "")
                {
                    GenericButtonClick(button1);
                }
                else if (button3.Text == "")
                {
                    GenericButtonClick(button3);
                }
                else if (button7.Text == "")
                {
                    GenericButtonClick(button7);
                }
                // option 8: play any empty side
                else if (button2.Text == "")
                {
                    GenericButtonClick(button2);
                }
                else if (button4.Text == "")
                {
                    GenericButtonClick(button4);
                }
                else if (button6.Text == "")
                {
                    GenericButtonClick(button6);
                }
                else if (button8.Text == "")
                {
                    GenericButtonClick(button8);
                }
            }
        }

        // below are the click actions for each button, which just call the GenericButtonClick method
        private void button1_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button6);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button7);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button8);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GenericButtonClick(button9);
        }
    }
}
