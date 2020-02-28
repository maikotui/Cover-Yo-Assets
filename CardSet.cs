using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoverYourAssets
{
    /// <summary>
    /// A representation of a set of all cards used in a single CoverYourAssets game
    /// </summary>
    class CardSet
    {
        /// <summary>
        /// A stack of cards that represents the main deck. Cards are dealt from this deck whenever players need a refill.
        /// </summary>
        public Stack<Card> Deck { get; private set; }

        /// <summary>
        /// A stack of cards that represents the discard pile.
        /// </summary>
        public Stack<Card> Pile { get; private set; }
        
        /// <summary>
        /// A list of all player's hands.
        /// </summary>
        private List<List<Card>> allHands;

        /// <summary>
        /// A list of all player's assets.
        /// </summary>
        private List<Stack<Queue<Card>>> allAssets;

        /// <summary>
        /// Builds a new cardset by generating a deck of 110 cards and then shuffling it. These cards are distributed as follows:
        /// <list type="bullet"><item>4 Gold</item><item>8 Silver, Home</item><item>10 ClassicAuto, Jewels, Stocks, Bank, Coin Collection,
        /// Stamp Collection, Baseball Cards, Piggy Bank, Cash Under The Mattress</item></list>
        /// Also evenly distributes 4 random cards from the deck to each player.
        /// Lastly, creates a pile and places the top card of the deck to the pile.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        public CardSet(int numberOfPlayers)
        {
            BuildDeck();

            DealCards(numberOfPlayers);

            Pile = new Stack<Card>();
            Pile.Push(Deck.Pop());
        }

        /// <summary>
        /// Creates a deck with the following cards:
        /// <list type="bullet"><item>4 Gold</item><item>8 Silver, Home</item><item>10 ClassicAuto, Jewels, Stocks, Bank, Coin Collection,
        /// Stamp Collection, Baseball Cards, Piggy Bank, Cash Under The Mattress</item></list>
        /// </summary>
        private void BuildDeck()
        {
            Deck = new Stack<Card>(110);
            for (int i = 0; i < 10; i++)
            {
                if (i < 4)
                {
                    Deck.Push(new Card(CardType.Gold));
                }
                if (i < 8)
                {
                    Deck.Push(new Card(CardType.Silver));
                    Deck.Push(new Card(CardType.Home));
                }
                Deck.Push(new Card(CardType.ClassicAuto));
                Deck.Push(new Card(CardType.Jewels));
                Deck.Push(new Card(CardType.Stocks));
                Deck.Push(new Card(CardType.BankAccount));
                Deck.Push(new Card(CardType.CoinCollection));
                Deck.Push(new Card(CardType.StampCollection));
                Deck.Push(new Card(CardType.BaseballCards));
                Deck.Push(new Card(CardType.PiggyBank));
                Deck.Push(new Card(CardType.CashUnderTheMattress));
            }

            // Shuffle the cards
            Random rnd = new Random();
            var values = Deck.ToArray();
            Deck.Clear();
            foreach (var value in values.OrderBy(x => rnd.Next()))
            {
                Deck.Push(value);
            }
        }

        /// <summary>
        /// Distributes 4 cards to each player
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void DealCards(int numberOfPlayers)
        {
            allHands = new List<List<Card>>();
            allAssets = new List<Stack<Queue<Card>>>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                List<Card> hand = new List<Card>();
                for (int j = 0; j < 4; j++)
                {
                    hand.Add(Deck.Pop());
                }
                allHands.Add(hand);

                allAssets.Add(new Stack<Queue<Card>>()); // Start the asset pile by creating an empty stack
            }
        }

        /// <summary>
        /// Gets the hand of the player given.
        /// </summary>
        /// <param name="playerID"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <returns></returns>
        public List<Card> GetHand(int playerID)
        {
            if(playerID >= 0 && playerID < allHands.Count)
            {
                return allHands[playerID];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }

        /// <summary>
        /// Gets the asset pile of the player given.
        /// </summary>
        /// <param name="playerID"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <returns></returns>
        public Stack<Queue<Card>> GetAssetPile(int playerID)
        {
            if (playerID >= 0 && playerID < allAssets.Count)
            {
                return allAssets[playerID];
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// Representation of a Cover Your Assets card. Each card has a cardType and value.
    /// </summary>
    public struct Card
    {
        public CardType type;
        public int Value { get
            {
                switch(type)
                {
                    case CardType.BankAccount:
                        return 10_000;
                    case CardType.BaseballCards:
                        return 5_000;
                    case CardType.CashUnderTheMattress:
                        return 5_000;
                    case CardType.ClassicAuto:
                        return 15_000;
                    case CardType.CoinCollection:
                        return 10_000;
                    case CardType.Gold:
                        return 50_000;
                    case CardType.Home:
                        return 20_000;
                    case CardType.Jewels:
                        return 15_000;
                    case CardType.PiggyBank:
                        return 5_000;
                    case CardType.Silver:
                        return 25_000;
                    case CardType.StampCollection:
                        return 5_000;
                    case CardType.Stocks:
                        return 10_000;

                    default:
                        return default;
                }
            } }
        
        public Card(CardType type)
        {
            this.type = type;
        }

        public Bitmap ToBitmap()
        {
            switch(type)
            {
                case CardType.BankAccount:
                    return Properties.Resources.BankAccount;
                case CardType.BaseballCards:
                    return Properties.Resources.BaseballCards;
                case CardType.CashUnderTheMattress:
                    return Properties.Resources.CashUnderTheMattress;
                case CardType.ClassicAuto:
                    return Properties.Resources.ClassicAuto;
                case CardType.CoinCollection:
                    return Properties.Resources.CoinCollection;
                case CardType.Gold:
                    return Properties.Resources.Gold;
                case CardType.Home:
                    return Properties.Resources.Home;
                case CardType.Jewels:
                    return Properties.Resources.Jewels;
                case CardType.PiggyBank:
                    return Properties.Resources.PiggyBank;
                case CardType.Silver:
                    return Properties.Resources.Silver;
                case CardType.StampCollection:
                    return Properties.Resources.StampCollection;
                case CardType.Stocks:
                    return Properties.Resources.Stocks;

                default:
                    return default;
            }
        }

        public bool IsWildcard()
        {
            return type.Equals(CardType.Silver) || type.Equals(CardType.Gold);
        }

        public String GetCardTypeAsString()
        {
            return Regex.Replace(type.ToString(), "(\\B[A-Z])", " $1");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('[');
            sb.Append(GetCardTypeAsString());
            sb.Append(',');
            sb.Append('$');
            sb.Append(Value);
            sb.Append(']');
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is Card card)
            {
                return type == card.type;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// The label on the card. 
    /// </summary>
    public enum CardType
    {
        Gold,
        Silver,
        Home,
        ClassicAuto,
        Jewels,
        Stocks,
        BankAccount,
        CoinCollection,
        StampCollection,
        BaseballCards,
        PiggyBank,
        CashUnderTheMattress
    }
}
