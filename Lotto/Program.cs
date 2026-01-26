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

    static void Main(string[] args)
    {
        PrintCard(playerCard);

        foreach (var num in numbers) { Console.WriteLine(num); }
    }
}