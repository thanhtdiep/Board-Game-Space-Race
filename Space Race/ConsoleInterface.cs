using System;
//DO NOT DELETE the two following using statements *********************************
using Game_Logic_Class;
using Object_Classes;


namespace Space_Race
{
    class Console_Class
    {
        /// <summary>
        /// Algorithm below currently plays only one game
        /// 
        /// when have this working correctly, add the abilty for the user to 
        /// play more than 1 game if they choose to do so.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)

        {      
             DisplayIntroductionMessage();
            /*                    
             Set up the board in Board class (Board.SetUpBoard)
             Determine number of players - initally play with 2 for testing purposes 
             Create the required players in Game Logic class
              and initialize players for start of a game             
             loop  until game is finished           
                call PlayGame in Game Logic class to play one round
                Output each player's details at end of round
             end loop
             Determine if anyone has won
             Output each player's details at end of the game
           */
            // INPUT CODE BELOW
            
            int game_state = 10;
            while (game_state != 0)
            {
                if (game_state == 10)       //First Round
                {
                    DisplayNumOfPlayerQuery();
                    Board.SetUpBoard();
                    SpaceRaceGame.Players.Clear();
                    SpaceRaceGame.SetUpPlayers();
                    DisplayFirstRoundMessage();     //First Round Message
                    SpaceRaceGame.PlayOneRound();
                    DisplayRoundDetails();
                    game_state = 1;
                }
                else if (game_state == 1)    //Next Round
                {
                    DisplayNextRoundMessage();
                    SpaceRaceGame.PlayOneRound();
                    if (SpaceRaceGame.GameFinish())
                    {
                        game_state = 2;
                    }
                    else DisplayRoundDetails();

                }
               
                else if (game_state == 2)
                {
                    DisplayRoundDetailsWithWinner();
                    game_state = 3;
                }

                else if (game_state == 3)    // Prompt asking to play again
                {
                    DisplayPromptToPlayAgain();
                    char x = Char.Parse(Console.ReadLine());
                    if (x == 'Y' || x == 'y')
                    {
                        game_state = 10;
                        //Console.Write(game_state);
                    }

                    else if (x == 'N' || x == 'n')
                    {
                        game_state = 0;
                    }
                }

            }
                
            PressEnter();

        }//end Main


        /// <summary>
        /// Display a question about how many number of players is playing
        /// Pre:    The state of the game
        /// Post:   Asking whether the player want to play again
        /// </summary>
        static void DisplayPromptToPlayAgain()
        {
            Console.Write("\n\tPlay Again (Y or N): ");
        } //end DisplayRoundDetails

        static int ReadOption()
        {
            string choice;
            int option;
            bool okayChoice;


            do
            {
                choice = Console.ReadLine();
                okayChoice = int.TryParse(choice, out option);
                if (!okayChoice)
                {
                    okayChoice = false;
                    //Console.Write("Error: invalid number of players entered.");
                }
                if (option < SpaceRaceGame.MIN_PLAYERS || option >SpaceRaceGame.MAX_PLAYERS)
                {
                    okayChoice = false;
                    Console.Write("\nError: invalid number of players entered.");
                    Console.WriteLine("\n\n\tThis game is for 2 to 6 players");
                    Console.Write("\tHow many players (2-6): ");
                }
            } while (!okayChoice);

            return option;
        }

        /// <summary>
        /// Display a question about how many number of players is playing
        /// Pre:    none.
        /// Post:   A question is displayed to the console.
        /// </summary>
        static void DisplayRoundDetailsWithWinner()
        {
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine("\n\t\t" + player.Name + " on square " + player.Position + " with " + player.RocketFuel + " yottawatt of power remaining");
            }
            Console.WriteLine("\n\tThe following player(s) finished the game");

            foreach (Player player in SpaceRaceGame.Players)
            {
                if (player.AtFinish)
                Console.WriteLine("\n\t\t" + player.Name);
            }

                Console.WriteLine("\n\tIndividual players finished with the at the locations specified.\n");
            foreach (Player player in SpaceRaceGame.Players)
            { 
                Console.WriteLine("\t\t" + player.Name + " with " + player.RocketFuel + " yattowatt of power at square " + player.Position + "\n");
                
            }
            Console.WriteLine("\tPress Enter key to continue ...");
            Console.ReadLine();
        } //end DisplayRoundDetails


        /// <summary>
        /// Display a question about how many number of players is playing
        /// Pre:    none.
        /// Post:   A question is displayed to the console.
        /// </summary>
        static void DisplayRoundDetails()
        {
            foreach (Player player in SpaceRaceGame.Players)
            {
                Console.WriteLine("\n\t\t" + player.Name + " on square " + player.Position + " with " + player.RocketFuel + " yottawatt of power remaining");
            }
            Console.WriteLine("\nPress Enter to play a round ...");
            Console.ReadLine();

        } //end DisplayRoundDetails

        /// <summary>
        /// Display a question about how many number of players is playing
        /// Pre:    none.
        /// Post:   A question is displayed to the console.
        /// </summary>
        static void DisplayNumOfPlayerQuery()
        {
            Console.WriteLine("\tThis game is for 2 to 6 players");
            Console.Write("\tHow many players (2-6): ");
            int numOfPlayers = ReadOption();
            SpaceRaceGame.NumberOfPlayers = numOfPlayers;
            Console.WriteLine("\n\nPress Enter to play a round ...");
            Console.ReadLine();
        } //end DisplayNumOfPlayerQuery

        static void DisplayFirstRoundMessage()
        {
            Console.WriteLine("\tFirst Round\n");
        } //end DisplayFirstRoundMessage
        static void DisplayNextRoundMessage()
        {
            Console.WriteLine("\tNext Round\n");
        } //end DisplayNextRoundMessage

        /// <summary>
        /// Display a welcome message to the console
        /// Pre:    none.
        /// Post:   A welcome message is displayed to the console.
        /// </summary>
        static void DisplayIntroductionMessage()
        {
            Console.WriteLine("Welcome to Space Race.\n");
        } //end DisplayIntroductionMessage

        /// <summary>
        /// Displays a prompt and waits for a keypress.
        /// Pre:  none
        /// Post: a key has been pressed.
        /// </summary>
        static void PressEnter()
        {
            Console.WriteLine("\n\tThanks for playing Space Race");
            Console.Write("\n\tPress Enter to terminate program ...");
            Console.ReadLine();
        } // end PressAny



    }//end Console class
}
