using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoverYourAssets
{
    class Program
    {
        private static GameController controller;
        private static GameMenu currentMenu;
        private static bool gameIsActive = true;

        // Used in start menu
        private static bool sm_badResponse;
        private static bool sm_notEnough;
        private static bool sm_hasReceivedCorrectResponse;

        // Used in game menu
        private static readonly int g_allHandsOffset = 3;
        private static readonly int g_currentPlayersOffset = 1;
        private static readonly int g_helperOffset = 2;
        private static readonly int g_inputLineOffset = 2;

        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 30);
            while (gameIsActive)
            {
                gameIsActive = controller == null ? gameIsActive : controller.IsActive;
                Update();
            }
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
                sm_badResponse = false;
                sm_notEnough = false;
                sm_hasReceivedCorrectResponse = false;

                currentMenu = GameMenu.Start;
            }

            // Process input based on menu
            switch (currentMenu)
            {
                case GameMenu.Start:
                    Console.CursorVisible = false;

                    sm_badResponse = false;
                    sm_notEnough = false;
                    sm_hasReceivedCorrectResponse = false;

                    while (!sm_hasReceivedCorrectResponse)
                    {
                        if (int.TryParse(input, out int result))
                        {
                            if (result <= 1)
                            {
                                sm_notEnough = true;
                                Draw();
                                input = Console.ReadLine();
                            }
                            else
                            {
                                controller = new GameController(result);
                                sm_hasReceivedCorrectResponse = true;
                                currentMenu = GameMenu.Game;
                            }
                        }
                        else
                        {
                            sm_badResponse = true;
                            Draw();
                            input = Console.ReadLine();
                        }
                    }

                    break;

                case GameMenu.Game:
                    switch (input)
                    {
                        case "a": // Create an asset from cards in hand
                            int[] assetSelection = ShowCardSelectionMenu(controller.currentPlayerID, 2);

                            if (assetSelection != null && controller.TryCreateAssetFromHand(controller.currentPlayerID, assetSelection[0], assetSelection[1]))
                            {
                                controller.MoveToNextPlayer();
                            }
                            else
                            {
                                // TODO: Display that move did not work
                            }

                            break;

                        case "s": // Attempt to steal from another player

                            break;
                        case "d": // Discard a single card from the player's hand
                            int[] discardSelection = ShowCardSelectionMenu(controller.currentPlayerID, 1);
                            if (discardSelection != null && controller.TryDiscardFromHand(controller.currentPlayerID, discardSelection[0]))
                            {
                                controller.MoveToNextPlayer();
                            }
                            else
                            {

                            }

                            break;
                        case "f": // Take the card from the top of the pile if able to

                            break;
                    }
                    break;
            }
        }

        private static void Draw()
        {
            switch (currentMenu)
            {
                case GameMenu.Start:
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Constants.TITLE_ASCII);
                    Console.ResetColor();

                    Console.WriteLine("");
                    Console.WriteLine("");

                    Console.ForegroundColor = ConsoleColor.Red;
                    if (sm_badResponse)
                    {
                        Console.WriteLine(">> Received an invalid number of players.");
                        sm_badResponse = false;
                    }
                    else if (sm_notEnough)
                    {
                        Console.WriteLine(">> Thats not enough players");
                        sm_badResponse = false;
                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                    Console.ResetColor();

                    Console.WriteLine("");
                    Console.WriteLine("<< How many players?");
                    Console.Write(">> ");
                    break;

                case GameMenu.Game:
                    // Prepare for drawing
                    Console.CursorVisible = false;
                    Console.Clear();

                    // Print the pile
                    Console.Write("Top of pile: ");
                    if (controller.TryGetTopOfPile(out Card topOfPile))
                    {
                        if (controller.TryGetPlayersHand(controller.currentPlayerID, out List<Card> playersHand) && playersHand.Contains(topOfPile))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        if (topOfPile.cardType == CardType.NullCard)
                        {
                            Console.Write(topOfPile);
                        }
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("Empty");
                    }

                    // Print all other players hands
                    Console.WriteLine(GenOffset(g_allHandsOffset));
                    for (int i = 0; i < controller.playersCount; i++)
                    {
                        if (i != controller.currentPlayerID)
                        {
                            Console.Write("Player " + (i + 1) + ": ");
                            if (controller.TryGetPlayersHand(i, out List<Card> hand))
                            {
                                DisplayHand(hand);
                            }
                            Console.Write("Top Asset: ");
                            if (controller.TryGetPlayersTopAsset(i, out Card topAsset))
                            {
                                if (controller.TryGetPlayersHand(controller.currentPlayerID, out List<Card> playersHand) && playersHand.Contains(topAsset))
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
                    Console.WriteLine(GenOffset(g_currentPlayersOffset));
                    controller.TryGetPlayersHand(controller.currentPlayerID, out List<Card> currentPlayersHand);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Your hand: ");
                    Console.ResetColor();
                    Console.Write(CardsToString(currentPlayersHand));

                    // Print helper info
                    Console.WriteLine(GenOffset(g_helperOffset));
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Constants.HELPER_TEXT);
                    Console.ResetColor();

                    // Print input area
                    Console.WriteLine(GenOffset(g_inputLineOffset));
                    Console.Write(">> ");
                    Console.CursorVisible = true;
                    break;
            }

        }

        private static int[] ShowCardSelectionMenu(int playerID, int numberOfChoices)
        {
            List<int> chosenCardsIndices = new List<int>(numberOfChoices);

            if (controller.TryGetPlayersHand(playerID, out List<Card> hand))
            {
                while (chosenCardsIndices.Count < numberOfChoices) // until the number of needed cards are chosen
                {
                    bool cardWasChosen = false;

                    while (!cardWasChosen) // until a single card is successfully chosen
                    {
                        // Draw the card selection screen
                        Console.Clear();
                        Console.WriteLine("Choose " + numberOfChoices + " of these cards");
                        for (int i = 0; i < hand.Count; i++)
                        {
                            if (chosenCardsIndices.Contains(i))
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                            }

                            Console.WriteLine(hand[i] + " ");
                            Console.ResetColor();
                        }

                        // process input
                        string input = Console.ReadLine();
                        if (input == "q" || input == "quit") // quiting
                        {
                            return null;
                        }
                        if (int.TryParse(input, out int cardID)) // input is a valid integer
                        {
                            cardID--; // Make cardID 0-based
                            if (cardID >= 0 && cardID < hand.Count) // cardID is valid card in current hand
                            {
                                if (chosenCardsIndices.Contains(cardID)) // card was already chosen
                                {
                                    chosenCardsIndices.Remove(cardID);
                                }
                                else
                                {
                                    chosenCardsIndices.Add(cardID);
                                    cardWasChosen = true;
                                }
                            }
                            else
                            {
                                // TODO: Invalid input
                            }
                        }
                    }
                }
            }

            return chosenCardsIndices.ToArray();
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

        private enum GameMenu
        {
            Start,
            Game,
            CardSelection,
            StealSelection
        }
    }
}