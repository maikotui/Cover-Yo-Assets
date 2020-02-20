using System;
using System.Collections.Generic;
using System.Text;

namespace CoverYourAssets
{
    class Program
    {
        private static GameController controller;

        static void Main(string[] args)
        {
            Start();
            while (controller.IsActive)
            {
                Update();
            }
        }

        private static void Start()
        {
            Console.SetWindowSize(100, 30);

            bool hasReceivedCorrectResponse = false;
            bool badResponse = false;
            while (!hasReceivedCorrectResponse)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("     _____                     __     __                                      _       ");
                Console.WriteLine("    / ____|                    \\ \\   / /                   /\\                | |      ");
                Console.WriteLine("   | |     _____   _____ _ __   \\ \\_/ /__  _   _ _ __     /  \\   ___ ___  ___| |_ ___ ");
                Console.WriteLine("   | |    / _ \\ \\ / / _ \\ '__|   \\   / _ \\| | | | '__|   / /\\ \\ / __/ __|/ _ \\ __/ __|");
                Console.WriteLine("   | |___| (_) \\ V /  __/ |       | | (_) | |_| | |     / ____ \\__ \\__ \\  __/ |_\\__ \\");
                Console.WriteLine("    \\_____\\___/ \\_/ \\___|_|       |_|\\___/ \\__,_|_|    /_/    \\_\\___/___/\\___|\\__|___/");
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine("");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(badResponse ? ">> Received an invalid number of players." : "");
                Console.ResetColor();

                Console.WriteLine("");
                Console.WriteLine(">> How many players?");
                Console.Write("<< ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    controller = new GameController(result);
                    hasReceivedCorrectResponse = true;
                }
                else
                {
                    badResponse = true;
                }
            }

            Console.Clear();
            Console.CursorVisible = false;
        }

        private static void Update()
        {
            if (controller.state == GameState.Drawing)
            {
                Draw();
            }
        }

        private static void Draw()
        {
            Console.SetCursorPosition(0, 0);
            Stack<Card> pile = controller.Cards.Pile;
            Console.WriteLine("Top of pile: " + (pile.Count != 0 ? pile.Peek().ToString() : "Empty"));
            Card[] currentPlayersHand = controller.Cards.GetPlayersHand(controller.currentPlayerID);
            Console.WriteLine("Your hand: " + CardsToString(currentPlayersHand));
        }

        private static void DisplayHand(Card[] cards)
        {
            for (int i = 0; i < 4; i++)
            {
                if(i >= cards.Length)
                {

                }
            }
        }

        private static string CardsToString(Card[] cards)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < cards.Length; i++)
            {
                sb.Append(cards[i]);
                if (i != cards.Length - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}