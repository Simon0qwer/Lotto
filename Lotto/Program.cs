using System;

namespace Lotto;

internal class Program
{
    static List<int> numbers = Enumerable.Range(1, 90).ToList();

    static bool gameState = true;

    static Random rand = new Random();

    static readonly string[,] playerCard = RandomCard();
    static readonly string[,] computerCard = RandomCard();

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

    static int[] RandomSpacesPositionsForCard()
    {
        int[] positions = { 0, 0, 0, 0, 1, 1, 1, 1, 1 };

        rand.Shuffle(positions);

        return positions;

    }

    static string[,] RandomCard()
    {
        string[,] card = new string[3, 9];
        int cols = card.GetLength(1);
        int rows = card.GetLength(0);

        for (int row = 0; row < rows; row++)
        {
            int[] spaces = RandomSpacesPositionsForCard();
            for (int col = 0; col < cols; col++)
            {
                
                if (spaces[col] == 1)
                {
                    switch (col)
                    {
                        case 0:
                            card[row, col] = rand.Next(1, 9).ToString();
                            break;
                        case 1:
                            card[row, col] = rand.Next(10, 19).ToString();
                            break;
                        case 2:
                            card[row, col] = rand.Next(20, 29).ToString();
                            break;
                        case 3:
                            card[row, col] = rand.Next(30, 39).ToString();
                            break;
                        case 4:
                            card[row, col] = rand.Next(40, 49).ToString();
                            break;
                        case 5:
                            card[row, col] = rand.Next(50, 59).ToString();
                            break;
                        case 6:
                            card[row, col] = rand.Next(60, 69).ToString();
                            break;
                        case 7:
                            card[row, col] = rand.Next(70, 79).ToString();
                            break;
                        case 8:
                            card[row, col] = rand.Next(80, 90).ToString();
                            break;
                    }
                }
                else
                {
                    card[row, col] = " ";
                }
            }
        }
        return card;
    }
    static void MarkNumber(string[,] card, int number)
    {
        string numberString = number.ToString();
        for (int row = 0; row < card.GetLength(0); row++)
        {
            for (int col = 0; col < card.GetLength(1); col++)
            {
                if (card[row, col] == numberString)
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