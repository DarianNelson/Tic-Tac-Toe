using System;
using System.Drawing;
using System.Text;

namespace Tic_Tac_Toesies {
    internal class Program {
        static void Main(string[] args) {
            //VARIABLES
            int xPosition = 0;
            int yPosition = 0;
            int turns = 9;
            char symbol = '\0';
            bool playAgain = true;

            //CREATE BOARD ARRAYS
            char[,] MainBoard = new char[22, 22];
            char[,] gameBoardArray = new char[3, 3];

            //LOOP FOR GAME
            while (turns > 0 && playAgain) {
                // SHOW EMPTY BOARD AND SYMBOLS
                DrawBoard(MainBoard);

                // INITIALIZE BOARD WITH NULL CHARS
                FillGameBoardArray(gameBoardArray, symbol);

                //DISPLAY SYMBOLS TO THE SIDE
                DrawSymbolsX(MainBoard);
                DrawSymbolsO(MainBoard);

                //LOOP THROUGH GAMEBOARD ARRAY
                for (int columns = 0; columns < gameBoardArray.GetLength(1); columns++) {
                    for (int rows = 0; rows < gameBoardArray.GetLength(0); rows++) {
                        //DETERMINE SYMBOL FOR TURN
                        if (turns % 2 == 0) {
                            symbol = 'O';
                        } else {
                            symbol = 'X';
                        }//end if

                        //GET DATA FROM THE USER
                        Console.SetCursorPosition(20, 11);
                        string userInput = Input($"Turn {turns}: {symbol}: Please enter [X Y] coordinates :");
                        string[] userCoords = userInput.Split(' ');

                        //GET XPOS TO UPDATE
                        xPosition = int.Parse(userCoords[0]);

                        //GET YPOS TO UPDATE
                        yPosition = int.Parse(userCoords[1]);

                        //VALIDATE X AND Y INPUTS
                        while (xPosition < 0 || xPosition > 2 || yPosition < 0 || yPosition > 2) {
                            //GIVE ERROR MESSAGE
                            Console.SetCursorPosition(20, 12);
                            Console.Write("Coordinates can not be less than 0 or greater than 2. Try again.");

                            //GET DATA AGAIN
                            Console.SetCursorPosition(20, 13);
                            userInput = Input($"Turn {turns}: Please enter [X Y] coordinates :");
                            userCoords = userInput.Split(' ');

                            //GET XPOS TO UPDATE
                            xPosition = int.Parse(userCoords[0]);

                            //GET YPOS TO UPDATE
                            yPosition = int.Parse(userCoords[1]);

                        }//end while

                        //DETERMINE IF SYMBOL CAN BE PLACED
                        if (PlaceMarker(gameBoardArray, symbol, xPosition, yPosition) == true) {

                            //DRAW SYMBOL ON MAIN BOARD
                            DrawSymbol(symbol, xPosition, yPosition);

                            //CHECK TO SEE IF PLAYER WON
                            WinCheck(gameBoardArray, symbol);

                            //IF PLAYER WON, ASK IF PLAYING AGAIN
                            if (WinCheck(gameBoardArray, symbol) == 1) {
                                turns = 0;
                                Console.SetCursorPosition(20, 11);
                                string input = Input("Would you like to play again? ");
                                string answer = input.ToLower();
                                //INPUT VALIDATION LOOP
                                while (answer != "yes" && answer != "no") {
                                    Console.SetCursorPosition(20, 11);
                                    Console.WriteLine("Please answer with yes or no. ");
                                    input = Input("Play again? ");
                                    answer = input.ToLower();
                                }//end loop
                                playAgain = answer == "yes";

                                //IF NOT PLAYING AGAIN, CLEAR BOARD AND EXIT
                                if (playAgain == false) {
                                    Console.Clear();
                                    Console.SetCursorPosition(20, 11);
                                    Console.WriteLine("Thanks for playing!");
                                    turns = 0;
                                }//end if

                                //IF PLAYING AGAIN, CLEAR BOARD AND START OVER
                                if (playAgain == true) {
                                    Console.Clear();
                                    // SHOW EMPTY BOARD AND SYMBOLS
                                    DrawBoard(MainBoard);
                                
                                    // INITIALIZE BOARD WITH NULL CHARS
                                    FillGameBoardArray(gameBoardArray, symbol);

                                    //DISPLAY SYMBOLS TO THE SIDE
                                    DrawSymbolsX(MainBoard);
                                    DrawSymbolsO(MainBoard);

                                    //RESET TURNS
                                    turns = 10;
                                }//end if
                            }//end if 

                            //IF DID NOT WIN, GO TO NEXT TURN
                            if (WinCheck(gameBoardArray, symbol) == 0) {
                                //GO TO NEXT TURN
                                if (turns > 0) {
                                    turns--;
                                }//end if
                            }//end if
                        
                        //IF CAN NOT PLACE MARKER, GIVE ERROR MESSAGE
                        } else if (PlaceMarker(gameBoardArray, symbol, xPosition, yPosition) == false) {
                            Console.SetCursorPosition(20, 12);
                            Console.WriteLine("Space Not Available!");
                        }//end if

                    }//end rows for
                }//end columns for

                //IF A TIE, ASK IF PLAYING AGAIN
                if (turns <= 0) {
                    Console.SetCursorPosition(20, 11);
                    string input = Input("Would you like to play again? ");
                    string answer = input.ToLower();

                    //INPUT VALIDATION LOOP
                    while (answer != "yes" && answer != "no") {
                        Console.SetCursorPosition(20, 12);
                        Console.WriteLine("Please answer with yes or no. ");
                        Console.SetCursorPosition(20, 13);
                        input = Input("Play again? ");
                        answer = input.ToLower();
                    }//end loop

                    playAgain = answer == "yes";

                }//end if

                //IF NOT PLAYINIG AGAIN, EXIT GAME
                if (playAgain == false) {
                    //CLEAR BOARD
                    Console.Clear();

                    //GIVE EXIT MESSAGE
                    Console.WriteLine("Thanks for playing!");

                    //SET TURNS TO ZERO
                    turns = 0;
                }//end if

                //IF PLAYING AGAIN, CLEAR BOARD AND RESTART
                if (playAgain == true) {
                    //CLEAR BOARD
                    Console.Clear();

                    // SHOW EMPTY BOARD AND SYMBOLS
                    DrawBoard(MainBoard);

                    // INITIALIZE BOARD WITH NULL CHARS
                    FillGameBoardArray(gameBoardArray, symbol);

                    //DISPLAY SYMBOLS TO THE SIDE
                    DrawSymbolsX(MainBoard);
                    DrawSymbolsO(MainBoard);

                    //RESET TURNS
                    turns = 9;
                }//end if
            }//end while Loop
            
        }//end main
        static void DrawBoard(char[,] MainBoard) {
            byte[] color = { 0, 0, 255 };
           
            //DRAW VERTICAL LINES
            for (int x = 0; x <= 16;  x++) {
                //LEFT
                ConsoleSetBlock(x, 5, color);
                //RIGHT
                ConsoleSetBlock(x, 11, color);
            }//end for

            //DRAW HORIZONTAL LINES
            for (int y = 0; y <= 16; y++) {
                //TOP
                ConsoleSetBlock(5, y, color);
                //BOTTOM
                ConsoleSetBlock(11, y, color);
            }//end for

            //LABEL EACH BOX
            MainBoard[0, 0] = '0';
            MainBoard[1, 0] = '0';

            MainBoard[6, 0] = '1';
            MainBoard[7, 0] = '0';

            MainBoard[12, 0] = '2';
            MainBoard[13, 0] = '0';

            MainBoard[0, 6] = '0';
            MainBoard[1, 6] = '1';

            MainBoard[6, 6] = '1';
            MainBoard[7, 6] = '1';

            MainBoard[12, 6] = '2';
            MainBoard[13, 6] = '1';

            MainBoard[0, 12] = '0';
            MainBoard[1, 12] = '2';

            MainBoard[6, 12] = '1';
            MainBoard[7, 12] = '2';

            MainBoard[12, 12] = '2';
            MainBoard[13, 12] = '2';

        }//end function
        static void DrawSymbolsX(char[,] MainBoard) {
            //COLOR ARRAY
            byte[] color = { 0, 255, 0 };

            //DRAW X TO THE RIGHT OF THE BOARD
            ConsoleSetBlock(20, 1, color);
            ConsoleSetBlock(22, 1, color);
            ConsoleSetBlock(21, 2, color);
            ConsoleSetBlock(20, 3, color);
            ConsoleSetBlock(22, 3, color);
                 
        }//end function
        static void DrawSymbolsO(char[,] MainBoard) {
            //COLOR ARRAY
            byte[] color = { 255, 0, 0 };

            //DRAW O TO THE RIGHT OF THE BOARD
            ConsoleSetBlock(20, 5, color);
            ConsoleSetBlock(20, 6, color);
            ConsoleSetBlock(20, 7, color);
            ConsoleSetBlock(21, 5, color);
            ConsoleSetBlock(21, 7, color);
            ConsoleSetBlock(22, 5, color);
            ConsoleSetBlock(22, 6, color);
            ConsoleSetBlock(22, 7, color);

        }//end function
        static void DrawSymbol(char symbol, int xPosition, int yPosition) {

            //DETERMINE NEW X Y COORDINATES
            if (xPosition == 0 && yPosition == 0) {
                xPosition = 0;
                yPosition = 0;
            } else if (xPosition == 1 && yPosition == 0) {
                xPosition = 6;
                yPosition = 0;
            } else if (xPosition == 2 && yPosition == 0) {
                xPosition = 12;
                yPosition = 0;
            } else if (xPosition == 0 && yPosition == 1) {
                xPosition = 0;
                yPosition = 6;
            } else if (xPosition == 1 && yPosition == 1) {
                xPosition = 6;
                yPosition = 6;
            } else if (xPosition == 2 && yPosition == 1) {
                xPosition = 12;
                yPosition = 6;
            } else if (xPosition == 0 && yPosition == 2) {
                xPosition = 0;
                yPosition = 12;
            } else if (xPosition == 1 && yPosition == 2) {
                xPosition = 6;
                yPosition = 12;
            } else if (xPosition == 2 && yPosition == 2) {
                xPosition = 12;
                yPosition = 12;
            }//end if

            //IF X
            if (symbol == 'X') {
                //ARRAY
                    byte[] color = { 0, 255, 0 };

                //DRAW X
                    ConsoleSetBlock(1 + xPosition, 1 + yPosition, color);
                    ConsoleSetBlock(3 + xPosition, 1 + yPosition, color);
                    ConsoleSetBlock(2 + xPosition, 2 + yPosition, color);
                    ConsoleSetBlock(1 + xPosition, 3 + yPosition, color);
                    ConsoleSetBlock(3 + xPosition, 3 + yPosition, color);
            }//end if

            //IF O
            if (symbol == 'O') {
                //ARRAY
                    byte[] color = { 255, 0, 0 };

                //DRAW O 
                    ConsoleSetBlock(1 + xPosition, 1 + yPosition, color);
                    ConsoleSetBlock(1 + xPosition, 2 + yPosition, color);
                    ConsoleSetBlock(1 + xPosition, 3 + yPosition, color);
                    ConsoleSetBlock(2 + xPosition, 1 + yPosition, color);
                    ConsoleSetBlock(2 + xPosition, 3 + yPosition, color);
                    ConsoleSetBlock(3 + xPosition, 1 + yPosition, color);
                    ConsoleSetBlock(3 + xPosition, 2 + yPosition, color);
                    ConsoleSetBlock(3 + xPosition, 3 + yPosition, color);
            }//end if

        }//end function
        static bool PlaceMarker(char[,] gameBoardArray, char symbol, int xSlot, int ySlot) {
            //IF SPACE IS EMPTY
            if (gameBoardArray[xSlot, ySlot] == '\0') {
                //PLACE SYMBOL ON EMPTY SPACE
                gameBoardArray[xSlot, ySlot] = symbol;
                return true;
            } else if (gameBoardArray[xSlot, ySlot] == 'X' || gameBoardArray[xSlot, ySlot] == 'O') {
                return false;
            }//end if
            return true;
        }//end function
        static int WinCheck(char[,] gameBoardArray, char symbol) {
            bool isWonGame = false;
            int wins = 0;

            for (int columns = 0; columns < 9; columns++) {
                for (int rows = 0; rows < 9; rows++) {
                    if (symbol == 'X') {
                        if (gameBoardArray[0, 0] == symbol && gameBoardArray[0, 1] == symbol && gameBoardArray[0, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[1, 0] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[1, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[2, 0] == symbol && gameBoardArray[2, 1] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;   
                        } else if (gameBoardArray[0, 0] == symbol && gameBoardArray[1, 0] == symbol && gameBoardArray[2, 0] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 1] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 1] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 2] == symbol && gameBoardArray[1, 2] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 0] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 2] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 0] == symbol) {
                            isWonGame = true;
                        }//end if
                    }//end if

