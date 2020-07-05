using System.Diagnostics;

namespace Object_Classes {
    /// <summary>
    /// Models a game board for Space Race consisting of three different types of squares
    /// 
    /// Ordinary squares, Wormhole squares and Blackhole squares.
    /// 
    /// landing on a Wormhole or Blackhole square at the end of a player's move 
    /// results in the player moving to another square
    /// 
    /// </summary>
    public static class Board {
        /// <summary>
        /// Models a game board for Space Race consisting of three different types of squares
        /// 
        /// Ordinary squares, Wormhole squares and Blackhole squares.
        /// 
        /// landing on a Wormhole or Blackhole square at the end of a player's move 
        /// results in the player moving to another square
        /// 
        /// 
        /// </summary>

        public const int NUMBER_OF_SQUARES = 56;
        public const int START_SQUARE_NUMBER = 0;
        public const int FINISH_SQUARE_NUMBER = NUMBER_OF_SQUARES - 1;

        private static Square[] squares = new Square[NUMBER_OF_SQUARES];

        public static Square[] Squares {
            get {
                Debug.Assert(squares != null, "squares != null",
                   "The game board has not been instantiated");
                return squares;
            }
        }

        public static Square StartSquare {
            get {
                return squares[START_SQUARE_NUMBER];
            }
        }


        /// <summary>
        ///  Eight Wormhole squares.
        ///  
        /// Each row represents a Wormhole square number, the square to jump forward to and the amount of fuel consumed in that jump.
        /// 
        /// For example {2, 22, 10} is a Wormhole on square 2, jumping to square 22 and using 10 units of fuel
        /// 
        /// </summary>
        private static int[,] wormHoles =
        {
            {2, 22, 10},
            {3, 9, 3},
            {5, 17, 6},
            {12, 24, 6},
            {16, 47, 15},
            {29, 38, 4},
            {40, 51, 5},
            {45, 54, 4}
        };

        /// <summary>
        ///  Eight Blackhole squares.
        ///  
        /// Each row represents a Blackhole square number, the square to jump back to and the amount of fuel consumed in that jump.
        /// 
        /// For example {10, 4, 6} is a Blackhole on square 10, jumping to square 4 and using 6 units of fuel
        /// 
        /// </summary>
        private static int[,] blackHoles =
        {
            {10, 4, 6},
            {26, 8, 18},
            {30, 19, 11},
            {35,11, 24},
            {36, 34, 2},
            {49, 13, 36},
            {52, 41, 11},
            {53, 42, 11}
        };
        private static int[] oHoles = new int[]
        {
           1, 4, 6, 7, 8, 9, 11, 13, 14, 15, 17, 18, 19, 20, 21,
            22, 23, 24, 25, 27, 28, 31, 32, 33, 34, 37, 38, 39
            , 41, 42, 43, 44, 46, 47, 48, 50, 51, 54
        };
        /// <summary>
        /// Parameterless Constructor
        /// Initialises a board consisting of a mix of Ordinary Squares,
        ///     Wormhole Squares and Blackhole Squares.
        /// 
        /// Pre:  none
        /// Post: board is constructed
        /// </summary>
        public static void SetUpBoard() {

            // Create the 'start' square where all players will start.
            squares[START_SQUARE_NUMBER] = new Square("Start", START_SQUARE_NUMBER);

            // Create the main part of the board, squares 1 .. 54
            //  CODE NEEDS TO BE ADDED HERE

            //
            //   Need to call the appropriate constructor for each square
            //       either new Square(...),  new WormholeSquare(...) or new BlackholeSquare(...)
            // Square for Wormhole
            for (int i = 0; i < wormHoles.GetLength(0); i++)
            {
                //string name = i.ToString();
                squares[wormHoles[i,0]] = new WormholeSquare(wormHoles[i, 0].ToString(), wormHoles[i,0], wormHoles[i,1], wormHoles[i,2]);
            }

            // Square for Blackhole
            for (int i = 0; i < blackHoles.GetLength(0); i++)
            {
               // string name = i.ToString();
                squares[blackHoles[i, 0]] = new BlackholeSquare(blackHoles[i, 0].ToString(), blackHoles[i, 0], blackHoles[i, 1], blackHoles[i, 2]);
            }

            // Square for holes
            for (int i = 0; i < oHoles.GetLength(0); i++)
            {
                //string name = i.ToString();
                squares[oHoles[i]] = new Square(oHoles[i].ToString(), oHoles[i]);
            }

            // Create the 'finish' square.
            squares[FINISH_SQUARE_NUMBER] = new Square("Finish", FINISH_SQUARE_NUMBER);
        } // end SetUpBoard

        /// <summary>
        /// Finds the destination square and the amount of fuel used for either a 
        /// Wormhole or Blackhole Square.
        /// 
        /// pre: squareNum is either a Wormhole or Blackhole square number
        /// post: destNum and amount are assigned correct values.
        /// </summary>
        /// <param name="holes">a 2D array representing either the Wormholes or Blackholes squares information</param>
        /// <param name="squareNum"> a square number of either a Wormhole or Blackhole square</param>
        /// <param name="destNum"> destination square's number</param>
        /// <param name="amount"> amont of fuel used to jump tyo the deestination square</param>
        private static void FindDestSquare(int[,] holes, int squareNum, out int destNum, out int amount) {
            //const int start = 0, exit = 1, 
            //const int fuel = 2;
            destNum = 0; amount = 0;

            //  CODE NEEDS TO BE ADDED HERE 
            for (int i = 0; i < holes.GetLength(0); i++)    // Loop through each row of holes
            {
                if (squareNum == holes[i, 0])
                {
                    destNum = holes[i, 1];
                    amount = holes[i, 2];
                    return;
                }
            }
        }  //end FindDestSquare

    } //end class Board
}