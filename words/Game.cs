using System;
using System.Collections.Generic;
using System.Threading;

namespace WordsGame
{
    public class Game
    {
        private int questionIndex = 0;
        private string[][] words;
        private List<int> askedQuestions = new List<int>();

        public Game()
        {
            Console.Title = "Flashcard Game"; // Set console title
            Console.Clear();
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║      WELCOME TO FLASHCARDS      ║");
            Console.WriteLine("╚═════════════════════════════════╝\n");

            StartGame();
        }

        public void StartGame()
        {
            words = new string[10][];
            for (int i = 0; i < 10; i++)
            {
                words[i] = new string[2];

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"Enter the word ({i + 1}/10): ");
                Console.ResetColor();
                words[i][0] = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Enter the answer: ");
                Console.ResetColor();
                words[i][1] = Console.ReadLine();

                Console.WriteLine();
            }

            Console.Clear();
            Console.WriteLine("All words are set. Let's start!\n");
            Thread.Sleep(1000);

            Play();
        }

        public void Play()
        {
            while (askedQuestions.Count < words.Length)
            {
                Console.Clear();
                int correctAnswerIndex = AskQuestion();

                int userChoice;
                while (true)
                {
                    Console.Write("\nYour choice (1-4): ");
                    if (int.TryParse(Console.ReadLine(), out userChoice) && userChoice >= 1 && userChoice <= 4)
                    {
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    Console.ResetColor();
                }

                if (userChoice - 1 == correctAnswerIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n✔ Correct! Well done!\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n✘ Incorrect. Try again!\n");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    continue;
                }

                askedQuestions.Add(questionIndex);
                Thread.Sleep(1500);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine("║  🎉 Congratulations! You won!  ║");
            Console.WriteLine("╚═════════════════════════════════╝\n");
            Console.ResetColor();
        }

        private int AskQuestion()
        {
            Random rand = new Random();
            do
            {
                questionIndex = rand.Next(0, words.Length);
            } while (askedQuestions.Contains(questionIndex));

            string questionWord = words[questionIndex][0];
            string correctAnswer = words[questionIndex][1];

            HashSet<int> optionsIndexes = new HashSet<int> { questionIndex };
            while (optionsIndexes.Count < 4)
            {
                int randomIndex = rand.Next(0, words.Length);
                optionsIndexes.Add(randomIndex);
            }

            List<string> options = new List<string>();
            int correctOptionIndex = 0;
            int index = 0;

            foreach (int idx in optionsIndexes)
            {
                if (idx == questionIndex)
                {
                    correctOptionIndex = index;
                }
                options.Add(words[idx][1]);
                index++;
            }

            Console.WriteLine("╔═════════════════════════════════╗");
            Console.WriteLine($"║ Question {askedQuestions.Count + 1}/10".PadRight(36) + "║");
            Console.WriteLine("╚═════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Word: ** {questionWord} **\n");
            Console.ResetColor();

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {options[i]}");
            }

            return correctOptionIndex;
        }
    }
}