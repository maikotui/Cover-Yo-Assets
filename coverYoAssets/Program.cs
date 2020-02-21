using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CoverYourAssets
{
    class Program
    {
        private static GameController controller;
        // TODO: create different menus
        private static int currentMenu;

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            DisplayStartScreen();
            while (controller.IsActive)
            {
                Update();
            }
        }

        private static void DisplayStartScreen()
        {
            bool hasReceivedCorrectResponse = false;
            bool badResponse = false;
            bool notEnough = false;

            // Until we get a good number of players, keep asking
            while (!hasReceivedCorrectResponse)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(Constants.TITLE_ASCII);
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.Red;
                if (badResponse)
                {
                    Console.WriteLine(">> Received an invalid number of players.");
                    badResponse = false;
                }
                else if (notEnough)
                {
                    Console.WriteLine(">> Thats not enough players");
                    badResponse = false;
                }
                else
                {
                    Console.WriteLine("");
                }

                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("<< How many players?");
                Console.Write(">> ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result <= 1)
                    {
                        notEnough = true;
                    }
                    else
                    {
                        controller = new GameController(result);
                        hasReceivedCorrectResponse = true;
                    }
                }
                else
                {
                    badResponse = true;
                }
            }

            // Prepare the console for drawing
            Console.Clear();
            Console.CursorVisible = false;
        }

        private static void Update()
        {
            Draw();
            ProcessInput();
        }

        private static void ProcessInput()
        {
            string input = Console.ReadLine();
            if (input == "q" || input == "quit")
            {
                DisplayStartScreen();
            }

            // Process input based on menu
            switch (currentMenu)
            {
                case 1:
                    switch (input)
                    {
                        case "a": // Create an asset from cards in hand

                            break;
                        case "s": // Attempt to steal from another player

                            break;
                        case "d": // Discard a single card from the player's hand

                            break;
                        case "f": // Take the card from the top of the pile if able to

                            break;
                    }
                    break;
            }
        }

        private static readonly int _allHandsOffset = 3;
        private static readonly int _currentPlayersOffset = 1;
        private static readonly int _helperOffset = 2;
        private static readonly int _inputLineOffset = 2;
        private static void Draw()
        {
            switch(currentMenu)
            {
                case 1:
                    // Prepare for drawing
                    Console.CursorVisible = false;
                    Console.Clear();

                    // Print the pile
                    Console.Write("Top of pile: ");
                    if (controller.TryGetTopOfPile(out Card topOfPile))
                    {
                        if (controller.GetPlayersHand(controller.currentPlayerID).Contains(topOfPile))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write(topOfPile);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("Empty");
                    }

                    // Print all other players hands
                    Console.WriteLine(GenOffset(_allHandsOffset));
                    for (int i = 0; i < controller.playersCount; i++)
                    {
                        if (i != controller.currentPlayerID)
                        {
                            Console.Write("Player " + (i + 1) + ": ");
                            DisplayHand(controller.GetPlayersHand(i));
                            Console.Write("Top Asset: ");
                            if (controller.TryGetPlayersTopAsset(i, out Card topAsset))
                            {
                                if (controller.GetPlayersHand(controller.currentPlayerID).Contains(topAsset))
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                }

                                Console.Write(topAsset.ToString());
                                Console.ResetColor();
                            }
                            Console.WriteLine();

                        }
                    }

                    // Print current player's hands
                    Console.WriteLine(GenOffset(_currentPlayersOffset));
                    List<Card> currentPlayersHand = controller.GetPlayersHand(controller.currentPlayerID);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Your hand: ");
                    Console.ResetColor();
                    Console.Write(CardsToString(currentPlayersHand));

                    // Print helper info
                    Console.WriteLine(GenOffset(_helperOffset));
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Constants.HELPER_TEXT);
                    Console.ResetColor();

                    // Print input area
                    Console.WriteLine(GenOffset(_inputLineOffset));
                    Console.Write(">> ");
                    Console.CursorVisible = true;
                    break;
            }
            
        }

        private static string GenOffset(int offset)
        {
            string valueToPrint = "";
            for (int i = 0; i < offset; i++)
            {
                valueToPrint += '\n';
            }
            return valueToPrint;
        }

        private static void DisplayHand(List<Card> cards)
        {
            foreach (Card card in cards)
            {
                string cardType = card.GetCardTypeAsString();
                cardType = string.Concat(cardType.Where(c => c >= 'A' && c <= 'Z'));
                string cardValue = card.value.ToString();

                switch (cardType)
                {
                    case "G":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    default:
                        break;
                }

                Console.Write(cardType + " - ");
                Console.ResetColor();
                Console.Write(cardValue + ", ");
            }
        }

        private static string CardsToString(List<Card> cards)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < cards.Count; i++)
            {
                sb.Append(cards[i]);
                if (i != cards.Count - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}