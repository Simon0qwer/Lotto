using System;

namespace Lotto;

internal class Program
{
    static readonly string[,,] cards = {
        {{"6", " ", "28", " ", "47", " ", "63", " ", "89"},
        {" ", " ", "20", "34", " ", "53", " ", "70", "86"},
        {" ", "11", " ", "32", "45", "51", " ", "77", " "}},

        {{"2", " ", " ", " ", "43", "59", "60", "73", ""},
        {" ", "17", "21", "38", "46", " ", " ", " ", "81"},
        {"9", "12", "24", " ", " ", " ", "65", " ", "87"}}
    };

    static List<int> numbers = Enumerable.Range(1, 90).ToList();

    static bool gameState = true;

    static Random rand = new Random();

    static string[,] Extract2DArray(string[,,] cards, int cardIndex)
    {
        int rows = cards.GetLength(1);
        int cols = cards.GetLength(2);
        var slice = new string[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                slice[row, col] = cards[cardIndex, row, col];
            }
        }

        return slice;
    }

    static readonly string[,] playerCard = Extract2DArray(cards, 0);
    static readonly string[,] computerCard = Extract2DArray(cards, 1);

    static void PrintCard(string[,] card)
    {
        for (int row = 0; row < card.GetLength(0); row++)
        {
            Console.WriteLine(" -------------------------------------------- ");
            for (int col = 0; col < card.GetLength(1); col++)
            {
                Console.Write("|");
                Console.Write(card[row, col].PadLeft(3));
                Console.Write(" ");
                
            }
            Console.Write("|");
            Console.WriteLine();
            
        }
        Console.WriteLine(" -------------------------------------------- ");
    }

    static int RandomNumber()
    {
        int index = rand.Next(numbers.Count);
        int number = numbers[index];
        numbers.RemoveAt(index);
        return number;
    }

    static void MarkNumber(string[,] card, int number) { 
        string numberString = number.ToString();
        for(int row = 0; row < card.GetLength(0); row++)
        {
            for(int col = 0; col < card.GetLength(1); col++)
            {
                if(card[row, col] == numberString)
                {
                    card[row, col] = "X";
                }
            }
        }
    }

    static bool WinState(string[,] card)
    {
        for (int row = 0; row < card.GetLength(0); row++)
        {
            bool rowComplete = true;

            for (int col = 0; col < card.GetLength(1); col++)
            {
                if (!string.IsNullOrWhiteSpace(card[row, col]) &&
                    card[row, col] != "X")
                {
                    rowComplete = false;
                    break;
                }
            }

            if (rowComplete)
                return true;
        }

        return false;
    }

    static void Turn()
    {
        int randomNumber = RandomNumber();

        MarkNumber(computerCard, randomNumber);
        MarkNumber(playerCard, randomNumber);

        Console.WriteLine("Computer's card");
        PrintCard(computerCard);

        Console.WriteLine("Your card");
        PrintCard(playerCard);

        Console.WriteLine($"Number: {randomNumber}");

        bool computerWon = WinState(computerCard);
        bool playerWon = WinState(playerCard);

        if (computerWon || playerWon)
        {
            if (computerWon)
            {
                Console.WriteLine("Computer is the winner!");
            }
            if (playerWon)
            {
                Console.WriteLine("You are the winner!");
            }
            gameState = false;
        }
    }

    static void Main(string[] args)
    {
        
        while (gameState)
        {
            Turn();
            Console.WriteLine("Press Enter to continue, or type 'exit' to quit:");
            string input = Console.ReadLine();
            if (input != null && input.ToLower() == "exit")
            {
                gameState = false;
            }
            Console.Clear();
        }
    }
}