                    if (symbol == 'O') {
                        if (gameBoardArray[0, 0] == symbol && gameBoardArray[0, 1] == symbol && gameBoardArray[0, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[1, 0] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[1, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[2, 0] == symbol && gameBoardArray[2, 1] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 0] == symbol && gameBoardArray[1, 0] == symbol && gameBoardArray[2, 0] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 1] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 1] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 2] == symbol && gameBoardArray[1, 2] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 0] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 2] == symbol) {
                            isWonGame = true;
                        } else if (gameBoardArray[0, 2] == symbol && gameBoardArray[1, 1] == symbol && gameBoardArray[2, 0] == symbol) {
                            isWonGame = true;
                        }//end if
                    }//end if
                }//end loop
            }//end loop

            if (isWonGame == true) {
                Console.Clear();
                Console.WriteLine($"{symbol} wins!");
                wins = 1;
            }//end if

            if (isWonGame == false) {
                wins = 0;
            }//end if

            return wins;

        }//end function
        static void FillGameBoardArray(char[,] gameBoardArray, char symbol) {
            symbol = '\0';
            for (int y = 0; y < gameBoardArray.GetLength(1); y++) {
                for (int x = 0; x < gameBoardArray.GetLength(0); x++) {
                    gameBoardArray[x, y] = symbol;
                }//end for
            }//end for
        }//end function
        static void PrintGameBoardArray (char[,] gameBoardArray, char symbol) {
            //LOOP THROUGH ALL ROWS
            for (int y = 0; y < gameBoardArray.GetLength(1) ; y++) {
                //LOOP THROUGH ALL COLUMNS OF EACH ROW
                for (int x = 0; x < gameBoardArray.GetLength(0); x++) {

                    //OUTPUT VALUE IN ARRAY AT CURRENT COLUMN
                    Console.Write(gameBoardArray[x, y] + " ");
                }//end for

                //MOVE TO NEXT LINE FOR THE NEXT ROW
                Console.WriteLine();
            }//end for
        }//end function
        #region HELPER FUNCTIONS

        static void ConsoleSetForeColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[38;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBackColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[48;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBlock(int xPos, int yPos, byte[] color) {
            //STORE OLD COLORS
            ConsoleColor origForeground = Console.ForegroundColor;
            ConsoleColor origBackground = Console.BackgroundColor;

            //SET BLOCK COLOR
            byte red = color[0];
            byte grn = color[1];
            byte blu = color[2];

            ConsoleSetForeColor(red, grn, blu);
            ConsoleSetBackColor(red, grn, blu);

            //MOVE CURSOR TO POSITION
            Console.SetCursorPosition(xPos, yPos);

            //DRAW BLOCK
            Console.Write(" ");

            //CHANGE COLORS BACK
            Console.ForegroundColor = origForeground;
            Console.BackgroundColor = origBackground;
        }//end function
        static string Input(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }//end function

        static decimal InputDecimal(string message) {
            string value = Input(message);
            return decimal.Parse(value);
        }//end function

        static decimal TryInputDecimal(string message) {
            //VARIABLES
            decimal parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DECIMAL HAS BEEN SUBMITTED
            do {
                gotParsed = decimal.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static double InputDouble(string message) {
            string value = Input(message);
            return double.Parse(value);
        }//end function

        static double TryInputDouble(string message) {
            //VARIABLES
            double parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DOUBLE HAS BEEN SUBMITTED
            do {
                gotParsed = double.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static int InputInt(string message) {
            string value = Input(message);
            return int.Parse(value);
        }//end function

        static int TryInputInt(string message) {
            //VARIABLES
            int parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID INT HAS BEEN SUBMITTED
            do {
                gotParsed = int.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static void Print(string message) {
            Console.WriteLine(message);
        }//end function

        static void PrintColor(string message, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }//end function
        #endregion

    }//end class
}//end namespace