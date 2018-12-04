//SWEN503 - Assignment 1
//Fiona Crook
//300442873

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC_hangman_CSharp
{
    class Program
    {
        //this method runs most of the game functionality
        static void Main(string[] args)
        {
            //endgame variables
            bool gameover = false;
            bool didwin = false;
            int liveLeft = 12;

            //works to read in file to string array
            string[] words = System.IO.File.ReadAllLines("dictionarylarge.txt");
            //file located in \FC_hangman_CSharp\FC_hangman_CSharp\bin\Debug

            //choose random word from list, then convert to uppercase
            string word = words[new Random().Next(0, words.Length)].ToUpper();

            string[] chosenWord = new string[word.Length]; 

            //read the word chars, convert to strings, save to string array
            for (int i = 0; i < word.Length; i++)
            {
                chosenWord[i] = (word[i].ToString());
            }

            //list of correct guessed letters - based on word size
            //initialise as underscores
            string[] correctGuess = new string[chosenWord.Length];
            int lettersLeftToGuess = chosenWord.Length;
            for (int i = 0; i < chosenWord.Length; i++)
            {
                correctGuess[i] = "_";
            }

            //pastGuesses needs to be a list because it changes size
            //initialise with space so first comparison has something to work with
            List<string> pastGuesses = new List<string> { " " };

            //initialise hangmanPic with spaces
            string[,] hangmanPic = new string[9, 8];
            for (int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    hangmanPic[r, c] = " ";
                }
            }

            //the start of playing the game
            Console.WriteLine("This is a word guessing game");
            Console.WriteLine("You need to correctly guess the letters in the word below before your lives run out");
            Console.WriteLine("Word to guess:");
            Console.WriteLine(string.Join(" ", correctGuess.ToArray())); //toArray is defunct now, but used in older .net versions
            Console.WriteLine();

            while (!gameover)
            {
                //get user input 
                Console.WriteLine("Guess a letter:");
                string letterTemp = Console.ReadLine().ToUpper();
                string letterGuess = letterTemp.Substring(0, 1);
                bool guessInList = false;
                Console.WriteLine();

                //check if letter is in pastGuesses
                foreach (string lett in pastGuesses)
                {
                    if (lett == letterGuess)
                    {
                        guessInList = true;
                        break;
                    }
                }

                //if letter is in pastGuesses - do nothing
                if (guessInList)
                {
                    Console.WriteLine("You have already guessed " + letterGuess);
                }
                else
                {
                    bool letterInWord = false;
                    pastGuesses.Add(letterGuess);

                    //check if letter in chosenWord - if yes, replace underscore with letter
                    for (int i = 0; i < chosenWord.Length; i++)
                    {
                        if (chosenWord[i] == letterGuess)
                        {
                            letterInWord = true;
                            correctGuess[i] = letterGuess;
                            lettersLeftToGuess--;
                        }
                    }

                    //if not letter in chosenWord - lose a life
                    if (!letterInWord)
                    {
                        Console.WriteLine("Sorry, " + letterGuess + " was not in the word");
                        liveLeft--;
                        hangmanPic = loseALife(hangmanPic, liveLeft);
                    }

                } //end else

                //draw outputs to screen
                drawHangmanPic(hangmanPic);
                Console.WriteLine("Word to guess:");
                Console.WriteLine(string.Join(" ", correctGuess.ToArray()));
                Console.WriteLine();
                Console.WriteLine("Your previous guesses:");
                Console.WriteLine(string.Join(" ", pastGuesses.ToArray()));
                Console.WriteLine();
                Console.WriteLine("*********************************************************");

                //check end conditions
                if (lettersLeftToGuess == 0)
                {
                    didwin = true;
                    gameover = true;
                }

                if (liveLeft == 0)
                {
                    gameover = true;
                }



            } //end while

            //endgame
            if (didwin)
            {
                Console.WriteLine("Congrats, you won!");
                Console.WriteLine("You correctly guessed the word " + word);
            }
            else
            {
                Console.WriteLine("Sorry, you have lost");
                Console.WriteLine("The word you were trying to guess was " + word);
            }

        } //end main

        //adds characters to hangmanPic depending on lives lost
        //passing in and out hangman pic cos global fields was being weird
        private static string[,] loseALife(string[,] pic, int livesStill)
        {
            if (livesStill == 11)
            {
                pic[8, 0] = "-";
                pic[8, 1] = "-";
                pic[8, 2] = "-";
                pic[8, 3] = "-";
                pic[8, 4] = "-";
            }

            if (livesStill == 10)
            {
                pic[1, 2] = "|";
                pic[2, 2] = "|";
                pic[3, 2] = "|";
                pic[4, 2] = "|";
                pic[5, 2] = "|";
                pic[6, 2] = "|";
                pic[7, 2] = "|";
            }
            if (livesStill == 9)
            {
                pic[7, 3] = "\\";
            }
            if (livesStill == 8)
            {
                pic[0, 2] = "_";
                pic[0, 3] = "_";
                pic[0, 4] = "_";
                pic[0, 5] = "_";
                pic[0, 6] = "_";
            }
            if (livesStill == 7)
            {
                pic[1, 3] = "/";
            }
            if (livesStill == 6)
            {
                pic[1, 6] = "|";
            }
            if (livesStill == 5)
            {
                pic[2, 6] = "O";
            }
            if (livesStill == 4)
            {
                pic[3, 6] = "|";
                pic[4, 6] = "|";
            }
            if (livesStill == 3)
            {
                pic[3, 5] = "-";
            }
            if (livesStill == 2)
            {
                pic[3, 7] = "-";
            }
            if (livesStill == 1)
            {
                pic[5, 5] = "/";
            }
            if (livesStill == 0)
            {
                pic[5, 7] = "\\";
            }

            return pic;
        }

        //draw hangmanPic to screen
        public static void drawHangmanPic(string[,] pic)
        {
            for (int r = 0; r < pic.GetLength(0); r++)
            {
                for (int c = 0; c < pic.GetLength(1); c++)
                {
                    Console.Write(pic[r, c]);
                }
                Console.WriteLine();
            }
        }
    }
}
